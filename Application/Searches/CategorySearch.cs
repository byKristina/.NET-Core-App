using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Searches
{
    public class CategorySearch
    {
        public string Keyword { get; set; }
        public bool? Active { get; set; }

        public int PerPage { get; set; } = 2;
        public int PageNumber { get; set; } = 1;
    }
}
