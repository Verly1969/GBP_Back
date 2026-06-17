using GBP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Infra.Database.Configurations
{
    public class SubCategoryConfiguration : IEntityTypeConfiguration<SubCategory>
    {
        public void Configure(EntityTypeBuilder<SubCategory> builder)
        {
            builder.ToTable("SubCategories");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(50);

            // SubCategory -> Category (Many-to-One)
            builder.HasOne(s => s.Category)
                .WithMany(c => c.SubCategories)
                .HasForeignKey(s => s.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // Data seeding
            builder.HasData(
                new SubCategory { Id = 1, Name = "Taxe", CategoryId = 1 },
                new SubCategory { Id = 2, Name = "Assurance", CategoryId = 1 },
                new SubCategory { Id = 3, Name = "Taxe", CategoryId = 2 },
                new SubCategory { Id = 4, Name = "Assurance", CategoryId = 2 },
                new SubCategory { Id = 5, Name = "Carburant", CategoryId = 2 },
                new SubCategory { Id = 6, Name = "Entretien", CategoryId = 2 },
                new SubCategory { Id = 7, Name = "Epicerie", CategoryId = 3 },
                new SubCategory { Id = 8, Name = "Restaurant", CategoryId = 3 }
            );  
        }
    }
}
