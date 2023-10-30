using sg_rentals.Models;

namespace sg_rentals.Repositories
{
    public interface IBookingRepository
    {
        IEnumerable<BookingViewModel> List();
        Booking? Get(int id);
        Booking Create(Booking booking);
        Booking Update(Booking booking);
        bool Delete(int id);
        decimal GetCarPrice(int id);
        decimal GetHousePrice(int id);
        IEnumerable<object> GetHouses();
    }
}
