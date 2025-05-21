using System;
using System.Collections.ObjectModel;

namespace BreadFactory.Models
{
    public class ProductionBatch
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public ObservableCollection<BatchStage> Stages { get; set; } = new ObservableCollection<BatchStage>();
        public string Status => EndTime.HasValue ? "Завершено" : "В процессе";
    }

    public class BatchStage
    {
        public ProductionStage Stage { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Status => EndTime.HasValue ? "Завершено" : "В процессе";
    }

    public class ProductionStage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
        public int Order { get; set; }
    }
}