using GBP.Core.Interfaces.Repositories;
using GBP.Domain.Entities;
using GBP.Infra.Database.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Infra.Repositories
{
    public class AccountRepository(GbpDbContext _context) : IAccountRepository
    {

        /// <summary>
        /// Ajoute un nouveau compte à la base de données et retourne le compte ajouté. 
        /// </summary>
        /// <param name="account"></param>
        /// <returns>Le compte ou une OperationCancelledException.</returns>
        public async Task<Account?> AddAsync(Account account)
        {
            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();

            return account;
        }

        /// <summary>
        /// Supprime un compte de la base de données en fonction de son identifiant.
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns>True si le compte a été supprimé, false sinon.</returns>
        public async Task<bool> DeleteAsync(Guid accountId)
        {
            var account = await _context.Accounts.FindAsync(accountId);

            if (account is null) return false;

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Récupère tous les comptes associés à un utilisateur spécifique en fonction de son identifiant.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>La liste des comptes associés à l'utilisateur.</returns>
        public async Task<IEnumerable<Account>> GetAllByUserIdAsync(Guid userId)
        {
            return await _context.Accounts
                .AsNoTracking()
                .Include(a => a.AccountType)
                .Where(a => a.UserId == userId)
                .ToListAsync();
        }

        /// <summary>
        /// Récupère un compte de la base de données en fonction de son identifiant.
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns>Le compte trouvé ou null s'il n'existe pas.</returns>
        public async Task<Account?> GetByIdAsync(Guid accountId)
        {
            return await _context.Accounts
                .Include(a => a.AccountType)
                .FirstOrDefaultAsync(a => a.Id == accountId);
        }

        /// <summary>
        /// Met à jour un compte existant dans la base de données. Si le compte n'existe pas, retourne null.
        /// </summary>
        /// <param name="account"></param>
        /// <returns>Le compte mis à jour ou null s'il n'existe pas.</returns>
        public async Task<Account?> UpdateAsync(Account account)
        {
            var existing = await _context.Accounts.FindAsync(account.Id);

            if (existing is null) return null;

            existing.AccountTypeId = account.AccountTypeId;
            existing.Balance = account.Balance;
            existing.Label = account.Label;
            existing.Number = account.Number;
            existing.UpdatedAt = DateTime.UtcNow;
            existing.UpdatedBy = account.UpdatedBy;

            await _context.SaveChangesAsync();
            return existing;
        }
    }
}
