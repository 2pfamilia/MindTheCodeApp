using AppCore.Models.BookModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Models.DTOs
{
    public class SearchPostDTO
    {
        public List<Book>? BooksFound { get; set; }
        public List<FilterDTO> AuthorFiltersFound { get; set; }
        public List<FilterDTO> CategoryFiltersFound { get; set; }
        public int? maxPrice { get; set; }

    }
}
