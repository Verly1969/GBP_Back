using GBP.Core.DTOs.Request;
using GBP.Core.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Core.Interfaces.Services.Data
{
    public interface ICreditService
    {
        Task<IEnumerable<CreditResponseDto>> GetAllByAccountIdAsync(Guid accountId);
        Task<CreditResponseDto?> GetByIdAsync(Guid creditId);
        Task<CreditResponseDto> CreateAsync(CreditRequestDto request, Guid accountId, Guid userId);
        Task<CreditResponseDto?> UpdateAsync(Guid creditId, CreditRequestDto request);
        Task<bool> DeleteAsync(Guid creditId);
    }
}
