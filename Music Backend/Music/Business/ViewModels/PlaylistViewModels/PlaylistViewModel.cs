using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;

namespace Business.ViewModels.PlaylistViewModels
{
    public class PlaylistViewModel
    {

        public List<Genre> Genres { get; set; }

        public List<Music> Musics { get; set; }
    }
}
