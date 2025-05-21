using BreadFactory.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Runtime.Remoting.Contexts;

namespace BreadFactory.Data
{
    public class BreadFactoryContext : DbContext
    {
        public BreadFactoryContext() : base("name=BreadFactoryDB")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipe>()
                .HasRequired(r => r.Product)
                .WithMany()
                .HasForeignKey(r => r.ProductId)
                .WillCascadeOnDelete(false);
        }
    }
}