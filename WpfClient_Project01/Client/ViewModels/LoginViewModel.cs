using Client.Models;
using Client.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MySqlX.XDevAPI;
using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly SocketClient _client = SocketClient.Instance;

        [ObservableProperty]
        private string userInput;
        public ObservableCollection<string> ErrorLog { get; } = new();

        public LoginViewModel()
        {
            // Debug.WriteLine("LoginViewModel Messenger Hash: " + WeakReferenceMessenger.Default.GetHashCode());
            WeakReferenceMessenger.Default.Send(new InitializeMessengerMessage());
        }

        private void ChangeToChatView()
        {
            var chatVM = new ChatViewModel();
            var chatView = new ChatView { DataContext = chatVM };

            WeakReferenceMessenger.Default.Send(new ViewTransitionMessage(chatVM, chatView));
        }

        [RelayCommand]
        public async Task Connect()
        {
            if (UserInput == null) { ErrorLog.Add("닉네임을 입력하십시오."); return; }
            await _client.ConnectAsync("127.0.0.1", 9000);
            _client.StartReceiveLoop();
            _client.ChatName = UserInput;
            await _client.SendAsync("CHAT " + UserInput + " \n");
            ChangeToChatView();
        }


    }
}
