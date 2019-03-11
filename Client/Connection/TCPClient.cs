using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using PacketModel.Connection.EventArgs;

namespace Client.Connection
{
    public class TCPClient
    {
        #region Delegates

        public delegate void PacketReceivedEventHandler(object sender, PacketReceivedEventArgs e);
        public delegate void ConnectedEventHandler(object sender, ClientConnectionChangedEventArgs e);
        public delegate void DisconnectedEventHandler(object sender, EventArgs e);
        public delegate void ConnectionFailedEventHandler(object sender, EventArgs e);

        #endregion Delegates

        #region Events

        public event PacketReceivedEventHandler PacketReceived;
        public event ConnectedEventHandler Connected;
        public event DisconnectedEventHandler Disconnected;
        public event ConnectionFailedEventHandler ConnectionFailed;

        #endregion Events

        #region Event Raiser

        protected void OnPacketReceived(PacketReceivedEventArgs e)
        {
            PacketReceived?.Invoke(this, e);
        }

        protected void OnConnected(ClientConnectionChangedEventArgs e)
        {
            Connected?.Invoke(this, e);
        }

        protected void OnDisconnected(EventArgs e)
        {
            Disconnected?.Invoke(this, e);
        }

        protected void OnConnectionFailed(EventArgs e)
        {
            ConnectionFailed?.Invoke(this, e);
        }

        #endregion Event Raiser

        #region Fields

        /// <summary>
        /// Der zugrundeliegende TCPClient (System.Net.Sockets).
        /// </summary>
        private TcpClient tcpClient;

        /// <summary>
        /// Der Puffer zum Empfangen von Nachrichten.
        /// </summary>
        private byte[] buffer;

        #endregion Fields

        #region Properties

        /// <summary>
        /// gibt zurück, ob der Socket des Clients mit einem Host verbunden ist
        /// </summary>
        public bool IsConnected {
            get { return tcpClient.Connected; }
        }

        public string IPAdrStr {
            get {
                if(tcpClient.Connected){
                    string[] part = tcpClient.Client.LocalEndPoint.ToString().Split(':');
                    return part[0];
                }else{
                    return string.Empty;
                }
            }
        }

        public string PortStr {
            get {
                if(tcpClient.Connected){
                    string[] part = tcpClient.Client.LocalEndPoint.ToString().Split(':');
                    return part[1];
                }else{
                    return string.Empty;
                }
            }
        }

        #endregion Properties

        /// <summary>
        /// Standard Konstruktor
        /// </summary>
        public TCPClient()
        {
            this.tcpClient = new TcpClient();
            // Puffer wird auf maximale Empfangsgröße von Packeten festgelegt.
            this.buffer = new byte[tcpClient.ReceiveBufferSize];
        }

        /// <summary>
        /// Startet einen Verbindungsversuch zum Server.
        /// Asynchron
        /// </summary>
        /// <param name="localaddr"></param>
        /// <param name="port"></param>
        public void Connect(IPAddress localaddr, int port)
        {
            // Wenn der Client verbunden ist, kein erneuter Verbindungsversuch.
            if(tcpClient.Connected)
                return;

            try{
                // Beginnt einen Asynchronen Verbindungsversuch in einem neuen Thread.
                this.tcpClient.BeginConnect(localaddr, port, ConnectCallback, null);

            }catch(Exception ex){
                throw new Exception("Fehler beim Verbinden zum Server.", ex);
            }
        }

        /// <summary>
        /// Trennt die Verbindung zum Server.
        /// Synchron
        /// </summary>
        public void Disconnect()
        {
            // Wenn der Client nicht verbunden ist kann nichts getrennt werden.
            if(!tcpClient.Connected)
                return;

            try{
                //tcpClient.ReceiveTimeout = 10;
                //tcpClient.SendTimeout = 10;
                // Schließt die Verbindung zum Server (Synchron, kein neuer Thread).
                tcpClient.Close();
                //Thread.Sleep(100);
                //Application.DoEvents();
                OnDisconnected(EventArgs.Empty);

            }catch(Exception ex){
                throw new Exception("Fehler beim Schließen der Verbindung zum Server.", ex);
            }
        }

