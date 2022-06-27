using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.ViewModels.HomeViewModel;

namespace Music.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFaqService _faqService;



        public HomeController(IFaqService faqService)
        {
            _faqService = faqService;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "MainUser");
            }

            var homeViewModel = new HomeViewModel()
            {
                Faqs = await  _faqService.GetAllAsync()
            };

            return View(homeViewModel);
        }

    }
}
