using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {


        public MainViewModel()
        {
        }

        [ObservableProperty]
        private string userInput;

        [RelayCommand]
        public void Login()
        {
            MessageBox.Show(userInput,"dd");
        }

    }
}
