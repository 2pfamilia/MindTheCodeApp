using Microsoft.EntityFrameworkCore;
using MindTheCodeApp.Repositories.IRepositories;
using MindTheCodeApp.Repositories.Models;
using MindTheCodeApp.Repositories.Models.BookModels;

namespace MindTheCodeApp.Repositories.Implementation
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;
        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> GetAllBooks()
        {
            var books = await _context.BookEntity.ToListAsync();
            return books;
        }
    }
}
