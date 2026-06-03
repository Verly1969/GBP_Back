using System.ComponentModel.DataAnnotations;

namespace GBP.Core.DTOs.Request
{
    public class LoginRequestDto
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [StringLength(255, ErrorMessage = "Email cannot exceed 255 characters.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(20, MinimumLength = 9, ErrorMessage = "Password must be between 9 and 20 characters.")]
        public required string Password { get; set; }
    }
}
