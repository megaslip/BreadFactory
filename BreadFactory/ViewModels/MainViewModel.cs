using BreadFactory.Data;
using BreadFactory.Data.Models;
using BreadFactory.Models;
using BreadFactory.Repositories;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Input;

namespace BreadFactory.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly IUserRepository _userRepository;
        private readonly BreadFactoryContext _db;
        private Recipe _selectedRecipe;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Recipe> Recipes { get; set; }
        public Recipe SelectedRecipe
        {
            get => _selectedRecipe;
            set
            {
                _selectedRecipe = value;
                OnPropertyChanged(nameof(SelectedRecipe));
            }
        }

        // Конструктор без параметров
        public MainViewModel() : this(new UserRepository(new BreadFactoryContext()))
        {
        }

        // Основной конструктор
        public MainViewModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _db = new BreadFactoryContext();
            _db.Database.Initialize(true);

            LoadRecipes();

            AddRecipeCommand = new RelayCommand(_ => AddRecipe());
            DeleteRecipeCommand = new RelayCommand(DeleteRecipe);
        }

        public ICommand AddRecipeCommand { get; }
        public ICommand DeleteRecipeCommand { get; }

        private void LoadRecipes()
        {
            _db.Recipes.Load();
            Recipes = new ObservableCollection<Recipe>(_db.Recipes.Local);
            OnPropertyChanged(nameof(Recipes));
        }

        private void AddRecipe()
        {
            var newRecipe = new Recipe
            {
                Name = "Новый рецепт",
                TargetProduct = "Новый продукт",
                Instructions = "Инструкции...",
                Duration = TimeSpan.Zero
            };

            _db.Recipes.Add(newRecipe);
            _db.SaveChanges();
            Recipes.Add(newRecipe);
        }

        private void DeleteRecipe(object parameter)
        {
            if (parameter is Recipe recipe)
            {
                _db.Recipes.Remove(recipe);
                _db.SaveChanges();
                Recipes.Remove(recipe);
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}