using AppCore.Models.DTOs;
using MindTheCodeApp.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Reflection.Metadata.BlobBuilder;
using Microsoft.Win32;
using AppCore.Models.BookModels;
using MindTheCodeApp.ViewModels.BookVMs;
using AppCore.Models.PhotoModels;
using System.Text.Json;
using System.Linq;
using MindTheCodeApp.ViewModels.AuthorVMs;
using MindTheCodeApp.ViewModels.ShopVMs;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;
using System;

namespace MindTheCodeApp.Controllers
{
    [Route("/shop")]
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


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var shopVM = new ShopVM();
            shopVM.Books = await _bookService.GetAllBooks();
            shopVM.Categories = new List<BookCategory>();

            shopVM.Authors = new List<BookAuthor>();

            shopVM.MaxPrice = 0;

            foreach (var book in shopVM.Books)
            {
                if (!shopVM.Categories.Contains(book.Category))
                {
                    shopVM.Categories.Add(book.Category);
                }

                if (!shopVM.Authors.Contains(book.Author))
                {
                    shopVM.Authors.Add(book.Author);
                }

                if (shopVM.MaxPrice < book.Price)
                {
                    shopVM.MaxPrice = book.Price;
                }
            }

            return View("/Views/Shop/Shop.cshtml", shopVM);
        }

        [HttpPost]
        [Route("search/")]
        public async Task<IActionResult> SearchBox(string? search)
        {
            var shopVM = new ShopVM();
            shopVM.Books = await _bookService.GetBooksByTitle(search);
            shopVM.Categories = new List<BookCategory>();

            shopVM.Authors =  new List<BookAuthor>();

            shopVM.MaxPrice = 0;

            foreach (var book in shopVM.Books)
            {
                if (!shopVM.Categories.Contains(book.Category))
                {
                    shopVM.Categories.Add(book.Category);
                }

                if (!shopVM.Authors.Contains(book.Author))
                {
                    shopVM.Authors.Add(book.Author);
                }

                if (shopVM.MaxPrice < book.Price)
                {
                    shopVM.MaxPrice = book.Price;
                }
            }           

            return View("/Views/Shop/Shop.cshtml", shopVM);
        }


        [HttpGet]
        [Route("category/{id}")]
        public async Task<IActionResult> SearchByCategory(int id)
        {
            var shopVM = new ShopVM();
            shopVM.Books = await _bookService.GetBooksByCategoryId(id);
            shopVM.Categories = new List<BookCategory>();
            shopVM.Categories.Add(shopVM.Books.FirstOrDefault().Category);

            shopVM.Authors = new List<BookAuthor>();

            shopVM.MaxPrice = 0;

            foreach (var book in shopVM.Books)
            {
                if (!shopVM.Authors.Contains(book.Author))
                {
                    shopVM.Authors.Add(book.Author);
                }

                if (shopVM.MaxPrice < book.Price)
                {
                    shopVM.MaxPrice = book.Price;
                }
            }

            return View("/Views/Shop/Shop.cshtml", shopVM);
        }


        [HttpPost]
        [Route("filters")]
        public async Task<IActionResult> SearchByFilters([FromBody] SearchDTO searchDTO)
        {
            //return View("/Views/Shop/Shop.cshtml");
            decimal maxPrice;
            Decimal.TryParse(searchDTO.maxPrice, out maxPrice);


            var shopVM = await _bookService.GetBooksByFilters(searchDTO.SearchTerm, searchDTO.CategoryIDs, searchDTO.AuthorIDs, maxPrice);

            
            var searchFieldsSereialize = JsonSerializer.Serialize(shopVM);

            return Ok(searchFieldsSereialize);
        }



        //[HttpPost("")]
        //public async Task<IActionResult> Search([FromBody] SearchDTO? searchDTO)
        //{
        //    //  Return a list of books for the view to display
        //    /*
        //        If searchDTO is null then return all the books
        //        or use the information inside it to filter
        //        the books you are gonna be returning

        //        The business logic will need to be in a service.
        //     */
        //    //var books = new List<Book>();
        //    SearchPostDTO searchPostDTO = null;
        //    if (searchDTO != null) searchPostDTO = _bookService.GetSearchPostDTO(searchDTO.SearchTerm, searchDTO.CategoryIDs, searchDTO.AuthorIDs, searchDTO.maxPrice);

        //    if (searchPostDTO == null)
        //    {
        //        var books = await _bookService.GetAllBooks();
        //        return View("/Views/Shop/Shop.cshtml", books);
        //    }
        //    else
        //    {
        //        return View("/Views/Shop/Shop.cshtml", searchPostDTO);
        //    }
        //}

        [Route("product/{id}")]
        public async Task<IActionResult> Product([FromRoute] int id)
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


            return View();
        }
    }
}