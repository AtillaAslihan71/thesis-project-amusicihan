using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class MusicConfigurations : IEntityTypeConfiguration<Music>
    {
        public void Configure(EntityTypeBuilder<Music> builder)
        {
            builder.Property(o => o.Title).IsRequired().HasColumnType("TEXT");
            builder.Property(o => o.Singer).IsRequired().HasMaxLength(255);
            builder.Property(o => o.Author).IsRequired().HasMaxLength(255);
            builder.Property(o => o.Link).IsRequired().HasColumnType("TEXT");
        }
    }
}
