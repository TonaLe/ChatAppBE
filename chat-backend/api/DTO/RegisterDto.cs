using System;
using System.ComponentModel.DataAnnotations;

namespace api.DTO
{
    public class RegisterDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string KnownAs { get; set; }

        public string Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        [Required]
        [StringLength(8, MinimumLength =4)]
        public string Password { get; set; }
    }
}
