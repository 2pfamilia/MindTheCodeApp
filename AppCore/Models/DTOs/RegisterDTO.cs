using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Models.DTOs
{
    public class RegisterDTO
    {
        public string? FirstName { get; set; }
        public DateTime Birthday { get; set; } = DateTime.Now;
        public string? Email { get; set; }
        public string? LastName { get; set; }
        public string Password { get; set; }
        public string? City { get; set; }
        public string? StreetAddress { get; set; }
        public string? Country { get; set; }
        public string? PostalCode { get; set; }
        public string? Phone { get; set; }




    }
}
