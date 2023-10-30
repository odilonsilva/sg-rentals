using Microsoft.AspNetCore.Mvc;
using sg_rentals.Models;
using sg_rentals.Repositories;

namespace sg_rentals.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserRepository _repository;
        private readonly sg_rentals.Helper.ISession _session;
        public LoginController(IUserRepository repository, sg_rentals.Helper.ISession session)
        {
            _repository = repository;
            _session = session;
        }

        public IActionResult Index()
        {
            if (_session.GetSession() != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult Logout()
        {
            _session.DeleteSession();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Index(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var user = _repository.FindByLoginAndPassword(loginModel.Login, loginModel.Password);
                if (user == null)
                {
                    TempData["ErrorMessage"] = "Login ou senha inválido(s).";
                    return View(loginModel);
                }
                _session.CreateSession(user);
                return RedirectToAction("Index", "Home");
            }
            return View(loginModel);
        }
    }
}
