using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.Utilities;
using Business.ViewModels.GenreViewModels;
using Core;
using Core.Entities;
using Microsoft.AspNetCore.Hosting;

namespace Business.Implementations
{
    public class GenreService : IGenreService
    {
        #region Injects

        private readonly IWebHostEnvironment _env;
        private readonly IUnitOfWork _unitOfWork;

        public GenreService(IUnitOfWork unitOfWork, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _env = env;
        }

        #endregion
        public async Task<List<Genre>> GetAllAsync()
        {
            return await _unitOfWork.genreRepository.GetAllAsync();
        }

        public async Task<Genre> Get(int id)
        {
            return await _unitOfWork.genreRepository.Get(b => b.Id == id);
        }

        public async Task Create(GenreCreateViewModel genreViewModel)
        {
            string image = await genreViewModel.Image.SaveFileAsync(_env.WebRootPath, "assets", "image");

            var newGenre = new Genre()
            {
                Name = genreViewModel.Name,
                Image = image
            };

            await _unitOfWork.genreRepository.CreateAsync(newGenre);
            await _unitOfWork.SaveAsync();
        }

        public async Task Update(int id, GenreUpdateViewModel genreViewModel)
        {
            Genre dbGenre = await _unitOfWork.genreRepository.Get(b => b.Id == id);

            dbGenre.Name = genreViewModel.Name;

            if (genreViewModel.Image != null)
            {

                string filename = await genreViewModel.Image.SaveFileAsync(_env.WebRootPath, "assets", "image");

                dbGenre.Image = filename;

            }


            await _unitOfWork.SaveAsync();
        }

        public async Task Remove(int id)
        {
            Genre dbGenre = await _unitOfWork.genreRepository.Get(b => b.Id == id);



            _unitOfWork.genreRepository.Remove(dbGenre);
            await _unitOfWork.SaveAsync();
        }
    }
}
