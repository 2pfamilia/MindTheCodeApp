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
            var bestSellers = await _context.BookEntity.Take(4).ToListAsync();
            return bestSellers;
        }

        public async Task<List<BookAuthor>> GetAllAuthors()
        {
            //george
            var allAuthors = await _context.BookAuthorEntity.Take(5).ToListAsync();
            return allAuthors;
        }

        public async Task<List<BookAuthor>> GetBestSellingAuthors()
        {
            var bestSelling = await _context.BookAuthorEntity.Take(5).ToListAsync();
            return bestSelling;
        }

        public async Task<List<Book>> GetBooksByAuthor(BookAuthor bookAuthor)
        {
            var booksByAuthors = await _context.BookEntity.Include(mybook => mybook.Author)
                .Where(mybook => mybook.Author == bookAuthor).ToListAsync();
            return booksByAuthors;
        }

        public async Task<List<Book>> GetBooksByCategory(BookCategory category)
        {
            var categoryBooks = await _context.BookEntity.Include(mybook => mybook.Category)
                .Where(mybook => mybook.Category == category).ToListAsync();
            return categoryBooks;
        }

        public async Task<List<Book>> GetBooksByPriceRange(int? minPrice, int? maxPrice)
        {
            if (minPrice == null && maxPrice == null)
            {
                return await GetAllBooks();
            }
            else
            {
                if (minPrice == null)
                {
                    var minBooks = await _context.BookEntity.Where(mybook => mybook.Price <= maxPrice).ToListAsync();
                    return minBooks;
                }
                else if (maxPrice == null)
                {
                    var maxBooks = await _context.BookEntity.Where(mybook => mybook.Price >= minPrice).ToListAsync();
                    return maxBooks;
                }
                else
                {
                    var rangeBooks = await _context.BookEntity
                        .Where(mybook => mybook.Price <= maxPrice && mybook.Price >= minPrice).ToListAsync();
                    return rangeBooks;
                }
            }
        }

        public async Task<List<Book>> GetBooksByTitle(string titleQuery)
        {
            var similarTitleBooks =
                await _context.BookEntity.Where(mybook => mybook.Title.Contains(titleQuery)).ToListAsync();
            return similarTitleBooks;
        }

        public async Task<List<Book>> GetNewArrivals()
        {
            //get the first 5 books that are newer in the library
            var newArrivals = await _context.BookEntity.OrderByDescending(mybook => mybook.DateCreated).Take(4)
                .ToListAsync();
            return newArrivals;
        }

        public async Task<List<Book>> GetBooks(int number)
        {
            int absNumber = (int)MathF.Abs(number); //safety measure
            var books = await _context.BookEntity.Take(absNumber).ToListAsync();
            return books;
        }

        public async Task<List<BookCategory>> GetCategoryByName(string name)
        {
            var categories = await _context.BookCategoryEntity.Where(myCategory => myCategory.Title == name)
                .ToListAsync();
            return categories;
        }

        public async Task<List<BookAuthor>> GetAuthorsByName(string name)
        {
            var authors = await _context.BookAuthorEntity.Where(myAuthor => myAuthor.Name == name).ToListAsync();
            return authors;
        }

        public async Task<List<BookCategory>> GetAllCategories()
        {
            var categories = await _context.BookCategoryEntity.ToListAsync();
            return categories;
        }
    }
}