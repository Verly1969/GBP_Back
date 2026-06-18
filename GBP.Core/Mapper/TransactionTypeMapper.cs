using GBP.Core.DTOs.Request;
using GBP.Core.DTOs.Response;
using GBP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Core.Mapper
{
    public static class TransactionTypeMapper
    {
        public static TransactionTypeResponseDto ToResponse(
            this TransactionType transactionType)
        {
            return new TransactionTypeResponseDto
            {
                Id = transactionType.Id,
                Name = transactionType.Name,
                Description = transactionType.Description
            };
        }

        public static IEnumerable<TransactionTypeResponseDto> ToResponseList(
            this IEnumerable<TransactionType> transactionTypes) =>
            transactionTypes.Select(tt => tt.ToResponse());

        public static TransactionType ToEntity(
            this TransactionTypeRequestDto request)
        {
            return new TransactionType
            {
                Name = request.Name,
                Description = request.Description
            };
        }
    }
}
