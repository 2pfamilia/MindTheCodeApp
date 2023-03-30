using AppCore.Models.BookModels;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MindTheCodeApp.ViewModels.AuthorVMs;

namespace MindTheCodeApp.Controllers
{
    public class AuthorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuthorController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("/authors")]
        public IActionResult Index()
        {
            var authors = _context.BookAuthorEntity.Include(a => a.Photo).ToList();
            ViewBag.Authors = authors;
            return View();
        }

        [Route("/authors/details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BookAuthorEntity == null)
            {
                return RedirectToAction("Index");
            }

            var authorDetailsVM = new AuthorDetailsVM();
            var author = _context.BookAuthorEntity.Where(a => a.AuthorId == id).Include(a => a.Photo).FirstOrDefault();


            if (author != null)
            {
                authorDetailsVM.Author = author;
                var books =  _context.BookEntity.Where(b => b.Author == author).Include(b => b.Photo).ToList();
                authorDetailsVM.Books = books;
            }

            return View(authorDetailsVM);
        }
    }
}
