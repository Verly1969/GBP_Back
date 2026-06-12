using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GBP.Core.DTOs.Request
{
    public class CreditRequestDto
    {
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Le montant doit-être supérieur à 0" )]
        public decimal  Amount           { get; init; }

        [Required]
        [Range(0, 100, ErrorMessage = "Le taux d'intérêt doit être entre 0 et 100" )]
        public decimal  InterestRate     { get; init; }

        [Required]
        [Range(1, 600, ErrorMessage = "La durée doit-être entre 1 et 600 mois" )]
        public int      DurationMonths   { get; init; }

        [Required]
        public DateTime StartDate        { get; init; }

        [MaxLength(255, ErrorMessage = "La raison ne peut dépasser 255 caractères" )]
        public string?  Raison           { get; init; }

        [Required( ErrorMessage = "Le type de crédit est obligatoire" )]
        public int      CreditTypeId     { get; init; }
        public Guid?    PreviousCreditId { get; init; }
    }
}
