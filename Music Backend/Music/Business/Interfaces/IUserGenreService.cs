using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Business.Interfaces
{
    public interface IUserGenreService
    {
        Task<List<UserGenre>> GetAllAsync();
        Task<UserGenre> Get(int id);
    }
}
