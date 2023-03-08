using MindTheCodeApp.Repositories.IRepositories;
using MindTheCodeApp.Repositories.Models.BookModels;
using MindTheCodeApp.Services.IServices;

namespace MindTheCodeApp.Services.Implementation
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
    }
}
