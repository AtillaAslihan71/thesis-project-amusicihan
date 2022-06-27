using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Core.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }

        public ICollection<Music> Musics { get; set; }
        

        public List<UserGenre> UserGenre { get; set; }
    }
}
