using System;
using System.Net;
using System.Net.Sockets;

namespace Server.Connection
{
    internal class Client
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        /// <param name="tcpClient"></param>
        /// <param name="buffer"></param>
        public Client(TcpClient tcpClient, byte[] buffer)
        {
            if (tcpClient == null) throw new ArgumentNullException("tcpClient");
            if (buffer == null) throw new ArgumentNullException("buffer");
            this.Tcpclient = tcpClient;
            this.Buffer = buffer;
        }

        public TcpClient Tcpclient { get; private set; }

        public byte[] Buffer { get; private set; }

        public NetworkStream Networkstream { get { return Tcpclient.GetStream(); } }
        public IPAddress Ip { get => ((IPEndPoint)Tcpclient.Client.RemoteEndPoint).Address; }

        /// <summary>
        /// Clear Client Buffer.
        /// </summary>
        public void ClearBuffer()
        {
            Buffer = new byte[Tcpclient.ReceiveBufferSize];
        }
    }
}