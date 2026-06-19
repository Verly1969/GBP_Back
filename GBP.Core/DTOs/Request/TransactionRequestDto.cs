using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GBP.Core.DTOs.Request
{
    public class TransactionRequestDto
    {
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Le montant est obligatoire.")]
        public decimal Amount { get; init; }

        [Required]
        public DateTime DateOfTransaction { get; init; }

        [StringLength(255, MinimumLength = 3, ErrorMessage = "La description doit être comprise entre 3 et 255 caractères.")]
        public string? Description { get; init; }
        public Guid? TargetAccountId { get; init; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "La sous-catégorie est obligatoire.")]
        public int SubCategoryId { get; init; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Le type de transaction est obligatoire.")]
        public int TransactionTypeId { get; init; }
    }
}
