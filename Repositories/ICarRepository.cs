using sg_rentals.Models;

namespace sg_rentals.Repositories
{
    public interface ICarRepository
    {
        IEnumerable<Car> List();
        Car? Get(int id);
        Car Create(Car car);
        Car Update(Car car);
        bool Delete(int id);
        bool IsLicenseUsed(string licensePlate, int id = 0);
        IEnumerable<CarBrandModel> GetModels(int id);
    }
}
