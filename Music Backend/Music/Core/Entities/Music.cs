using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Music
    {
        public int Id { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public string Title { get; set; }
        public string Singer { get; set; }
        public string Author { get; set; }
        public string Link { get; set; }
    }
}
