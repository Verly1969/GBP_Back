using GBP.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Domain.Entities;

public class Credit
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public decimal InterestRate { get; set; }
    public int DurationMonths { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public StatusCredit Status { get; set; }
    public string? Raison { get; set; }

    // Auto référence
    public Guid? PreviousCreditId { get; set; }

    // Foreign keys
    public Guid AccountId { get; set; }
    public int CreditTypeId { get; set; }

    // Navigation properties - Parent entities
    public required Account Account { get; set; }
    public required CreditType CreditType { get; set; }

    // Navigation properties - Child entities
    public ICollection<Payment> Payments { get; set; } = [];
}
