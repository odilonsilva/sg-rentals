using Microsoft.AspNetCore.Mvc;

namespace sg_rentals.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
