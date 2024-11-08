using Proje_model.Models.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proje_model.TypeConfigurations.Concrete
{
    public class FirmaSahisMap : IEntityTypeConfiguration<FirmaSahis>
    {
        public void Configure(EntityTypeBuilder<FirmaSahis> builder)
        {
            builder.Property(f => f.Adi).IsRequired().HasMaxLength(100);
            builder.Property(f => f.Adres).HasMaxLength(250);
            builder.Property(f => f.Telefon).HasMaxLength(15);
            builder.Property(f => f.Email).HasMaxLength(100);
            builder.Property(f => f.VergiNo).HasMaxLength(10);
            builder.Property(f => f.VergiDairesi).HasMaxLength(100);
            builder.Property(f => f.TC).HasMaxLength(11);
            builder.Property(f => f.PostaNo).HasMaxLength(10);
            builder.Property(f => f.Tur).HasMaxLength(20);

            builder.HasOne(a => a.AppUser).WithMany(a => a.FirmaSahis).HasForeignKey(a => a.AppUserID).OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(f => f.Ulke)
                  .WithMany(u => u.FirmaSahislar)
                  .HasForeignKey(f => f.UlkeId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(f => f.Sehir)
                   .WithMany(s => s.FirmaSahislar)
                   .HasForeignKey(f => f.SehirId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
