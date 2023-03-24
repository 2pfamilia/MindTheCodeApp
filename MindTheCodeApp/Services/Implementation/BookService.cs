using AppCore.IRepositories;
using AppCore.Models.AuthModels;
using AppCore.Models.BookModels;
using AppCore.Models.DTOs;
using AppCore.Models.OrderModels;
using MindTheCodeApp.Services.IServices;

namespace AppCore.Services.Implementation
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        #region Functions For Book Lists

        public async Task<List<Book>> GetAllBooks()
        {
            var books = await _bookRepository.GetAllBooks();
            return books;
        }

        public async Task<List<Book>> GetBestSellers()
        {
            var bestSellers = await _bookRepository.GetBestSellers();
            return bestSellers;
        }

        public async Task<List<Book>> GetBooksByAuthor(BookAuthor author)
        {
            var booksByAuthor = await _bookRepository.GetBooksByAuthor(author);
            return booksByAuthor;
        }

        public async Task<List<Book>> GetBooksByCategory(BookCategory category)
        {
            var categoryBooks = await _bookRepository.GetBooksByCategory(category);
            return categoryBooks;
        }

        public async Task<List<Book>> GetBooksByPriceRange(int? minRange, int? maxRange)
        {
            var rangeBooks = await _bookRepository.GetBooksByPriceRange(minRange, maxRange);
            return rangeBooks;
        }

        public async Task<List<Book>> GetBooksByTitle(string titleQuery)
        {
            var booksByTitle = await _bookRepository.GetBooksByTitle(titleQuery);
            return booksByTitle;
        }

        public async Task<List<Book>> GetNewArrivals()
        {
            var newArrivals = await _bookRepository.GetNewArrivals();
            return newArrivals;
        }

        public async Task<List<Book>> GetBooks(int number)
        {
            var books = await _bookRepository.GetBooks(number);
            return books;
        }

        #endregion

        //george
        /*
        public async Task<List<Book>> GetNewArrivals()
        {
            var newArrivals = await _bookRepository.GetNewArrivals();
            return newArrivals;
        }
        */

        //george


        #region DTOs


        public HomeDTO GetHomeDTO()
        {
            var dto = new HomeDTO
            {
                BestSellers = _bookRepository.GetBestSellers().Result,
                NewArrivals = _bookRepository.GetNewArrivals().Result,
                Authors = _bookRepository.GetAllAuthors().Result
            };

            return dto;
        }

        public SearchDTO GetSearchDTO(string searchTerm)
        {
            List<Book> authorbooks = new List<Book>();
            List<Book> categorybooks = new List<Book>();

            var authors = _bookRepository.GetAuthorsByName(searchTerm).Result;
            foreach (var author in authors)
            {
                var found = _bookRepository.GetBooksByAuthor(author).Result;
                authorbooks.AddRange(found);
            }

            var categories = _bookRepository.GetCategoryByName(searchTerm).Result;
            foreach (var category in categories)
            {
                var found = _bookRepository.GetBooksByCategory(category).Result;
                categorybooks.AddRange(found);
            }

            SearchDTO searchDTO = new SearchDTO
            {
                SearchTerm = searchTerm,
                BooksByTitle = _bookRepository.GetBooksByTitle(searchTerm).Result,
                BooksByAuthor = authorbooks,
                BooksByCategory = categorybooks
            };
            return searchDTO;
        }

        #endregion

        #region Functions for BookAuthor Lists

        public async Task<List<BookAuthor>> GetBestSellingAuthors()
        {
            var bestSelling = await _bookRepository.GetBestSellingAuthors();
            return bestSelling;
        }

        public async Task<List<BookAuthor>> GetAllAuthors()
        {
            var allAuthors = await _bookRepository.GetAllAuthors();
            return allAuthors;
        }

        #endregion

        #region Model Object Functions

        public async Task<Book> GetBooksById(int id)
        {
            var bookList = await _bookRepository.GetAllBooks();
            var book = bookList.Where(mybook => mybook.BookId == id).FirstOrDefault();
            return book;
        }

        public async Task<BookAuthor> GetAuthorById(int id)
        {
            var authorList = await _bookRepository.GetAllAuthors();
            var author = authorList.Where(myAuthor => myAuthor.AuthorId == id).FirstOrDefault();
            return author;
        }

        public async Task<BookCategory> GetCategoryById(int id)
        {
            var categoryList = await _bookRepository.GetAllCategories();
            var category = categoryList.Where(myCategory => myCategory.CategoryId == id).FirstOrDefault();
            return category;
        }

        #endregion
    }
}