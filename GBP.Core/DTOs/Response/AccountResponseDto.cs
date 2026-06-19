using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Core.DTOs.Response
{
    public class AccountResponseDto
    {
        public Guid Id            { get; init; }
        public string Label       { get; init; } = string.Empty;
        public string? Number     { get; init; }
        public decimal Balance    { get; init; }
        public string Status      { get; init; } = string.Empty;
        public string AccountType { get; init; } = string.Empty;
        public int AccountTypeId  { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime? UpdateAt  { get; init; }
        public string? UpdatedBy  { get; init; }

    }
}
