using AppCore.Models.BookModels;

namespace AppCore.Services.IServices
{
    public interface IBookService
    {
        Task<List<Book>> GetAllBooks();
    }
}
