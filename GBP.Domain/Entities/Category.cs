using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Domain.Entities;

public class Category
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? SubCategory { get; set; }
    
    // Auto référence
    public int? ParentId { get; set; }

    // Navigation properties
    public Category? Parent { get; set; }
    public ICollection<Category> Children { get; set; } = [];
    public ICollection<Transaction> Transactions { get; set; } = [];
}
