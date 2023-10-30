namespace sg_rentals.Models
{
    public class BookingViewModel
    {
        public Booking Booking { get; set; }
        public Customer Customer { get; set; }
        public House House { get; set; }
        public CarBrandModel CarModel { get; set; }
        public Brand Brand { get; set; }
    }
}
