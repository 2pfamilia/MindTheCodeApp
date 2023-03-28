using AppCore.Models.BookModels;
using AppCore.Models.DTOs;
using AppCore.Models.PhotoModels;
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

            var photo = _context.PhotoEntity.First();

            // var authorsIdLookup = new HashSet<int?>();
            // var categoryIdLookup = new HashSet<int?>();
            //
            // foreach (var item in dto)
            // {
            //     if (item.Author.AuthorId is not null)
            //     {
            //         authorsIdLookup.Add(item.Author.AuthorId);
            //     }
            //
            //     if (item.Category.CategoryId is not null)
            //     {
            //         categoryIdLookup.Add(item.Category.CategoryId);
            //     }
            // }

            //var existingAuthors = _context.BookAuthorEntity.Where(bae => authorsIdLookup.Contains(bae.AuthorId))
            //    .ToDictionary(bae => bae.AuthorId);
            //var existingCategories = _context.BookCategoryEntity.Where(bce => categoryIdLookup.Contains(bce.CategoryId))
            //    .ToDictionary(bce => bce.CategoryId);

            foreach (var item in dto)
            {
                //var bookAuthor = item.Author.AuthorId is not null ?
                //    existingAuthors[(int)item.Author.AuthorId] : ConvertBookAuthorCsvDTO(item.Author);
                //var bookCategory = item.Category.CategoryId is not null ?
                //    existingCategories[(int)item.Category.CategoryId] : ConvertBookCategoryCsvDTO(item.Category);

                var bookAuthor = ConvertBookAuthorCsvDTO(item.Author);
                var bookCategory = ConvertBookCategoryCsvDTO(item.Category);
                var book = ConvertBookCsvDTO(
                    item,
                    photo,
                    bookAuthor,
                    bookCategory,
                    item.Author.AuthorId,
                    item.Category.CategoryId
                );

                books.Add(book);
                bookAuthors.Add(bookAuthor);
                bookCategories.Add(bookCategory);
            }
        }

        private Book ConvertBookCsvDTO
        (
            BookCsvDTO dto,
            Photo photo,
            BookAuthor bookAuthor,
            BookCategory bookCategory,
            int? authorId,
            int? categoryId
        )
        {
            return new()
            {
                Title = dto.Title,
                Description = dto.Description,
                Photo = photo,
                Count = dto.Count,
                Price = dto.Price,
                Category = categoryId is not null
                    ? _context.BookCategoryEntity
                        .Single(bce => bce.CategoryId.Equals(categoryId))
                    : bookCategory,
                Author = authorId is not null
                    ? _context.BookAuthorEntity
                        .Single(bae => bae.AuthorId.Equals(authorId))
                    : bookAuthor,
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