using Microsoft.AspNetCore.Mvc;
using sg_rentals.Models;
using sg_rentals.Repositories;
using System.Diagnostics;

namespace sg_rentals.Controllers
{
    public class HouseController : Controller
    {
        private readonly ILogger<HouseController> _logger;

        private readonly IHouseRepository _repository;

        public HouseController(ILogger<HouseController> logger, IHouseRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public IActionResult Index()
        {
            IEnumerable<House> houses = _repository.List();
            return View(houses);
        }
             

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(House house)
        {
            if (ModelState.IsValid)
            {
                _repository.Create(house);

                TempData["SuccessMessage"] = "Casa cadastrada com sucesso.";
                return RedirectToAction("Index");
            }
            return View(house);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                TempData["ErrorMessage"] = "Falha ao buscar essa Casa.";
                return RedirectToAction("Index");
            }

            var house = _repository.Get((int)id);
            if (house == null)
            {
                TempData["ErrorMessage"] = "Falha ao buscar essa Casa.";
                return RedirectToAction("Index");
            }

            return View(house);
        }

        [HttpPost]
        public IActionResult Edit(House house)
        {
            _repository.Update(house);
                
            TempData["SuccessMessage"] = "Casa editada com sucesso.";
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id) {
            if(_repository.Delete(id))
            {
                TempData["SuccessMessage"] = "Casa excluida com sucesso.";
                return RedirectToAction("Index");
            }
            TempData["ErrorMessage"] = "Casa não encontrada.";
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}