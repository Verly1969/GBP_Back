using GBP.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Domain.Entities;

public class Account
{
    public Guid Id { get; set; }
    public required string Label { get; set; }
    public string? Number { get; set; }
    public decimal Balance { get; set; }
    public Status Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }

    // Foreign keys
    public Guid UserId { get; set; }
    public int AccountTypeId { get; set; }

    // Navigation properties - Parent entities
    public required User User { get; set; }
    public required AccountType AccountType { get; set; }

    // Navigation properties - Child entities
    public ICollection<Transaction> Transactions { get; set; } = [];
    public ICollection<Credit> Credits { get; set; } = [];
    public ICollection<Wallet> Wallets { get; set; } = [];
    public ICollection<Thrift> Thrifts { get; set; } = [];
}
