using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Domain.Entities;

public class InvestMovement
{
    public Guid Id { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Charge { get; set; }

    // Foreign keys
    public Guid WalletId { get; set; }
    public int InvestTypeId { get; set; }

    // Navigation properties - Parent entities
    public required Wallet Wallet { get; set; }
    public required InvestMovementType InvestMovementType { get; set; }
}
