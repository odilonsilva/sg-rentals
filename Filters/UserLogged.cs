using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using sg_rentals.Models;

namespace sg_rentals.Filters
{
    public class UserLogged : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string userLogged = context.HttpContext.Session.GetString("userLogged");
            if (string.IsNullOrEmpty(userLogged)) 
            {
                context.Result = new RedirectToRouteResult(
                        new RouteValueDictionary
                        {
                            { "controller", "Login" },
                            { "action", "Index" }
                        }
                    );
            } else
            {
                User user = JsonConvert.DeserializeObject<User>(userLogged);
                if (user == null)
                {
                    context.Result = new RedirectToRouteResult(
                        new RouteValueDictionary
                        {
                            { "controller", "Login" },
                            { "action", "Index" }
                        }
                    );
                }
            }
            base.OnActionExecuting(context);
        }
    }
}
