using System;
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs
{
    public class UserForRegisterDto
    {
        public UserForRegisterDto()
        {
            CreatedAt = DateTime.Now;
            LastActive = DateTime.Now;
        }

        [Required] public string Username { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "A jelszónak minimum 6 karakteresnek kell lennie")]
        public string Password { get; set; }

        [Required] public string Email { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime LastActive { get; set; }
    }
}