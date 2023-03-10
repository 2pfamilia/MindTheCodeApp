using MindTheCodeApp.Models.BookModels;

namespace MindTheCodeApp.IRepositories
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllBooks();
    }
}
