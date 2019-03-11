using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.Connection
{
    class Client
    {
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

        public void ClearBuffer()
        {
            Buffer = new byte[Tcpclient.ReceiveBufferSize];
        }
    }
}
