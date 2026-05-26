using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Domain.Entities;

public class InvestMovementType
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }

    // Navigation properties - Child entities
    public ICollection<InvestMovement> InvestMovements { get; set; } = [];
}
