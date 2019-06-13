using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DTO
{
    public class RoleDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="This field is required")]
        [MinLength(3, ErrorMessage = "Role name must be 3 characters or longer")]
        public string Name { get; set; }

    }
}
