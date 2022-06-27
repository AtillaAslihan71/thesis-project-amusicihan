using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;

namespace Core
{
    public interface IUnitOfWork
    {
        public IFaqRepository faqRepository { get; }    
        public IGenreRepository genreRepository { get; }
        public ITermsConditionsRepository termsConditionsRepository { get; }
        public IUserGenreRepository userGenreRepository { get; }    
        public IMusicRepository musicRepository { get; }    
        Task SaveAsync();
    }   
}
