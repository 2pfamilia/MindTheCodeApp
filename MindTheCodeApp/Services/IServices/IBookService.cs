using MindTheCodeApp.Repositories.Models.BookModels;

namespace MindTheCodeApp.Services.IServices
{
    public interface IBookService
    {
        Task<List<Book>> GetAllBooks();
    }
}
