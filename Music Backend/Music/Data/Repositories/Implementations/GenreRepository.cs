using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;
using Core.Interfaces;
using Data.DAL;

namespace Data.Repositories.Implementations
{
    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        public GenreRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        private readonly AppDbContext _context;
    }
}
