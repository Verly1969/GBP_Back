using GBP.Core.DTOs.Request;
using GBP.Core.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Core.Interfaces.Services.Data
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionResponseDto>> GetAllByAccountIdAsync(Guid accountId);
        Task<TransactionResponseDto?> GetByIdAsync(Guid id);
        Task<TransactionResponseDto> CreateAsync(TransactionRequestDto request, Guid accountId, Guid userId);
        Task<TransactionResponseDto?> UpdateAsync(Guid id, TransactionRequestDto request);
        Task<bool> DeleteAsync(Guid id);
    }
}
