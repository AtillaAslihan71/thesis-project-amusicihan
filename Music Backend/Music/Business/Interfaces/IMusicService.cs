using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Business.ViewModels.FaqViewModels;
using Business.ViewModels.MainUserViewModels;
using Core.Entities;

namespace Business.Interfaces
{
    public interface IMusicService
    {
        Task<List<Music>> GetAllAsync();
        Task<Music> Get(int id);
        Task Create(MainUserViewModel mainUserViewModel, ApplicationUser myUser);
        Task Remove(int id);
    }
}
