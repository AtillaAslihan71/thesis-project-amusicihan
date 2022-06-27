using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Core.Entities;

namespace Business.ViewModels.AccountViewModels
{
    public class UserProfileViewModel
    {
        public string Email { get; set; }



        [Required, MaxLength(255), DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password), Compare(nameof(NewPassword))]
        public string ConfirmPassword { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }


        public List<Genre> Genres { get; set; }

        public List<UserGenre> UserGenres { get; set; }

        public List<ApplicationUser> ApplicationUsers { get; set; }
    }
}
