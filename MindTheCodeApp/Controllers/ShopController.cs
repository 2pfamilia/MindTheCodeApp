using AppCore.Models.DTOs;
using MindTheCodeApp.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Reflection.Metadata.BlobBuilder;
using Microsoft.Win32;
using AppCore.Models.BookModels;

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
        public async Task<IActionResult> Search([FromBody] SearchDTO? searchDTO)
        {
            //  Return a list of books for the view to display
            /*
                If searchDTO is null then return all the books
                or use the information inside it to filter
                the books you are gonna be returning

                The business logic will need to be in a service.
             */
            //var books = new List<Book>();
            SearchPostDTO searchPostDTO = null;
            if(searchDTO!=null) searchPostDTO = _bookService.GetSearchPostDTO(searchDTO.SearchTerm, searchDTO.CategoryIDs, searchDTO.AuthorIDs, searchDTO.maxPrice);
            

            if (searchPostDTO == null)
            {
                var books = await _bookService.GetAllBooks();
                return View("/Views/Shop/Shop.cshtml", books);
            }
            else
            {
                return View("/Views/Shop/Shop.cshtml", searchPostDTO);
            }
        }

        [Route("Product/{id}")]
        public async Task <IActionResult> ProductInfo([FromRoute]int id)
        {
            //int x = id;

            Book book = _bookService.GetBookById(id).Result;
            string str = book.Author.Name;
            string str2 = book.Category.Title;
            
            ViewData["Book"] = book;

            return View("/Views/Shop/Product.cshtml");
            //return View("/Views/Auth/Register.cshtml");

        }
    }
}