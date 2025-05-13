using Client.Models;
using Client.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MahApps.Metro.Controls.Dialogs;
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
        private readonly IDialogCoordinator _idialogcoordinator;

        [ObservableProperty]
        private ObservableObject currentViewModel;

        [ObservableProperty]
        private UserControl currentView;

        [ObservableProperty]
        private string status;

        public MainViewModel(IDialogCoordinator idialogcoordinator)
        {
            this._idialogcoordinator = idialogcoordinator;
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
        public async void Exit()
        {
            var result = await this._idialogcoordinator.ShowMessageAsync(this, "종료", "종료하시겠습니까?", MessageDialogStyle.AffirmativeAndNegative);
            if (result == MessageDialogResult.Affirmative)
            {
                Application.Current.Shutdown();
            }
            else
            {

            }
        }

    }
}
