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
    public class AccountTypeService(
        IAccountTypeRepository _accountTypeRepository) : IAccountTypeService
    {
        /// <summary>
        /// Crée un nouveau type de compte.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Le type de compte créé.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<AccountTypeResponseDto> CreateAsync(AccountTypesRequestDto request)
        {
            var accountType = request.ToEntity();

            var createdAccountType = await _accountTypeRepository.AddAsync(accountType)
                ?? throw new InvalidOperationException("Echec de la création du type de compte.");

            return createdAccountType.ToResponse();
        }

        /// <summary>
        /// Supprime un type de compte existant par son ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True si le type de compte a été supprimé, false sinon.</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _accountTypeRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Type de compte avec l'id '{id}' non trouvé.");

            return await _accountTypeRepository.DeleteAsync(existing.Id);
        }

        /// <summary>
        /// Récupère tous les types de compte disponibles.
        /// </summary>
        /// <returns>Une liste de tous les types de compte.</returns>
        public async Task<IEnumerable<AccountTypeResponseDto>> GetAllAsync()
        {
            var accountTypes = await _accountTypeRepository.GetAllAsync();

            return accountTypes.ToResponseList();
        }

        /// <summary>
        /// Récupère un type de compte par son ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Le type de compte correspondant à l'ID, ou null s'il n'est pas trouvé.</returns>
        public async Task<AccountTypeResponseDto?> GetByIdAsync(int id)
        {
            var accountType = await _accountTypeRepository.GetByIdAsync(id);

            return accountType?.ToResponse();
        }

        /// <summary>
        /// Met à jour un type de compte existant avec les nouvelles données fournies.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns>Le type de compte mis à jour, ou null s'il n'est pas trouvé.</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<AccountTypeResponseDto?> UpdateAsync(int id, AccountTypesRequestDto request)
        {
            var existing = await _accountTypeRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Type de compte avec l'id '{id}' non trouvé.");

            existing.Name = request.Name;
            existing.Description = request.Description;

            var updated = await _accountTypeRepository.UpdateAsync(existing)
                ?? throw new InvalidOperationException("Echec de la mise à jour du type de compte.");

            return updated.ToResponse();
        }
    }
}
