using AppCore.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace MindTheCodeApp.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IBookRepository _bookRepository;
        private readonly IOrderRepository _orderRepository;

        public AdminController(ILogger<AdminController> logger, IBookRepository bookRepository,
            IOrderRepository orderRepository)
        {
            _bookRepository = bookRepository;
            _orderRepository = orderRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var books = await _bookRepository.GetAllBooks();

            return View(books);
        }
    }
}