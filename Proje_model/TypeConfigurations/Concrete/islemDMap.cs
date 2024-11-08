using Proje_model.Models.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proje_model.TypeConfigurations.Concrete
{
    public class islemDMap : IEntityTypeConfiguration<islemD>
    {
        public void Configure(EntityTypeBuilder<islemD> builder)
        {

            builder.Property(i => i.MalzemeFiyat).HasColumnType("decimal(18,2)");
            builder.Property(i => i.IscilikFiyat).HasColumnType("decimal(18,2)");
            builder.Property(i => i.ToplamFiyat).HasColumnType("decimal(18,2)");
            builder.Property(i => i.islemAciklama).HasMaxLength(5000);
            builder.Property(i => i.islemTur).IsRequired();
            builder.Property(i => i.BakimKM)  .IsRequired();



            builder.HasOne(a => a.AppUser).WithMany(a => a.İslemDs).HasForeignKey(a => a.AppUserID).OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(i => i.FirmaSahis)
               .WithMany(f => f.IslemDetaylar)
               .HasForeignKey(i => i.FirmaSahisId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.islem)
                .WithMany(i => i.IslemDetaylar)
                .HasForeignKey(i => i.IslemId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasOne(i => i.Arac)
                .WithMany(a => a.IslemDetaylar)
                .HasForeignKey(i => i.AracId);
        }
    }
}
