using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.ViewModels.FaqViewModels;
using Business.ViewModels.MainUserViewModels;
using Core;
using Core.Entities;

namespace Business.Implementations
{
    public class MusicService :IMusicService
    {
        #region Injects

        private readonly IUnitOfWork _unitOfWork;

        public MusicService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion
        public async Task<List<Music>> GetAllAsync()
        {
            return await _unitOfWork.musicRepository.GetAllAsync();
        }

        public async Task<Music> Get(int id)
        {
            return await _unitOfWork.musicRepository.Get(b => b.Id == id);
        }

        public async Task Create(MainUserViewModel mainUserViewModel, ApplicationUser myUser)
        {
            var newMusic = new Music()
            {
                Title = mainUserViewModel.Title,
                Singer = mainUserViewModel.Singer,
                Link = mainUserViewModel.Link,
                GenreId = mainUserViewModel.GenreId,
                Author = myUser.Name
            };

            await _unitOfWork.musicRepository.CreateAsync(newMusic);
            await _unitOfWork.SaveAsync();
        }
        public async Task Remove(int id)
        {
            Music dbMusic = await _unitOfWork.musicRepository.Get(b => b.Id == id);



            _unitOfWork.musicRepository.Remove(dbMusic);
            await _unitOfWork.SaveAsync();
        }
    }
}
