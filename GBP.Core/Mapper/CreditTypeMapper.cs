using GBP.Core.DTOs.Request;
using GBP.Core.DTOs.Response;
using GBP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace GBP.Core.Mapper
{
    public static class CreditTypeMapper
    {
        // Entité -> DTO réponse
        public static CreditTypeResponseDto ToResponse(
            this CreditType creditType)
        {
            return new CreditTypeResponseDto
            {
                Id          = creditType.Id,
                Name        = creditType.Name,
                Description = creditType.Description
            };
        }

        // Liste -> Liste de DTOs
        public static IEnumerable<CreditTypeResponseDto> ToResponseList(
            this IEnumerable<CreditType> creditTypes) =>
            creditTypes.Select(c => c.ToResponse());

        // DTO création -> entité
        public static CreditType ToEntity(
            this CreditTypeRequestDto request) => new()
            {
                Name        = request.Name,
                Description = request.Description
            };
    }
}
