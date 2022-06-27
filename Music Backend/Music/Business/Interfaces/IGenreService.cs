using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Business.ViewModels.GenreViewModels;
using Core.Entities;

namespace Business.Interfaces
{
    public interface IGenreService
    {

        Task<List<Genre>> GetAllAsync();
        Task<Genre> Get(int id);
        Task Create(GenreCreateViewModel genreViewModel);
        Task Update(int id, GenreUpdateViewModel genreViewModel);
        Task Remove(int id);
    }
}
