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
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<SecurityLog> SecurityLogs { get; set; } = null!;
        public DbSet<Account> Accounts { get; set; } = null!;
        public DbSet<AccountType> AccountTypes { get; set; } = null!;

        // Override OnModelCreating to apply configurations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GbpDbContext).Assembly);

            // Ignorer les entités non encore utilisées
            modelBuilder.Ignore<Transaction>();
            modelBuilder.Ignore<Credit>();
            modelBuilder.Ignore<Wallet>();
            modelBuilder.Ignore<Thrift>();
            modelBuilder.Ignore<Payment>();
            modelBuilder.Ignore<InvestMovement>();
            modelBuilder.Ignore<Deposit>();
            modelBuilder.Ignore<Log>();
        }
    }
}
