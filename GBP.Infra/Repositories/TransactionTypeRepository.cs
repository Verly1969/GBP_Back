using GBP.Core.Interfaces.Repositories;
using GBP.Domain.Entities;
using GBP.Infra.Database.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Infra.Repositories
{
    public class TransactionTypeRepository(
        GbpDbContext _context) : ITransactionTypeRepository
    {
        public async Task<TransactionType> AddAsync(TransactionType transactionType)
        {
            await _context.TransactionTypes.AddAsync(transactionType);
            await _context.SaveChangesAsync();

            return transactionType;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.TransactionTypes.FindAsync(id);

            if (existing is null) return false;
            
            _context.TransactionTypes.Remove(existing);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<TransactionType>> GetAllAsync()
        {
            return await _context.TransactionTypes
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<TransactionType?> GetByIdAsync(int id)
        {
            return await _context.TransactionTypes.FindAsync(id);
        }

        public async Task<TransactionType?> UpdateAsync(TransactionType transactionType)
        {
            var existing = await _context.TransactionTypes.FindAsync(transactionType.Id);

            if (existing is null) return null;

            existing.Name = transactionType.Name;
            existing.Description = transactionType.Description;

            await _context.SaveChangesAsync();

            return existing;
        }
    }
}
