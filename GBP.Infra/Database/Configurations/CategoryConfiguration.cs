using GBP.Domain.Entities;
using GBP.Infra.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Infra.Database.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50);

            // Data seeding
            builder.HasData(
                new Category { Id = 1, Name = "Immobilier" },
                new Category { Id = 2, Name = "Véhicules" },
                new Category { Id = 3, Name = "Alimentation" },
                new Category { Id = 4, Name = "Loisirs" },
                new Category { Id = 5, Name = "Santé" }
            );
        }
    }
}
