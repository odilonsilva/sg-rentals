using Microsoft.AspNetCore.Mvc;

namespace sg_rentals.Controllers
{
    [Filters.UserLogged]
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}