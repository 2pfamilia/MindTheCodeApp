using AppCore.Models.BookModels;

namespace AppCore.Services.IServices
{
    public interface IBookService
    {
        Task<List<Book>> GetAllBooks();
        Task<List<Book>> GetBestSellers();
        Task<List<Book>> GetNewArrivals();
        Task<List<Book>> GetBooksByCategory(BookCategory category);
        Task<List<Book>> GetBooksByAuthor(BookAuthor author);
        Task<List<Book>> GetBooksByTitle(string titleQuery);
        Task<List<Book>> GetBooksByPriceRange(int? minRange, int? maxRange);
        Task<List<BookAuthor>> GetBestSellingAuthors();
    }
}
