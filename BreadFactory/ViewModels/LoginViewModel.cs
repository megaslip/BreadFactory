using BreadFactory.Models;
using BreadFactory.Repositories;
using BreadFactory.Views;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace BreadFactory.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly UserRepository _userRepo = new UserRepository();

        private string _username;
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(Login, CanLogin);
        }

        private bool CanLogin(object parameter)
        {
            return !string.IsNullOrWhiteSpace(Username) &&
                   !string.IsNullOrWhiteSpace(Password);
        }

        private void Login(object parameter)
        {
            ErrorMessage = string.Empty;
            var user = _userRepo.Authenticate(Username, Password);

            if (user != null)
            {
                var mainWindow = new MainWindow();
                mainWindow.DataContext = new MainViewModel(user);
                mainWindow.Show();

                Application.Current.Windows.OfType<LoginWindow>().FirstOrDefault()?.Close();
            }
            else
            {
                ErrorMessage = "Неверные учетные данные";
            }
        }
    }
}