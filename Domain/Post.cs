using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Post : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImagePath { get; set; }

        public int CategoryId { get; set; }
        public int UserId { get; set; }

        public Category Category { get; set; }
        public User User { get; set; }

        public ICollection<Comment> Comments { get; set; }

    }
}
