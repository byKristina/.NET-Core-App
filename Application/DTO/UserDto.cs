using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DTO
{
    public class UserDto
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Minimum number of characters is 3")]
        [MaxLength(30, ErrorMessage = "Maximum number of characters is 30")]
        [RegularExpression(@"^([A-Z][a-z]+)(\s[A-Z][a-z]+)*$", ErrorMessage = "First name must begin with capital letter")]

        public string FirstName { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Minimum number of characters is 3")]
        [MaxLength(20, ErrorMessage = "Maximum number of characters is 20")]
        [RegularExpression(@"^([A-Z][a-z]+)(\s[A-Z][a-z]+)*$", ErrorMessage = "Last name must begin with capital letter")]

        public string LastName { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Minimum number of characters is 3")]
        [MaxLength(20, ErrorMessage = "Maximum number of characters is 20")]

        public string Username { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }

        [Required]
        [MaxLength(15, ErrorMessage = "The password must be max 15 characters long")]
        [MinLength(7, ErrorMessage = "Minimum number of characters for password is 7")]
        public string Password { get; set; }

        [Required]
        public int RoleId { get; set; }

    }
}
