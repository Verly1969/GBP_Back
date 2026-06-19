using GBP.Core.DTOs.Request;
using GBP.Core.DTOs.Response;
using GBP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Core.Mapper
{
    public static class TransactionMapper
    {
        // Entité vers DTO
        public static TransactionResponseDto ToResponse(
            this Transaction transaction)
        {
            return new TransactionResponseDto
            {
                Id = transaction.Id,
                Amount = transaction.Amount,
                DateOfTransaction = transaction.DateOfTransaction,
                Description = transaction.Description,
                SourceAccountId = transaction.SourceAccountId,
                SourceAccountLabel = transaction.SourceAccount.Label,
                TargetAccountId = transaction.TargetAccountId,
                TargetAccountLabel = transaction.TargetAccount?.Label,
                SubCategoryId = transaction.SubCategoryId,
                SubCategoryName = transaction.SubCategory.Name,
                CategoryName = transaction.SubCategory.Category?.Name ?? string.Empty,
                TransactionTypeId = transaction.TransactionTypeId,
                TransactionTypeName = transaction.TransactionType.Name
            };
        }

        // Liste d'entités -> liste DTOs
        public static IEnumerable<TransactionResponseDto> ToResponseList(
            this IEnumerable<Transaction> transactions)
        {
            return transactions.Select(t => t.ToResponse());
        }

        // DTO -> Entité
        public static Transaction ToEntity(
            this TransactionRequestDto request, Guid sourceAccountId)
        {
            return new Transaction
            {
                Id = Guid.NewGuid(),
                Amount = request.Amount,
                DateOfTransaction = request.DateOfTransaction,
                Description = request.Description,
                SourceAccountId = sourceAccountId,
                SourceAccount = null!,
                TargetAccountId = request.TargetAccountId,
                SubCategory = null!,
                SubCategoryId = request.SubCategoryId,
                TransactionType = null!,
                TransactionTypeId = request.TransactionTypeId
            };
        }
    }
}
