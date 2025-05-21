using BreadFactory.Data.Models;
using BreadFactory.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace BreadFactory.Data.Initializer
{
    public class DatabaseInitializer : CreateDatabaseIfNotExists<BreadFactoryContext>
    {
        protected override void Seed(BreadFactoryContext context)
        {
            // Добавляем тестового пользователя
            context.Users.Add(new User
            {
                Username = "admin",
                Password = "admin123",
                Role = "Admin"
            });

            // Добавляем тестовые рецепты
            var recipe = new Recipe
            {
                Name = "Белый хлеб",
                TargetProduct = "Хлеб белый",
                Instructions = "1. Замесить тесто...",
                Duration = TimeSpan.FromHours(2),
                Ingredients = new List<Ingredient>
                {
                    new Ingredient { Name = "Мука", Quantity = 1, Unit = "кг" },
                    new Ingredient { Name = "Вода", Quantity = 0.5, Unit = "л" }
                }
            };

            context.Recipes.Add(recipe);
            base.Seed(context);
        }
    }
}