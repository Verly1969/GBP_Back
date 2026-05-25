using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Domain.Entities;

public class Wallet
{
    public Guid Id { get; set; }
    public string? Ticker { get; set; }
    public required string Label { get; set; }
    public decimal Quantity { get; set; }
    public decimal AveragePurchasePrice { get; set; }
    public decimal ActualPrice { get; set; }
    public DateTime CreatedAt { get; set; } // Date of the creation of the wallet
    public DateTime? UpdatedAt { get; set; } // Date of the last update of the wallet (price, quantity, etc.)

    // Foreign keys
    public Guid AccountId { get; set; }
    public int InvestmentTypeId { get; set; }
    public int CurrencyId { get; set; }

    // Navigation properties - Parent entities
    public required Account Account { get; set; }
    public required InvestmentType InvestmentType { get; set; }
    public required Currency Currency { get; set; }

    // Navigation properties - Child entitie
    public ICollection<InvestMovement> InvestMovements { get; set; } = [];
}
