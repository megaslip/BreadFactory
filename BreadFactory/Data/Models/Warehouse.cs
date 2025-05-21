using System;
using System.Collections.ObjectModel;

namespace BreadFactory.Models
{
    public class WarehouseItem
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public DateTime ProductionDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string StorageLocation { get; set; }
    }

    public class WarehouseTransaction
    {
        public int Id { get; set; }
        public WarehouseItem Item { get; set; }
        public int Quantity { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; }
        public string Notes { get; set; }
        public User ResponsiblePerson { get; set; }
    }
}