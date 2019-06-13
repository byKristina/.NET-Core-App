using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Searches
{
    public class CommentSearch : BaseSearch
    {
        public int? PostId { get; set; }

        public int? UserId { get; set; }
    }
}
