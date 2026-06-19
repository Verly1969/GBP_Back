using GBP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Core.Interfaces.Repositories
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetAllByAccountAsync(Guid accountId);
        Task<Transaction?> GetByIdAsync(Guid id);
        Task<Transaction> AddAsync(Transaction transaction);
        Task<Transaction?> UpdateAsync(Transaction transaction);
        Task<bool> DeleteAsync(Guid id);
    }
}
