using GBP.Core.DTOs.Request;
using GBP.Core.DTOs.Response;
using GBP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace GBP.Core.Mapper
{
    public static class SubCategoryMapper
    {
        /// <summary>
        /// Mapper méthode pour convertir un objet SubCategory en SubCategoryResponseDto
        /// </summary>
        /// <param name="subCategory"></param>
        /// <returns>Un objet SubCategoryResponseDto</returns>
        public static SubCategoryResponseDto ToResponse(
            this SubCategory subCategory)
        {
            return new SubCategoryResponseDto
            {
                Id = subCategory.Id,
                Name = subCategory.Name,
                CategoryId = subCategory.CategoryId,
                CategoryName = subCategory.Category?.Name ?? string.Empty
            };
        }

        /// <summary>
        /// Mapper une liste SubCategory en une liste de SubCategoryResponseDto
        /// </summary>
        /// <param name="categories"></param>
        /// <returns>Une liste SubCategoryResponse Dto</returns>
        public static IEnumerable<SubCategoryResponseDto> ToResponseList(
            this IEnumerable<SubCategory> subCategories) =>
                subCategories.Select(c => ToResponse(c));

        /// <summary>
        /// Mapper un SubCategoryResponseDto en SubCategory
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Un objet SubCategory</returns>
        public static SubCategory ToEntity(
            this SubCategoryRequestDto request) => new()
            {
                Name = request.Name,
                CategoryId = request.CategoryId
            };
    }
}
