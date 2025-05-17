using BreadFactory.Models;
using System.Collections.Generic;
using System.Linq; 
using BreadFactory.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace BreadFactory.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private User _currentUser;
        private ObservableCollection<Product> _products = new ObservableCollection<Product>();
        private ObservableCollection<ProductionBatch> _batches = new ObservableCollection<ProductionBatch>();
        private ObservableCollection<Equipment> _equipment = new ObservableCollection<Equipment>();
        private ObservableCollection<Recipe> _recipes = new ObservableCollection<Recipe>();
        private ObservableCollection<Ingredient> _allIngredients = new ObservableCollection<Ingredient>();
        private ObservableCollection<WarehouseItem> _warehouseItems = new ObservableCollection<WarehouseItem>();
        private ObservableCollection<WarehouseTransaction> _warehouseTransactions = new ObservableCollection<WarehouseTransaction>();

        private Product _selectedProduct;
        private ProductionBatch _selectedBatch;
        private Equipment _selectedEquipment;
        private Recipe _selectedRecipe;
        private WarehouseItem _selectedWarehouseItem;

        private string _warehouseSearchText;
        private string _selectedWarehouseFilter = "Все";
        private ObservableCollection<string> _warehouseFilterOptions = new ObservableCollection<string> { "Все", "Только в наличии", "Просроченные" };

        private Ingredient _newIngredient;
        private decimal _newIngredientAmount;

        public MainViewModel(User user)
        {
            _currentUser = user;
            InitializeTestData();

            // Команды производства
            StartProductionCommand = new RelayCommand(StartProduction, CanStartProduction);
            CompleteStageCommand = new RelayCommand(CompleteStage, CanCompleteStage);

            // Команды рецептов
            AddRecipeCommand = new RelayCommand(AddRecipe);
            DeleteRecipeCommand = new RelayCommand(DeleteRecipe, _ => SelectedRecipe != null);
            SaveRecipeCommand = new RelayCommand(SaveRecipe);
            AddIngredientCommand = new RelayCommand(AddIngredient, CanAddIngredient);
            RemoveIngredientCommand = new RelayCommand(RemoveIngredient);

            // Команды склада
            AddIncomingCommand = new RelayCommand(AddIncoming);
            AddOutgoingCommand = new RelayCommand(AddOutgoing, CanAddOutgoing);

            // Команда выхода
            LogoutCommand = new RelayCommand(Logout);
        }

        #region Properties

        public string CurrentUserInfo => $"Пользователь: {_currentUser.FullName} ({_currentUser.Role})";

        public ObservableCollection<Product> Products
        {
            get => _products;
            set => SetProperty(ref _products, value);
        }

        public ObservableCollection<ProductionBatch> Batches
        {
            get => _batches;
            set => SetProperty(ref _batches, value);
        }

        public ObservableCollection<Equipment> Equipment
        {
            get => _equipment;
            set => SetProperty(ref _equipment, value);
        }

        public ObservableCollection<Recipe> Recipes
        {
            get => _recipes;
            set => SetProperty(ref _recipes, value);
        }

        public ObservableCollection<Ingredient> AllIngredients
        {
            get => _allIngredients;
            set => SetProperty(ref _allIngredients, value);
        }

        public ObservableCollection<WarehouseItem> WarehouseItems
        {
            get => _warehouseItems;
            set => SetProperty(ref _warehouseItems, value);
        }

        public ObservableCollection<WarehouseTransaction> WarehouseTransactions
        {
            get => _warehouseTransactions;
            set => SetProperty(ref _warehouseTransactions, value);
        }

        public Product SelectedProduct
        {
            get => _selectedProduct;
            set => SetProperty(ref _selectedProduct, value);
        }

        public ProductionBatch SelectedBatch
        {
            get => _selectedBatch;
            set => SetProperty(ref _selectedBatch, value);
        }

        public Equipment SelectedEquipment
        {
            get => _selectedEquipment;
            set => SetProperty(ref _selectedEquipment, value);
        }

        public Recipe SelectedRecipe
        {
            get => _selectedRecipe;
            set => SetProperty(ref _selectedRecipe, value);
        }

        public WarehouseItem SelectedWarehouseItem
        {
            get => _selectedWarehouseItem;
            set => SetProperty(ref _selectedWarehouseItem, value);
        }

        public string WarehouseSearchText
        {
            get => _warehouseSearchText;
            set
            {
                SetProperty(ref _warehouseSearchText, value);
                OnPropertyChanged(nameof(FilteredWarehouseItems));
            }
        }

        public string SelectedWarehouseFilter
        {
            get => _selectedWarehouseFilter;
            set
            {
                SetProperty(ref _selectedWarehouseFilter, value);
                OnPropertyChanged(nameof(FilteredWarehouseItems));
            }
        }

        public ObservableCollection<string> WarehouseFilterOptions
        {
            get => _warehouseFilterOptions;
            set => SetProperty(ref _warehouseFilterOptions, value);
        }

        public Ingredient NewIngredient
        {
            get => _newIngredient;
            set => SetProperty(ref _newIngredient, value);
        }

        public decimal NewIngredientAmount
        {
            get => _newIngredientAmount;
            set => SetProperty(ref _newIngredientAmount, value);
        }

        public IEnumerable<WarehouseItem> FilteredWarehouseItems
        {
            get
            {
                var query = WarehouseItems.AsEnumerable();

                switch (SelectedWarehouseFilter)
                {
                    case "Только в наличии":
                        query = query.Where(i => i.Quantity > 0 && i.ExpiryDate >= DateTime.Today);
                        break;
                    case "Просроченные":
                        query = query.Where(i => i.ExpiryDate < DateTime.Today);
                        break;
                }

                if (!string.IsNullOrWhiteSpace(WarehouseSearchText))
                {
                    query = query.Where(i => i.Product.Name.IndexOf(WarehouseSearchText, StringComparison.OrdinalIgnoreCase) >= 0);
                }

                return query.OrderBy(i => i.Product.Name);
            }
        }

        #endregion

        #region Commands

        public ICommand StartProductionCommand { get; }
        public ICommand CompleteStageCommand { get; }
        public ICommand AddRecipeCommand { get; }
        public ICommand DeleteRecipeCommand { get; }
        public ICommand SaveRecipeCommand { get; }
        public ICommand AddIngredientCommand { get; }
        public ICommand RemoveIngredientCommand { get; }
        public ICommand AddIncomingCommand { get; }
        public ICommand AddOutgoingCommand { get; }
        public ICommand LogoutCommand { get; }

        #endregion

        #region Command Methods

        private void StartProduction(object parameter)
        {
            if (SelectedProduct == null) return;

            var batch = new ProductionBatch
            {
                Id = Batches.Count + 1,
                Product = SelectedProduct,
                StartTime = DateTime.Now
            };

            batch.Stages.Add(new BatchStage
            {
                Stage = new ProductionStage { Id = 1, Name = "Замес теста", Duration = TimeSpan.FromMinutes(30) },
                StartTime = DateTime.Now
            });

            batch.Stages.Add(new BatchStage
            {
                Stage = new ProductionStage { Id = 2, Name = "Формовка", Duration = TimeSpan.FromMinutes(20) },
                StartTime = DateTime.Now.AddMinutes(30)
            });

            batch.Stages.Add(new BatchStage
            {
                Stage = new ProductionStage { Id = 3, Name = "Выпечка", Duration = TimeSpan.FromMinutes(60) },
                StartTime = DateTime.Now.AddMinutes(50)
            });

            Batches.Add(batch);
        }

        private bool CanStartProduction(object parameter) => SelectedProduct != null;

        private void CompleteStage(object parameter)
        {
            if (SelectedBatch == null || !(parameter is BatchStage)) return;

            var stage = parameter as BatchStage;
            stage.EndTime = DateTime.Now;

            var lastStageIndex = SelectedBatch.Stages.Count - 1;
            var currentStageIndex = SelectedBatch.Stages.IndexOf(stage);

            if (currentStageIndex == lastStageIndex)
            {
                SelectedBatch.EndTime = DateTime.Now;
            }
            else if (currentStageIndex < lastStageIndex)
            {
                SelectedBatch.Stages[currentStageIndex + 1].StartTime = DateTime.Now;
            }
        }

        private bool CanCompleteStage(object parameter) =>
            SelectedBatch != null && parameter is BatchStage stage && stage.EndTime == null;

        private void AddRecipe(object parameter)
        {
            var newRecipe = new Recipe
            {
                Id = Recipes.Count + 1,
                Name = "Новый рецепт"
            };
            Recipes.Add(newRecipe);
            SelectedRecipe = newRecipe;
        }

        private void DeleteRecipe(object parameter)
        {
            if (SelectedRecipe == null) return;
            Recipes.Remove(SelectedRecipe);
            SelectedRecipe = null;
        }

        private void SaveRecipe(object parameter)
        {
            MessageBox.Show("Рецепт сохранен", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private bool CanAddIngredient(object parameter) =>
            SelectedRecipe != null && NewIngredient != null && NewIngredientAmount > 0;

        private void AddIngredient(object parameter)
        {
            SelectedRecipe.Ingredients.Add(new RecipeItem
            {
                Ingredient = NewIngredient,
                Amount = NewIngredientAmount,
                Unit = NewIngredient.Unit
            });

            NewIngredient = null;
            NewIngredientAmount = 0;
            OnPropertyChanged(nameof(SelectedRecipe));
        }

        private void RemoveIngredient(object parameter)
        {
            if (parameter is RecipeItem item)
            {
                SelectedRecipe.Ingredients.Remove(item);
                OnPropertyChanged(nameof(SelectedRecipe));
            }
        }

        private void AddIncoming(object parameter)
        {
            var dialog = new WarehouseTransactionWindow(_currentUser, Products, "Поступление");
            if (dialog.ShowDialog() == true)
            {
                var transaction = dialog.Transaction;
                WarehouseTransactions.Add(transaction);

                var existingItem = WarehouseItems.FirstOrDefault(i =>
                    i.Product.Id == transaction.Item.Product.Id &&
                    i.ProductionDate.Date == transaction.Item.ProductionDate.Date);

                if (existingItem != null)
                {
                    existingItem.Quantity += transaction.Quantity;
                }
                else
                {
                    WarehouseItems.Add(new WarehouseItem
                    {
                        Id = WarehouseItems.Count + 1,
                        Product = transaction.Item.Product,
                        Quantity = transaction.Quantity,
                        ProductionDate = transaction.Item.ProductionDate,
                        ExpiryDate = transaction.Item.ExpiryDate,
                        StorageLocation = transaction.Item.StorageLocation
                    });
                }

                OnPropertyChanged(nameof(FilteredWarehouseItems));
            }
        }

        private bool CanAddOutgoing(object parameter) =>
            SelectedWarehouseItem != null && SelectedWarehouseItem.Quantity > 0;

        private void AddOutgoing(object parameter)
        {
            var dialog = new WarehouseTransactionWindow(_currentUser, SelectedWarehouseItem, "Списание");
            if (dialog.ShowDialog() == true)
            {
                var transaction = dialog.Transaction;
                WarehouseTransactions.Add(transaction);

                SelectedWarehouseItem.Quantity -= transaction.Quantity;
                if (SelectedWarehouseItem.Quantity <= 0)
                {
                    WarehouseItems.Remove(SelectedWarehouseItem);
                }

                OnPropertyChanged(nameof(FilteredWarehouseItems));
            }
        }

        private void Logout(object parameter)
        {
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            Application.Current.Windows.OfType<MainWindow>().First().Close();
        }

        #endregion

        #region Initialization

        private void InitializeTestData()
        {
            InitializeProducts();
            InitializeEquipment();
            InitializeRecipes();
            InitializeWarehouseTestData();
        }

        private void InitializeProducts()
        {
            Products.Add(new Product { Id = 1, Name = "Белый хлеб", Weight = 0.5m, ProductionTime = TimeSpan.FromHours(2), Cost = 30 });
            Products.Add(new Product { Id = 2, Name = "Ржаной хлеб", Weight = 0.7m, ProductionTime = TimeSpan.FromHours(3), Cost = 45 });
            Products.Add(new Product { Id = 3, Name = "Багет", Weight = 0.3m, ProductionTime = TimeSpan.FromHours(1.5), Cost = 25 });
        }

        private void InitializeEquipment()
        {
            Equipment.Add(new Equipment { Id = 1, Name = "Печь №1", Type = "Печь", LastMaintenance = DateTime.Now.AddDays(-10) });
            Equipment.Add(new Equipment { Id = 2, Name = "Тестомес", Type = "Миксер", LastMaintenance = DateTime.Now.AddDays(-5) });
            Equipment.Add(new Equipment { Id = 3, Name = "Конвейер", Type = "Транспорт", LastMaintenance = DateTime.Now.AddDays(-2) });
        }

        private void InitializeRecipes()
        {
            AllIngredients.Add(new Ingredient { Id = 1, Name = "Мука пшеничная", Category = "Мука", Unit = "кг", PricePerUnit = 25 });
            AllIngredients.Add(new Ingredient { Id = 2, Name = "Мука ржаная", Category = "Мука", Unit = "кг", PricePerUnit = 30 });
            AllIngredients.Add(new Ingredient { Id = 3, Name = "Дрожжи сухие", Category = "Разрыхлители", Unit = "кг", PricePerUnit = 500 });
            AllIngredients.Add(new Ingredient { Id = 4, Name = "Соль", Category = "Пряности", Unit = "кг", PricePerUnit = 20 });
            AllIngredients.Add(new Ingredient { Id = 5, Name = "Сахар", Category = "Сладости", Unit = "кг", PricePerUnit = 60 });
            AllIngredients.Add(new Ingredient { Id = 6, Name = "Масло подсолнечное", Category = "Жиры", Unit = "л", PricePerUnit = 120 });
            AllIngredients.Add(new Ingredient { Id = 7, Name = "Вода", Category = "Жидкости", Unit = "л", PricePerUnit = 0 });

            var whiteBreadRecipe = new Recipe
            {
                Id = 1,
                Name = "Белый хлеб",
                TargetProduct = Products[0],
                Instructions = "1. Замесить тесто\n2. Дать подойти 1 час\n3. Выпекать 40 мин при 200°C"
            };
            whiteBreadRecipe.Ingredients.Add(new RecipeItem { Ingredient = AllIngredients[0], Amount = 1, Unit = "кг" });
            whiteBreadRecipe.Ingredients.Add(new RecipeItem { Ingredient = AllIngredients[2], Amount = 0.02m, Unit = "кг" });
            whiteBreadRecipe.Ingredients.Add(new RecipeItem { Ingredient = AllIngredients[3], Amount = 0.02m, Unit = "кг" });
            whiteBreadRecipe.Ingredients.Add(new RecipeItem { Ingredient = AllIngredients[6], Amount = 0.6m, Unit = "л" });
            Recipes.Add(whiteBreadRecipe);

            var ryeBreadRecipe = new Recipe
            {
                Id = 2,
                Name = "Ржаной хлеб",
                TargetProduct = Products[1],
                Instructions = "1. Замесить тесто\n2. Дать подойти 1.5 часа\n3. Выпекать 50 мин при 180°C"
            };
            ryeBreadRecipe.Ingredients.Add(new RecipeItem { Ingredient = AllIngredients[1], Amount = 0.7m, Unit = "кг" });
            ryeBreadRecipe.Ingredients.Add(new RecipeItem { Ingredient = AllIngredients[0], Amount = 0.3m, Unit = "кг" });
            ryeBreadRecipe.Ingredients.Add(new RecipeItem { Ingredient = AllIngredients[2], Amount = 0.025m, Unit = "кг" });
            ryeBreadRecipe.Ingredients.Add(new RecipeItem { Ingredient = AllIngredients[6], Amount = 0.7m, Unit = "л" });
            Recipes.Add(ryeBreadRecipe);
        }

        private void InitializeWarehouseTestData()
        {
            var random = new Random();
            for (int i = 1; i <= 20; i++)
            {
                var product = Products[random.Next(Products.Count)];
                var productionDate = DateTime.Today.AddDays(-random.Next(10));

                WarehouseItems.Add(new WarehouseItem
                {
                    Id = i,
                    Product = product,
                    Quantity = random.Next(1, 100),
                    ProductionDate = productionDate,
                    ExpiryDate = productionDate.AddDays(product.Name.Contains("Белый") ? 3 : 5),
                    StorageLocation = $"Стеллаж {random.Next(1, 6)}, Полка {random.Next(1, 4)}"
                });
            }
        }

        #endregion
    }
}