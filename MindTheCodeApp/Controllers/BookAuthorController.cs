using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MindTheCodeApp.ViewModels.BookVMs;

namespace MindTheCodeApp.Controllers
{
    public class BookAuthorController : Controller
    {
        private readonly ApplicationDbContext _context;
        private List<BookAuthorVM> IndexAuthorsVM { get; set; } = new();
        private BookAuthorVM DetailsBookAuthorVM { get; set; } = new BookAuthorVM();
        private EditBookAuthorVM EditBookAuthorVM { get; set; } = new EditBookAuthorVM();
        public BookAuthorController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var authors = await _context.BookAuthorEntity.ToListAsync();

            foreach (var author in authors)
            {
                var authorVM = new BookAuthorVM();
                authorVM.AuthorId = author.AuthorId;
                authorVM.Name = author.Name;
                authorVM.Description = author.Description;
                IndexAuthorsVM.Add(authorVM);
            }

            return View("/Views/Admin/BookAuthor/Index.cshtml", IndexAuthorsVM);
        }
        public IActionResult Create()
        {
            return View("/Views/Admin/BookAuthor/Create.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookAuthorVM authorVM)
        {
            var newAuthor = _context.BookAuthorEntity.Add(new AppCore.Models.BookModels.BookAuthor
            {
                Name = authorVM.Name,
                Description = authorVM.Description,
                DateCreated = DateTime.Now
            });
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BookAuthorEntity == null)
            {
                return NotFound();
            }

            var author = await _context.BookAuthorEntity
                .FirstOrDefaultAsync(m => m.AuthorId == id);
            if (author == null)
            {
                return NotFound();
            }

            return View("/Views/Admin/BookAuthor/Delete.cshtml", author);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BookAuthorEntity == null)
            {
                return Problem("Entity set 'ApplicationDbContext.BookAuthorEntity'  is null.");
            }
            var author = await _context.BookAuthorEntity.FindAsync(id);
            if (author != null)
            {
                _context.BookAuthorEntity.Remove(author);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BookAuthorEntity == null)
            {
                return NotFound();
            }

            var author = await _context.BookAuthorEntity
                .FirstOrDefaultAsync(m => m.AuthorId == id);

            if (author == null)
            {
                return NotFound();
            }
            else
            {
                DetailsBookAuthorVM.AuthorId = author.AuthorId;
                DetailsBookAuthorVM.Name = author.Name;
                DetailsBookAuthorVM.Description = author.Description;
            }

            return View("/Views/Admin/BookAuthor/Details.cshtml", DetailsBookAuthorVM);
        }

        public IActionResult Edit(int id)
        {
            var author = _context.BookAuthorEntity.FirstOrDefault(b => b.AuthorId == id);
            if (author != null)
            {
                EditBookAuthorVM.EditBookAuthorId = author.AuthorId;
                EditBookAuthorVM.Name = author.Name;
                EditBookAuthorVM.Description = author.Description;
            }
            return View("/Views/Admin/BookAuthor/Edit.cshtml", EditBookAuthorVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditBookAuthorVM editBookAuthorVM)
        {
            var author = _context.BookAuthorEntity.FirstOrDefault(b => b.AuthorId == id);
            if (author != null)
            {
                if (id != author.AuthorId)
                {
                    return NotFound();
                }

                author.Name = editBookAuthorVM.Name;
                author.Description = editBookAuthorVM.Description;

                await _context.SaveChangesAsync();
            }
            else
            {
                return NotFound();
            }

            return RedirectToAction("Index");
        }
    }
}
