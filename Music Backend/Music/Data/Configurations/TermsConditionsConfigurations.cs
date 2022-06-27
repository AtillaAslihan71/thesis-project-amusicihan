using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class TermsConditionsConfigurations : IEntityTypeConfiguration<TermsConditions>
    {
        public void Configure(EntityTypeBuilder<TermsConditions> builder)
        {
            builder.Property(o => o.Text).IsRequired().HasColumnType("TEXT");
        }
    }
}
