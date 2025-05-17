using BreadFactory.Models;
using BreadFactory.ViewModels;
using System.Collections.ObjectModel;
using System.Windows;

namespace BreadFactory.Views
{
    public partial class WarehouseTransactionWindow : Window
    {
        public WarehouseTransactionWindow(User currentUser, ObservableCollection<Product> products, string transactionType)
        {
            InitializeComponent();
            DataContext = new WarehouseTransactionViewModel(currentUser, products, transactionType);
        }

        public WarehouseTransactionWindow(User currentUser, WarehouseItem item, string transactionType)
        {
            InitializeComponent();
            DataContext = new WarehouseTransactionViewModel(currentUser, item, transactionType);
        }

        public WarehouseTransaction Transaction => ((WarehouseTransactionViewModel)DataContext).Transaction;
    }
}