using BreadFactory.Models;
using BreadFactory.Views;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace BreadFactory.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly Func<string> _getPassword;
        private string _username;
        private string _errorMessage;
        private ObservableCollection<User> _users = new ObservableCollection<User>();

        public LoginViewModel(Func<string> getPassword)
        {
            _getPassword = getPassword;
            InitializeTestUsers();
            LoginCommand = new RelayCommand(Login);
        }

        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public ICommand LoginCommand { get; }

        private void InitializeTestUsers()
        {
            _users.Add(new User { Id = 1, Username = "admin", Password = "admin123", Role = UserRoles.Admin, FullName = "Иванов А.А." });
            _users.Add(new User { Id = 2, Username = "tech", Password = "tech123", Role = UserRoles.Technologist, FullName = "Петрова С.И." });
            _users.Add(new User { Id = 3, Username = "oper", Password = "oper123", Role = UserRoles.Operator, FullName = "Сидоров В.М." });
        }

        private void Login(object parameter)
        {
            ErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(Username))
            {
                ErrorMessage = "Введите имя пользователя";
                return;
            }

            var password = _getPassword();
            if (string.IsNullOrWhiteSpace(password))
            {
                ErrorMessage = "Введите пароль";
                return;
            }

            var user = _users.FirstOrDefault(u =>
                u.Username.Equals(Username, StringComparison.OrdinalIgnoreCase) &&
                u.Password == password);

            if (user == null)
            {
                ErrorMessage = "Неверное имя пользователя или пароль";
                return;
            }

            var mainWindow = new MainWindow();
            var mainViewModel = new MainViewModel(user);
            mainWindow.DataContext = mainViewModel;

            mainWindow.Show();
            Application.Current.Windows.OfType<LoginWindow>().First().Close();
        }
    }
}