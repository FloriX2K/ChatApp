using ChatApp.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ChatApp
{
    /// <summary>
    /// App.xaml etkileşim mantığı
    /// </summary>
    public partial class App : Application
    {
        private static MainViewModel _mainViewModel;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _mainViewModel = new MainViewModel();

            MainWindow mainWindow = new MainWindow();
            mainWindow.DataContext = _mainViewModel;
            mainWindow.Show();
        }
    }
}
