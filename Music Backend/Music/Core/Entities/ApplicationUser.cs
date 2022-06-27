using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<UserGenre> UserGenre { get; set; }
    }
}
