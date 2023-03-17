using AppCore.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace MindTheCodeApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IBookRepository _bookRepository;

        public AdminController(ILogger<AdminController> logger, IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            var books = await _bookRepository.GetAllBooks();

            return View(books);
        }
    }
}
