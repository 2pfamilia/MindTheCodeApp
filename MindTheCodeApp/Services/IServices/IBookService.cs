﻿using AppCore.Models.BookModels;

namespace AppCore.Services.IServices
{
    public interface IBookService
    {
        Task<List<Book>> GetAllBooks();
        Task<List<Book>> GetBestSellers();
        //george
        Task<List<Book>> GetNewArrivals();
        //george
        Task<List<BookAuthor>> GetAllAuthors();




    }
}
