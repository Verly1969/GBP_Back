using GBP.Core.DTOs.Request;
using GBP.Core.DTOs.Response;
using GBP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace GBP.Core.Mapper
{
    public static class AccountTypesMapper
    {
        public static AccountTypeResponseDto ToResponse(
            this AccountType accountType)
            {
                return new AccountTypeResponseDto
                {
                    Id = accountType.Id,
                    Name = accountType.Name,
                    Description = accountType.Description
                };
            }

        public static IEnumerable<AccountTypeResponseDto> ToResponseList(
            this IEnumerable<AccountType> accountTypes) =>
            accountTypes.Select(at => at.ToResponse());

        public static AccountType ToEntity(
            this AccountTypesRequestDto request) => new()
            {
                Name = request.Name,
                Description = request.Description
            };
    }
}
