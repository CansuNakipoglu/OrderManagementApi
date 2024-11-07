using System.ComponentModel.DataAnnotations;

namespace OrderManagement.Core.DTOs.User
{
    public class LoginUserRequestDTO
    {
        [Required(ErrorMessage = "Email Zorunlu Alan")]
        public string Email { get; set; }

        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 20 characters")]
        public string Password { get; set; }
    }
}
