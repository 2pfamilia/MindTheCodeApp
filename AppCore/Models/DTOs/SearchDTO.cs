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
        //public string? SearchTerm { get; set; }

        public List<Book>? BooksByTitle { get; set; }
        public List<Book>? BooksByAuthor { get; set; }
        public List<Book>? BooksByCategory { get; set; }


        //public int? PageIndex { get; set; }
        //public int? PageCount { get; set; }
    }
}