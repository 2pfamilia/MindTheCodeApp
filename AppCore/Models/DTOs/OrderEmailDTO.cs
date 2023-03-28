using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Models.DTOs
{
    public class OrderEmailDTO
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string StreetAddress { get; set; } = string.Empty;
        //Quantity of product
        public decimal TotalCost { get; set; }
    }
}
