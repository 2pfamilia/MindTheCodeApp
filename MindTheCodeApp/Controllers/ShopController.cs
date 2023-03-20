using AppCore.Models.BookModels;
using AppCore.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MindTheCodeApp.Controllers
{
    [Route("/Shop/")]
    [AllowAnonymous]
    public class ShopController : Controller
    {
        private readonly ILogger<ShopController> _logger;

        public ShopController(ILogger<ShopController> logger)
        {
            _logger = logger;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            //  Return a list of books for the view to display
            /*
                The business logic will need to be in a service.
             */

            var books = new List<Book>();

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

            var books = new List<Book>();

            return View("/Views/Shop/Shop.cshtml", books);
        }
    }
}
