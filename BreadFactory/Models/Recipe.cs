using System.Collections.ObjectModel;
using System.Linq;

namespace BreadFactory.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Product TargetProduct { get; set; }
        public ObservableCollection<RecipeItem> Ingredients { get; set; } = new ObservableCollection<RecipeItem>();
        public string Instructions { get; set; }
        public decimal TotalCost => Ingredients.Sum(i => i.Cost);
        public decimal CostPerUnit => TotalCost / (TargetProduct?.Weight ?? 1m);
    }

    public class RecipeItem
    {
        public Ingredient Ingredient { get; set; }
        public decimal Amount { get; set; }
        public string Unit { get; set; }
        public decimal Cost => Ingredient?.PricePerUnit * Amount ?? 0m;
    }

    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Unit { get; set; }
        public decimal PricePerUnit { get; set; }
        public string Supplier { get; set; }
        public decimal Stock { get; set; }
    }
}