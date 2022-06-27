using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Business.ViewModels.GenreViewModels
{
    public class GenreCreateViewModel
    {
        public string Name { get; set; }
        public IFormFile Image { get; set; }
    }
}
