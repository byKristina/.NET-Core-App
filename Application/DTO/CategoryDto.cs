using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DTO
{
    public class CategoryDto
    {
        public int Id { get; set; }
        [MinLength(4, ErrorMessage = "Category name must be 4 characters or longer")]
        public string Name { get; set; }
    }
}
