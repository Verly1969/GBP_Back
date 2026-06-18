using GBP.Core.DTOs.Request;
using GBP.Core.DTOs.Response;
using GBP.Core.Interfaces.Repositories;
using GBP.Core.Interfaces.Services.Data;
using GBP.Core.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Security.Services.Data
{
    public class SubCategoryService(
        ISubCategoryRepository _subCategoryRepository) : ISubCategoryService
    {
        public async Task<SubCategoryResponseDto> AddAsync(SubCategoryRequestDto request)
        {
            var subCategory = request.ToEntity();

            var created = await _subCategoryRepository.AddAsync(subCategory);

            return created.ToResponse();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var subCategory = await _subCategoryRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"La sous-catégorie avec l'identifiant {id} n'a pas été trouvé.");

            return await _subCategoryRepository.DeleteAsync(subCategory.Id);
        }

        public async Task<IEnumerable<SubCategoryResponseDto>> GetAllByCategoryIdAsync(int categoryId)
        {
            var subCategories = await _subCategoryRepository.GetAllByCategoryIdAsync(categoryId);

            return subCategories.ToResponseList();
        }

        public async Task<SubCategoryResponseDto?> GetByIdAsync(int id)
        {
            var subCategory = await _subCategoryRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"La sous-catégorie avec l'identifiant {id} n'a pas été trouvé.");

            return subCategory.ToResponse();
        }

        public async Task<SubCategoryResponseDto?> UpdateAsync(int id, SubCategoryRequestDto request)
        {
            var subCategory = await _subCategoryRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"La sous-catégorie avec l'identifiant {id} n'a pas été trouvé.");

            subCategory.Name = request.Name;
            subCategory.CategoryId = request.CategoryId;

            var updated = await _subCategoryRepository.UpdateAsync(subCategory);

            return updated!.ToResponse();
        }
    }
}
