using Client.Connection;
using Client.Handler;
using Client.Helpers;
using PacketModel.Connection.EventArguments;
using PacketModel.Enums;
using PacketModel.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace Client.Forms
{
    public partial class FormExamSelection : Form
    {
        private IPAddress _ip;
        private static int _port = 15000;

        private bool _testStarted = false;
        private bool _isconnected = false;
        private TCPClient _client;
        private FormMultipleChoiceTest _formchoicetest;

        public FormExamSelection()
        {
            InitializeComponent();

            btnStartTest.BackColor = Color.FromArgb(158, 158, 158);
        }

        private void FormExamSelection_Load(object sender, EventArgs e)
        {
            _client = new TCPClient();
            _client.Connection += OnConnectionChanged;
            _client.PacketReceived += OnPacketReceived;
            ConnectToServer();
            this.txtUsername.Focus();
        }

        private void ConnectToServer()
        {
            var ip = IpInputHelper.IpDialog();
            if (ip != string.Empty)
            {
                _ip = IPAddress.Parse(ip);
                _client.Connect(_ip, _port);
                if (!GetExamList())
                {
                    MessageBox.Show("Connection to server failed");
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid ip address");
            }
        }

        private bool GetExamList()
        {
            var v = new DefaultMessage(Command.SendExamList);
            //wait three connection attempts
            for (int i = 0; i < 3; i++)
            {
                if (_client.SendPacket(v))
                    return true;
                i++;
                Thread.Sleep(200);
            }
            return false;
        }

        public delegate void ExecuteTask(object o);

        /// <summary>
        /// Fill combobox with available exams.
        /// </summary>
        /// <param name="examnames"></param>
        private void FillComboBox(object examnames)
        {
            var ad = examnames as List<string>;
            Invoke(new Action(() =>
            {
                comboExamSelection.Items.Clear();
                foreach (var v in ad)
                {
                    comboExamSelection.Items.Add(v);
                }

                comboExamSelection.SelectedIndex = -1;
            }));
        }

        private void GetExercises(object exercises)
        {
            var ex = exercises as List<DefaultExercise>;
            if (ex != null)
            {
                _client.Connection -= OnConnectionChanged;
                _client.PacketReceived -= OnPacketReceived;
                Invoke(new Action(() =>
                {
                    Hide();
                    _formchoicetest = new FormMultipleChoiceTest(_client, ex);
                    _formchoicetest.NewTest += OnNewTest;
                    _formchoicetest.Show();
                }));
            }
        }

        private void OnNewTest(object sender, EventArgs e)
        {
            _formchoicetest.Dispose();
            _client.Connection += OnConnectionChanged;
            _client.PacketReceived += OnPacketReceived;
            GetExamList();
            Show();
        }

        /// <summary>
        /// On Connection Changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnConnectionChanged(object sender, ClientConnectionChangedEventArgs e)
        {
            _isconnected = e.IsConnected;
        }

        /// <summary>
        /// On Packet Received.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPacketReceived(object sender, PacketReceivedEventArgs e)
        {
            PacketHandler h = new PacketHandler();
            ExecuteTask del = new ExecuteTask(FillComboBox);
            del += GetExercises;
            h.ProcessPacket(e, del);
        }

        /// <summary>
        /// When the input changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Input_TextChanged(object sender, System.EventArgs e)
        {
            // Input completed
            if (txtUsername.Text != String.Empty && comboExamSelection.Text != String.Empty)
            {
                btnStartTest.BackColor = Color.FromArgb(76, 175, 80);
                _testStarted = true;
            }
            // Input incompleted
            else
            {
                btnStartTest.BackColor = Color.FromArgb(158, 158, 158);
                _testStarted = false;
            }
        }

        /// <summary>
        /// Start Exame
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartTest_Click(object sender, EventArgs e)
        {
            if (_testStarted)
            {
                String UserName = txtUsername.Text;
                String ExameStartedAt = DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss");
                String SelectedExame = comboExamSelection.Text;

                DefaultMessage m = new DefaultMessage(Command.SendExercises);
                m.MessageString = SelectedExame;
                _client.SendPacket(m);

                m = new DefaultMessage(Command.SetUserName);
                m.MessageString = UserName;
                _client.SendPacket(m);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void changeServerIPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConnectToServer();
        }
    }
}