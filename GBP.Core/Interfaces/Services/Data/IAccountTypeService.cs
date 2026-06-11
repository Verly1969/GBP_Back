using GBP.Core.DTOs.Request;
using GBP.Core.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Core.Interfaces.Services.Data
{
    public interface IAccountTypeService
    {
        Task<AccountTypeResponseDto> CreateAsync(AccountTypesRequestDto resquest);
        Task<AccountTypeResponseDto?> UpdateAsync(int id, AccountTypesRequestDto request);
        Task<bool> DeleteAsync(int id);
        Task<AccountTypeResponseDto?> GetByIdAsync(int id);
        Task<IEnumerable<AccountTypeResponseDto>> GetAllAsync();
    }
}
