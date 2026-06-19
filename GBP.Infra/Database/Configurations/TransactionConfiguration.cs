using GBP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Infra.Database.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transactions");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Amount)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(t => t.DateOfTransaction)
                .IsRequired();

            builder.Property(t => t.Description)
                .HasMaxLength(255);

            // Relations
            // Transactions -> SourceAccount (Many-to-One)
            builder.HasOne(s => s.SourceAccount)
                .WithMany()
                .HasForeignKey(t => t.SourceAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            // Transactions -> TargetAccount (Many-to-One) optionnel
            builder.HasOne(ta => ta.TargetAccount)
                .WithMany()
                .HasForeignKey(tr => tr.TargetAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            // Transactions -> SubCategory (Many-to-One)
            builder.HasOne(s => s.SubCategory)
                .WithMany(t => t.Transactions)
                .HasForeignKey(t => t.SubCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Transactions -> TransactionType (Many-to-One)
            builder.HasOne(tr => tr.TransactionType)
                .WithMany(tt => tt.Transactions)
                .HasForeignKey(tr => tr.TransactionTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Index pour les requêtes fréquentes
            builder.HasIndex(t => t.Id)
                .HasDatabaseName("IX_Transactions_SourceAccountId");
        }
    }
}
