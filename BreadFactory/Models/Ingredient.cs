using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BreadFactory.Models
{
    public class Ingredient
    {
        [Key]
        public int IngredientId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Category { get; set; } // Мука, Сахар и т.д.

        [Required]
        [StringLength(20)]
        public string Unit { get; set; } // кг, л, г

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal PricePerUnit { get; set; }

        [StringLength(100)]
        public string Supplier { get; set; }
    }
}