using GBP.Core.DTOs.Request;
using GBP.Core.DTOs.Response;
using GBP.Core.Interfaces.Repositories;
using GBP.Core.Interfaces.Services.Data;
using GBP.Core.Mapper;
using GBP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Security.Services.Data
{
    public class TransactionService(
        ITransactionRepository _transactionRepository,
        IAccountRepository _accountRepository,
        ISubCategoryRepository _subCategoryRepository,
        ITransactionTypeRepository _transactionTypeRepository) : ITransactionService
    {
        public async Task<TransactionResponseDto> CreateAsync(TransactionRequestDto request, Guid accountId, Guid userId)
        {
            // Vérifier que le compte de débit existe
            var sourceAccount = await _accountRepository.GetByIdAsync(accountId)
                ?? throw new KeyNotFoundException(
                    $"Le compte de débit avec l'identifiant {accountId} est introuvable.");

            // Vérifier que le compte de débit est bien attribué à l'utilisateur courant
            if (sourceAccount.UserId != userId)
                throw new UnauthorizedAccessException(
                    "Vous n'êtes pas autorisé à accéder à ce compte.");

            // Vérifier que le compte de crédit est indiqué
            if (request.TargetAccountId is not null)
            {
                // Vérifier qu'il existe
                var targetAccount = await _accountRepository.GetByIdAsync(request.TargetAccountId.Value)
                    ?? throw new KeyNotFoundException(
                        $"Le compte de crédit avec l'identifiant {request.TargetAccountId} est introuvable.");

                // Vérifier qu'il appartient à l'utilisateur courant
                if (targetAccount.UserId != userId)
                    throw new UnauthorizedAccessException(
                        "Vous n'êtes pas titulaire de ce compte.");
            }

            // Vérifier que la sous-catégorie existe
            var subCategory = await _subCategoryRepository.GetByIdAsync(request.SubCategoryId)
                ?? throw new KeyNotFoundException(
                    $"La sous-catégorie {request.SubCategoryId} n'existe pas.");

            // Vérifier que le type de transaction existe
            var typeTransaction = await _transactionTypeRepository.GetByIdAsync(request.TransactionTypeId)
                ?? throw new KeyNotFoundException(
                    $"Le type de transaction ({request.TransactionTypeId}) est introuvable");

            var transaction = request.ToEntity(accountId);

            var created = await _transactionRepository.AddAsync(transaction);

            var result = await _transactionRepository.GetByIdAsync(created.Id)
                ?? throw new InvalidOperationException("Erreur lors de la création de la transaction.");

            return result.ToResponse();
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TransactionResponseDto>> GetAllByAccountIdAsync(Guid accountId)
        {
            var transactions = await _transactionRepository.GetAllByAccountAsync(accountId);

            return transactions.ToResponseList();
        }

        public async Task<TransactionResponseDto?> GetByIdAsync(Guid id)
        {
            var existing = await _transactionRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"La transaction '{id}' est introuvable.");

            return existing.ToResponse();
        }

        public async Task<TransactionResponseDto?> UpdateAsync(Guid id, TransactionRequestDto request)
        {
            // Vérifier que la transaction existe
            // Vérifier que la sous-catégorie existe
            // Vérifier que
        }
    }
}
