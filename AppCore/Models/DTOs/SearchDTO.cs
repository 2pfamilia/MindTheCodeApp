using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Models.DTOs
{
    public class SearchDTO
    {
        public string? SearchTerm { get; set; }
        public int[]? Categories { get; set; }
        public int[]? Authors { get; set; }

        //public int? PageIndex { get; set; }
        //public int? PageSize { get; set; }
        //public int? PageCount { get; set; }
    }
}
