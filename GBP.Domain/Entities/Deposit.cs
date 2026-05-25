using GBP.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Domain.Entities;

public class Deposit
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public Frequency Frequency { get; set; }
    public int DayOfMonth { get; set; } // Jour du mois ( 1-28)
    public DateTime CreatedAt { get; set; }

    // Foreign key
    public Guid ThriftId { get; set; }

    // Navigation property - Parent entity
    public required Thrift Thrift { get; set; }
}
