using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.ViewModels.TermsConditionsViewModels;

namespace Music.Controllers
{
    public class TermsConditionsController : Controller
    {
        private readonly ITermsConditionsService _termsConditionsService;



        public TermsConditionsController(ITermsConditionsService termsConditionsService)
        {
            _termsConditionsService = termsConditionsService;
        }

        public async Task<IActionResult> Index()
        {
            var termsConditionsViewModel = new TermsConditionsViewModel()
            {
                TermsConditions = await _termsConditionsService.GetAllAsync()
            };

            return View(termsConditionsViewModel);
        }
    }
}
