using GBP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Infra.Database.Configurations
{
    public class AccountTypeConfiguration : IEntityTypeConfiguration<AccountType>
    {
        public void Configure(EntityTypeBuilder<AccountType> builder)
        {
            builder.ToTable("AccountTypes");

            builder.HasKey(at => at.Id);

            builder.Property(at => at.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(at => at.Description)
                .HasMaxLength(200);

            // Seed data
            builder.HasData(
                new AccountType { Id = 1, Name = "Courant", Description = "Compte courant" },
                new AccountType { Id = 2, Name = "Epargne", Description = "Compte d'épargne" },
                new AccountType { Id = 3, Name = "Investissement", Description = "Compte d'investissement" }
            );
        }
    }
}
