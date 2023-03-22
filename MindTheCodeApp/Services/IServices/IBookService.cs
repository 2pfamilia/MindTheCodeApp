﻿using AppCore.Models.BookModels;
using AppCore.Models.DTOs;

namespace AppCore.Services.IServices
{
    public interface IBookService
    {
        Task<List<Book>> GetAllBooks();
        Task<List<Book>> GetBooks(int number);
        Task<List<Book>> GetBestSellers();

        
        //george
        // Task<List<Book>> GetNewArrivals();
        //george
        Task<List<BookAuthor>> GetAllAuthors();

        HomeDTO GetHomeDTO();

        SearchDTO GetSearchDTO(string searchTerm);

        Task<List<Book>> GetNewArrivals();
        Task<List<Book>> GetBooksByCategory(BookCategory category);
        Task<List<Book>> GetBooksByAuthor(BookAuthor author);
        Task<List<Book>> GetBooksByTitle(string titleQuery);
        Task<Book> GetBooksById(int id);
        Task<BookAuthor> GetAuthorById(int id);
        Task<BookCategory> GetCategoryById(int id);
        Task<List<Book>> GetBooksByPriceRange(int? minRange, int? maxRange);
        Task<List<BookAuthor>> GetBestSellingAuthors();
    }
}
