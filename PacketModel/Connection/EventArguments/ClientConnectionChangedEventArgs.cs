using System;
using System.Net;
using System.Net.Sockets;

namespace PacketModel.Connection.EventArguments
{
    public class ClientConnectionChangedEventArgs : EventArgs
    {
        public bool IsConnected { get; set; }
        public int ReceivedBufferSize { get; set; }
        public string IP { get; set; }
        public TcpClient Client { get; set; }

        public ClientConnectionChangedEventArgs(bool connnected)
        {
            IsConnected = connnected;
        }

        public ClientConnectionChangedEventArgs(TcpClient client)
        {
            IsConnected = client.Connected;
            ReceivedBufferSize = client.ReceiveBufferSize;
            Client = client;
            IP = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
        }

        public ClientConnectionChangedEventArgs(IPAddress address, bool connected)
        {
            IsConnected = connected;
            IP = address.ToString();
        }
    }
}