using AppCore.Models.BookModels;
using AppCore.Models.DTOs;
using AppCore.Models.OrderModels;
using AppCore.Models.AuthModels;
using System.Runtime.CompilerServices;
using AppCore.Models.PhotoModels;

namespace MindTheCodeApp.Services.IServices
{
    public interface IBookService
    {
        Task<List<Book>> GetAllBooks();
        Task<List<Book>> GetBooks(int number);
        Task<List<Book>> GetBestSellers();
        Task<List<BookAuthor>> GetAllAuthors();
        HomeDTO GetHomeDTO();
        SearchPostDTO GetSearchPostDTO(string? searchTerm, List<int>? categoryIDs, List<int>? authorIDs, int? maxPrice);

        Task<List<SearchByFilterResultDTO>> GetBooksByFilters(string? searchTerm, List<int>? categoryIDs, List<int>? authorIDs, decimal? maxPrice);
        Task<List<Book>> GetNewArrivals();
        Task<List<Book>> GetBooksByCategoryId(int categoryId);
        Task<List<Book>> GetBooksByAuthor(BookAuthor author);
        Task<List<Book>> GetBooksByTitle(string titleQuery);
        Task<Book> GetBooksById(int id);
        Task<BookAuthor> GetAuthorById(int id);
        Task<BookCategory> GetCategoryById(int id);
        Task<List<Book>> GetBooksByPriceRange(int maxRange);
        Task<List<BookAuthor>> GetBestSellingAuthors();
        Task<Book> GetBookById(int bookId);

        Task<Photo> GetPhotoById(int photoId);
        
    }
}