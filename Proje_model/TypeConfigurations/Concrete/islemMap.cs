using Proje_model.Models.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proje_model.TypeConfigurations.Concrete
{
    public class islemMap : IEntityTypeConfiguration<islem>
    {
        public void Configure(EntityTypeBuilder<islem> builder)
        {
            builder.Property(i => i.Yil).IsRequired();
            builder.Property(i => i.No).IsRequired();
            builder.Property(i => i.FirmaSahisId).IsRequired();
            builder.Property(i => i.Tarih).IsRequired();




            builder.HasOne(a => a.AppUser).WithMany(a => a.İslems).HasForeignKey(a => a.AppUserID).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.FirmaSahis)
                .WithMany(f => f.islems)
                .HasForeignKey(i => i.FirmaSahisId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(i => i.IslemDetaylar)
                .WithOne(d => d.islem)
                .HasForeignKey(d => d.IslemId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
