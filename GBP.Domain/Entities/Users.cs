using GBP.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Domain.Entities;

public class Users
{
    public required Guid Id { get; set; }
    public required string LastName { get; set; }
    public required string FirstName { get; set; }
    public required string Email { get; set; }
    public required string SecretKeyHash { get; set; }
    public required string PasswordHash { get; set; }
    public required Role Role { get; set; }
    public required DateTime CreatedAt { get; set; } = DateTime.UtcNow;

}