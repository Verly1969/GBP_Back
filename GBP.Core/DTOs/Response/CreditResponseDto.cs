using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Core.DTOs.Response
{
    public class CreditResponseDto
    {
        public Guid      Id               { get; init; }
        public decimal   Amount           { get; init; }
        public decimal   InterestRate     { get; init; }
        public int       DurationMonths   { get; init; }
        public DateTime  StartDate        { get; init; }
        public DateTime? EndDate          { get; init; }
        public string    Status           { get; init; } = null!;
        public string?   Raison           { get; init; }
        public Guid?     PreviousCreditId { get; init; }
        public Guid      AccountId        { get; init; }
        public string    AccountLabel     { get; init; } = null!;
        public int       CreditTypeId     { get; init; }
        public string    CreditType       { get; init; } = null!;
    }
}
