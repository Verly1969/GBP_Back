using GBP.Core.DTOs.Request;
using GBP.Core.DTOs.Response;
using GBP.Domain.Entities;
using GBP.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Core.Mapper
{
    public static class CreditMapper
    {
        // Entité -> DTO
        public static CreditResponseDto ToResponse(
            this Credit credit)
        {
            return new CreditResponseDto
            {
                Id = credit.Id,
                Amount = credit.Amount,
                InterestRate = credit.InterestRate,
                DurationMonths = credit.DurationMonths,
                StartDate = credit.StartDate,
                EndDate = credit.EndDate,
                Status = credit.Status.ToString(),
                Raison = credit.Raison,
                PreviousCreditId = credit.PreviousCreditId,
                AccountId = credit.AccountId,
                AccountLabel = credit.Account.Label,
                CreditTypeId = credit.CreditTypeId,
                CreditType = credit.CreditType.Name
            };
        }

        // Liste d'entité -> liste DTO
        public static IEnumerable<CreditResponseDto> ToResponseList(
            this IEnumerable<Credit> credits) =>
            credits.Select(c => c.ToResponse());

        // Dto -> Entité
        public static Credit ToEntity(
            this CreditRequestDto request, Guid accountId)
        {
            return new Credit
            {
                Id = Guid.NewGuid(),
                Amount =           request.Amount,
                InterestRate =     request.InterestRate,
                DurationMonths =   request.DurationMonths,
                StartDate =        request.StartDate,
                EndDate =          request.StartDate.AddMonths(request.DurationMonths),
                Status =           StatusCredit.Active,
                Raison =           request.Raison,
                PreviousCreditId = request.PreviousCreditId,
                AccountId =        accountId,
                CreditTypeId =     request.CreditTypeId,
                Account =          null!,
                CreditType =       null!
            };
        }
    }
}
