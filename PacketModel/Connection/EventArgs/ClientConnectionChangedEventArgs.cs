using System;
using System.Net.Sockets;

namespace PacketModel.Connection.EventArgs
{
    public class ClientConnectionChangedEventArgs : Exception
    {
        public TcpClient tcpClient { get; set; }

        public ClientConnectionChangedEventArgs(TcpClient client)
        {
            this.tcpClient = client;
        }
    }
}