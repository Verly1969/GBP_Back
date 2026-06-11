using GBP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Core.Interfaces.Repositories
{
    public interface IAccountRepository
    {
        Task<Account?> AddAsync(Account account);
        Task<Account?> UpdateAsync(Account account);
        Task<bool> DeleteAsync(Guid accountId);
        Task<Account?> GetByIdAsync(Guid accountId);
        Task<IEnumerable<Account>> GetAllByUserIdAsync(Guid userId);
    }
}
