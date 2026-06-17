using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Domain.Entities;

public class Category
{
    public int Id { get; set; }
    public required string Name { get; set; } = null!;

    // Navigation properties
    public ICollection<SubCategory> SubCategories { get; set; } = [];
    public ICollection<Transaction> Transactions { get; set; } = [];
}
