using Infrastructure;
using Microsoft.EntityFrameworkCore;
using MindTheCodeApp.IRepositories;
using MindTheCodeApp.Models.BookModels;
using MindTheCodeApp.Repositories.Models;

namespace MindTheCodeApp.Repositories
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
