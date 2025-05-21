using BreadFactory.Data.Initializer;
using BreadFactory.Data.Models;
using BreadFactory.Models;
using System.Data.Entity;

namespace BreadFactory.Data
{
    public class BreadFactoryContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }

        public BreadFactoryContext() : base("name=BreadFactoryContext")
        {
            Database.SetInitializer(new DatabaseInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipe>()
                .HasMany(r => r.Ingredients)
                .WithRequired(i => i.Recipe)
                .HasForeignKey(i => i.RecipeId);
        }
    }
}