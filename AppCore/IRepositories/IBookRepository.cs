using AppCore.Models.BookModels;

namespace AppCore.IRepositories
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllBooks();
        Task<List<Book>> GetBestSellers();
        Task<List<Book>> GetNewArrivals();
        Task<List<BookAuthor>> GetBestSellingAuthors();
    }
}
