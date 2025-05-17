using BreadFactory.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace BreadFactory.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            DataContext = new LoginViewModel(() => PasswordBox.Password);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // 1. Получаем ссылку на нажатую кнопку
            Button button = sender as Button;

/*            // 2. Пример простого действия
            MessageBox.Show("Успешный вход!", "Уведомление",
                           MessageBoxButton.OK, MessageBoxImage.Information);

            // 3. Дополнительные действия можно добавить здесь
            // Например, изменение свойств окна или других элементов
            button.Content = "Успешный вход!";
            button.IsEnabled = false;
*/
/*            // 4. Или вызов методов ViewModel, если используется MVVM
            if (DataContext is MainViewModel vm)
            {
                vm.SomeMethod();
            }*/
        }
    }
}