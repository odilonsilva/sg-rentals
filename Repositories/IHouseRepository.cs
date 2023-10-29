using sg_rentals.Models;

namespace sg_rentals.Repositories
{
    public interface IHouseRepository
    {
        IEnumerable<House> List();
        House? Get(int id);
        House Create(House house);
        House Update(House house);
        bool Delete(int id);
    }
}
