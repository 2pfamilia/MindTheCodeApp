using AppCore.Models.BookModels;
using AppCore.Models.OrderModels;
using AppCore.Models.PhotoModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Models.DTOs
{
    public class SearchByFilterResultDTO
    {
        public int BookId { get; set; }
       
        public string? Title { get; set; }
      
        public string? Description { get; set; }

        public BookCategory? Category { get; set; }

        public Photo? Photo { get; set; }

        public int? Count { get; set; }

        public BookAuthor? Author { get; set; }

       
        public decimal? Price { get; set; }

    }
}
