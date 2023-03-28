using Microsoft.EntityFrameworkCore;
using AppCore.IRepositories;
using AppCore.Models.BookModels;
using AppCore.Models.AuthModels;

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

        public async Task<List<Book>> GetBooksByPriceRange(int maxPrice)
        {
            var minBooks = await _context.BookEntity.Where(mybook => mybook.Price <= maxPrice).ToListAsync();
            return minBooks;
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

        public async Task<Book> GetBookInfoById(int bookId)
        {
            Book book = await _context.BookEntity.Include(x => x.Author).Include(x => x.Category).SingleAsync(bookById => bookById.BookId == bookId);

            return book;
        }

        public async Task<BookCategory> GetBookCategoryById(int id)
        {
            BookCategory bookCategory = await _context.BookCategoryEntity.Include(x=>x.Books).SingleAsync(category=>category.CategoryId== id);
            return bookCategory;
        }

        public async Task<BookAuthor> GetAuthorInfoById(int id)
        {
            BookAuthor bookAuthor = await _context.BookAuthorEntity.Include(x=>x.Books).SingleAsync(author=>author.AuthorId== id);
            return bookAuthor;
        }
    }
}