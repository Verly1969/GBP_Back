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
    public class CategoryService(
        ICategoryRepository _categoryRepository) : ICategoryService
    {
        public async Task<CategoryResponseDto> AddAsync(CategoryRequestDto request)
        {
            var category = request.ToEntity();

            var created = await _categoryRepository.AddAsync(category);

            return created!.ToResponse();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"La catégorie avec l'identifiant {id} n'a pas été trouvé.");

            return await _categoryRepository.DeleteAsync(category.Id);
        }

        public async Task<IEnumerable<CategoryResponseDto>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();

            return categories.ToResponseList();
        }

        public async Task<CategoryResponseDto?> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"La catégorie avec l'identifiant {id} n'a pas été trouvé.");

            return category.ToResponse();
        }

        public async Task<CategoryResponseDto?> UpdateAsync(int id, CategoryRequestDto request)
        {
            var category = await _categoryRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"La catégorie avec l'identifiant {id} n'a pas été trouvé.");

            category.Name = request.Name;

            var updated = await _categoryRepository.UpdateAsync(category);

            return updated?.ToResponse();
        }
    }
}
