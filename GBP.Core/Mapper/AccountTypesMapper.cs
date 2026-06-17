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
        /// <summary>
        /// Mapper méthode pour convertir un AccountType en AccountTypeResponseDto
        /// </summary>
        /// <param name="accountType"></param>
        /// <returns>Un objet AccountTypeResponseDto</returns>
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

        /// <summary>
        /// Mapper méthode pour convertir une liste d'AccountType en une liste d'AccountTypeResponseDto
        /// </summary>
        /// <param name="accountTypes"></param>
        /// <returns>Une liste d'objets AccountTypeResponseDto</returns>
        public static IEnumerable<AccountTypeResponseDto> ToResponseList(
            this IEnumerable<AccountType> accountTypes) =>
            accountTypes.Select(at => at.ToResponse());

        /// <summary>
        /// Mapper méthode pour convertir un AccountTypesRequestDto en AccountType
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Un objet AccountType</returns>
        public static AccountType ToEntity(
            this AccountTypesRequestDto request) => new()
            {
                Name = request.Name,
                Description = request.Description
            };
    }
}
