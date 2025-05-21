using BreadFactory.Repositories;
using BreadFactory.ViewModels;
using System.Windows;

namespace BreadFactory.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();

            // Создаем экземпляр контекста и репозитория
            var context = new Data.BreadFactoryContext();
            var userRepository = new Repositories.UserRepository(context);

            // Передаем репозиторий в ViewModel
            DataContext = new LoginViewModel(userRepository);
        }
    }
}