using AppCore.Models.BookModels;
using AppCore.Models.DTOs;
using AppCore.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Reflection.Metadata.BlobBuilder;

namespace MindTheCodeApp.Controllers
{
    [Route("/Shop/")]
    [AllowAnonymous]
    public class ShopController : Controller
    {
        private readonly ILogger<ShopController> _logger;
        private readonly IBookService _bookService;

        public ShopController(ILogger<ShopController> logger, IBookService bookService)
        {
            _logger = logger;
            _bookService = bookService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            //  Return a list of books for the view to display
            /*
                The business logic will need to be in a service.
             */
            var books = await _bookService.GetBooks(10);

            return View("/Views/Shop/Shop.cshtml", books);
        }

        [HttpPost("")]
        public async Task<IActionResult> Search([FromForm] SearchDTO? searchDTO)
        {
            //  Return a list of books for the view to display
            /*
                If searchDTO is null then return all the books
                or use the information inside it to filter
                the books you are gonna be returning

                The business logic will need to be in a service.
             */

            if (searchDTO == null)
            {
                var books = await _bookService.GetAllBooks();
                return View("/Views/Shop/Shop.cshtml", books);
            }
            else
            {
                return View("/Views/Shop/Shop.cshtml", searchDTO);
            }
        }
    }
}