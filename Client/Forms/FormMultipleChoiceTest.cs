using Client.Connection;
using Client.Handler;
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
        private ExecuteTask _del;

        public event EventHandler NewTest;

        private delegate void ExecuteTask(object obj);

        /// <summary>
        /// Initializes a new instance of the <see cref="FormMultipleChoiceTest"/> class.
        /// </summary>
        public FormMultipleChoiceTest(TCPClient tcpclient, List<DefaultExercise> exercises)
        {
            InitializeComponent();
            _client = tcpclient;
            _connected = _client.IsConnected;
            _client.Connection += OnConnectionChanged;
            _client.PacketReceived += OnPacketReceived;
            _exercises = exercises;
        }

        private void FormMultipleChoiceTest_Load(object sender, EventArgs e)
        {
            // Display Exercises
            for (int i = 0; i < _exercises.Count; i++)
            {
                this.CreateTabForExercise(i + 1, _exercises[i].Question, _exercises[i].Answers);
            }
        }

        #region Event Raiser

        private void OnNewTest(object sender, EventArgs e)
        {
            NewTest?.Invoke(this, e);
        }

        /// <summary>
        /// Packet Received Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPacketReceived(object sender, PacketReceivedEventArgs e)
        {
            Console.WriteLine("bruh");
            PacketHandler h = new PacketHandler();
            _del = new ExecuteTask(ShowExamResult);
            h.ProcessPacket(e, _del);
        }

        /// <summary>
        /// Connection state has changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnConnectionChanged(object sender, ClientConnectionChangedEventArgs e)
        {
            if (_connected == true && e.IsConnected == false)
            {
                MessageBox.Show("Connection lost");
                Invoke(new Action(() =>
                {
                    tabControlExam.Enabled = false;
                    btnSendResults.Enabled = false;
                    button_selectnewtest.Visible = false;
                }));
            }
            _connected = e.IsConnected;
        }

        #endregion Event Raiser

        private void ShowExamResult(object obj)
        {
            MessageBox.Show((string)obj);
            Invoke(new Action((() =>
            {
                button_selectnewtest.Visible = true;
                tabControlExam.Enabled = false;
                btnCancel.Visible = false;
                btnSendResults.Visible = false;
            })));
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

            if (id < 10)
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

            newLabel.AutoSize = true;

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
                //Console.WriteLine(answers[i]);
                var newAnswers = new RadioButton()
                {
                    Text = answers[i],
                    Name = i.ToString(),
                    Font = new Font("Arial", 12f, FontStyle.Regular),
                    Location = new Point(xLocation, yLocation)
                };

                newAnswers.AutoSize = true;

                // Increase position
                yLocation += 40;

                // Add to GroupBox
                newGroupBox.Controls.Add(newAnswers);

                // Check for second column
                //if (i != 0 && i % 3 == 0)
                //{
                //    yLocation = 40;
                //    xLocation = 410;
                //}
            }

            // Add Controls to the new TabPages
            newTabPage.Controls.Add(newLabel);
            newTabPage.Controls.Add(newGroupBox);

            //Add the generated TabPage to the TabControl
            tabControlExam.TabPages.Add(newTabPage);
        }

        /// <summary>
        /// Button Send Result
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSendResults_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Sind Sie sicher, dass Sie den Test beenden möchten?", "", MessageBoxButtons.OKCancel);

            if (result == DialogResult.OK)
            {
                DefaultAnswer answer;
                List<DefaultAnswer> answers = new List<DefaultAnswer>();

                foreach (TabPage exercise in tabControlExam.TabPages)
                {
                    foreach (Control c in exercise.Controls)
                    {
                        if (c.GetType() == typeof(GroupBox))
                        {
                            foreach (RadioButton r in c.Controls)
                            {
                                if (r.Checked == true)
                                {
                                    answer = new DefaultAnswer()
                                    {
                                        ID = Convert.ToInt32(exercise.Name) - 1,
                                        ResultIndex = Convert.ToInt32(r.Name)
                                    };
                                    answers.Add(answer);
                                }
                            }
                        }
                    }
                }
                _client.SendPacket(answers);
            }
        }

        private void FormMultipleChoiceTest_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void button_selectnewtest_Click(object sender, EventArgs e)
        {
            _client.Connection -= OnConnectionChanged;
            _client.PacketReceived -= OnPacketReceived;
            OnNewTest(this, new EventArgs());
        }
    }
}