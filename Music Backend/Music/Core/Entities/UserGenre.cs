using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class UserGenre
    {
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
