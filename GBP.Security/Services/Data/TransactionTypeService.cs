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
    public class TransactionTypeService(
        ITransactionTypeRepository _transactionTypeRepository) : ITransactionTypeService
    {
        public async Task<TransactionTypeResponseDto> AddAsync(TransactionTypeRequestDto request)
        {
            var entity = request.ToEntity();

            var created = await _transactionTypeRepository.AddAsync(entity)
                ?? throw new InvalidOperationException("Echec lors de l'ajout du nouveau type de transaction.");

            return created.ToResponse();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _transactionTypeRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Le type de transaction avec pour id '{id}' n'a pas été trouvé.");

            return await _transactionTypeRepository.DeleteAsync(entity.Id);
        }

        public async Task<IEnumerable<TransactionTypeResponseDto>> GetAllAsync()
        {
            var transactionTypes = await _transactionTypeRepository.GetAllAsync();

            return transactionTypes.ToResponseList();
        }

        public async Task<TransactionTypeResponseDto?> GetByIdAsync(int id)
        {
            var existing = await _transactionTypeRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Le type de transaction avec pour id '{id}' n'a pas été trouvé.");

            return existing.ToResponse();
        }

        public async Task<TransactionTypeResponseDto?> UpdateAsync(int id, TransactionTypeRequestDto request)
        {
            var existing = await _transactionTypeRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Le type de transaction avec pour id '{id}' n'a pas été trouvé.");

            existing.Name = request.Name;
            existing.Description = request.Description;

            var updated = await _transactionTypeRepository.UpdateAsync(existing)
                ?? throw new InvalidOperationException("Erreur lors de la mise à jour.");

            return updated.ToResponse();
        }
    }
}
