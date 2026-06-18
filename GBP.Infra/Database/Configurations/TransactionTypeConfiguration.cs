using GBP.Domain.Entities;
using GBP.Infra.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Infra.Database.Configurations
{
    public class TransactionTypeConfiguration : IEntityTypeConfiguration<TransactionType>
    {
        public void Configure(EntityTypeBuilder<TransactionType> builder)
        {
            builder.ToTable("TransactionTypes");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(t => t.Description)
                .HasMaxLength(255);

            // Seed data
            builder.HasData(
                new TransactionType { Id = 1, Name = "Transfert", Description = "Transfert entre comptes propre" },
                new TransactionType { Id = 2, Name = "Retrait", Description = "Retrait d'argent ATM"},
                new TransactionType { Id = 3, Name = "Virement", Description = "Mouvement d'argent vers comptes tiers"},
                new TransactionType { Id = 4, Name = "Consolidation", Description = "Mise à jour des soldes suivant décalage"}
                );
        }
    }
}
