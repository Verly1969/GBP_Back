using GBP.Core.Interfaces.Repositories;
using GBP.Domain.Entities;
using GBP.Infra.Database.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Infra.Repositories
{
    public class CreditTypeRepository(GbpDbContext _context) : ICreditTypeRepository
    {
        /// <summary>
        /// Ajoute un type de crédit en base de donnée
        /// </summary>
        /// <param name="creditType"></param>
        /// <returns>Le type de crédit ou null</returns>
        public async Task<CreditType?> AddAsync(CreditType creditType)
        {
            if (creditType is null) return null;

            await _context.CreditTypes.AddAsync(creditType);
            await _context.SaveChangesAsync();

            return creditType;
        }

        /// <summary>
        /// Supprime un type de crédit suivant son ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retourne true ou false en cas d'erreur</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            var creditType = await _context.CreditTypes.FindAsync(id);

            if (creditType is null) return false;

            _context.CreditTypes.Remove(creditType);
            await _context.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Récupère tous les types de crédit
        /// </summary>
        /// <returns>Une liste de types de crédit</returns>
        public async Task<IEnumerable<CreditType>> GetAllAsync()
        {
            return await _context.CreditTypes
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Récupère un tupe de crédit suivant son ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retourne le type de crédit ou null si pas trouvé</returns>
        public async Task<CreditType?> GetByIdAsync(int id)
        {
            return await _context.CreditTypes.FirstOrDefaultAsync(c => c.Id == id);
        }

        /// <summary>
        /// Modifie un type de crédit
        /// </summary>
        /// <param name="creditType"></param>
        /// <returns>Retourne le type de crédit modifié ou null</returns>
        public async Task<CreditType?> UpdateAsync(CreditType creditType)
        {
            var existing = await _context.CreditTypes.FindAsync(creditType.Id);

            if (existing is null) return null;

            existing.Name = creditType.Name;
            existing.Description = creditType.Description;

            await _context.SaveChangesAsync();
            return existing;
        }
    }
}
