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
    public class CreditTypeService(
        ICreditTypeRepository _creditTypeRepository) : ICreditTypeService
    {
        /// <summary>
        /// Crée un nouveau type de crédit
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Le type de crédit créé sinon message d'erreur</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<CreditTypeResponseDto> CreateAsync(CreditTypeRequestDto request)
        {
            var creditType = request.ToEntity();

            var createCreditType = await _creditTypeRepository.AddAsync(creditType)
                ?? throw new InvalidOperationException("Echec lors de l'ajout du nouveau type de crédit.");

            return createCreditType.ToResponse();
        }

        /// <summary>
        /// Supprime un type de crédit suivant son ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retourne true ou un message d'erreur</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _creditTypeRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Le type de crédit avec pour id '{id}' n'a pas été trouvé.");

            return await _creditTypeRepository.DeleteAsync(existing.Id);
        }

        /// <summary>
        /// Récupère tous les types de crédits
        /// </summary>
        /// <returns>Retourne une liste de tous les types de crédits</returns>
        public async Task<IEnumerable<CreditTypeResponseDto>> GetAllAsync()
        {
            var creditTypes = await _creditTypeRepository.GetAllAsync();

            return creditTypes.ToResponseList();
        }

        /// <summary>
        /// Recherche un type de crédit suivant son ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Le type de crédit ou null</returns>
        public async Task<CreditTypeResponseDto?> GetById(int id)
        {
            var creditType = await _creditTypeRepository.GetByIdAsync(id);

            return creditType?.ToResponse();
        }

        /// <summary>
        /// Modifie un type de crédit
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns>Le type de crédit modifié ou un message d'erreur</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<CreditTypeResponseDto?> UpdateAsync(int id, CreditTypeRequestDto request)
        {
            var existing = await _creditTypeRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Le type de crédit avec pour id '{id}' n'a pas été trouvé.");

            existing.Name        = request.Name;
            existing.Description = request.Description;

            var updated = await _creditTypeRepository.UpdateAsync(existing)
                ?? throw new InvalidOperationException("Erreur lors de la mise à jour.");

            return updated.ToResponse();
        }
    }
}
