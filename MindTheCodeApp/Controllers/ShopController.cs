using AppCore.Models.DTOs;
using MindTheCodeApp.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace MindTheCodeApp.Controllers
{
    [Route("/Shop/")]
    [AllowAnonymous]
    public class ShopController : Controller
    {
        private readonly ILogger<ShopController> _logger;
        private readonly IBookService _bookService;
       // private List<CategoryDTO> _categories;
       // private List<AuthorDTO> _authors;

        public ShopController(ILogger<ShopController> logger, IBookService bookService)
        {
            _logger = logger;
            _bookService = bookService;
            //create categoryDTOs for all categories
            //LazyInitializer()
           // Init();
        }

       /* async void Init()
        {
            _categories = await _bookService.CreateCategoryDTOs();
            _authors = await _bookService.CreateAuthorDTOs();
        }*/

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

        public async Task<IActionResult> SearchByCategory(string categoryName) 
        {
            var selectedCategoryDTOs = await _bookService.FindCategoryDTOsByTitle(categoryName);
            foreach(var selected in selectedCategoryDTOs) 
            {
                await Task.Run(() => { _bookService.SelectCategoryDTO(selected); }); 
            }
            return RedirectToAction("SearchPost", selectedCategoryDTOs);
        }


        [HttpPost("")]
        public async Task<IActionResult> SearchPost([FromForm] string? searchTerm, List<CategoryDTO>? categoryDTOs, List<AuthorDTO>? authorDTOs)
        {
            //  Return a list of books for the view to display
            /*
                If searchDTO is null then return all the books
                or use the information inside it to filter
                the books you are gonna be returning

                The business logic will need to be in a service.
             */
            //var books = new List<Book>();
            List<CategoryDTO> selectedCategoryDTO = null;
            List<AuthorDTO> selectedAuthorDTO = null;
            if (categoryDTOs != null) selectedCategoryDTO = categoryDTOs.Where(myCategoryDTO => myCategoryDTO.IsSelected == true).ToList();
            if (authorDTOs != null) selectedAuthorDTO = authorDTOs.Where(myAuthorDTO => myAuthorDTO.IsSelected == true).ToList();

            var searchDTO = new SearchDTO();
            if (searchTerm != null)
            {
                searchDTO.BooksByTitle = await _bookService.GetBooksByTitle(searchTerm);
            }
            if (selectedCategoryDTO != null)
            {
                foreach (var categoryDTO in selectedCategoryDTO)
                {
                    searchDTO.BooksByCategory.AddRange(await _bookService.GetBooksByCategory(await _bookService.GetCategoryById(categoryDTO.Id)));
                }
            }
            if (selectedAuthorDTO != null)
            {
                foreach (var authorDTO in selectedAuthorDTO)
                {
                    searchDTO.BooksByAuthor.AddRange(await _bookService.GetBooksByAuthor(await _bookService.GetAuthorById(authorDTO.Id)));
                }
            }

            if (searchDTO == null)
            {
                _logger.LogDebug("lmaoooooooooooooooooo");
                var books = await _bookService.GetAllBooks();
                return View("/Views/Shop/Shop.cshtml", books);
            }
            else
            {
               // _logger.LogDebug("Got em bookies");
                return View("/Views/Shop/Shop.cshtml", searchDTO);
            }
        }
    }
}