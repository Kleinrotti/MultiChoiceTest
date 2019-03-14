namespace Client.Forms
{
    partial class FormChangeIP
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnChangeIP = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtIP);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(308, 79);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            // 
            // txtIP
            // 
            this.txtIP.Font = new System.Drawing.Font("Arial", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIP.Location = new System.Drawing.Point(10, 38);
            this.txtIP.MaxLength = 32;
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(286, 26);
            this.txtIP.TabIndex = 7;
            this.txtIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(267, 19);
            this.label2.TabIndex = 6;
            this.label2.Text = "Geben Sie die Server-Adresse ein.";
            // 
            // btnChangeIP
            // 
            this.btnChangeIP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnChangeIP.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChangeIP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangeIP.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangeIP.Location = new System.Drawing.Point(55, 98);
            this.btnChangeIP.Name = "btnChangeIP";
            this.btnChangeIP.Size = new System.Drawing.Size(209, 39);
            this.btnChangeIP.TabIndex = 9;
            this.btnChangeIP.Text = "Bestätigen";
            this.btnChangeIP.UseVisualStyleBackColor = false;
            this.btnChangeIP.Click += new System.EventHandler(this.btnChangeIP_Click);
            // 
            // FormChangeIP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 149);
            this.Controls.Add(this.btnChangeIP);
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormChangeIP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Server Adresse ändern";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnChangeIP;
    }
}