using GBP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Infra.Database.Configurations
{
    public class SecurityLogConfiguration : IEntityTypeConfiguration<SecurityLog>
    {
        public void Configure(EntityTypeBuilder<SecurityLog> builder)
        {
            builder.ToTable("SecurityLog");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.IpAddress)
                .IsRequired()
                .HasMaxLength(45);

            builder.Property(s => s.EndPoint)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(s => s.EndPoint)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(s => s.UserAgent)
                .HasMaxLength(255);

            builder.Property(s => s.BanRaison)
                .HasMaxLength(255);

            builder.Property(s => s.CreatedBy)
                .HasMaxLength(100);

            builder.Property(s => s.DateAttempt)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(s => s.IsBanned)
                .HasDefaultValue(false);

            // Index pour la vérification rapide des tentatives d'accès par adresse IP
            builder.HasIndex(s => new { s.IpAddress, s.DateAttempt })
                .HasDatabaseName("IX_SecurityLog_IpAddress_DateAttempt");

            // Index pour le comptage dans la fenêtre de temps
            builder.HasIndex(s => new {s.IpAddress, s.DateAttempt })
                .HasDatabaseName("IX_SecurityLog_IpAddress_DateAttempt_BanCheck");
        }
    }
}
