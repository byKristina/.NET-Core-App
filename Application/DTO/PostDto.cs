using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DTO
{
    public class PostDto
    {

        public int Id { get; set; }
        [Required]
        [MinLength(4, ErrorMessage = "Minimum number of characters is 4")]
        [MaxLength(70, ErrorMessage = "Maximum number of characters is 70")]
        public string Title { get; set; }

        [Required]
        [MinLength(4, ErrorMessage = "Minimum number of characters is 4")]
        public string Content { get; set; }

        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int UserId { get; set; }

        public string ImagePath { get; set; }
      
        public IFormFile Image { get; set; }

        public DateTime PostedOn { get; set; }

    }
}

