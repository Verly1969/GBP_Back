using GBP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Infra.Database.Context
{
    public class GbpDbContext(DbContextOptions<GbpDbContext> options) : DbContext(options)
    {
        // DbSets for entities
        public DbSet<User> Users => Set<User>();
        public DbSet<SecurityLog> SecurityLogs => Set<SecurityLog>();
        public DbSet<Account> Accounts => Set<Account>();
        public DbSet<AccountType> AccountTypes => Set<AccountType>();
        public DbSet<Credit> Credits => Set<Credit>();
        public DbSet<CreditType> CreditTypes => Set<CreditType>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<SubCategory> SubCategories => Set<SubCategory>();
        public DbSet<TransactionType> TransactionTypes => Set<TransactionType>();
        public DbSet<Transaction> Transactions => Set<Transaction>();

        // Override OnModelCreating to apply configurations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GbpDbContext).Assembly);

            // Ignorer les entités non encore utilisées
            modelBuilder.Ignore<Wallet>();
            modelBuilder.Ignore<Thrift>();
            modelBuilder.Ignore<Payment>();
            modelBuilder.Ignore<InvestMovement>();
            modelBuilder.Ignore<Deposit>();
            modelBuilder.Ignore<Log>();
        }
    }
}
