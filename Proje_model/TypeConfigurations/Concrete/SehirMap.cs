using Proje_model.Models.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proje_model.TypeConfigurations.Concrete
{
    public class SehirMap : IEntityTypeConfiguration<Sehir>
    {

       

        public void Configure(EntityTypeBuilder<Sehir> builder)
        {
            // Properties
            builder.Property(s => s.sehir_plaka).IsRequired().HasMaxLength(2); // Plaka kodu (01, 34 gibi)
            builder.Property(s => s.sehir_adi).IsRequired().HasMaxLength(100);
            builder.Property(s => s.sehir_posta_kod).HasMaxLength(10);

            // Relationships
            builder.HasOne(s => s.Ulke)
                       .WithMany(u => u.Sehirler)
                       .HasForeignKey(s => s.UlkeId);

            builder.HasMany(s => s.FirmaSahislar)
                       .WithOne(f => f.Sehir)
                       .HasForeignKey(f => f.SehirId);
        }
    }
}
