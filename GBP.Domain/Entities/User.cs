using GBP.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public required string LastName { get; set; }
    public required string FirstName { get; set; }
    public required string Email { get; set; }
    public required string SecretKeyHash { get; set; }
    public required string PasswordHash { get; set; }
    public Role Role { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public Status Status { get; set; }
    public DateTime? LastConnected { get; set; }

    // Navigation properties - Child entities
    public ICollection<Account> Accounts { get; set; } = [];
    public ICollection<Log> Logs { get; set; } = [];
}