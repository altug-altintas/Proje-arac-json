using Proje_model.Models.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proje_model.TypeConfigurations.Concrete
{
    public class AppUserMap : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(a => a.FirstName).IsRequired();
            // builder.Property(a => a.FirtsName).IsRequired().HasColumnName("AD").HasMaxLength(30);
            builder.Property(a => a.LastName).IsRequired();
        }
    }
}
