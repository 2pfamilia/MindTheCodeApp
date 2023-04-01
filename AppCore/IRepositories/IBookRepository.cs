using AppCore.Models.BookModels;
using AppCore.Models.DTOs;
using AppCore.Models.PhotoModels;

namespace AppCore.IRepositories
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllBooks();
        Task<List<Book>> GetBooks(int number);
        Task<List<Book>> GetBestSellers();
        Task<List<Book>> GetNewArrivals();

        //get the most recently added books
        Task<List<BookAuthor>> GetAllAuthors();

        //get all authors
        Task<List<BookCategory>> GetAllCategories();

        //get all categories
        Task<List<Book>> GetBooksByCategoryId(int categoryId);

        //get books by category
        Task<List<Book>> GetBooksByAuthor(BookAuthor bookAuthor);

        //get books by author
        Task<List<Book>> GetBooksByTitle(string titleQuery);

        Task<List<SearchByFilterResultDTO>> GetBooksByFilters(string? searchTerm, List<int>? categoryIDs, List<int>? authorIDs, decimal? maxPrice);

        //get books which has a title similar to the query
        Task<List<Book>> GetBooksByPriceRange(int maxPrice);

        //get books whose prices are within the price range
        Task<List<BookAuthor>> GetBestSellingAuthors();

        //test method - get the first 5 authors
        Task<List<BookCategory>> GetCategoryByName(string name);
        Task<List<BookAuthor>> GetAuthorsByName(string name);

        //Get Book Info by Id
        Task<Book> GetBookInfoById(int id);
        Task<BookCategory> GetBookCategoryById(int id);
        Task<BookAuthor> GetAuthorInfoById(int id);
        Task<Photo> GetPhotoById(int photoId);

    }
}