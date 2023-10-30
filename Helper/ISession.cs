using sg_rentals.Models;

namespace sg_rentals.Helper
{
    public interface ISession
    {
        void CreateSession(User user);
        void DeleteSession();
        User? GetSession();
    }
}
