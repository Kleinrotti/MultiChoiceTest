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

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientConnectionChangedEventArgs"/> class.
        /// </summary>
        /// <param name="connnected"></param>
        public ClientConnectionChangedEventArgs(bool connnected)
        {
            IsConnected = connnected;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientConnectionChangedEventArgs"/> class.
        /// </summary>
        /// <param name="client"></param>
        public ClientConnectionChangedEventArgs(TcpClient client)
        {
            IsConnected = client.Connected;
            ReceivedBufferSize = client.ReceiveBufferSize;
            Client = client;
            //IP = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientConnectionChangedEventArgs"/> class.
        /// </summary>
        /// <param name="address"></param>
        /// <param name="connected"></param>
        public ClientConnectionChangedEventArgs(IPAddress address, bool connected)
        {
            IsConnected = connected;
            IP = address.ToString();
        }
    }
}