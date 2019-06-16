using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DTO
{
    public class GetCommentDto
    {
        public int Id { get; set; }

        public string Comment { get; set; } 

        public string User { get; set; }

        public string Post { get; set; }

    }
}
