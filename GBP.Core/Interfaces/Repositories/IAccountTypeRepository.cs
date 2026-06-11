using GBP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Core.Interfaces.Repositories
{
    public interface IAccountTypeRepository
    {
        Task<AccountType?> AddAsync(AccountType accountType);
        Task<AccountType?> UpdateAsync(AccountType accountType);
        Task<bool> DeleteAsync(int id);
        Task<AccountType?> GetByIdAsync(int id);
        Task<IEnumerable<AccountType>> GetAllAsync();
    }
}
