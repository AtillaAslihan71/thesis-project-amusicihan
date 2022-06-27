using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;

namespace Business.ViewModels.AccountViewModels
{
    public class SecondRegisterViewModel
    {
        //public List<Genre> Genres { get; set; }
        public int Id { get; set; }
        public List<CheckBoxItem> Genres { get; set; }
        public string UserId { get; set; }
    }
}
