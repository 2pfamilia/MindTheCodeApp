using AppCore.Models.BookModels;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MindTheCodeApp.ViewModels.BookVMs;
using Serilog;
using System.Data;

namespace MindTheCodeApp.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminBookAuthorController : Controller
    {
        private readonly ApplicationDbContext _context;
        Serilog.ILogger myLog = Log.ForContext<AdminBookAuthorController>();
        private List<BookAuthorVM> IndexAuthorsVM { get; set; } = new();
        private BookAuthorVM DetailsBookAuthorVM { get; set; } = new BookAuthorVM();
        private EditBookAuthorVM EditBookAuthorVM { get; set; } = new EditBookAuthorVM();

        public AdminBookAuthorController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            myLog.Verbose("Start - Index");
            var authors = await _context.BookAuthorEntity.ToListAsync();
            
            foreach (var author in authors)
            {
                //var photo = _context.BookAuthorEntity.FirstOrDefault(a => a.AuthorId == author.AuthorId).Photo.FilePath;
                var authorVM = new BookAuthorVM();
                authorVM.AuthorId = author.AuthorId;
                authorVM.Name = author.Name;
                authorVM.Description = author.Description;
                //authorVM.PhotoFilePath = photo;
                IndexAuthorsVM.Add(authorVM);
            }
            myLog.Verbose("End - Index");
            return View("/Views/Admin/BookAuthor/Index.cshtml", IndexAuthorsVM);
        }

        public IActionResult Create()
        {
            ViewData["AuthorPhoto"] = new SelectList(_context.PhotoEntity.Where(p => p.IsAuthor == true), "PhotoId", "Title");
            return View("/Views/Admin/BookAuthor/Create.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookAuthorVM authorVM)
        {
            myLog.Verbose("Start - Create");
            try
            {
                var photo = _context.PhotoEntity.FirstOrDefault(p => p.PhotoId == authorVM.PhotoId);
                var newAuthor = _context.BookAuthorEntity.Add(new AppCore.Models.BookModels.BookAuthor
                {
                    Name = authorVM.Name,
                    Description = authorVM.Description,
                    Photo = photo,
                    DateCreated = DateTime.Now
                });
                await _context.SaveChangesAsync();

                myLog.Verbose("End - Create");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                myLog.Error(ex, "Exception at Create");
                return null;
            }
            
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
            var authorPhoto = _context.BookAuthorEntity.FirstOrDefault(c => c.AuthorId == id).Photo.FilePath;


            if (author == null)
            {
                return NotFound();
            }
            else
            {
                DetailsBookAuthorVM.AuthorId = author.AuthorId;
                DetailsBookAuthorVM.Name = author.Name;
                DetailsBookAuthorVM.Description = author.Description;
                DetailsBookAuthorVM.PhotoFilePath = authorPhoto;
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
                //EditBookAuthorVM.PhotoId = author.Photo.PhotoId;
            }

            ViewData["AuthorPhoto"] = new SelectList(_context.PhotoEntity.Where(b => b.IsAuthor == true), "PhotoId", "Title");

            return View("/Views/Admin/BookAuthor/Edit.cshtml", EditBookAuthorVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditBookAuthorVM editBookAuthorVM)
        {
            myLog.Verbose("Start - Edit");
            try
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
                    author.Photo.PhotoId = editBookAuthorVM.PhotoId;

                    await _context.SaveChangesAsync();
                }
                else
                {
                    return NotFound();
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                myLog.Error(ex, "Exception at Edit");
                return null;
            }
            
        }
    }
}