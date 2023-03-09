using MindTheCodeApp.Repositories.Models.BookModels;

namespace MindTheCodeApp.Repositories.IRepositories
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllBooks();
    }
}
