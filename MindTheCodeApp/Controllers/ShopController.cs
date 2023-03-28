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

        [HttpGet("/SearchByCategory/{categoryName}")]
        public async Task<IActionResult> SearchByCategory(int[] categoryIDs) 
        {
            SearchDTO searchDTO = new SearchDTO { CategoryIDs = categoryIDs};
            return RedirectToAction("SearchPost", searchDTO);
           
        }


        [HttpPost("Shop")]
        public async Task<IActionResult> SearchPost([FromBody] SearchDTO searchDTO)
        {
            SearchPostDTO searchPostDTO = _bookService.GetSearchPostDTO(searchDTO.SearchTerm, searchDTO.CategoryIDs, searchDTO.AuthorIDs, searchDTO.maxPrice);
            if (searchPostDTO == null)
            {
               // _logger.LogDebug("lmaoooooooooooooooooo");
                var books = await _bookService.GetAllBooks();
                return View("/Views/Shop/Shop.cshtml", books);
            }
            else
            {
               // _logger.LogDebug("Got em bookies");
                return View("/Views/Shop/Shop.cshtml",searchPostDTO);
            }
        }
    }
}