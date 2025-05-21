using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace BreadFactory.Models
{
    public class Recipe
    {
        [Key]
        public int RecipeId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public ObservableCollection<RecipeItem> Ingredients { get; set; } = new ObservableCollection<RecipeItem>();

        [NotMapped]
        public decimal TotalCost => Ingredients?.Sum(i => i.Cost) ?? 0;

        [NotMapped]
        public string IngredientList =>
            Ingredients != null ?
            string.Join("\n", Ingredients.Select(i => $"{i.Ingredient.Name} - {i.Amount} {i.Ingredient.Unit}")) :
            string.Empty;

        public void AddIngredient(Ingredient ingredient, decimal amount)
        {
            Ingredients.Add(new RecipeItem
            {
                Ingredient = ingredient,
                Amount = amount,
                Unit = ingredient.Unit
            });
        }
    }

    public class RecipeItem
    {
        [Key]
        public int RecipeItemId { get; set; }

        [ForeignKey("Recipe")]
        public int RecipeId { get; set; }
        public virtual Recipe Recipe { get; set; }

        [ForeignKey("Ingredient")]
        public int IngredientId { get; set; }
        public virtual Ingredient Ingredient { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }

        [StringLength(20)]
        public string Unit { get; set; }

        [NotMapped]
        public decimal Cost => Ingredient != null ? Amount * Ingredient.PricePerUnit : 0;
    }
}