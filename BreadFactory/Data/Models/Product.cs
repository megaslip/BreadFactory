using System;

namespace BreadFactory.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Weight { get; set; }
        public TimeSpan ProductionTime { get; set; }
        public decimal Cost { get; set; }
    }
}