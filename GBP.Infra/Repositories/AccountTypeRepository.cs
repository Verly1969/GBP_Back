using GBP.Core.Interfaces.Repositories;
using GBP.Domain.Entities;
using GBP.Infra.Database.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Infra.Repositories
{
    public class AccountTypeRepository(GbpDbContext _context) : IAccountTypeRepository
    {
        /// <summary>
        /// Ajoute un nouveau type de compte à la base de données.
        /// </summary>
        /// <param name="accountType"></param>
        /// <returns>Le type de compte ajouté ou null en cas d'erreur</returns>
        public async Task<AccountType?> AddAsync(AccountType accountType)
        {
            if (accountType is null) return null;
            await _context.AccountTypes.AddAsync(accountType);
            await _context.SaveChangesAsync();

            return accountType;
        }

        /// <summary>
        /// Supprime un type de compte de la base de données en fonction de son identifiant.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True si le type de compte a été supprimé, false sinon</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            var accountType = await _context.AccountTypes.FindAsync(id);
            if (accountType is null) return false;

            _context.AccountTypes.Remove(accountType);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Récupère tous les types de compte disponibles dans la base de données.
        /// </summary>
        /// <returns>Une liste de tous les types de compte</returns>
        public async Task<IEnumerable<AccountType>> GetAllAsync()
        {
            return await _context.AccountTypes
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Récupère un type de compte spécifique par son identifiant.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Le type de compte ou null en cas d'erreur</returns>
        public async Task<AccountType?> GetByIdAsync(int id)
        {
            return await _context.AccountTypes
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        /// <summary>
        /// Met à jour un type de compte existant dans la base de données. 
        /// Si le type de compte n'existe pas, retourne null.
        /// </summary>
        /// <param name="accountType"></param>
        /// <returns>Le type de compte mis à jour ou null en cas d'erreur</returns>
        public async Task<AccountType?> UpdateAsync(AccountType accountType)
        {
            var existing = await _context.AccountTypes.FindAsync(accountType.Id);

            if (existing is null) return null;

            existing.Name = accountType.Name;
            existing.Description = accountType.Description;

            await _context.SaveChangesAsync();
            return existing;
        }
    }
}
