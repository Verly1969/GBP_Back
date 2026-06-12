using GBP.Core.Interfaces.Repositories;
using GBP.Domain.Entities;
using GBP.Infra.Database.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Infra.Repositories
{
    /// <summary>
    /// Implémente les opérations de gestion des crédits dans la base de données en utilisant Entity Framework Core.
    /// </summary>
    /// <param name="_context"></param>
    /// <returns>Le crédit ajouté</returns>
    public class CreditRepository(GbpDbContext _context) : ICreditRepository
    {
        public async Task<Credit> AddAsync(Credit credit)
        {
            await _context.Credits.AddAsync(credit);
            await _context.SaveChangesAsync();

            return credit;
        }
        
        /// <summary>
        /// Supprime un crédit de la base de données en fonction de son identifiant. 
        /// 
        /// </summary>
        /// <param name="creditId"></param>
        /// <returns>true si le crédit a été supprimé, false sinon</returns>
        public async Task<bool> DeleteAsync(Guid creditId)
        {
            var existing = await _context.Credits.FindAsync(creditId);

            if (existing is null) return false;

            _context.Credits.Remove(existing);
            await _context.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Récupère tous les crédits associés à un compte spécifique en fonction de l'identifiant du compte.
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns>La liste des crédits associés au compte</returns>
        public async Task<IEnumerable<Credit>> GetAllByAccountIdAsync(Guid accountId)
        {
            return await _context.Credits
                .AsNoTracking()
                .Include(c => c.CreditType)
                .Where(c => c.AccountId == accountId)
                .ToListAsync();
        }

        /// <summary>
        /// Récupère un crédit spécifique en fonction de son identifiant, en incluant les informations 
        /// sur le type de crédit et le compte associé.
        /// </summary>
        /// <param name="creditId"></param>
        /// <returns>Le crédit trouvé ou null s'il n'existe pas</returns>
        public async Task<Credit?> GetByIdAsync(Guid creditId)
        {
            return await _context.Credits
                .Include(c => c.CreditType)
                .Include(c => c.Account)
                .FirstOrDefaultAsync(c => c.Id == creditId);
        }

        /// <summary>
        /// Met à jour les informations d'un crédit existant dans la base de données.
        /// </summary>
        /// <param name="credit"></param>
        /// <returns>Le crédit mis à jour ou null s'il n'existe pas</returns>
        public async Task<Credit?> UpdateAsync(Credit credit)
        {
            var existing = await _context.Credits.FindAsync(credit.Id);

            if (existing is null) return null;

            existing.Amount = credit.Amount;
            existing.InterestRate = credit.InterestRate;
            existing.DurationMonths = credit.DurationMonths;
            existing.StartDate = credit.StartDate;
            existing.EndDate = credit.EndDate;
            existing.Status = credit.Status;
            existing.Raison = credit.Raison;
            existing.CreditTypeId = credit.CreditTypeId;

            await _context.SaveChangesAsync();

            return existing;
        }
    }
}
