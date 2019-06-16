using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Post> Posts { get; set; }

    }
}
