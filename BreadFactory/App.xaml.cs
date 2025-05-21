using BreadFactory.Data;
using BreadFactory.Data.Initializer;
using BreadFactory.Views;
using System.Data.Entity;
using System.Windows;

namespace BreadFactory
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Инициализация БД
            Database.SetInitializer(new DatabaseInitializer());
            using (var context = new BreadFactoryContext())
            {
                context.Database.Initialize(true);
            }

            // Запуск окна входа
            var loginWindow = new LoginWindow();
            loginWindow.Show();
        }
    }
}