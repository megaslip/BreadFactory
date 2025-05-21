using System;

namespace BreadFactory.Models
{
    public class Equipment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Status { get; set; } = "Работает";
        public DateTime LastMaintenance { get; set; }
    }
}