using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.ViewModels.FaqViewModels;
using Core;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Music.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class FaqsController : Controller
    {
        private readonly IFaqService _faqService;
        private readonly IUnitOfWork _unitOfWork;


        public FaqsController(IUnitOfWork unitOfWork, IFaqService faqService)
        {
            _unitOfWork = unitOfWork;
            _faqService = faqService;
        }


        public async Task<IActionResult> Index()
        {
            return View(await _unitOfWork.faqRepository.GetAllAsync());
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FaqCreateViewModel faqViewModel)
        {

            if (ModelState.IsValid)
            {

                


                await _faqService.Create(faqViewModel);
                return RedirectToAction(nameof(Index));
            }



            return View(faqViewModel);
        }


        public async Task<IActionResult> Delete(int id)
        {

            await _faqService.Remove(id);

            return RedirectToAction(nameof(Index));
        }


        public async Task<ActionResult> Update(int id)
        {
            Faq faq = await _faqService.Get(id);

            if (faq == null) return NotFound();


            var faqViewModel = new FaqUpdateViewModel()
            {
                Question = faq.Question,
                Answer = faq.Answer

            };



            return View(faqViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(int id, FaqUpdateViewModel faqViewModel)
        {
            if (ModelState.IsValid)
            {
                


                await _faqService.Update(id, faqViewModel);
                return RedirectToAction(nameof(Index));
            }

            
            return View(faqViewModel);
        }

    }
}
