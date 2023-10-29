using sg_rentals.Models;
using Microsoft.EntityFrameworkCore;
namespace sg_rentals.Repositories
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<CarBrandModel> CarBrandModels { get; set; }
    }
}
