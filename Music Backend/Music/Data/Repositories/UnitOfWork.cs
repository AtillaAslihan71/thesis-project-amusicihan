using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core;
using Core.Interfaces;
using Data.DAL;
using Data.Repositories.Implementations;

namespace Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IFaqRepository _faqRepository;
        private IGenreRepository _genreRepository;
        private ITermsConditionsRepository _termsConditionsRepository;
        private IUserGenreRepository _userGenreRepository;
        private IMusicRepository _musicRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }


        public IFaqRepository faqRepository =>
            _faqRepository = _faqRepository ?? new FaqRepository(_context);


        public IGenreRepository genreRepository =>
            _genreRepository = _genreRepository ?? new GenreRepository(_context);


        public ITermsConditionsRepository termsConditionsRepository =>
            _termsConditionsRepository = _termsConditionsRepository ?? new TermsConditionsRepository(_context);

        public IUserGenreRepository userGenreRepository =>
            _userGenreRepository = _userGenreRepository ?? new UserGenreRepository(_context);

        public IMusicRepository musicRepository =>
            _musicRepository = _musicRepository ?? new MusicRepository(_context);
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
