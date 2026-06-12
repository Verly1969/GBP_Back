using GBP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Infra.Database.Configurations
{
    public class CreditTypeConfiguration : IEntityTypeConfiguration<CreditType>
    {
        public void Configure(EntityTypeBuilder<CreditType> builder)
        {
            builder.ToTable("CreditTypes");

            builder.HasKey(ct => ct.Id);

            builder.Property(ct => ct.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(ct => ct.Description)
                .HasMaxLength(255);

            builder.HasData(
                new CreditType
                {
                    Id = 1,
                    Name = "Hypothécaire",
                    Description = "Crédit hypothécaire pour l'achat d'un bien immobilier"
                },
                new CreditType
                {
                    Id = 2,
                    Name = "Consommation",
                    Description = "Crédit personnel pour des dépenses diverses"
                },
                new CreditType
                {
                    Id = 3,
                    Name = "Automobile",
                    Description = "Crédit auto pour l'achat d'un véhicule"
                }
            );
        }
    }
}
