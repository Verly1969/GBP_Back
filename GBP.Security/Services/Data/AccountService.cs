using GBP.Core.DTOs.Request;
using GBP.Core.DTOs.Response;
using GBP.Core.Interfaces.Repositories;
using GBP.Core.Interfaces.Services.Data;
using GBP.Core.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Security.Services.Data
{
    public class AccountService(
        IAccountRepository _accountRepository) : IAccountService
    {
        /// <summary>
        /// Crée un nouveau compte pour un utilisateur donné. 
        /// Le compte est initialisé avec un solde de 0 et un statut actif.
        /// </summary>
        /// <param name="accountDto"></param>
        /// <param name="userId"></param>
        /// <returns>Le compte créé</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<AccountResponseDto> CreateAsync(AccountRequestDto requestDto, Guid userId)
        {
            var account = requestDto.ToEntity(userId);
            var created = await _accountRepository.AddAsync(account)
                ?? throw new InvalidOperationException("Echec de la création du compte");

            // Recharger avec AccountType pour le mapper
            var result = await _accountRepository.GetByIdAsync(created.Id);
            return result!.ToResponse();
        }

        /// <summary>
        /// Supprime un compte existant.
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns>True si le compte est supprimé, false sinon</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task<bool> DeleteAsync(Guid accountId)
        {
            var existing = await _accountRepository.GetByIdAsync(accountId)
                ?? throw new KeyNotFoundException($"Compte avec ID {accountId} non trouvé");

            return await _accountRepository.DeleteAsync(existing.Id);
        }

        /// <summary>
        /// Récupère tous les comptes associés à un utilisateur donné.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Une liste de comptes</returns>
        public async Task<IEnumerable<AccountResponseDto>> GetAllByUserIdAsync(Guid userId)
        {
            var accounts = await _accountRepository.GetAllByUserIdAsync(userId);

            return accounts.ToResponseList();
        }

        /// <summary>
        /// Récupère un compte par son ID.
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns>Le compte correspondant ou null s'il n'est pas trouvé</returns>
        public async Task<AccountResponseDto?> GetByIdAsync(Guid accountId)
        {
            var account = await _accountRepository.GetByIdAsync(accountId);

            return account?.ToResponse();
        }

        /// <summary>
        /// Met à jour les informations d'un compte existant. 
        /// Seules les propriétés modifiables sont mises à jour (Label, Number, AccountTypeId).
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="request"></param>
        /// <param name="updatedBy"></param>
        /// <returns>Le compte mis à jour ou null s'il n'est pas trouvé</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task<AccountResponseDto?> UpdateAsync(Guid accountId, AccountRequestDto request, string updatedBy)
        {
            var existing = await _accountRepository.GetByIdAsync(accountId)
                ?? throw new KeyNotFoundException($"Compte avec ID {accountId} non trouvé");

            existing.Label = request.Label;
            existing.Number = request.Number;
            existing.AccountTypeId = request.AccountTypeId;
            existing.UpdatedBy = updatedBy;

            var updated = await _accountRepository.UpdateAsync(existing);

            return updated?.ToResponse();
        }
    }
}
