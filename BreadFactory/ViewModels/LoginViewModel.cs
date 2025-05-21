using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BreadFactory.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string _login;
        private string _password;

        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        // Простая проверка аутентификации (замените на реальную логику!)
        public bool Authenticate()
        {
            return !string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Password);
            // Пример: Login == "admin" && Password == "12345"
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}