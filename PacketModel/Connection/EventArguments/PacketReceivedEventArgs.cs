using PacketModel.Translator;
using System;
using System.Net.Sockets;

namespace PacketModel.Connection.EventArguments
{
    public class PacketReceivedEventArgs : EventArgs
    {
        public object Packet { get; set; }

        public TcpClient Sender { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PacketReceivedEventArgs"/> class.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="buffer"></param>
        /// <param name="length"></param>
        public PacketReceivedEventArgs(TcpClient sender, byte[] buffer, int length)
        {
            this.Sender = sender;
            Packet = PacketSerializer.Deserialize(buffer);
        }
    }
}