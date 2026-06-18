using GBP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Core.Interfaces.Repositories
{
    public interface ITransactionTypeRepository
    {
        Task<IEnumerable<TransactionType>> GetAllAsync();
        Task<TransactionType?> GetByIdAsync(int id);
        Task<TransactionType> AddAsync(TransactionType transactionType);
        Task<TransactionType?> UpdateAsync(TransactionType transactionType);
        Task<bool> DeleteAsync(int id);
    }
}
