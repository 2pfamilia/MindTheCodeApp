using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppCore.Models.BookModels;
using Infrastructure.Data;
using MindTheCodeApp.ViewModels.BookVMs;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Serilog;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using MindTheCodeApp.Utils;
using AppCore.Models.DTOs;
using CsvHelper;
using System.Globalization;

namespace MindTheCodeApp.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminBooksController : Controller
    {
        Serilog.ILogger myLog = Log.ForContext<AdminBooksController>();
        private readonly ApplicationDbContext _context;
        private List<IndexBookVM> IndexBooksVM { get; set; } = new List<IndexBookVM>();
        private EditBookVM EditBookVM { get; set; } = new EditBookVM();
        private IndexBookVM DetailBookVM { get; set; } = new IndexBookVM();
        private readonly CsvUtils _csvUtils;

        public AdminBooksController(ApplicationDbContext context, CsvUtils csvUtils)
        {
             _csvUtils = csvUtils;
             _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var books = await _context.BookEntity.Include(b => b.Author).Include(b => b.Category).Include(p => p.Photo).ToListAsync();

            foreach (var book in books)
            {
                var bookVM = new IndexBookVM();
                var bookCategory = _context.BookEntity.FirstOrDefault(c => c.BookId == book.BookId).Category.Title;
                var bookAuthor = _context.BookEntity.FirstOrDefault(c => c.BookId == book.BookId).Author.Name;
                var bookPhoto = _context.BookEntity.FirstOrDefault(c => c.BookId == book.BookId).Photo.FilePath;
                bookVM.BookId = book.BookId;
                bookVM.Title = book.Title;
                bookVM.Description = book.Description;
                if (bookCategory != null)
                {
                    bookVM.Category = bookCategory;
                }

                bookVM.Count = (int)book.Count;
                if (bookAuthor != null)
                {
                    bookVM.Author = bookAuthor;
                }
                if (bookPhoto != null)
                {
                    bookVM.PhotoPath = bookPhoto;
                }

                bookVM.Price = (decimal)book.Price;
                IndexBooksVM.Add(bookVM);
            }

            return View("/Views/Admin/Books/Index.cshtml", IndexBooksVM);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BookEntity == null)
            {
                return NotFound();
            }

            var book = await _context.BookEntity.Include(b => b.Author).Include(b => b.Category).Include(p => p.Photo)
                .FirstOrDefaultAsync(m => m.BookId == id);
            var bookCategory = _context.BookEntity.FirstOrDefault(c => c.BookId == book.BookId).Category.Title;
            var bookAuthor = _context.BookEntity.FirstOrDefault(c => c.BookId == book.BookId).Author.Name;
            var bookPhoto = _context.BookEntity.FirstOrDefault(c => c.BookId == book.BookId).Photo.FilePath;
            if (book == null)
            {
                return NotFound();
            }
            else
            {
                DetailBookVM.BookId = book.BookId;
                DetailBookVM.Title = book.Title;
                DetailBookVM.Description = book.Description;
                DetailBookVM.Category = bookCategory;
                DetailBookVM.Count = (int)book.Count;
                DetailBookVM.Author = bookAuthor;
                DetailBookVM.PhotoPath = bookPhoto;
                DetailBookVM.Price = (decimal)book.Price;
            }

            return View("/Views/Admin/Books/Details.cshtml", DetailBookVM);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            ViewData["BookCategories"] = new SelectList(_context.BookCategoryEntity, "CategoryId", "Title");
            ViewData["BookPhoto"] = new SelectList(_context.PhotoEntity.Where(p => p.IsBook == true), "PhotoId", "Title");
            ViewData["BookAuthors"] = new SelectList(_context.BookAuthorEntity, "AuthorId", "Name");

            return View("/Views/Admin/Books/Create.cshtml");
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookVM book)
        {
            try
            {
                myLog.Verbose("Start - Create");
                var author = _context.BookAuthorEntity.FirstOrDefault(a => a.AuthorId == book.BookAuthorId);
                var photo = _context.PhotoEntity.FirstOrDefault(p => p.PhotoId == book.PhotoId);
                var category = _context.BookCategoryEntity.FirstOrDefault(c => c.CategoryId == book.BookCategoryId);

                var newBook = _context.BookEntity.Add(new Book
                {
                    Title = book.Title,
                    Description = book.Description,
                    Category = category,
                    Count = book.Count,
                    Author = author,
                    Photo = photo,
                    Price = book.Price,
                    DateCreated = DateTime.Now
                });

                await _context.SaveChangesAsync();
                myLog.Verbose("End - Create");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                myLog.Error(ex, "Exception on Create");
                throw;
            }
            
        }

        // GET: Books/Edit/5
        public IActionResult Edit(int id)
        {
            var book = _context.BookEntity.Include(b => b.Author).Include(b => b.Category).Include(p => p.Photo)
                .FirstOrDefault(b => b.BookId == id);
            if (book != null)
            {
                EditBookVM.EditBookId = book.BookId;
                EditBookVM.Title = book.Title;
                EditBookVM.Description = book.Description;
                EditBookVM.Count = (int)book.Count;
                EditBookVM.Price = (decimal)book.Price;
                EditBookVM.CategoryId = book.Category.CategoryId;
                EditBookVM.AuthorId = book.Author.AuthorId;
                EditBookVM.PhotoId = book.Photo.PhotoId;
            }

            ViewData["BookCategories"] = new SelectList(_context.BookCategoryEntity, "CategoryId", "Title");
            ViewData["BookPhoto"] = new SelectList(_context.PhotoEntity.Where(b => b.IsBook == true), "PhotoId", "Title");
            ViewData["BookAuthors"] = new SelectList(_context.BookAuthorEntity, "AuthorId", "Name");

            return View("/Views/Admin/Books/Edit.cshtml", EditBookVM);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditBookVM editBookVM)
        {
            try
            {
                myLog.Verbose("Start - Edit");
                var book = _context.BookEntity.Include(b => b.Author).Include(b => b.Category).Include(p => p.Photo)
                .FirstOrDefault(b => b.BookId == id);
                if (book != null)
                {
                    if (id != book.BookId)
                    {
                        return NotFound();
                    }

                    book.Title = editBookVM.Title;
                    book.Description = editBookVM.Description;
                    book.Author.AuthorId = editBookVM.AuthorId;
                    book.Category.CategoryId = editBookVM.CategoryId;
                    book.Photo.PhotoId = editBookVM.PhotoId;
                    book.Count = editBookVM.Count;
                    book.Price = editBookVM.Price;

                    await _context.SaveChangesAsync();
                    myLog.Verbose("End - Edit");
                }
                else
                {
                    return NotFound();
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                myLog.Error(ex, "Exception on Edit");
                throw;
            }
            
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BookEntity == null)
            {
                return NotFound();
            }

            var book = await _context.BookEntity
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View("/Views/Admin/Books/Delete.cshtml", book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                myLog.Verbose("Start - Delete");
                if (_context.BookEntity == null)
                {
                    return Problem("Entity set 'ApplicationDbContext.BookEntity'  is null.");
                }

                var book = await _context.BookEntity.FindAsync(id);
                if (book != null)
                {
                    _context.BookEntity.Remove(book);
                }

                await _context.SaveChangesAsync();
                myLog.Verbose("End - Delete");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                myLog.Error(ex, "Exception on Delete");
                throw;
            }
            
        }

        [HttpGet]
        public IActionResult UploadCsv()
        {
            return View("/Views/Admin/Books/CSVTest.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> UploadCsv(IFormFile file)
        {
            if (file is null)
                return BadRequest();

            using (var reader = new StreamReader(file.OpenReadStream()))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<BookCsvDTOMap>();
                csv.Context.RegisterClassMap<AuthorCsvDTOMap>();
                csv.Context.RegisterClassMap<CategoryCsvDTOMap>();

                var records = csv.GetRecords<BookCsvDTO>().ToList();

                _csvUtils.ProcessCsv(
                    records,
                    out List<Book> books,
                    out List<BookAuthor> bookAuthors,
                    out List<BookCategory> bookCategories);

                _context.AddRange(bookAuthors);
                _context.AddRange(bookCategories);
                _context.AddRange(books);

                _context.SaveChanges();

                return RedirectToPage("/Views/Admin/Books/Index.cshtml");
            }
        }

        private bool BookExists(int id)
        {
            return (_context.BookEntity?.Any(e => e.BookId == id)).GetValueOrDefault();
        }
    }
}