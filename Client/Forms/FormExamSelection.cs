using System;
using System.Drawing;
using System.Windows.Forms;

namespace Client.Forms
{
    public partial class FormExamSelection : Form
    {
        public bool testStarted = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormExamSelection"/> class.
        /// </summary>
        public FormExamSelection()
        {
            InitializeComponent();
            btnStartTest.BackColor = Color.FromArgb(158, 158, 158);
        }

        /// <summary>
        /// When the input changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Input_TextChanged(object sender, System.EventArgs e)
        {
            // Input completed
            if(txtUsername.Text != String.Empty /*&& comboExamSelection.Text != String.Empty*/)
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
            if(testStarted)
            {
                String UserName = txtUsername.Text;
                String ExameStartedAt = DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss");
                String SelectedExame = comboExamSelection.Text;

                // Send selection to Server

                this.Close();
            }
        }
    }
}