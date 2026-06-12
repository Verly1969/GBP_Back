using GBP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Core.Interfaces.Repositories
{
    public interface ICreditRepository
    {
        Task<Credit> AddAsync(Credit credit);
        Task<Credit?> UpdateAsync(Credit credit);
        Task<bool> DeleteAsync(Guid creditId);
        Task<Credit?> GetByIdAsync(Guid creditId);
        Task<IEnumerable<Credit>> GetAllByAccountIdAsync(Guid accountId);
    }
}
