using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Searches
{
    public class PostSearch : BaseSearch
    {
        public string Title { get; set; }
        
        public int? CategoryId { get; set; }

        public int? UserId { get; set; }
    }
}
