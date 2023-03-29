using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Models.BookModels;


namespace AppCore.Models.DTOs
{
    public class SearchDTO
    {
        public string? SearchTerm { get; set; }
        public List<int>? CategoryIDs { get; set; }
        public List<int>? AuthorIDs { get; set; }
        public int? maxPrice;
    
    }
}