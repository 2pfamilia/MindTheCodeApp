using AppCore.IRepositories;
using AppCore.Models;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AppCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IBookRepository _bookRepo;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IBookRepository bookRepo)
        {
            _logger = logger;
            _context = context;
            _bookRepo = bookRepo;
        }

        public async Task<IActionResult> Index()
        {
            // Test that ApplicationDbContext works
            var appDbContextBooks = await _context.BookEntity.ToListAsync();
            _logger.LogInformation($"DbContext returned {appDbContextBooks.Count} book entries.");

            // Test that IBookRepository works
            var bookRepoBooks = await _bookRepo.GetAllBooks();
            _logger.LogInformation($"Repository returned {bookRepoBooks.Count} book entries.");

            return View();
        }
    }
}