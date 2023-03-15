using AppCore.Models.BookModels;

namespace AppCore.Services.IServices
{
    public interface IBookService
    {
        Task<List<Book>> GetAllBooks();
        Task<List<Book>> GetBestSellers();
        Task<List<Book>> GetNewArrivals();
        Task<List<BookAuthor>> GetBestSellingAuthors();
    }
}
