using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using sg_rentals.Models;
using sg_rentals.Repositories;
using System.Diagnostics;

namespace sg_rentals.Controllers
{
    public class CarController : Controller
    {
        private readonly ILogger<CarController> _logger;

        private readonly ICarRepository _repository;

        private readonly AppDBContext _dbContext;

        public CarController(ILogger<CarController> logger, ICarRepository repository, AppDBContext appDBContext)
        {
            _logger = logger;
            _repository = repository;
            _dbContext = appDBContext;
        }

        public IActionResult Index()
        {
            IEnumerable<Car> cars = _repository.List();
            return View(cars);
        }
             

        public IActionResult Create()
        {
            SetViewBagStuff(false);
            return View();
        }

        [HttpPost]
        public IActionResult Create(Car car)
        {

            if (ModelState.IsValid)
            {
                bool isLicensePlateUsed = _repository.IsLicenseUsed(car.LicensePlate);

                if (isLicensePlateUsed)
                {
                    TempData["ErrorMessage"] = "Essa placa está em uso por em Carro.";
                    return View(car);
                } else {
                    _repository.Create(car);

                    TempData["SuccessMessage"] = "Carro cadastrado com sucesso.";
                    return RedirectToAction("Index");
                }
            }

            SetViewBagStuff(false);
            return View(car);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                TempData["ErrorMessage"] = "Falha ao buscar esse Carro.";
                return RedirectToAction("Index");
            }

            var car = _repository.Get((int)id);
            if (car == null)
            {
                TempData["ErrorMessage"] = "Falha ao buscar esse Carro.";
                return RedirectToAction("Index");
            }

            SetViewBagStuff();
            return View(car);
        }

        [HttpPost]
        public IActionResult Edit(Car car)
        {
            if (ModelState.IsValid)
            {
                bool isLicensePlateUsed = _repository.IsLicenseUsed(car.LicensePlate, car.Id);

                if (isLicensePlateUsed)
                {
                    SetViewBagStuff();

                    TempData["ErrorMessage"] = "Essa placa está em uso por outro Carro.";
                    return View(car); 
                } else { 
                    _repository.Update(car);
                
                    TempData["SuccessMessage"] = "Carro editado com sucesso.";
                    return RedirectToAction("Index");
                }
            }

            SetViewBagStuff();
            return View(car);
        }

        public IActionResult Delete(int id) {
            if(_repository.Delete(id))
            {
                TempData["SuccessMessage"] = "Carro excluido com sucesso.";
                return RedirectToAction("Index");
            }
            TempData["ErrorMessage"] = "Carro não encontrado.";
            return RedirectToAction("Index");
        }

        public JsonResult GetCarModels(int id)
        {
            var carModels = _repository.GetModels(id);
            return Json(carModels);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private void SetViewBagStuff(bool IsEdit = true)
        {
            Dictionary<int, string> statusMap = new Dictionary<int, string>()
             {
                 {0,"Disponivel"},
                 {1,"Indisponivel"},
                 {2,"Alugado"}
             };

            var selectItems = statusMap.Select(x => new SelectListItem
            {
                Text = x.Value,
                Value = x.Key.ToString()
            });

            ViewBag.Status = new SelectList(selectItems, "Value", "Text");
            ViewBag.Brands = new SelectList(_dbContext.Brands, "Id", "Name");
            
            if (IsEdit)
                ViewBag.CarModels = new SelectList(_dbContext.CarBrandModels, "Id", "Name");
        }
    }
}