using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DTO
{
    public class CommentDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [MinLength(4, ErrorMessage = "Comment must be 4 characters or longer")]
        [MaxLength(150, ErrorMessage = "Comment must be max 150 characters")]
        public string Text { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public int UserId { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public int PostId { get; set; }
    }
}
