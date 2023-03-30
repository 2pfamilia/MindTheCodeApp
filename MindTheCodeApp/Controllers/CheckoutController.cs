using AppCore.Models.DTOs;
using AppCore.Models.BookModels;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Mvc;
using MindTheCodeApp.Services.IServices;
using System.Net;
using System.Net.Cache;
using System.Security.Claims;
using System.Web;

namespace MindTheCodeApp.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ILogger<CheckoutController> _logger;
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        private readonly IBookService _bookService;

        public CheckoutController(ILogger<CheckoutController> logger, IOrderService orderService,
            IUserService userService, IBookService bookService)
        {
            _logger = logger;
            _orderService = orderService;
            _userService = userService;
            _bookService = bookService;
        }

        public async Task<IActionResult> Index(string userCart, string userCartTotal)
        {
            // var cart = 
            //_logger.LogDebug(userCart + userCartTotal);
            //i need info from local storage
            return View("/Views/User/Cart.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            //var checkoutDTO = new CheckoutDTO();
            return View("/Views/Checkout/Checkout.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] List<CheckoutDTO> checkoutDTO)
        {
            //create user
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "-1");
           // var user = await _userService.Get
            //create the dictionary of ordered books and their quantities
            Dictionary<Book,int> orderedBooks= new Dictionary<Book, int>();
            foreach (var book in checkoutDTO) 
            {
                orderedBooks.Add(await _bookService.GetBookById(book.bookId), book.quantity);
            }
            var newOrder = await _orderService.CreateNewOrder(userId, orderedBooks);
            return RedirectToAction("Index", "Home");
        }
    }
}