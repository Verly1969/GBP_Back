using GBP.Core.Interfaces.Repositories;
using GBP.Domain.Entities;
using GBP.Infra.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Infra.Repositories
{
    public class TransactionRepository(
        GbpDbContext _context) : ITransactionRepository
    {
        /// <summary>
        /// Ajoute une nouvelle transaction à la base de données et retourne la transaction ajoutée. 
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns>La transaction ou une OperationCancelledException.</returns>
        public async Task<Transaction> AddAsync(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();

            return transaction;
        }

        /// <summary>
        /// Supprime une transaction de la base de donnée.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True si ok sinon false</returns>
        public async Task<bool> DeleteAsync(Guid id)
        {
            var existing = await _context.Transactions.FindAsync(id);

            if (existing is null) return false;

            _context.Transactions.Remove(existing);
            await _context.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Renvoie une liste de transaction en fonction du compte financier.
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns>Une liste de transactions ou une liste vide</returns>
        public async Task<IEnumerable<Transaction>> GetAllByAccountAsync(Guid accountId)
        {
            return await _context.Transactions
                .AsNoTracking() // Lecture seule, pas de mise en mémoire
                .Include(t => t.SourceAccount)
                .Include(t => t.TargetAccount)
                .Include(t => t.SubCategory).ThenInclude(s => s.Category)
                .Include(t => t.TransactionType)
                .Where(t => t.SourceAccount.Id == accountId || t.TargetAccountId == accountId)
                .ToListAsync();
        }

        /// <summary>
        /// Renvoie une transaction en fonction de l'id donné en paramètre.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Null ou la transaction trouvée</returns>
        public async Task<Transaction?> GetByIdAsync(Guid id)
        {
            return await _context.Transactions
                .Include(t => t.SourceAccount)
                .Include(t => t.SubCategory).ThenInclude(s => s.Category)
                .Include(t => t.TransactionType)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        /// <summary>
        /// Met à jour une transaction donnée en paramètre
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns>Null ou la transaction modifiée</returns>
        public async Task<Transaction?> UpdateAsync(Transaction transaction)
        {
            var existing = await _context.Transactions.FindAsync(transaction.Id);

            if (existing is null) return null;

            existing.Amount = transaction.Amount;
            existing.DateOfTransaction = transaction.DateOfTransaction;
            existing.Description = transaction.Description;
            existing.SubCategoryId = transaction.SubCategoryId;
            existing.TransactionTypeId = transaction.TransactionTypeId;

            await _context.SaveChangesAsync();

            return existing;
        }
    }
}
