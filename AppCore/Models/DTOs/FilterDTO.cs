using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Models.DTOs
{
    public class FilterDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool isActive { get; set; } = false;
    }
}
