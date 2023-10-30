using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sg_rentals.Models;
using sg_rentals.Repositories;
using System.Collections.Specialized;
using System;
using System.Diagnostics;

namespace sg_rentals.Controllers
{
    [Filters.UserLogged]
    public class BookingController : Controller
    {
        private readonly ILogger<BookingController> _logger;

        private readonly IBookingRepository _repository;

        private readonly AppDBContext _dbContext;

        public BookingController(ILogger<BookingController> logger, IBookingRepository repository, AppDBContext appDBContext)
        {
            _logger = logger;
            _repository = repository;
            _dbContext = appDBContext;
        }

        public IActionResult Index()
        {
            IEnumerable<BookingViewModel> bookings = _repository.List();
            return View(bookings);
        }
             

        public IActionResult Create()
        {
            SetViewBagStuff(false);
            return View();
        }

        [HttpPost]
        public IActionResult Create(Booking booking)
        {

            if (ModelState.IsValid)
            {
                if (booking.Type == 0 && (booking.CarId == 0 || booking.CarId == null))
                {
                    SetViewBagStuff(false);
                    TempData["ErrorMessage"] = "Falha ao criar reserva. Escolha o carro."+ booking.CarId;
                    return View(booking);
                } else if (booking.Type == 1 && (booking.HouseId == 0 || booking.HouseId == null))
                {
                   SetViewBagStuff(false);
                   TempData["ErrorMessage"] = "Falha ao criar reserva. Escolha a casa.";
                   return View(booking);
                }

                _repository.Create(booking);
                TempData["SuccessMessage"] = "Reserva criada com sucesso.";
                return RedirectToAction("Index");
            }

            SetViewBagStuff(false);
            return View(booking);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                TempData["ErrorMessage"] = "Falha ao buscar essa reserva";
                return RedirectToAction("Index");
            }

            var booking = _repository.Get((int)id);
            if (booking == null)
            {
                TempData["ErrorMessage"] = "Falha ao buscar essa reserva.";
                return RedirectToAction("Index");
            }

            SetViewBagStuff();
            return View(booking);
        }

        [HttpPost]
        public IActionResult Edit(Booking booking)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(booking);
                
                TempData["SuccessMessage"] = "Reserva editada com sucesso.";
                return RedirectToAction("Index");
            }

            SetViewBagStuff();
            return View(booking);
        }

        public IActionResult Delete(int id) {
            if(_repository.Delete(id))
            {
                TempData["SuccessMessage"] = "Reserva excluida com sucesso.";
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Reserva não encontrada.";
            return RedirectToAction("Index");
        }

        public JsonResult GetHouses()
        {
            var houses = _repository.GetHouses();
            return Json(houses);
        }
        
        public JsonResult getPrice()
        {
            var parameters = System.Web.HttpUtility.ParseQueryString(Request.QueryString.Value);
            int qtdDays = int.Parse(parameters["days"]);
            int type = int.Parse(parameters["type"]);
            int itemId = int.Parse(parameters["itemId"]);
            decimal priceBase;

            if (type == 0)
            {
                priceBase = _repository.GetCarPrice(itemId);
            } else
            {
                priceBase = _repository.GetHousePrice(itemId);
            }

            return Json(priceBase * qtdDays);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private void SetViewBagStuff(bool IsEdit = true)
        {
            ViewBag.Customers = new SelectList(_dbContext.Customers, "Id", "Name");
            var cars = _dbContext.Cars
                    .Where(c => c.Status.Equals(0))
                    .Include(b => b.CarModel)
                    .ToList();

            var selectItems = cars.Select(x => new SelectListItem
            {
                Text = x.CarModel.Name +' '+ x.LicensePlate,
                Value = x.Id.ToString()
            });
            ViewBag.Cars = new SelectList(selectItems, "Value", "Text");

            if (IsEdit)
            {
                var houses = _dbContext.Houses.ToList();

                var selectHouses = houses.Select(x => new SelectListItem
                {
                    Text = x.Description + ' ' + x.City,
                    Value = x.Id.ToString()
                });
                ViewBag.Houses = new SelectList(selectHouses, "Value", "Text");
            }
        }
    }
}