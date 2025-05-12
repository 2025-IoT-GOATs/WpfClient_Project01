using Client.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly SocketClient _client = new();
        private string _chatName;

        [ObservableProperty]
        private string userInput;

        public ObservableCollection<string> Logs { get; } = new();

        public MainViewModel()
        {
            _client.MessageReceived += OnMessageReceived;
        }

        private void OnMessageReceived(string msg)
        {
            App.Current.Dispatcher.Invoke(() => Logs.Add("[수신] " + msg));
        }

        [RelayCommand]
        public async Task ConnectAsync()
        {
            await _client.ConnectAsync("127.0.0.1", 9000);
            _client.StartReceiveLoop();
            _chatName = UserInput;
            await _client.SendAsync("CHAT " + UserInput + " \n");
            Logs.Add("[시스템] 서버 연결됨");
        }

        [RelayCommand]
        public async Task SendAsync()
        {
            await _client.SendAsync("CHAT "+_chatName +" "+ UserInput+" \n");
            Logs.Add("[송신] " + UserInput);
            UserInput = string.Empty;
        }

    }
}
