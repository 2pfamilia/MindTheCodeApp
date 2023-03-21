using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Models.DTOs
{
    public class BookCsvDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public CategoryCsvDTO Category { get; set; } = new CategoryCsvDTO();
        public AuthorCsvDTO Author { get; set; } = new AuthorCsvDTO();
        public int PhotoId { get; set; } = 1;
        public int Count { get; set; } = 0;
        public decimal Price { get; set; } = 0.01m;
    }

    public class AuthorCsvDTO
    {
        public int? AuthorId { get; set; } = null;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class CategoryCsvDTO
    {
        public int? CategoryId { get; set; } = null;
        public string Code { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class BookCsvDTOMap : ClassMap<BookCsvDTO>
    {
        public BookCsvDTOMap()
        {
            Map(b => b.Title).Name("BookTitle");
            Map(b => b.Description).Name("BookDescription");
            References<CategoryCsvDTOMap>(c => c.Category);
            References<AuthorCsvDTOMap>(c => c.Author);
            Map(b => b.PhotoId).Name("BookPhotoId");
            Map(b => b.Count).Name("BookCount");
            Map(b => b.Price).Name("BookPrice");
        }
    }

    public class AuthorCsvDTOMap : ClassMap<AuthorCsvDTO>
    {
        public AuthorCsvDTOMap()
        {
            Map(a => a.AuthorId).Name("AuthorId");
            Map(a => a.Name).Name("AuthorName");
            Map(a => a.Description).Name("AuthorDescription");
        }
    }

    public class CategoryCsvDTOMap : ClassMap<CategoryCsvDTO>
    {
        public CategoryCsvDTOMap()
        {
            Map(a => a.CategoryId).Name("CategoryId");
            Map(c => c.Code).Name("CategoryCode");
            Map(c => c.Title).Name("CategoryTitle");
            Map(c => c.Description).Name("CategoryDescription");
        }
    }

}
