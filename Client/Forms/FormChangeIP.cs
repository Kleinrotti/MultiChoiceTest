using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Forms
{
    public partial class FormChangeIP : Form
    {
        public IPAddress changedIP { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormExamSelection"/> class.
        /// </summary>
        public FormChangeIP()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Button Change IP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChangeIP_Click(object sender, EventArgs e)
        {
            try
            {
                changedIP = IPAddress.Parse(txtIP.Text);
                MessageBox.Show("Server address has been successfully changed.", "", MessageBoxButtons.OK);
            }
            catch
            {
                MessageBox.Show("Invalid input.", "Warning", MessageBoxButtons.OK);
                txtIP.Text = string.Empty;
            }
        }
    }
}
