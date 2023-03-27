using AppCore.Models.BookModels;

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
        Task<List<Book>> GetBooksByCategory(BookCategory category);

        //get books by category
        Task<List<Book>> GetBooksByAuthor(BookAuthor bookAuthor);

        //get books by author
        Task<List<Book>> GetBooksByTitle(string titleQuery);

        //get books which has a title similar to the query
        Task<List<Book>> GetBooksByPriceRange(int? minPrice, int? maxPrice);

        //get books whose prices are within the price range
        Task<List<BookAuthor>> GetBestSellingAuthors();

        //test method - get the first 5 authors
        Task<List<BookCategory>> GetCategoryByName(string name);
        Task<List<BookAuthor>> GetAuthorsByName(string name);

        //Get Book Info by Id
        public Book GetBookInfoById(int id);
    }
}