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
<<<<<<< HEAD
        [StringLength(20, MinimumLength = 9, ErrorMessage = "Password must be between 9 and 20 characters.")]
=======
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 20 characters.")]
>>>>>>> cd5a8a7e7e6f91cd650125a16ede1543b8dc2cf0
        public required string Password { get; set; }
    }
}
