using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;

namespace Business.ViewModels.MainUserViewModels
{
    public class MainUserViewModel
    {
        public List<Genre> Genres { get; set; }
        public List<UserGenre> UserGenres { get; set; }
        public List<Music> Musics { get; set; }
        public ApplicationUser User { get; set; }
        public Genre Genre { get; set; }
        public string Title { get; set; }
        public string Singer { get; set; }
        public string Link { get; set; }
        public string Author { get; set; }
        public int GenreId { get; set; }

    }
}
