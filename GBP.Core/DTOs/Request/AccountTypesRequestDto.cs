using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GBP.Core.DTOs.Request
{
    public class AccountTypesRequestDto
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Le nom ne peut pas dépasser 50 caractères.")]
        [MinLength(3, ErrorMessage = "Le nom doit contenir au moins 3 caractères")]
        public required string Name { get; set; } = null!;

        [MaxLength(255, ErrorMessage = "La description ne peut pas dépasser 255 caractères.")]
        public string? Description { get; set; }
    }
}
