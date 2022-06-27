using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;
using Core.Interfaces;
using Data.DAL;

namespace Data.Repositories.Implementations
{
    public class UserGenreRepository : Repository<UserGenre>, IUserGenreRepository
    {
        public UserGenreRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        private readonly AppDbContext _context;
    }
}
