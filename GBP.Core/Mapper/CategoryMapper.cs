using GBP.Core.DTOs.Request;
using GBP.Core.DTOs.Response;
using GBP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace GBP.Core.Mapper
{
    public static class CategoryMapper
    {
        /// <summary>
        /// Mapper méthode pour convertir un objet Category en CategoryResponseDto
        /// </summary>
        /// <param name="category"></param>
        /// <returns>Un objet CategoryResponseDto</returns>
        public static CategoryResponseDto ToResponse(
            this Category category)
        {
            return new CategoryResponseDto
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        /// <summary>
        /// Mapper une liste Category en une liste de CategoryResponseDto
        /// </summary>
        /// <param name="categories"></param>
        /// <returns>Une liste CategoryResponse Dto</returns>
        public static IEnumerable<CategoryResponseDto> ToResponseList(
            this IEnumerable<Category> categories) =>
                categories.Select(c => ToResponse(c));

        /// <summary>
        /// Mapper un CategoryResponseDto en Category
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Un objet Category</returns>
        public static Category ToEntity(
            this CategoryRequestDto request) => new()
            {
                Name = request.Name
            };
    }
}
