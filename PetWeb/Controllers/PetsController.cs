using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PetData;
using PetWeb.Services;

namespace PetWeb.Controllers
{
    public class PetsController : Controller
    {
        private readonly IPetViewService _petViewService;
        public PetsController(IPetViewService petViewService)
        {
            _petViewService = petViewService;
        }
        public async Task<IActionResult> Index()
        {
            ViewData["PetTypes"] = _petViewService.GetPetTypes().Select(p => new SelectListItem()
            {
                Value = p,
                Text = p
            })
            .ToList();
            ViewData["Cities"] = _petViewService.GetCities().Select(p => new SelectListItem()
            {
                Value = p,
                Text = p
            })
            .ToList();
            return View(await _petViewService.GetPetsAsync(new SearchCriteria() { }));
        }
        public async Task<IActionResult> PetGrid(string city, string petName, string petType)
        {
            var searchParams = new SearchCriteria()
            {
                City = city,
                PetName = petName,
                PetType = petType
            };
            return PartialView(await _petViewService.GetPetsAsync(searchParams));
        }
    }
}
