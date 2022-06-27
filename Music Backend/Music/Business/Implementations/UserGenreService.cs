using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Business.Interfaces;
using Core;
using Core.Entities;

namespace Business.Implementations
{
    public class UserGenreService : IUserGenreService
    {
        #region Injects

        private readonly IUnitOfWork _unitOfWork;

        public UserGenreService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion
        public async Task<List<UserGenre>> GetAllAsync()
        {
            return await _unitOfWork.userGenreRepository.GetAllAsync();
        }

        public async Task<UserGenre> Get(int id)
        {
            return await _unitOfWork.userGenreRepository.Get(b => b.Id == id);
        }
        
    }
}
