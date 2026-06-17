using GBP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Core.Interfaces.Repositories
{
    public interface ISubCategoryRepository
    {
        Task<ICollection<SubCategory>> GetAllByCategoryIdAsync(int categoryId);
        Task<SubCategory?> GetByIdAsync(int id);
        Task<SubCategory> AddAsync(SubCategory subCategory);
        Task<SubCategory?> UpdateAsync(SubCategory subCategory);
        Task<bool> DeleteAsync(int id);
    }
}
