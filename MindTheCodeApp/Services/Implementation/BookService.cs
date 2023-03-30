using AppCore.IRepositories;
using AppCore.Models.AuthModels;
using AppCore.Models.BookModels;
using AppCore.Models.DTOs;
using AppCore.Models.OrderModels;
using AppCore.Models.PhotoModels;
using MindTheCodeApp.Services.IServices;

namespace AppCore.Services.Implementation
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        #region Functions For Book Lists

        public async Task<List<Book>> GetAllBooks()
        {
            var books = await _bookRepository.GetAllBooks();
            return books;
        }

        public async Task<List<Book>> GetBestSellers()
        {
            var bestSellers = await _bookRepository.GetBestSellers();
            return bestSellers;
        }

        public async Task<List<Book>> GetBooksByAuthor(BookAuthor author)
        {
            var booksByAuthor = await _bookRepository.GetBooksByAuthor(author);
            return booksByAuthor;
        }

        public async Task<List<Book>> GetBooksByCategory(BookCategory category)
        {
            var categoryBooks = await _bookRepository.GetBooksByCategory(category);
            return categoryBooks;
        }

        public async Task<List<Book>> GetBooksByPriceRange(int maxRange)
        {
            var rangeBooks = await _bookRepository.GetBooksByPriceRange(maxRange);
            return rangeBooks;
        }

        public async Task<List<Book>> GetBooksByTitle(string titleQuery)
        {
            var booksByTitle = await _bookRepository.GetBooksByTitle(titleQuery);
            return booksByTitle;
        }

        public async Task<List<Book>> GetNewArrivals()
        {
            var newArrivals = await _bookRepository.GetNewArrivals();
            return newArrivals;
        }

        public async Task<List<Book>> GetBooks(int number)
        {
            var books = await _bookRepository.GetBooks(number);
            return books;
        }

        #endregion

        //george
        /*
        public async Task<List<Book>> GetNewArrivals()
        {
            var newArrivals = await _bookRepository.GetNewArrivals();
            return newArrivals;
        }
        */

        //george


        #region DTOs

        public HomeDTO GetHomeDTO()
        {
            var dto = new HomeDTO
            {
                BestSellers = _bookRepository.GetBestSellers().Result,
                NewArrivals = _bookRepository.GetNewArrivals().Result,
                Authors = _bookRepository.GetBestSellingAuthors().Result
            };

            return dto;
        }

        public SearchPostDTO GetSearchPostDTO(string? searchTerm, List<int>? categoryIDs, List<int>? authorIDs, int? maxPrice)
        {
            //filters applied by order:
            //-matching title
            //-category
            //-author
            //-price
            List<Book> filteredItems = new List<Book>();
            List<FilterDTO> categoryFilters = new List<FilterDTO>();
            List<FilterDTO> authorFilters = new List<FilterDTO>();

            //initialization of filterDTOs
            List<BookCategory> allCategories = _bookRepository.GetAllCategories().Result;
            List<BookAuthor> allAuthors = _bookRepository.GetAllAuthors().Result;
            foreach (var category in allCategories)
            {
                categoryFilters.Add(new FilterDTO { Id = category.CategoryId, Name = category.Code });
            }
            foreach (var author in allAuthors)
            {
                authorFilters.Add(new FilterDTO { Id = author.AuthorId, Name = author.Name });
            }

            //1st filter: matching title
            if (searchTerm != null) filteredItems = _bookRepository.GetBooksByTitle(searchTerm).Result;
            else filteredItems = _bookRepository.GetAllBooks().Result;

            //2nd filter: selected categories
            if (categoryIDs != null)
            {
                //remove irrelevant entries from the filteredItems list
                //update relevant categoryDTOs
                List<Book> relevant = new List<Book>();

                foreach (var category in categoryIDs)
                {
                    foreach (var book in filteredItems)
                    {
                        if (book.Category.CategoryId == category)
                        {
                            relevant.Add(book);
                        }
                    }
                    foreach (var categoryDTO in categoryFilters)
                    {
                        if (categoryDTO.Id == category)
                        {
                            categoryDTO.isActive = true;
                        }
                    }
                }

                filteredItems.RemoveAll(x => !relevant.Contains(x));
            }

            //3rd filter: selected authors
            if (authorIDs != null)
            {
                //remove irrelevant entries from the filteredItems list
                //update relevant authorDTos
                List<Book> relevant = new List<Book>();

                foreach (var author in authorIDs)
                {
                    foreach (var book in filteredItems)
                    {
                        if (book.Author.AuthorId == author)
                        {
                            relevant.Add(book);
                        }
                    }
                    foreach (var authorDTO in authorFilters)
                    {
                        if (authorDTO.Id == author)
                        {
                            authorDTO.isActive = true;
                        }
                    }
                }

                filteredItems.RemoveAll(x => !relevant.Contains(x));
            }

            //4th filter: price range
            if (maxPrice != null) 
            {
                filteredItems.RemoveAll(x => x.Price > maxPrice);
            }

            SearchPostDTO searchPostDTO = new SearchPostDTO
            {
                BooksFound = filteredItems,
                AuthorFiltersFound = authorFilters,
                CategoryFiltersFound = categoryFilters,
                maxPrice = maxPrice
            };
            return searchPostDTO;
        }

        #endregion

        #region Functions for BookAuthor Lists

        public async Task<List<BookAuthor>> GetBestSellingAuthors()
        {
            var bestSelling = await _bookRepository.GetBestSellingAuthors();
            return bestSelling;
        }

        public async Task<List<BookAuthor>> GetAllAuthors()
        {
            var allAuthors = await _bookRepository.GetAllAuthors();
            return allAuthors;
        }

        #endregion

        #region Model Object Functions

        public async Task<Book> GetBooksById(int id)
        {
            var bookList = await _bookRepository.GetAllBooks();
            var book = bookList.Where(mybook => mybook.BookId == id).FirstOrDefault();
            return book;
        }

        public async Task<BookAuthor> GetAuthorById(int id)
        {
            var authorList = await _bookRepository.GetAllAuthors();
            var author = authorList.Where(myAuthor => myAuthor.AuthorId == id).FirstOrDefault();
            return author;
        }

        public async Task<BookCategory> GetCategoryById(int id)
        {
            var categoryList = await _bookRepository.GetAllCategories();
            var category = categoryList.Where(myCategory => myCategory.CategoryId == id).FirstOrDefault();
            return category;
        }
        public async Task<Book> GetBookById(int bookId)
        {
            Book book = await _bookRepository.GetBookInfoById(bookId);
            return book;
        }

        public async Task<Photo> GetPhotoById(int photoId)
        {
            Photo photo = await _bookRepository.GetPhotoById(photoId);
            return photo;
        }

        #endregion


    }
}