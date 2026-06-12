using GBP.Core.DTOs.Request;
using GBP.Core.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Core.Interfaces.Services.Data
{
    public interface ICreditTypeService
    {
        Task<IEnumerable<CreditTypeResponseDto>> GetAllAsync();
        Task<CreditTypeResponseDto?> GetById(int id);
        Task<CreditTypeResponseDto> CreateAsync(CreditTypeRequestDto request);
        Task<CreditTypeResponseDto?> UpdateAsync(int id, CreditTypeRequestDto request);
        Task<bool> DeleteAsync(int id);
    }
}
