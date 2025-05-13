using Client.Models;
using Client.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Client.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableObject currentViewModel;

        [ObservableProperty]
        private UserControl currentView;

        [ObservableProperty]
        private string status;

        public MainViewModel()
        {
            WeakReferenceMessenger.Default.Register<InitializeMessengerMessage>(this, (r, m) =>
            {
                RegisterMessengerHandlers();
            });

            var vm = new LoginViewModel();
            var view = new LoginView { DataContext = vm };
            CurrentViewModel = vm;
            CurrentView = view;

            Status = "Test Version 0.2";
        }
        private void RegisterMessengerHandlers()
        {
            WeakReferenceMessenger.Default.Register<ViewTransitionMessage>(this, (r, m) =>
            {
                CurrentViewModel = m.ViewModel;
                CurrentView = m.View;
                CurrentView.DataContext = m.ViewModel;
            });
        }

        [RelayCommand]
        public async void ExitCommand()
        {

        }

    }
}
