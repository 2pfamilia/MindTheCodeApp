using AppCore.Models.AuthModels;
using AppCore.Models.BookModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Models.DTOs
{
    public class CheckoutDTO
    {
        public int bookId { get; set; }
        public int quantity { get; set; }
    }
}