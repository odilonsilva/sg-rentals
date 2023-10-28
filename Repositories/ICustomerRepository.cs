using sg_rentals.Models;

namespace sg_rentals.Repositories
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> List();
        Customer Get(int id);
        Customer Create(Customer customer);
        Customer Update(Customer customer, Customer? oldCustomer = null);
        bool Delete(int id);
        bool IsEmailUsed(string email, int id = 0);
    }
}
