using System.Windows;
using WPFLogin.ViewModel;

namespace WPFLogin
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var loginView = new LoginView();
            loginView.DataContext = new LoginViewModel();
            loginView.Show();
        } 
    }
}
