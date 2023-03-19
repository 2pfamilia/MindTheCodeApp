using AppCore.IRepositories;
using AppCore.Models.BookModels;
using AppCore.Models.DTOs;
using AppCore.Services.IServices;

namespace AppCore.Services.Implementation
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

       

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
        
        //george
        /*
        public async Task<List<Book>> GetNewArrivals()
        {
            var newArrivals = await _bookRepository.GetNewArrivals();
            return newArrivals;
        }
        */
        
        //george
        public async Task<List<BookAuthor>> GetAllAuthors()
        {
            var allAuthors = await _bookRepository.GetAllAuthors();
            return allAuthors;
        }

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
        
        public async Task<List<BookAuthor>> GetBestSellingAuthors()
        {
            var bestSelling = await _bookRepository.GetBestSellingAuthors();
            return bestSelling;
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
    }
}
