using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Implementations;
using Business.Interfaces;
using Business.ViewModels.DashboardViewModels;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Music.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMusicService _musicService;

        public DashboardController(UserManager<ApplicationUser> userManager,IMusicService musicService)
        {
            _userManager = userManager;
            _musicService = musicService;
        }

        public async Task<IActionResult> Index()
        {
            var music = await _musicService.GetAllAsync();
            var allUsers = await _userManager.Users.ToListAsync();

            var musicCount = music.Count();
            var allUserCount = allUsers.Count();


            var dashboardViewModel = new DashboardViewModel()
            {
                MusicCount = musicCount,
                UsersCount = allUserCount

            };

            return View(dashboardViewModel);
        }
    }
}
