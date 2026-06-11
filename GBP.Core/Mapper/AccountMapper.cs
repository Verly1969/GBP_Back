using GBP.Core.DTOs.Request;
using GBP.Core.DTOs.Response;
using GBP.Domain.Entities;
using GBP.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Core.Mapper
{
    public static class AccountMapper
    {
        // Entité -> DTO
        public static AccountResponseDto ToResponse(
            this Account account)
        {
            return new AccountResponseDto
            {
                Id = account.Id,
                Label = account.Label,
                Number = account.Number,
                Balance = account.Balance,
                Status = account.Status.ToString(),
                AccountType = account.AccountType.Name,
                AccountTypeId = account.AccountTypeId,
                CreatedAt = account.CreatedAt,
                UpdateAt = account.UpdatedAt,
                UpdatedBy = account.UpdatedBy
            };
        }

        // Liste d'entités -> Liste de DTOs
        public static IEnumerable<AccountResponseDto> ToResponseList(
            this IEnumerable<Account> accounts)
        {
            return accounts.Select(a => a.ToResponse());
        }

        // DTO -> Entité (si nécessaire, par exemple pour les créations ou mises à jour)
        public static Account ToEntity(
            this AccountRequestDto dto, Guid userId)
        {
            return new Account
            {
                Id = Guid.NewGuid(),
                Label = dto.Label,
                Number = dto.Number,
                Balance = 0,
                Status = Status.Active, // Par défaut à la création
                AccountTypeId = dto.AccountTypeId,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                User = null!, // À remplir selon le contexte
                AccountType = null!, // À remplir selon le contexte
            };
        }
    }
}
