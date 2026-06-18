using GBP.Core.DTOs.Request;
using GBP.Core.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Core.Interfaces.Services.Data
{
    public interface ITransactionTypeService
    {
        Task<IEnumerable<TransactionTypeResponseDto>> GetAllAsync();
        Task<TransactionTypeResponseDto?> GetByIdAsync(int id);
        Task<TransactionTypeResponseDto> AddAsync(TransactionTypeRequestDto request);
        Task<TransactionTypeResponseDto?> UpdateAsync(int id, TransactionTypeRequestDto request);
        Task<bool> DeleteAsync(int id);
    }
}
