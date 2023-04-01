using AppCore.IRepositories;
using AppCore.Models.BookModels;
using AppCore.Models.DTOs;
using AppCore.Models.PhotoModels;
using Microsoft.EntityFrameworkCore;

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
            var books = await _context.BookEntity.Include(b => b.Photo).Include(b => b.Author).Include(b => b.Category)
                .ToListAsync();
            return books;
        }

        public async Task<List<Book>> GetBestSellers()
        {
            var bestSellers = await _context.BookEntity.Include(b => b.Author).Include(b => b.Photo).Take(4)
                .ToListAsync();
            return bestSellers;
        }

        public async Task<List<BookAuthor>> GetAllAuthors()
        {
            //george
            var allAuthors = await _context.BookAuthorEntity.Include(ba => ba.Photo).ToListAsync();
            return allAuthors;
        }

        public async Task<List<BookAuthor>> GetBestSellingAuthors()
        {
            var bestSelling = await _context.BookAuthorEntity.Include(ba => ba.Photo).Take(5).ToListAsync();
            return bestSelling;
        }

        public async Task<List<Book>> GetBooksByAuthor(BookAuthor bookAuthor)
        {
            var booksByAuthors = await _context.BookEntity.Include(mybook => mybook.Author)
                .Where(mybook => mybook.Author == bookAuthor).ToListAsync();
            return booksByAuthors;
        }

        public async Task<List<Book>> GetBooksByCategoryId(int categoryId)
        {
            var categoryBooks = await _context.BookEntity.Include(mybook => mybook.Category)
                .Where(mybook => mybook.Category.CategoryId == categoryId).Include(b => b.Photo).Include(b => b.Author)
                .ToListAsync();
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
                await _context.BookEntity.Where(mybook => mybook.Title.Contains(titleQuery)).Include(b => b.Photo)
                    .Include(b => b.Author).Include(b => b.Category).ToListAsync();
            return similarTitleBooks;
        }

        public async Task<List<SearchByFilterResultDTO>> GetBooksByFilters(string? searchTerm, List<int>? categoryIDs, List<int>? authorIDs, decimal? maxPrice)
        {
            var similarTitleBooks = await _context.BookEntity.Where(b => b.Title.Contains(searchTerm) && maxPrice >= b.Price)
                .Include(b => b.Category).Where(b => categoryIDs.Contains(b.Category.CategoryId))
                .Include(b => b.Author).Where(b => authorIDs.Contains(b.Author.AuthorId))
                .Include(b => b.Photo).Select(b => new SearchByFilterResultDTO
                {
                    BookId = b.BookId,
                    Title = b.Title,
                    Description = b.Description,
                    Price = b.Price,
                    Author = new BookAuthor
                    {
                        AuthorId = b.Author.AuthorId,
                        Name = b.Author.Name,
                    },
                    Category = new BookCategory
                    {
                        CategoryId = b.Category.CategoryId,
                        Title = b.Category.Title
                    },
                    Photo = new Photo
                    {
                        PhotoId = b.Photo.PhotoId,
                        FilePath = b.Photo.FilePath,
                    }
                }).ToListAsync();


            //var similarTitleBooks =
            //    await _context.BookEntity.Where(mybook => mybook.Title.Contains(searchTerm)).Include(b => categoryIDs.Contains(b.Category.CategoryId)).Where(b => authorIDs.Contains(b.Author.AuthorId)).ToListAsync();


            return similarTitleBooks;
        }

        public async Task<List<Book>> GetNewArrivals()
        {
            //get the first 4 books that are newer in the library
            var newArrivals = await _context.BookEntity.Include(b => b.Author).Include(b => b.Photo)
                .OrderByDescending(mybook => mybook.DateCreated).Take(4)
                .ToListAsync();
            return newArrivals;
        }

        public async Task<List<Book>> GetBooks(int number)
        {
            int absNumber = (int)MathF.Abs(number); //safety measure
            var books = await _context.BookEntity.Take(absNumber).Include(b => b.Photo).Include(b => b.Author)
                .ToListAsync();
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

        public async Task<Book> GetBookInfoById(int id)
        {
            var book = await _context.BookEntity.Include(x => x.Author).Include(x => x.Category).Include(x => x.Photo)
                .SingleOrDefaultAsync(bookById => bookById.BookId == id);

            return book;
        }

        public async Task<Photo> GetPhotoById(int photoId)
        {
            var photo = await _context.PhotoEntity.SingleOrDefaultAsync(photoById => photoById.PhotoId == photoId);
            return photo;
        }

        public async Task<BookCategory> GetBookCategoryById(int id)
        {
            var category = await _context.BookCategoryEntity.SingleOrDefaultAsync(x => x.CategoryId == id);
            return category;
        }

        public async Task<BookAuthor> GetAuthorInfoById(int id)
        {
            var author = await _context.BookAuthorEntity.SingleOrDefaultAsync(x => x.AuthorId == id);
            return author;
        }
    }
}