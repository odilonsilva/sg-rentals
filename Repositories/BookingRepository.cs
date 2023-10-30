using Microsoft.EntityFrameworkCore;
using sg_rentals.Models;

namespace sg_rentals.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private AppDBContext _dbContext;
        public BookingRepository(AppDBContext appDBContext) 
        {
            _dbContext = appDBContext;
        }

        public Booking Create(Booking booking)
        {
            _dbContext.Bookings.Add(booking);
            _dbContext.SaveChanges();
            return booking;
        }

        public bool Delete(int id)
        {
            var booking = _dbContext.Bookings.Where(u => u.Id == id).Single();
            if (booking != null)
            {
                _dbContext.Bookings.Remove(booking);
                _dbContext.SaveChanges(true);
                return true;
            }
            return false;
        }

        public Booking? Get(int id)
        {
            return _dbContext.Bookings.Find(id);
        }

        public IEnumerable<BookingViewModel> List()
        {
            var query = from booking in _dbContext.Bookings
                        join customer in _dbContext.Customers on booking.CustomerId equals customer.Id into customerGroup
                        from customer in customerGroup.DefaultIfEmpty()
                        join car in _dbContext.Cars on booking.CarId equals car.Id into carGroup
                        from car in carGroup.DefaultIfEmpty()
                        join brand in _dbContext.Brands on car.BrandId equals brand.Id into brandGroup
                        from brand in brandGroup.DefaultIfEmpty()
                        join carModel in _dbContext.CarBrandModels on car.CarBrandModelId equals carModel.Id into carModelGroup
                        from carModel in carModelGroup.DefaultIfEmpty()
                        join house in _dbContext.Houses on booking.HouseId equals house.Id into houseGroup
                        from house in houseGroup.DefaultIfEmpty()
                        select new BookingViewModel
                        {
                            Booking = booking,
                            Customer = customer,
                            CarModel = carModel,
                            Brand = brand,
                            House = house
                        };

            return query.ToList();
        }

        public Booking Update(Booking booking)
        {
            _dbContext.Bookings.Update(booking);
            _dbContext.SaveChanges();
            return booking;
        }

        public decimal GetCarPrice(int id)
        {
            var car = _dbContext.Cars
                .Select(c => new
                {
                    c.Id,
                    c.Price
                })
                .Where(c => c.Id == id)
                .Single();

            return car.Price;
        }

        public decimal GetHousePrice(int id)
        {
            var houses = _dbContext.Houses
                .Select(h => new
                {
                    h.Id,
                    h.Price
                })
                .Where(h => h.Id == id)
                .Single();

            return houses.Price;
        }
    
        public IEnumerable<object> GetHouses()
            {
            var houses = _dbContext.Houses
                .Select(h => new
                {
                    h.Id,
                    h.Description
                })
                .ToList();
            
                return houses;
            }
        }
}
