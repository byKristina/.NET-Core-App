using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.DTO
{
    public class PostImageDto
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

        [Required(ErrorMessage = "Image is required.")]
        public IFormFile Image { get; set; }

    }
}
