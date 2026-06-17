using GBP.Core.Interfaces.Repositories;
using GBP.Domain.Entities;
using GBP.Infra.Database.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace GBP.Infra.Repositories
{
    public class CategoryRepository(GbpDbContext _context) : ICategoryRepository
    {
        /// <summary>
        /// Ajoute une nouvelle catégorie à la base de données.
        /// </summary>
        /// <param name="category"></param>
        /// <returns>La catégorie ajoutée</returns>
        public async Task<Category> AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return category;
        }

        /// <summary>
        /// Supprime une catégorie de la base de données en fonction de son identifiant.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True si la catégorie a été supprimée, false sinon</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category is null) return false;

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Récupère toutes les catégories de la base de données.
        /// </summary>
        /// <returns>Une collection de catégories</returns>
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Récupère une catégorie de la base de données en fonction de son identifiant.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>La catégorie trouvée ou null</returns>
        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id); 
        }

        public async Task<Category?> UpdateAsync(Category category)
        {
            var existing = await _context.Categories.FindAsync(category.Id);

            if (existing is null) return null;

            existing.Name = category.Name;
            await _context.SaveChangesAsync();

            return existing;
        }
    }
}
