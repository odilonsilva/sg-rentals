using Microsoft.EntityFrameworkCore;
using sg_rentals.Models;

namespace sg_rentals.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private AppDBContext _dbContext;
        public CustomerRepository(AppDBContext appDBContext) 
        {
            _dbContext = appDBContext;
        }

        public Customer Create(Customer customer)
        {
            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();
            return customer;
        }

        public bool Delete(int id)
        {
            var customer = _dbContext.Customers.Where(u => u.Id == id).Single();
            if (customer != null)
            {
                _dbContext.Customers.Remove(customer);
                _dbContext.SaveChanges(true);
                return true;
            }
            return false;
        }

        public Customer Get(int id)
        {
            return _dbContext.Customers.Where(u => u.Id == id).Single();
        }

        public bool IsEmailUsed(string email, int id = 0)
        {
            var customer = _dbContext.Customers
                .Where(u => u.Email.Equals(email))
                .SingleOrDefault();

            if (customer == null)
            {
                return false;

            } else {
                _dbContext.Entry(customer).State = EntityState.Detached;
                
                if (id > 0 && customer.Id == id)
                {
                    return false;
                }
            }
            return true;
        }

        public IEnumerable<Customer> List()
        {
            return _dbContext.Customers.ToList();
        }

        public Customer Update(Customer customer)
        {
            _dbContext.Customers.Update(customer);
            _dbContext.SaveChanges();
            return customer;
        }
    }
}
