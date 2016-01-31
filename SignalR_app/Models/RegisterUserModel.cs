using System;
using System.ComponentModel.DataAnnotations;

namespace SignalR_app.Models
{
    public class RegisterModel
    {
        [Required]
        public string Email { get; set; }

        public DateTime RegisteredFrom { get; set; }

        [Required(ErrorMessage = "Enter user nickname (will be displayed only).")]
        public string DisplayName { get; set; }

        public string Gender { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords are not equal.")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}