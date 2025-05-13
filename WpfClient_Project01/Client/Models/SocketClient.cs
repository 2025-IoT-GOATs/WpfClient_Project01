using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    public sealed class SocketClient : ObservableObject
    {
        private static readonly Lazy<SocketClient> _instance = new(() => new SocketClient());
        public static SocketClient Instance => _instance.Value;

        private TcpClient _client;
        private NetworkStream _stream;
        private string _chatName;
        public string ChatName
        {
            get => _chatName;
            set => SetProperty(ref _chatName, value);
        }

        public event Action<string> MessageReceived;

        private SocketClient() { } // private 생성자

        public async Task ConnectAsync(string ip, int port)
        {
            _client = new TcpClient();
            await _client.ConnectAsync(ip, port);
            _stream = _client.GetStream();
        }

        public void StartReceiveLoop()
        {
            Task.Run(async () =>
            {
                var buffer = new byte[1024];
                while (true)
                {
                    try
                    {
                        int len = await _stream.ReadAsync(buffer, 0, buffer.Length);
                        if (len == 0) break; // 연결 종료

                        string msg = Encoding.UTF8.GetString(buffer, 0, len);
                        MessageReceived?.Invoke(msg);
                    }
                    catch
                    {
                        break;
                    }
                }
            });
        }

        public async Task SendAsync(string msg)
        {
            if (_stream == null) return;
            byte[] data = Encoding.UTF8.GetBytes(msg);
            await _stream.WriteAsync(data, 0, data.Length);
        }
    }
}
