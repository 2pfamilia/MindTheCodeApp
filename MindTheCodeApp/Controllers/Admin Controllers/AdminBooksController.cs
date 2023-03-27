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

namespace MindTheCodeApp.Controllers
{
    public class AdminBooksController : Controller
    {
        Serilog.ILogger myLog = Log.ForContext<AdminBooksController>();
        private readonly ApplicationDbContext _context;
        private List<IndexBookVM> IndexBooksVM { get; set; } = new List<IndexBookVM>();
        private EditBookVM EditBookVM { get; set; } = new EditBookVM();
        private IndexBookVM DetailBookVM { get; set; } = new IndexBookVM();

        public AdminBooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var books = await _context.BookEntity.Include(b => b.Author).Include(b => b.Category).ToListAsync();

            foreach (var book in books)
            {
                var bookVM = new IndexBookVM();
                var bookCategory = _context.BookEntity.FirstOrDefault(c => c.BookId == book.BookId).Category.Title;
                var bookAuthor = _context.BookEntity.FirstOrDefault(c => c.BookId == book.BookId).Author.Name;
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

            var book = await _context.BookEntity.Include(b => b.Author).Include(b => b.Category)
                .FirstOrDefaultAsync(m => m.BookId == id);
            var bookCategory = _context.BookEntity.FirstOrDefault(c => c.BookId == book.BookId).Category.Title;
            var bookAuthor = _context.BookEntity.FirstOrDefault(c => c.BookId == book.BookId).Author.Name;

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
                DetailBookVM.Price = (decimal)book.Price;
            }

            return View("/Views/Admin/Books/Details.cshtml", DetailBookVM);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            ViewData["BookCategories"] = new SelectList(_context.BookCategoryEntity, "CategoryId", "Title");
            ViewData["BookPhoto"] = new SelectList(_context.BookPhotoEntity, "PhotoId", "File");
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
                var photo = _context.BookPhotoEntity.FirstOrDefault(p => p.PhotoId == book.PhotoId);
                var category = _context.BookCategoryEntity.FirstOrDefault(c => c.CategoryId == book.BookCategoryId);

                var newBook = _context.BookEntity.Add(new Book
                {
                    Title = book.Title,
                    Description = book.Description,
                    Category = category,
                    Count = book.Count,
                    Author = author,
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
            var book = _context.BookEntity.Include(b => b.Author).Include(b => b.Category)
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
                //BookVM.PhotoId = book.Photo.PhotoId;
            }

            ViewData["BookCategories"] = new SelectList(_context.BookCategoryEntity, "CategoryId", "Title");
            //ViewData["BookPhoto"] = new SelectList(_context.BookPhotoEntity, "PhotoId", "File");
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
                var book = _context.BookEntity.Include(b => b.Author).Include(b => b.Category)
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

        private bool BookExists(int id)
        {
            return (_context.BookEntity?.Any(e => e.BookId == id)).GetValueOrDefault();
        }
    }
}