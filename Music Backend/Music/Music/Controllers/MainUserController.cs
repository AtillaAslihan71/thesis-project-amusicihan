using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.ViewModels.MainUserViewModels;
using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Music.Controllers
{
    public class MainUserController : Controller
    {
        private readonly IGenreService _genreService;
        private readonly IMusicService _musicService;
        private readonly IUserGenreService _userGenreService;
        private readonly UserManager<ApplicationUser> _userManager;



        public MainUserController(UserManager<ApplicationUser> userManager,IGenreService genreService,IMusicService musicService, IUserGenreService userGenreService)
        {
            _genreService = genreService;
            _musicService = musicService;
            _userGenreService = userGenreService;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            ApplicationUser myUser = await _userManager.GetUserAsync(User);

            var mainUserViewModel = new MainUserViewModel()
            {
                Genres = await _genreService.GetAllAsync(),
                Musics = await _musicService.GetAllAsync(),
                UserGenres = await _userGenreService.GetAllAsync(),
                User = myUser
            };

            return View(mainUserViewModel);
        }

        public async Task<IActionResult> Playlist(int id)
        {
            Genre dbGenre = await _genreService.Get(id);
            if (dbGenre == null)
            {
                return NotFound();
            }
            

            ApplicationUser myUser = await _userManager.GetUserAsync(User);

            var mainUserViewModel = new MainUserViewModel()
            {
                Genres = await _genreService.GetAllAsync(),
                Musics = await _musicService.GetAllAsync(),
                UserGenres = await _userGenreService.GetAllAsync(),
                User = myUser,
                Genre = await _genreService.Get(id)
            };

            return View(mainUserViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMusic(MainUserViewModel mainUserViewModel)
        {

            ApplicationUser myUser = await _userManager.GetUserAsync(User);

            await _musicService.Create(mainUserViewModel,myUser);

            return RedirectToAction("Playlist","MainUser");
        }

    }
}
