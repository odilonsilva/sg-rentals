using sg_rentals.Models;

namespace sg_rentals.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> List();
        User Get(int id);
        User Create(User user);
        User Update(User user, User? oldUser = null);
        bool Delete(int id);
        bool IsEmailUsed(string email, int id = 0);
    }
}
