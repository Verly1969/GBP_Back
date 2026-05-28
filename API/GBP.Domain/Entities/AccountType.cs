using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Domain.Entities
{
    public class AccountType
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }

        // Navigation properties 
        // Account
    }
}
