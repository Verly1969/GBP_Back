using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Core.DTOs.Response
{
    public class TransactionResponseDto
    {
        public Guid Id { get; init; }
        public decimal Amount { get; init; }
        public DateTime DateOfTransaction { get; init; }
        public string? Description { get; init; }
        public Guid SourceAccountId { get; init; }
        public string SourceAccountLabel { get; init; } = string.Empty;
        public Guid? TargetAccountId { get; init; }
        public string? TargetAccountLabel { get; init; }
        public int SubCategoryId { get; init; }
        public string SubCategoryName { get; init; } = string.Empty;
        public string CategoryName { get; init; } = string.Empty;
        public int TransactionTypeId { get; init; }
        public string TransactionTypeName { get; init; } = string.Empty;
    }
}
