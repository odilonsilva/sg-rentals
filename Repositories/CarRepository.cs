using Microsoft.EntityFrameworkCore;
using sg_rentals.Models;

namespace sg_rentals.Repositories
{
    public class CarRepository : ICarRepository
    {
        private AppDBContext _dbContext;
        public CarRepository(AppDBContext appDBContext) 
        {
            _dbContext = appDBContext;
        }

        public Car Create(Car car)
        {
            _dbContext.Cars.Add(car);
            _dbContext.SaveChanges();
            return car;
        }

        public bool Delete(int id)
        {
            var car = _dbContext.Cars.Where(u => u.Id == id).Single();
            if (car != null)
            {
                _dbContext.Cars.Remove(car);
                _dbContext.SaveChanges(true);
                return true;
            }
            return false;
        }

        public Car? Get(int id)
        {
            return _dbContext.Cars.Find(id);
        }

        public bool IsLicenseUsed(string licensePlate, int id = 0)
        {
            var car = _dbContext.Cars
                .Where(u => u.LicensePlate.Equals(licensePlate))
                .SingleOrDefault();

            if (car == null)
            {
                return false;

            } else {
                _dbContext.Entry(car).State = EntityState.Detached;
                
                if (id > 0 && car.Id == id)
                {
                    return false;
                }
            }
            return true;
        }

        public IEnumerable<Car> List()
        {
            return _dbContext.Cars
                .Include(b => b.Brand)
                .Include(c => c.CarModel)
                .ToList();
        }

        public Car Update(Car car)
        {
            _dbContext.Cars.Update(car);
            _dbContext.SaveChanges();
            return car;
        }

        public IEnumerable<CarBrandModel> GetModels(int id)
        {
            return _dbContext.CarBrandModels.Where(c => c.BrandId == id).ToList();
        }
    }
}
