using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GBP.Core.DTOs.Request
{
    public class CreditTypeRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Minimum 3 caractères")]
        [MaxLength(50, ErrorMessage = "Le nom ne doit pas dépasser 50 caractères")]
        public required string Name { get; init; }

        [MinLength(3, ErrorMessage = "Minimum 3 caractères")]
        [MaxLength(255, ErrorMessage = "La description ne doit pas dépasser 255 caractères")]
        public string? Description { get; init; }
    }
}
