using Proje_model.Models.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proje_model.TypeConfigurations.Concrete
{
    public class UlkeMap : IEntityTypeConfiguration<Ulke>
    {
        public void Configure(EntityTypeBuilder<Ulke> builder)
        {
            // Properties
            builder.Property(u => u.ulke_kodu).IsRequired().HasMaxLength(3); // Örn. TR, US gibi kodlar
            builder.Property(u => u.ulke_adi).IsRequired().HasMaxLength(100);

            // Relationships
            builder.HasMany(u => u.FirmaSahislar)
                   .WithOne(f => f.Ulke)
                   .HasForeignKey(f => f.UlkeId);
        }
    }
}
