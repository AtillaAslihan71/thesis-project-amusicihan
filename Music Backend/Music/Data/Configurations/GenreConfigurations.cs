using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class GenreConfigurations : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.Property(o => o.Name).IsRequired().HasColumnType("TEXT");
            builder.Property(o => o.Image).IsRequired().HasColumnType("TEXT");
        }
    }

}