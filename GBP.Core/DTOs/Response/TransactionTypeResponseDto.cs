using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Core.DTOs.Response
{
    public class TransactionTypeResponseDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = null!;
        public string? Description { get; init; }
    }
}
