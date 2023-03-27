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
            var authors = _context.BookAuthorEntity.ToList();
            ViewBag.Authors = authors;
            return View();
        }

        [Route("/authors/details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BookAuthorEntity == null)
            {
                return NotFound();
            }

            var authorDetailsVM = new AuthorDetailsVM();
            var author = _context.BookAuthorEntity.FirstOrDefault(a => a.AuthorId == id);


            if (author != null)
            {
                authorDetailsVM.Author = author;
                var books =  _context.BookEntity.Where(b => b.Author == author).ToList();
                authorDetailsVM.Books = books;
            }

            return View(authorDetailsVM);
        }
    }
}
