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
    public class CreditService(
        ICreditRepository _creditRepository,
        IAccountRepository _accountRepository) : ICreditService
    {
        /// <summary>
        /// Crée un nouveau crédit pour un compte donné.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="accountId"></param>
        /// <returns>Le crédit créé.</returns>
        public async Task<CreditResponseDto> CreateAsync(CreditRequestDto request, Guid accountId, Guid userId)
        {
            // Vérifier que le compte existe
            var account = await _accountRepository.GetByIdAsync(accountId)
                ?? throw new KeyNotFoundException(
                    $"Le compte avec l'identifiant {accountId} est introuvable.");

            // Vérifier que le compte est bien attribué à l'utilisateur courant
            if (account.UserId != userId) 
                throw new UnauthorizedAccessException(
                    "Vous n'êtes pas autorisé à accéder à ce compte.");

            // On vérifie qu'il s'agit bien d'un compte courant
            if (account.AccountType.Name != "Courant")
                throw new InvalidOperationException(
                    "Ce compte ne peut pas être utilisé pour enregistrer un crédit " +
                    "Veuillez utilisé un compte courant");

            var credit = request.ToEntity(accountId);
            var created = await _creditRepository.AddAsync(credit);

            // Recharger avec Account + CreditType pour le mapper
            var result = await _creditRepository.GetByIdAsync(created.Id);

            return result!.ToResponse();
        }

        /// <summary>
        /// Supprime un crédit existant par son ID.
        /// </summary>
        /// <param name="creditId"></param>
        /// <returns>True si le crédit est supprimé, false sinon.</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task<bool> DeleteAsync(Guid creditId)
        {
            var existing = await _creditRepository.GetByIdAsync(creditId)
                ?? throw new KeyNotFoundException($"Le crédit '{creditId}' est introuvable.");

            return await _creditRepository.DeleteAsync(existing.Id);
        }

        /// <summary>
        /// Récupère tous les crédits associés à un compte donné.
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns>La liste des crédits associés au compte.</returns>
        public async Task<IEnumerable<CreditResponseDto>> GetAllByAccountIdAsync(Guid accountId)
        {
            var credits = await _creditRepository.GetAllByAccountIdAsync(accountId);

            return credits.ToResponseList();
        }

        /// <summary>
        /// Récupère un crédit par son ID.
        /// </summary>
        /// <param name="creditId"></param>
        /// <returns>Le crédit trouvé ou null.</returns>
        public async Task<CreditResponseDto?> GetByIdAsync(Guid creditId)
        {
            var credit = await _creditRepository.GetByIdAsync(creditId);

            return credit?.ToResponse();
        }

        /// <summary>
        /// Met à jour un crédit existant avec les nouvelles données fournies.
        /// </summary>
        /// <param name="creditId"></param>
        /// <param name="request"></param>
        /// <returns>Le crédit mis à jour ou null.</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task<CreditResponseDto?> UpdateAsync(Guid creditId, CreditRequestDto request)
        {
            var existing = await _creditRepository.GetByIdAsync(creditId)
                ?? throw new KeyNotFoundException($"Le crédit '{creditId}' est introuvable.");

            existing.Amount = request.Amount;
            existing.InterestRate = request.InterestRate;
            existing.DurationMonths = request.DurationMonths;
            existing.StartDate = request.StartDate;
            existing.EndDate = request.StartDate.AddMonths(request.DurationMonths);
            existing.Raison = request.Raison;
            existing.CreditTypeId = request.CreditTypeId;

            var updated = await _creditRepository.UpdateAsync(existing);

            return updated?.ToResponse();
        }
    }
}
