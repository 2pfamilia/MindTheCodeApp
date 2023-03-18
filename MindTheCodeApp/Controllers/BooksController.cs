using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppCore.Models.BookModels;
using Infrastructure.Data;
using MindTheCodeApp.ViewModels;

namespace MindTheCodeApp.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;
        public IndexBookVM BookVM { get; set; } = new();

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var books = await _context.BookEntity.ToListAsync(); //Include authors, categories
            foreach (var book in books)
            {
                //var bookCategory = _context.BookEntity.FirstOrDefault(c => c.BookId == book.BookId).Category.Title;
                //var bookAuthor = _context.BookEntity.FirstOrDefault(c => c.BookId == book.BookId).Author.Name;
                BookVM.BookId = book.BookId;
                BookVM.Title = book.Title;
                BookVM.Description = book.Description;
                //if (bookCategory != null)
                //{
                //    bookVM.Category = bookCategory;
                //}
                BookVM.Count = (int)book.Count;
                //if (bookAuthor != null)
                //{
                //    bookVM.Author = bookAuthor;
                //}
                BookVM.Price = (decimal)book.Price;
                BookVM.DateCreated = book.DateCreated;
            }
            return View();
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
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

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            ViewData["BookCategories"] = new SelectList(_context.BookCategoryEntity, "CategoryId", "Title");
            ViewData["BookPhoto"] = new SelectList(_context.BookPhotoEntity, "PhotoId", "File");
            ViewData["BookAuthors"] = new SelectList(_context.BookAuthorEntity, "AuthorId", "Name");

            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookVM book)
        {
            var author = _context.BookAuthorEntity.FirstOrDefault(a => a.AuthorId == book.BookAuthorId);
            var photo = _context.BookPhotoEntity.FirstOrDefault(p => p.PhotoId == book.PhotoId);
            var category = _context.BookCategoryEntity.FirstOrDefault(c => c.CategoryId == book.BookCategoryId);

            var newBook = _context.BookEntity.Add(new Book
            {
                Title = book.Title,
                Description = book.Description,
                Category = category,
                Photo = photo,
                Count = book.Count,
                Author = author,
                Price = book.Price,
                DateCreated = book.DateCreated
            });

            await _context.SaveChangesAsync();

            return View(newBook);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BookEntity == null)
            {
                return NotFound();
            }

            var book = await _context.BookEntity.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookId,Title,Description,Count,Price,DateCreated")] Book book)
        {
            if (id != book.BookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.BookId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(book);
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

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
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
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
          return (_context.BookEntity?.Any(e => e.BookId == id)).GetValueOrDefault();
        }
    }
}
