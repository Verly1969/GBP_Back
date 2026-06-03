using GBP.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Domain.Entities;

public class Thrift
{
    public Guid Id { get; set; }
    public required string Label { get; set; }
    public decimal Target { get; set; } // Montant cible à atteindre
    public decimal Actual { get; set; } // Montant actuel épargné
    public DateTime TargetDate { get; set; } // Date limite pour atteindre le montant cible
    public StatusThrift Status { get; set; }

    // Foreign key
    public Guid AccountId { get; set; }

    // Navigation property - Parent entity
    public required Account Account { get; set; }

    // Navigation property - Child entitie
    public ICollection<Deposit> Deposits { get; set; } = [];
}
