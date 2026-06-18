using GBP.Core.DTOs.Request;
using GBP.Core.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Core.Interfaces.Services.Data
{
    public interface ISubCategoryService
    {
        Task<IEnumerable<SubCategoryResponseDto>> GetAllByCategoryIdAsync(int categoryId);
        Task<SubCategoryResponseDto?> GetByIdAsync(int id);
        Task<SubCategoryResponseDto> AddAsync(SubCategoryRequestDto request);
        Task<SubCategoryResponseDto?> UpdateAsync(int id, SubCategoryRequestDto request);
        Task<bool> DeleteAsync(int id);
    }
}