        /// <summary>
        /// Sendet ein Paket (Byte-Array) an den Server.
        /// </summary>
        /// <param name="bytes"></param>
        public void SendPacket(byte[] bytes)
        {
            // Wenn der Client nicht verbunden ist kann nichts gesendet werden.
            if(!tcpClient.Connected)
                return;

            try{
                // Stream holen und Asynchron das Paket in den Stream schreiben (neuer Thread).
                NetworkStream networkStream = tcpClient.GetStream();
                networkStream.BeginWrite(bytes, 0, bytes.Length, SendCallback, null);

            }catch(Exception ex){
                throw new Exception("Fehler beim Senden des Paketes.", ex);
            }
        }

        /// <summary>
        /// Sendet ein String als Byte-Paket an den Server.
        /// </summary>
        /// <param name="bytes"></param>
        public void SendPacket(string strData)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(strData);
            SendPacket(bytes);
        }

        #region Public Callbacks

        /// <summary>
        /// Callback-Methode die nach dem Verbinden zum Server aufgerufen wird.
        /// </summary>
        /// <param name="result"></param>
        private void ConnectCallback(IAsyncResult result)
        {
            try{
                // Beendet den Verbindungsversuch und schreibt benötigte Rückgabewerte in result.
                this.tcpClient.EndConnect(result);
                // Führt das Event Connected aus.
                OnConnected(new ClientConnectionChangedEventArgs(tcpClient));
                // Holt den Stream und beginnt einen asynchronen Lesezyklus (neuer Thread).
                NetworkStream networkStream = tcpClient.GetStream();
                networkStream.BeginRead(this.buffer, 0, buffer.Length, ReadCallback, null);

            }catch(SocketException){
                OnConnectionFailed(EventArgs.Empty);

            }catch(Exception ex){
                throw new Exception("Fehler beim Verbinden zum Server.", ex);
            }
        }

        /// <summary>
        /// Callback-Methode die nach dem Senden einer Nachricht an den Server aufgerufen wird.
        /// </summary>
        /// <param name="result"></param>
        private void SendCallback(IAsyncResult result)
        {
            try{
                // Holt den Stream und beendet den asynchronen Sendevorgang (Nachricht wurde gesendet).
                NetworkStream networkStream = tcpClient.GetStream();
                networkStream.EndWrite(result);

            }catch(Exception ex){
                throw new Exception("Fehler beim Senden des Paketes.", ex);
            }
        }

        /// <summary>
        /// Callback-Methode die aufgerufen wird sobald eine Nachricht vom Server ankommt.
        /// </summary>
        /// <param name="result"></param>
        private void ReadCallback(IAsyncResult result)
        {
            try{
                // Holt den Stream und beendet den asynchronen Lesevorgang (Nachricht empfangen).
                // Anzahl der empfangenen Bytes wird in read geschrieben.
                NetworkStream networkStream = tcpClient.GetStream();
                int read = networkStream.EndRead(result);

                // Kopiert die Anzahl empfangener Bytes aus dem Puffer (Puffer 8kb).
                byte[] actualBytes = new byte[read];
                Array.Copy(buffer, actualBytes, read);

                // PacketReceived Event ausführen.
                OnPacketReceived(new PacketReceivedEventArgs(null, actualBytes, read));
                // Neuen Lesevorgang starten.
                networkStream.BeginRead(buffer, 0, buffer.Length, ReadCallback, null);

            }catch(IOException){
                OnDisconnected(EventArgs.Empty);

            }catch //(Exception ex)
            {
                //throw new Exception("Fehler beim Empfangen eines Paketes.", ex);
            }
        }

        #endregion Public Callbacks
    }
}
