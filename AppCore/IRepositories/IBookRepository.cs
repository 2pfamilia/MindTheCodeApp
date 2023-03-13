using AppCore.Models.BookModels;

namespace AppCore.IRepositories
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllBooks();
    }
}
