using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Domain.Entities;

public class Currency
{
    public int Id { get; set; }
    public required string Code { get; set; } // Ex: USD, EUR, GBP
    public string? Name { get; set; } // Ex: Dollar, Euro, Pound

    // Navigation properties - Child entitie
    public ICollection<Wallet> Wallets { get; set; } = [];
}
