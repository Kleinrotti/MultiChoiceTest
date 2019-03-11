using PacketModel.Translator;
using System;
using System.Net.Sockets;

namespace PacketModel.Connection.EventArguments
{
    public class PacketReceivedEventArgs : EventArgs
    {
        public object Packet { get; set; }

        public TcpClient Sender { get; set; }

        public PacketReceivedEventArgs(TcpClient sender, byte[] buffer, int length)
        {
            this.Sender = sender;
            Packet = PacketSerializer.Deserialize(buffer);
        }
    }
}