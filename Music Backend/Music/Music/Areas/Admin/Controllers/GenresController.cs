using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.Utilities;
using Business.ViewModels.GenreViewModels;
using Core;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Music.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class GenresController : Controller
    {
        private readonly IGenreService _genreService;
        private readonly IUnitOfWork _unitOfWork;


        public GenresController(IUnitOfWork unitOfWork, IGenreService genreService)
        {
            _unitOfWork = unitOfWork;
            _genreService = genreService;
        }


        public async Task<IActionResult> Index()
        {
            return View(await _unitOfWork.genreRepository.GetAllAsync());
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GenreCreateViewModel genreViewModel)
        {

            if (ModelState.IsValid)
            {

                if (!genreViewModel.Image.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Image", "The file you select must be of image type! ");
                    return View(genreViewModel);
                }

                if (!genreViewModel.Image.CheckFileSize(500))
                {
                    ModelState.AddModelError("Image", "The size of the selected file should not exceed 500 kb !");
                    return View(genreViewModel);
                }


                await _genreService.Create(genreViewModel);
                return RedirectToAction(nameof(Index));
            }



            return View(genreViewModel);
        }


        public async Task<IActionResult> Delete(int id)
        {

            await _genreService.Remove(id);

            return RedirectToAction(nameof(Index));
        }


        public async Task<ActionResult> Update(int id)
        {
            Genre genre = await _genreService.Get(id);

            if (genre == null) return NotFound();


            var genreViewModel = new GenreUpdateViewModel()
            {
                Name = genre.Name

            };



            return View(genreViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(int id, GenreUpdateViewModel genreViewModel)
        {
            if (ModelState.IsValid)
            {

                if (genreViewModel.Image != null)
                {
                    if (!genreViewModel.Image.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("ImageFiles", "The file you select must be of image type! ");
                        return View(genreViewModel);
                    }

                    if (!genreViewModel.Image.CheckFileSize(500))
                    {
                        ModelState.AddModelError("ImageFiles", "The size of the selected file should not exceed 500 kb !");
                        return View(genreViewModel);
                    }
                }


                await _genreService.Update(id, genreViewModel);
                return RedirectToAction(nameof(Index));
            }

            
            return View(genreViewModel);
        }

    }
}
