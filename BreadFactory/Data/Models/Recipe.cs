using System;
using System.Collections.Generic;

namespace BreadFactory.Data.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TargetProduct { get; set; }
        public string Instructions { get; set; }
        public TimeSpan Duration { get; set; }
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    }
}