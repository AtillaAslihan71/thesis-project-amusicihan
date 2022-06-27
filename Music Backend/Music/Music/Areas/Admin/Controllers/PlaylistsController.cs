using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.ViewModels.PlaylistViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Music.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PlaylistsController : Controller
    {
        private readonly IGenreService _genreService;
        private readonly IMusicService _musicService;



        public PlaylistsController(IGenreService genreService, IMusicService musicService)
        {
            _genreService = genreService;
            _musicService = musicService;
        }

        public async Task<IActionResult> Index()
        {
            var playlistViewModel = new PlaylistViewModel()
            {
                Genres = await _genreService.GetAllAsync(),
                Musics = await  _musicService.GetAllAsync()
            };

            return View(playlistViewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {

            await _musicService.Remove(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
