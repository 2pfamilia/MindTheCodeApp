using AppCore.Models.BookModels;
using AppCore.Models.DTOs;
using Infrastructure.Data;

namespace MindTheCodeApp.Utils
{
    public class CsvUtils
    {
        private readonly ApplicationDbContext _context;

        public CsvUtils(ApplicationDbContext context)
        {
            _context = context;
        }

        public void ProcessCsv
            (
                List<BookCsvDTO> dto,
                out List<Book> books,
                out List<BookAuthor> bookAuthors,
                out List<BookCategory> bookCategories
            )
        {
            books = new();
            bookAuthors = new();
            bookCategories = new();

            var photo = _context.BookPhotoEntity.First();

            foreach (var item in dto)
            {
                var bookCategory = ConvertBookCategoryCsvDTO(item.Category);
                var bookAuthor = ConvertBookAuthorCsvDTO(item.Author);
                var book = ConvertBookCsvDTO(item, photo, bookAuthor, bookCategory);

                books.Add(book);
                bookAuthors.Add(bookAuthor);
                bookCategories.Add(bookCategory);
            }

        }

        private Book ConvertBookCsvDTO
            (
                BookCsvDTO dto,
                BookPhoto photo,
                BookAuthor bookAuthor,
                BookCategory bookCategory
            )
        {
            return new()
            {
                Title = dto.Title,
                Description = dto.Description,
                Photo = photo,
                Count = dto.Count,
                Price = dto.Price,
                Category = bookCategory,
                Author = bookAuthor,
            };
        }

        private BookAuthor ConvertBookAuthorCsvDTO(AuthorCsvDTO dto)
        {
            return new()
            {
                Name = dto.Name,
                Description = dto.Description,
            };
        }

        private BookCategory ConvertBookCategoryCsvDTO(CategoryCsvDTO dto)
        {
            return new()
            {
                Code = dto.Code,
                Title = dto.Title,
                Description = dto.Description,
            };
        }

    }
}
