using GBP.Core.DTOs.Request;
using GBP.Core.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Core.Interfaces.Services.Data
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountResponseDto>> GetAllByUserIdAsync(Guid userId);
        Task<AccountResponseDto?> GetByIdAsync(Guid accountId);
        Task<AccountResponseDto> CreateAsync(AccountRequestDto accountDto, Guid userId);
        Task<AccountResponseDto?> UpdateAsync(Guid accountId, AccountRequestDto request, string updatedBy);
        Task<bool> DeleteAsync(Guid accountId);
    }
}
