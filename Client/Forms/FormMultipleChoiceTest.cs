using Client.Connection;
using PacketModel.Connection;
using PacketModel.Connection.EventArguments;
using PacketModel.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Windows.Forms;

namespace Client.Forms
{
    public partial class FormMultipleChoiceTest : Form
    {
        private TCPClient _client;

        private bool _connected = false;

        private static IPAddress _ip = IPAddress.Parse("127.0.0.1");
        private static int _port = 15000;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormMultipleChoiceTest"/> class.
        /// </summary>
        public FormMultipleChoiceTest()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Form Load Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMultipleChoiceTest_Load(object sender, EventArgs e)
        {
            this.CreateClient();
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
            // Create a new TabPage
            var newTabPage = new TabPage() {
                Text = id.ToString(),
                BackColor = Color.White

            };

            // Create a new Label for displaing the question.
            var newLabel = new Label() {
                Text = question,
                Location = new Point(17, 18),
                Font = new Font("Arial", 14.25f, FontStyle.Bold)
            };

            // Create a new GroupBox
            var newGroupBox = new GroupBox() {
                Text = "Antwortmöglichkeiten",
                Location = new Point(20, 109),
                Size = new Size(820, 225),
                Font = new Font("Arial", 12f, FontStyle.Bold)
            };

            // Create RadioButtons for the answers
            int yLocation = 40;
            int xLocation = 30;
            for(int i = 0; i < answers.Count; i++)
            {
                var newAnswers = new RadioButton() {
                    Text = answers[i],
                    Font = new Font("Arial", 12f, FontStyle.Regular),
                    Location = new Point(xLocation, yLocation)
                };
                // Increase position
                yLocation += 40;
                // Add to GroupBox
                newGroupBox.Controls.Add(newAnswers);

                // Check for second column
                if(i != 0 && i % 3 == 0)
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



            //Create a new Button
            //var newButton = new Button() {
            //    Text = "NewButton",
            //    Location = new Point(20, 20)
            //};

            //Assign dynamic handler for the Button's click event
            //newButton.Click += new EventHandler(button_click);

            //Add the Button to the tab page
            //newTabPage.Controls.Add(newButton);
        }

        //public void button_click(object sender, EventArgs e)
        //{
        //    Button buttonClicked = (Button)sender;
        //    MessageBox.Show(String.Format("Button with text: {0} clicked!", buttonClicked.Text));
        //}

        /// <summary>
        /// Packet Received Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPacketReceived(object sender, PacketReceivedEventArgs e)
        {
            PacketHandler h = new PacketHandler();
            h.ProcessPacket(e.Packet);
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
        /// Create a new client
        /// </summary>
        private void CreateClient()
        {
            _client = new TCPClient();
            _client.Connection += OnConnectionChanged;
            _client.PacketReceived += OnPacketReceived;
            _client.Connect(_ip, _port);
        }

        /// <summary>
        /// Send a test message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (!_connected)
                return;

            var v = new DefaultConnectionInfo();
            v.Message = "Hallo du da";
            _client.SendPacket(v);
        }
    }
}