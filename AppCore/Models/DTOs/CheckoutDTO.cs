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
        public User User { get; set; } 
        public Dictionary<Book, int> bookQuantities { get; set; }
        
    }
}
