using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GBP.Core.DTOs.Request
{
    public class CategoryRequestDto
    {
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Le nom de la catégorie doit avoir entre 3 et 50 caractères.")]
        public required string Name { get; set; } = null!;
    }
}
