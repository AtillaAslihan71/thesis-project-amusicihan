using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.ViewModels.TermsConditionsViewModels;
using Core;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Music.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TermsConditionsController : Controller
    {
        private readonly ITermsConditionsService _termsConditionsService;
        private readonly IUnitOfWork _unitOfWork;


        public TermsConditionsController(IUnitOfWork unitOfWork, ITermsConditionsService termsConditionsService)
        {
            _unitOfWork = unitOfWork;
            _termsConditionsService = termsConditionsService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _unitOfWork.termsConditionsRepository.GetAllAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TermsConditionsCreateViewModel termsConditionsViewModel)
        {

            if (ModelState.IsValid)
            {




                await _termsConditionsService.Create(termsConditionsViewModel);
                return RedirectToAction(nameof(Index));
            }



            return View(termsConditionsViewModel);
        }


        public async Task<IActionResult> Delete(int id)
        {

            await _termsConditionsService.Remove(id);

            return RedirectToAction(nameof(Index));
        }


        public async Task<ActionResult> Update(int id)
        {
            TermsConditions termsConditions = await _termsConditionsService.Get(id);

            if (termsConditions == null) return NotFound();


            var termsConditionsViewModel = new TermsConditionsUpdateViewModel()
            {
                Text= termsConditions.Text

            };



            return View(termsConditionsViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(int id, TermsConditionsUpdateViewModel termsConditionsViewModel)
        {
            if (ModelState.IsValid)
            {



                await _termsConditionsService.Update(id, termsConditionsViewModel);
                return RedirectToAction(nameof(Index));
            }


            return View(termsConditionsViewModel);
        }
    }
}
