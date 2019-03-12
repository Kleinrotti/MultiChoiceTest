using Client.Connection;
using PacketModel.Connection;
using PacketModel.Connection.EventArguments;
using PacketModel.Enums;
using PacketModel.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Windows.Forms;

namespace Client.Forms
{
    public partial class FormExamSelection : Form
    {
        public bool testStarted = false;
        private static IPAddress _ip = IPAddress.Parse("127.0.0.1");
        private static int _port = 15000;
        private TCPClient _client;
        private bool _isconnected = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormExamSelection"/> class.
        /// </summary>
        public FormExamSelection()
        {
            InitializeComponent();
            btnStartTest.BackColor = Color.FromArgb(158, 158, 158);
            _client = new TCPClient();
            _client.Connection += OnConnectionChanged;
            _client.PacketReceived += OnPacketReceived;
            _client.Connect(_ip, _port);
        }

        public delegate void ExecuteTask(object o);

        private void FillComboBox(object examnames)
        {
            var ad = examnames as List<string>;
            Invoke(new Action(() =>
            {
                foreach (var v in ad)
                {
                    comboExamSelection.Items.Add(v);
                }

                comboExamSelection.SelectedIndex = 0;
            }));
        }

        private void GetExercises(object exercises)
        {
            var ex = exercises as List<DefaultExercise>;
            if (ex != null)
            {
                Invoke(new Action(() =>
                {
                    Hide();
                    FormMultipleChoiceTest test = new FormMultipleChoiceTest(_client, ex);
                    test.ShowDialog();
                    Close();
                }));
            }
        }

        private void FormExamSelection_Load(object sender, EventArgs e)
        {
            var v = new DefaultMessage(HandlerOperator.Server, Command.SendExamList);
            _client.SendPacket(v);
        }

        private void OnConnectionChanged(object sender, ClientConnectionChangedEventArgs e)
        {
            _isconnected = e.IsConnected;
        }

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
            if (txtUsername.Text != String.Empty /*&& comboExamSelection.Text != String.Empty*/)
            {
                btnStartTest.BackColor = Color.FromArgb(76, 175, 80);
                testStarted = true;
            }
            // Input incompleted
            else
            {
                btnStartTest.BackColor = Color.FromArgb(158, 158, 158);
                testStarted = false;
            }
        }

        /// <summary>
        /// Start Exame
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartTest_Click(object sender, EventArgs e)
        {
            if (testStarted)
            {
                String UserName = txtUsername.Text;
                String ExameStartedAt = DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss");
                String SelectedExame = comboExamSelection.Text;
                DefaultMessage m = new DefaultMessage(HandlerOperator.Server, Command.SendExercises);
                m.MessageString = SelectedExame;
                _client.SendPacket(m);
            }
        }
    }
}