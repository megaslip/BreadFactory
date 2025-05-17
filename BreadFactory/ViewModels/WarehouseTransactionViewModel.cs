using BreadFactory.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace BreadFactory.ViewModels
{
    public class WarehouseTransactionViewModel : ViewModelBase
    {
        private WarehouseTransaction _transaction;
        private bool _isProductEditable;
        private string _title;

        public WarehouseTransactionViewModel(User currentUser, ObservableCollection<Product> products, string transactionType)
        {
            _transaction = new WarehouseTransaction
            {
                TransactionType = transactionType,
                TransactionDate = DateTime.Now,
                ResponsiblePerson = currentUser,
                Item = new WarehouseItem
                {
                    ProductionDate = DateTime.Today,
                    ExpiryDate = DateTime.Today.AddDays(3)
                }
            };

            Products = products;
            IsProductEditable = true;
            Title = $"{transactionType} продукции";

            InitializeCommands();
        }

        public WarehouseTransactionViewModel(User currentUser, WarehouseItem item, string transactionType)
        {
            _transaction = new WarehouseTransaction
            {
                TransactionType = transactionType,
                TransactionDate = DateTime.Now,
                ResponsiblePerson = currentUser,
                Item = item
            };

            IsProductEditable = false;
            Title = $"{transactionType} {item.Product.Name}";

            InitializeCommands();
        }

        public WarehouseTransaction Transaction
        {
            get => _transaction;
            set => SetProperty(ref _transaction, value);
        }

        public ObservableCollection<Product> Products { get; }

        public bool IsProductEditable
        {
            get => _isProductEditable;
            set => SetProperty(ref _isProductEditable, value);
        }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public ICommand OkCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        private void InitializeCommands()
        {
            OkCommand = new RelayCommand(Ok, CanOk);
            CancelCommand = new RelayCommand(Cancel);
        }

        private bool CanOk(object parameter)
        {
            return Transaction.Item.Product != null &&
                   Transaction.Quantity > 0 &&
                   Transaction.Item.ProductionDate <= DateTime.Today;
        }

        private void Ok(object parameter)
        {
            if (Transaction.TransactionType == "Поступление")
            {
                Transaction.Item.ExpiryDate = Transaction.Item.ProductionDate.AddDays(
                    Transaction.Item.Product.Name.Contains("Белый") ? 3 : 5);
            }

            ((Window)parameter).DialogResult = true;
        }

        private void Cancel(object parameter)
        {
            ((Window)parameter).DialogResult = false;
        }
    }
}