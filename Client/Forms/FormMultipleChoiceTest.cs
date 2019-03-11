using Client.Connection;
using PacketModel.Connection;
using PacketModel.Connection.EventArguments;
using PacketModel.Models;
using System;
using System.Net;
using System.Windows.Forms;

namespace Client.Forms
{
    public partial class FormMultipleChoiceTest : Form
    {
        public FormMultipleChoiceTest()
        {
            InitializeComponent();
        }

        private TCPClient _client;
        private IPAddress _ip = IPAddress.Parse("127.0.0.1");

        private void FormMultipleChoiceTest_Load(object sender, EventArgs e)
        {
            CreateClient();
            _connected = true;
        }

        private void OnPacketReceived(object sender, PacketReceivedEventArgs e)
        {
            PacketHandler h = new PacketHandler();
            h.ProcessPacket(e.Packet);
        }

        private void OnConnectionChanged(object sender, ClientConnectionChangedEventArgs e)
        {
            Invoke(new Action(() =>
            {
                label_status.Text = "Verbunden: " + e.IsConnected.ToString();
            }));
        }

        private bool _connected = false;

        private void button_connect_Click(object sender, EventArgs e)
        {
            if (_connected)
            {
                _connected = false;
                _client.Disconnect();
                _client.Connection -= OnConnectionChanged;
                _client.PacketReceived -= OnPacketReceived;
            }
            else
            {
                _connected = true;
                CreateClient();
            }
        }

        private void CreateClient()
        {
            _client = new TCPClient();
            _client.Connection += OnConnectionChanged;
            _client.PacketReceived += OnPacketReceived;
            _client.Connect(_ip, 15000);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!_connected) return;
            var v = new DefaultConnectionInfo();
            v.Message = "Hallo du da";
            _client.SendPacket(v);
        }
    }
}