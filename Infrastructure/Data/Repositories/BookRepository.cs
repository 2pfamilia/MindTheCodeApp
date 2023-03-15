using Microsoft.EntityFrameworkCore;
using AppCore.IRepositories;
using AppCore.Models.BookModels;

namespace Infrastructure.Data.Repositories
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

        public async Task<List<Book>> GetBestSellers()
        {
            var bestSellers = await _context.BookEntity.Take(5).ToListAsync();
            return bestSellers;
        }

        public async Task<List<Book>> GetNewArrivals()
        {
            //george
            var newArrivals = await _context.BookEntity.Take(5).ToListAsync();
            return newArrivals;
        }

        public async Task<List<BookAuthor>> GetAllAuthors()
        {
            //george
            var allAuthors = await _context.BookAuthorEntity.Take(5).ToListAsync();
            return allAuthors;

        }
    }
}
