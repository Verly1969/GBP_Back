using GBP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Core.Interfaces.Repositories
{
    public interface ICreditTypeRepository
    {
        Task<CreditType?> AddAsync(CreditType creditType);
        Task<CreditType?> UpdateAsync(CreditType creditType);
        Task<bool> DeleteAsync(int id);
        Task<CreditType?> GetByIdAsync(int id);
        Task<IEnumerable<CreditType>> GetAllAsync();
    }
}
