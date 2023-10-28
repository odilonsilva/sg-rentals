using Microsoft.EntityFrameworkCore;
using sg_rentals.Models;

namespace sg_rentals.Repositories
{
    public class UserRepository : IUserRepository
    {
        private AppDBContext _dbContext;
        public UserRepository(AppDBContext appDBContext) 
        {
            _dbContext = appDBContext;
        }

        public User Create(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return user;
        }

        public bool Delete(int id)
        {
            var user = _dbContext.Users.Where(u => u.Id == id).Single();
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                _dbContext.SaveChanges(true);
                return true;
            }
            return false;
        }

        public User Get(int id)
        {
            return _dbContext.Users.Where(u => u.Id == id).Single();
        }

        public bool IsEmailUsed(string email, int id = 0)
        {
            var user = _dbContext.Users
                .Where(u => u.Email.Equals(email))
                .SingleOrDefault();

            if (user == null)
            {
                return false;

            } else {
                _dbContext.Entry(user).State = EntityState.Detached;
                
                if (id > 0 && user.Id == id)
                {
                    return false;
                }
            }
            return true;
        }

        public IEnumerable<User> List()
        {
            return _dbContext.Users.ToList();
        }

        public User Update(User user, User? oldUser = null)
        {
            if (oldUser != null) 
            {
                _dbContext.Entry(oldUser).State = EntityState.Detached;
            }
            _dbContext.Users.Update(user);
            _dbContext.SaveChanges();
            return user;
        }
    }
}
