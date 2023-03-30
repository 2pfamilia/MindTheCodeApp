using AppCore.Models.DTOs;
using MindTheCodeApp.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Reflection.Metadata.BlobBuilder;
using Microsoft.Win32;
using AppCore.Models.BookModels;
using MindTheCodeApp.ViewModels.BookVMs;
using AppCore.Models.PhotoModels;

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

        public async Task<IActionResult> SearchByCategory(string category)
        { 
            //return View("/Views/Shop/Shop.cshtml");
            SearchDTO searchDTO = new SearchDTO { SearchTerm = category};
            return RedirectToAction("Search", searchDTO);
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
            Book book = _bookService.GetBookById(id).Result;
            
            //Get Photo of the Book
            int photoBookId = book.Photo.PhotoId;
            Photo photoBook = _bookService.GetPhotoById(photoBookId).Result;

            //Get Author's Photo
            BookAuthor bookAuthor = _bookService.GetAuthorById(book.Author.AuthorId).Result;
            Photo authorPhoto = bookAuthor.Photo;

            //Send Data to View Page
            ViewData["AuthorPhoto"] = authorPhoto;
            ViewData["Photo"] = photoBook;
            ViewData["Book"] = book;


            return View("/Views/Shop/Product.cshtml");
        }
    }
}