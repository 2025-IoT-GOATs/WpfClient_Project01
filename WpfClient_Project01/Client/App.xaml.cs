using Client.ViewModels;
using Client.Views;
using MahApps.Metro.Controls.Dialogs;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var id = DialogCoordinator.Instance;

            var viewModel = new MainViewModel(id);
            var view = new MainView();
            view.DataContext = viewModel;
            view.ShowDialog();
            Common.LOGGER.Info("[Application_Startup] : Sucess");
        }
    }

}
