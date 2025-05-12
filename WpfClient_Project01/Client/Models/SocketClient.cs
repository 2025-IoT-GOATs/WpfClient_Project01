using CommunityToolkit.Mvvm.ComponentModel;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    public class SocketClient : ObservableObject
    {
        private TcpClient _client;
        private NetworkStream _stream;

        public event Action<string> MessageReceived;

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
                        if (len == 0) break; // 연결 끊김

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
