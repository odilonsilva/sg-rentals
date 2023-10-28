using Microsoft.AspNetCore.Mvc;
using sg_rentals.Models;
using sg_rentals.Repositories;
using System.Diagnostics;

namespace sg_rentals.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;

        private readonly IUserRepository _repository;

        public UserController(ILogger<UserController> logger, IUserRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public IActionResult Index()
        {
            IEnumerable<User> users = _repository.List();
            return View(users);
        }
             

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                bool isEmailUsed = _repository.IsEmailUsed(user.Email);

                if (isEmailUsed)
                {
                    TempData["ErrorMessage"] = "Esse e-mail está em uso por outro Usuário.";
                    return View(user);
                } else {
                    _repository.Create(user);

                    TempData["SuccessMessage"] = "Usuário cadastrado com sucesso.";
                    return RedirectToAction("Index");
                }
            }
            return View(user);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                TempData["ErrorMessage"] = "Falha ao buscar esse Usuário.";
                return RedirectToAction("Index");
            }

            var user = _repository.Get((int)id);
            if (user == null)
            {
                TempData["ErrorMessage"] = "Falha ao buscar esse Usuário.";
                return RedirectToAction("Index");
            }

            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(User user)
        {
            User oldUser = null;

            if (user.Password == null)
            {
                oldUser = _repository.Get(user.Id);

                if (oldUser == null) return RedirectToAction("Index");

                user.Password = oldUser.Password;
            }

            bool isEmailUsed = _repository.IsEmailUsed(user.Email, user.Id);

            if (isEmailUsed)
            {
                TempData["ErrorMessage"] = "Esse e-mail está em uso por outro Usuário.";
                return View(user); 
            } else { 
                _repository.Update(user, oldUser);
                
                TempData["SuccessMessage"] = "Usuário editado com sucesso.";
                return RedirectToAction("Index");
            }
            
        }

        public IActionResult Delete(int id) {
            if(_repository.Delete(id))
            {
                TempData["SuccessMessage"] = "Usuário excluido com sucesso.";
                return RedirectToAction("Index");
            }
            TempData["ErrorMessage"] = "Usuário não encontrado.";
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}