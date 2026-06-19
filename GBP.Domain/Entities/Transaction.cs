using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Domain.Entities;

public class Transaction
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime DateOfTransaction { get; set; }
    public string? Description { get; set; }

    // Foreigen Keys
    public Guid SourceAccountId { get; set; }
    public Guid? TargetAccountId { get; set; }
    public int SubCategoryId { get; set; }
    public int TransactionTypeId { get; set; }

    // Navigation Properties - Parent entities
    public required Account SourceAccount { get; set; }
    public Account? TargetAccount { get; set; }
    public required SubCategory SubCategory { get; set; }
    public required TransactionType TransactionType { get; set; }
}
