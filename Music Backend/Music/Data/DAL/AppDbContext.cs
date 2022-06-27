using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;
using Data.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.DAL
{
    public class AppDbContext:IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.ApplyConfiguration(new ApplicationUserConfigurations());
            builder.ApplyConfiguration(new FaqConfigurations());
            builder.ApplyConfiguration(new GenreConfigurations());
            builder.ApplyConfiguration(new MusicConfigurations());
            builder.ApplyConfiguration(new TermsConditionsConfigurations());




            base.OnModelCreating(builder);

        }


        public DbSet<Faq> Faqs { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Music> Musics { get; set; }
        public DbSet<TermsConditions> TermsConditions { get; set; }
        public DbSet<UserGenre> UserGenres { get; set; }
    }
}
