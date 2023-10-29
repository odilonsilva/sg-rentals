using sg_rentals.Models;

namespace sg_rentals.Repositories
{
    public class HouseRepository : IHouseRepository
    {
        private AppDBContext _dbContext;
        public HouseRepository(AppDBContext appDBContext) 
        {
            _dbContext = appDBContext;
        }

        public House Create(House house)
        {
            _dbContext.Houses.Add(house);
            _dbContext.SaveChanges();
            return house;
        }

        public bool Delete(int id)
        {
            var house = _dbContext.Houses.Where(u => u.Id == id).Single();
            if (house != null)
            {
                _dbContext.Houses.Remove(house);
                _dbContext.SaveChanges(true);
                return true;
            }
            return false;
        }

        public House? Get(int id)
        {
            return _dbContext.Houses.Find(id);
        }

       
        public IEnumerable<House> List()
        {
            return _dbContext.Houses.ToList();
        }

        public House Update(House house)
        {
            _dbContext.Houses.Update(house);
            _dbContext.SaveChanges();
            return house;
        }
    }
}
