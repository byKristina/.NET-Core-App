using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class GetPostDto
    {
        public int Id { get; set; }
 
        public string Title { get; set; }

        public string Content { get; set; }

        public string Category { get; set; }
      
        public string User { get; set; }

        public DateTime PostedOn { get; set; }

    }
}
