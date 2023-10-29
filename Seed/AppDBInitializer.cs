using sg_rentals.Models;
using sg_rentals.Repositories;

namespace sg_rentals.Seed
{
    public class AppDBInitializer
    {
        public static void Seed(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDBContext>();

                if (!context.Brands.Any())
                {
                    context.AddRange(
                        new Brand()
                        {
                            Id = 1,
                            Name = "Fiat",
                        },
                        new Brand()
                        {
                            Id = 2,
                            Name = "Ford",
                        },
                        new Brand()
                        {
                            Id = 3,
                            Name = "Honda",
                        },
                        new Brand()
                        {
                            Id = 4,
                            Name = "Volkswagen",
                        }
                    );
                    context.SaveChanges();
                }

                if (!context.CarBrandModels.Any())
                {
                    context.AddRange(
                        new CarBrandModel()
                        {
                            Id= 1,
                            BrandId = 1,
                            Name = "Argo"
                        },
                        new CarBrandModel()
                        {
                            Id= 2,
                            BrandId = 1,
                            Name = "FastBack"
                        },
                        new CarBrandModel()
                        {
                            Id = 3,
                            BrandId = 1,
                            Name = "Mobi"
                        },
                        new CarBrandModel()
                        {
                            Id = 4,
                            BrandId = 2,
                            Name = "F-150"
                        },
                        new CarBrandModel()
                        {
                            Id = 5,
                            BrandId = 2,
                            Name = "Maverick"
                        },
                        new CarBrandModel()
                        {
                            Id = 6,
                            BrandId = 2,
                            Name = "Ranger"
                        },
                        new CarBrandModel()
                        {
                            Id = 7,
                            BrandId = 3,
                            Name = "HR-V"
                        },
                        new CarBrandModel()
                        {
                            Id = 8,
                            BrandId = 3,
                            Name = "City"
                        },
                        new CarBrandModel()
                        {
                            Id = 9,
                            BrandId = 3,
                            Name = "Civic"
                        },
                        new CarBrandModel()
                        {
                            Id = 10,
                            BrandId = 4,
                            Name = "Polo"
                        },
                        new CarBrandModel()
                        {
                            Id = 11,
                            BrandId = 4,
                            Name = "T-Cross"
                        },
                        new CarBrandModel()
                        {
                            Id = 12,
                            BrandId = 4,
                            Name = "Nivus"
                        }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}
