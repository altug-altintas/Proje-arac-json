using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Proje_model.Models.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proje_model.TypeConfigurations.Concrete
{
    public class isLemNewMap : IEntityTypeConfiguration<isLemNew>
    {
        public void Configure(EntityTypeBuilder<isLemNew> builder)
        {
            throw new NotImplementedException();
        }
    }
}
