using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GBP.Core.DTOs.Request
{
    public class TransactionTypeRequestDto
    {
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Le nom doit comporter entre 3 et 50 caractères.")]
        public required string Name { get; init; }

        [MaxLength(255, ErrorMessage = "La description ne doit pas dépasser 255 caractères.")]
        public string? Description { get; init; }
    }
}
