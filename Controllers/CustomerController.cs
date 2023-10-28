using Microsoft.AspNetCore.Mvc;
using sg_rentals.Models;
using sg_rentals.Repositories;
using System.Diagnostics;

namespace sg_rentals.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ILogger<CustomerController> _logger;

        private readonly ICustomerRepository _repository;

        public CustomerController(ILogger<CustomerController> logger, ICustomerRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public IActionResult Index()
        {
            IEnumerable<Customer> customers = _repository.List();
            return View(customers);
        }
             

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                bool isEmailUsed = _repository.IsEmailUsed(customer.Email);

                if (isEmailUsed)
                {
                    TempData["ErrorMessage"] = "Esse e-mail está em uso por outro Cliente.";
                    return View(customer);
                } else {
                    _repository.Create(customer);

                    TempData["SuccessMessage"] = "Cliente cadastrado com sucesso.";
                    return RedirectToAction("Index");
                }
            }
            return View(customer);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                TempData["ErrorMessage"] = "Falha ao buscar esse Cliente.";
                return RedirectToAction("Index");
            }

            var customer = _repository.Get((int)id);
            if (customer == null)
            {
                TempData["ErrorMessage"] = "Falha ao buscar esse Cliente.";
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            bool isEmailUsed = _repository.IsEmailUsed(customer.Email, customer.Id);

            if (isEmailUsed)
            {
                TempData["ErrorMessage"] = "Esse e-mail está em uso por outro Cliente.";
                return View(customer); 
            } else { 
                _repository.Update(customer);
                
                TempData["SuccessMessage"] = "Cliente editado com sucesso.";
                return RedirectToAction("Index");
            }
            
        }

        public IActionResult Delete(int id) {
            if(_repository.Delete(id))
            {
                TempData["SuccessMessage"] = "Cliente excluido com sucesso.";
                return RedirectToAction("Index");
            }
            TempData["ErrorMessage"] = "Cliente não encontrado.";
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}