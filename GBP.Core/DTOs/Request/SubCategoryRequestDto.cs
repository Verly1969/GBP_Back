using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GBP.Core.DTOs.Request
{
    public class SubCategoryRequestDto
    {
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Le nom de la sous-catégorie doit avoir entre 3 et 50 caractères.")]
        public required string Name { get; set; } = null!;

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "L'ID de la catégorie doit être un entier positif.")]
        public int CategoryId { get; set; }
    }
}
