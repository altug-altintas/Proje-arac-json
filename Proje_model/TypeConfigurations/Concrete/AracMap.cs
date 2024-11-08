using Proje_model.Models.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proje_model.TypeConfigurations.Concrete
{
    public class AracMap : IEntityTypeConfiguration<Arac>
    {
        public void Configure(EntityTypeBuilder<Arac> builder)
        {
            builder.Property(a => a.Plaka).IsRequired().HasMaxLength(10); // Plaka birincil anahtar
            builder.Property(a => a.Marka).IsRequired().HasMaxLength(50);
            builder.Property(a => a.Model).IsRequired().HasMaxLength(50);
            builder.Property(a => a.Yil).IsRequired();
            builder.Property(a => a.SasiNo).HasMaxLength(50);
            builder.Property(a => a.YakitTur).HasMaxLength(20);
            builder.Property(a => a.Renk).HasMaxLength(30);
            builder.Property(a => a.MotorHacim).HasColumnType("decimal(10, 2)");
            builder.Property(a => a.MotorBeygir).HasColumnType("decimal(10, 2)");
            builder.Property(a => a.Km).IsRequired();
            builder.Property(a => a.SonBakim).IsRequired();
            builder.Property(a => a.BakimKM).IsRequired();
            builder.Property(a => a.SiradakiBakim).IsRequired();
         

            // Relationships
            builder.HasOne(a => a.FirmaSahis)
                   .WithMany(f => f.Araclar)
                   .HasForeignKey(a => a.FirmaSahisId)
                   .OnDelete(DeleteBehavior.Cascade);


            builder.HasMany(a => a.IslemDetaylar)
              .WithOne(d => d.Arac)
              .HasForeignKey(d => d.AracId)
              .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.AppUser).WithMany(a => a.Aracs).HasForeignKey(a => a.AppUserID).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
