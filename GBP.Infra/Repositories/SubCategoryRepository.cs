using GBP.Core.Interfaces.Repositories;
using GBP.Domain.Entities;
using GBP.Infra.Database.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Infra.Repositories
{
    public class SubCategoryRepository(GbpDbContext _context) : ISubCategoryRepository
    {
        /// <summary>
        /// Ajoute une nouvelle sous-catégorie à la base de données.
        /// </summary>
        /// <param name="subCategory"></param>
        /// <returns>La sous-catégorie ajoutée</returns>
        public async Task<SubCategory> AddAsync(SubCategory subCategory)
        {
            await _context.SubCategories.AddAsync(subCategory);
            await _context.SaveChangesAsync();

            return subCategory;
        }

        /// <summary>
        /// Supprime une sous-catégorie de la base de données en fonction de son identifiant.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True si la sous-catégorie a été supprimée, false sinon</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.SubCategories.FindAsync(id);

            if (entity is null) return false;

            _context.SubCategories.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Récupère toutes les sous-catégories associées à une catégorie spécifique en fonction de l'identifiant de la catégorie.
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns>Une collection de sous-catégories</returns>
        public async Task<ICollection<SubCategory>> GetAllByCategoryIdAsync(int categoryId)
        {
            return await _context.SubCategories
                .AsNoTracking()
                .Include(s => s.Category)
                .Where(s => s.CategoryId == categoryId)
                .ToListAsync();
        }

        /// <summary>
        /// Récupère une sous-catégorie en fonction de son identifiant.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>La sous-catégorie trouvée ou null</returns>
        public async Task<SubCategory?> GetByIdAsync(int id)
        {
            var entity = await _context.SubCategories
                .Include(s => s.Category)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (entity is null) return null;

            return entity;
        }

        /// <summary>
        /// Met à jour une sous-catégorie existante dans la base de données.
        /// </summary>
        /// <param name="subCategory"></param>
        /// <returns>La sous-catégorie mise à jour ou null</returns>
        public async Task<SubCategory?> UpdateAsync(SubCategory subCategory)
        {
            var entity = await _context.SubCategories.FindAsync(subCategory.Id);

            if (entity is null) return null;

            entity.Name = subCategory.Name;
            entity.CategoryId = subCategory.CategoryId;
            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
