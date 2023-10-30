using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using sg_rentals.Models;

namespace sg_rentals.ViewComponents
{
    public class Logout : ViewComponent
    {
        public async Task<IViewComponentResult?> InvokeAsync()
        {
            var userLogged = HttpContext.Session.GetString("userLogged");
            
            if (string.IsNullOrEmpty(userLogged)){
                return null;
            }
            
            User user = JsonConvert.DeserializeObject<User>(userLogged);
            return View(user);
        }
    }
}
