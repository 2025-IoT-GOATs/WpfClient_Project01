using Client.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MySqlX.XDevAPI;
using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Interop;

namespace Client.ViewModels
{
    public partial class ChatViewModel : ObservableObject
    {
        private readonly SocketClient _client = SocketClient.Instance;
        public ObservableCollection<string> Logs { get; } = new();
        public IRelayCommand<KeyEventArgs> SendMoveCommand { get; }
        [ObservableProperty]
        private string userInput;

        public ChatViewModel()
        {
            SendMoveCommand = new RelayCommand<KeyEventArgs>(async (e) => await SendMove(e));
            _client.MessageReceived += OnMessageReceived;
        }
        private void OnMessageReceived(string msg)
        {
            msg = msg.Substring(0,msg.Length - 1);
            if (msg == "CHAT OK") { 
                App.Current.Dispatcher.Invoke(() => Logs.Add("[시스템] 채팅방 연결 완료."));
            }
            else
            {
                msg = msg.Substring(5, msg.Length - 5);
                App.Current.Dispatcher.Invoke(() => Logs.Add(msg));
                Common.LOGGER.Info($"[CHAT] {msg}");
            }
        }
        public async Task SendMove(KeyEventArgs KeyInput)
        {
            string dir = "";
            if (KeyInput != null)
            {
                switch (KeyInput.Key)
                {
                    case Key.Left:
                        dir = "LEFT";
                        break;
                    case Key.Right:
                        dir = "RIGHT";
                        break;
                    case Key.Up:
                        dir = "UP";
                        break;
                    case Key.Down:
                        dir = "DOWN";
                        break;
                    default:
                        return; // 방향키 아니면 무시
                }
            }
            await _client.SendAsync("MOVE " + dir + "\n");
        }
        [RelayCommand]
        public async Task SendAsync()
        {
            await _client.SendAsync("CHAT " + _client.ChatName + " " + UserInput + "\n");
            UserInput = string.Empty;
        }
    }
}
