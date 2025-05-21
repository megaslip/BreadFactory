using BreadFactory.Models;
using System.Data.Entity;

namespace BreadFactory.Data.Initializer
{
    public class DatabaseInitializer : CreateDatabaseIfNotExists<BreadFactoryContext>
    {
        protected override void Seed(BreadFactoryContext context)
        {
            var products = new[]
            {
                new Product { Name = "Белый хлеб", Weight = 0.5m, ProductionTime = 120, Cost = 30.0m },
                new Product { Name = "Ржаной хлеб", Weight = 0.7m, ProductionTime = 180, Cost = 45.0m }
            };

            var users = new[]
            {
                new User { Username = "admin", PasswordHash = "AQAAAAIAAYagAAAAEE...", Salt = "salt", Role = "Admin" }
            };

            context.Products.AddRange(products);
            context.Users.AddRange(users);
            context.SaveChanges();
        }
    }
}