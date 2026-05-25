using GBP.Domain.Entities;
using GBP.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Infra.Database.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    private static readonly Guid AdminId = Guid.Parse("00000000-0000-0000-0000-000000000001");
    private const string AdminPasswordHash = "password-hash"; // Replace with actual hash in production
    private const string AdminSecretKeyHash = "secret-key-hash"; // Replace with actual hash in production
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(u => u.SecretKeyHash)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(u => u.PasswordHash)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(u => u.Role)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(u => u.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(u => u.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()");

        builder.HasIndex(u => u.Email).IsUnique();

        // Seed admin
        builder.HasData(new User
        {
            Id = AdminId,
            LastName = "Admin",
            FirstName = "User",
            Email = "admin@example.com",
            SecretKeyHash = AdminSecretKeyHash,
            PasswordHash = AdminPasswordHash,
            Role = Role.Admin,
            Status = Status.Active,
            CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
        });
    }
}
