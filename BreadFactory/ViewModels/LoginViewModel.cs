using BreadFactory.Repositories;
using BreadFactory.Views;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace BreadFactory.ViewModels
{
    public class LoginViewModel
    {
        private readonly IUserRepository _userRepository;

        public string Username { get; set; }
        public string Password { get; set; }

        public ICommand LoginCommand { get; }

        public LoginViewModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            LoginCommand = new RelayCommand(_ => Login());
        }

        private void Login()
        {
            var user = _userRepository.GetUser(Username, Password);
            if (user != null)
            {
                var mainWindow = new MainWindow();
                mainWindow.DataContext = new MainViewModel(_userRepository);
                mainWindow.Show();

                // Закрываем текущее окно
                Application.Current.Windows.OfType<LoginWindow>().FirstOrDefault()?.Close();
            }
            else
            {
                MessageBox.Show("Неверные учетные данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}