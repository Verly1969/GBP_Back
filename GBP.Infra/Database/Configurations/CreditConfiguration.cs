using GBP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Infra.Database.Configurations
{
    public class CreditConfiguration : IEntityTypeConfiguration<Credit>
    {
        public void Configure(EntityTypeBuilder<Credit> builder)
        {
            builder.ToTable("Credit");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Amount)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(c => c.InterestRate)
                .HasColumnType("decimal(5,2)")
                .IsRequired();

            builder.Property(c => c.DurationMonths)
                .IsRequired();

            builder.Property(c => c.StartDate)
                .IsRequired();

            builder.Property(c => c.Status)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(20);

            builder.Property(c => c.Raison)
                .HasMaxLength(255);

            // Relations
            // Credit -> Account (Many-to-One)
            builder.HasOne(c => c.Account)
                .WithMany(a => a.Credits)
                .HasForeignKey(c => c.AccountId)
                .OnDelete(DeleteBehavior.Restrict);

            // Credit -> CreditType (Many-to-One)
            builder.HasOne(c => c.CreditType)
                .WithMany(a => a.Credits)
                .HasForeignKey(c => c.CreditTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Credit -> PreviousCredit (Auto-référence, One-to-Many)
            builder.HasOne(c => c.PreviousCredit)
                .WithMany(c => c.NextCredits)
                .HasForeignKey(c => c.PreviousCreditId)
                .OnDelete(DeleteBehavior.Restrict);

            // Index sur AccountId pour les requêtes fréquentes
            builder.HasIndex(c => c.AccountId)
                .HasDatabaseName("IX_Credits_AccountId");
        }
    }
}
