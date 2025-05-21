namespace BreadFactory.Data.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}