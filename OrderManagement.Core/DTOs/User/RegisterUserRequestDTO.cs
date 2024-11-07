using OrderManagement.Core.Attributes;
using System.ComponentModel.DataAnnotations;

namespace OrderManagement.Core.DTOs.User
{
    public class RegisterUserRequestDTO
    {
        [Required(ErrorMessage = "Email is required")]
        [CustomEmail(ErrorMessage = "Please provide a valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 20 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "FullName is required")]
        public string FullName { get; set; }

    }
}
