using Client.Connection;
using PacketModel.Connection;
using PacketModel.Connection.EventArguments;
using PacketModel.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Client.Forms
{
    public partial class FormMultipleChoiceTest : Form
    {
        private TCPClient _client;

        private bool _connected = false;
        private List<DefaultExercise> _exercises;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormMultipleChoiceTest"/> class.
        /// </summary>
        public FormMultipleChoiceTest(TCPClient tcpclient, List<DefaultExercise> exercises)
        {
            InitializeComponent();
            _client = tcpclient;
            _exercises = exercises;
        }

        /// <summary>
        /// Form Load Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMultipleChoiceTest_Load(object sender, EventArgs e)
        {
            //this.CreateClient();
            _connected = true;

            this.CreateTabForExercise(1, "Test Aufgabe.", new List<String> { "test", "tets2", "test", "tets2", "test", "tets2" });
        }

        /// <summary>
        /// Generate a TabPage for one Exercise
        /// </summary>
        /// <param name="id"></param>
        /// <param name="question"></param>
        /// <param name="answers"></param>
        private void CreateTabForExercise(Int32 id, String question, List<String> answers)
        {
            string tabPageText = "";

            if(id < 10)
                tabPageText = "#0" + id.ToString();
            else
                tabPageText = "#" + id.ToString();

            // Create a new TabPage
            var newTabPage = new TabPage()
            {
                Text = tabPageText,
                BackColor = Color.White,
                Name = id.ToString()
            };

            // Create a new Label for displaing the question.
            var newLabel = new Label()
            {
                Text = question,
                Location = new Point(17, 18),
                Font = new Font("Arial", 14.25f, FontStyle.Bold)
            };

            // Create a new GroupBox
            var newGroupBox = new GroupBox()
            {
                Text = "Antwortmöglichkeiten",
                Location = new Point(20, 109),
                Size = new Size(820, 225),
                Font = new Font("Arial", 12f, FontStyle.Bold)
            };

            // Create RadioButtons for the answers
            int yLocation = 40;
            int xLocation = 30;
            for (int i = 0; i < answers.Count; i++)
            {
                var newAnswers = new RadioButton()
                {
                    Text = answers[i],
                    Font = new Font("Arial", 12f, FontStyle.Regular),
                    Location = new Point(xLocation, yLocation)
                };
                // Increase position
                yLocation += 40;
                // Add to GroupBox
                newGroupBox.Controls.Add(newAnswers);

                // Check for second column
                if (i != 0 && i % 3 == 0)
                {
                    yLocation = 40;
                    xLocation = 410;
                }
            }

            // Add Controls to the new TabPages
            newTabPage.Controls.Add(newLabel);
            newTabPage.Controls.Add(newGroupBox);

            //Add the generated TabPage to the TabControl
            tabControlExam.TabPages.Add(newTabPage);

        }

        /// <summary>
        /// Packet Received Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPacketReceived(object sender, PacketReceivedEventArgs e)
        {
            PacketHandler h = new PacketHandler();
            h.ProcessPacket(e, null);
        }

        /// <summary>
        /// Connection state has changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnConnectionChanged(object sender, ClientConnectionChangedEventArgs e)
        {
            Invoke(new Action(() =>
            {
                label_status.Text = "Verbunden: " + e.IsConnected.ToString();
            }));
        }

        /// <summary>
        /// Send a test message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            //    if (!_connected)
            //        return;

            //    var v = new DefaultConnectionInfo();
            //    v.Message = "Hallo du da";
            //    _client.SendPacket(v);
        }
    }
}