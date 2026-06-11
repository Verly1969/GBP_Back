using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GBP.Core.DTOs.Request
{
    public class AccountRequestDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Le libellé ne peut pas dépasser 100 caractères.")]
        [MinLength(3, ErrorMessage = "Le libellé doit comporter au moins 3 caractères.")]
        public required string Label { get; init; }

        [MaxLength(50, ErrorMessage = "Le numéro ne peut pas dépasser 50 caractères.")]
        public string? Number { get; init; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "L'ID du type de compte doit être un entier positif.")]
        public required int AccountTypeId { get; init; }
    }
}
