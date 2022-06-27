using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class FaqConfigurations : IEntityTypeConfiguration<Faq>
    {
        public void Configure(EntityTypeBuilder<Faq> builder)
        {
            builder.Property(o => o.Question).IsRequired().HasColumnType("TEXT");
            builder.Property(o => o.Answer).IsRequired().HasColumnType("TEXT");
        }

    }
}
