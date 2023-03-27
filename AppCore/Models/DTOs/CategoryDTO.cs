using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Models.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public bool? IsSelected { get; set; }
    }
}
