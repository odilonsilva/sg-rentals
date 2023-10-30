using Newtonsoft.Json;
using sg_rentals.Models;

namespace sg_rentals.Helper
{
    public class Session : ISession
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public Session(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public void CreateSession(User user)
        {
            var userLogged = JsonConvert.SerializeObject(user);
            _contextAccessor.HttpContext.Session.SetString("userLogged", userLogged);
        }

        public void DeleteSession()
        {
            _contextAccessor.HttpContext.Session.Remove("userLogged");
        }

        public User? GetSession()
        {
            var userLogged = _contextAccessor.HttpContext.Session.GetString("userLogged");

            if (string.IsNullOrEmpty(userLogged)) return null;

            User user = JsonConvert.DeserializeObject<User>(userLogged);
            return user;
        }
    }
}
