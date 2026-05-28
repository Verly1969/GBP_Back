using GBP.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Domain.Entities;

public class Log
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public Level Level { get; set; }
    public required string Message { get; set; }
    public string? Source { get; set; }
    public string? Exception { get; set; }
    public string? CorrelationId { get; set; }
    public string? Context { get; set; }

    // Foreign key
    public Guid? UserId { get; set; }

    // Navigation property - Parent entity
    public User? User { get; set; }
}
