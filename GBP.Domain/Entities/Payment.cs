using GBP.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Domain.Entities;

public class Payment
{
    public Guid Id { get; set; }
    public DateTime DatePayment { get; set; }
    public decimal Amount { get; set; } // Mensualité due
    public decimal PayAmount { get; set; } // Montant payé
    public StatusPayment Status { get; set; }

    // Foreign key
    public Guid CreditId { get; set; }

    // Navigation property - Parent entity
    public required Credit Credit { get; set; }
}
