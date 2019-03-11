namespace Client.Forms
{
    partial class FormMultipleChoiceTest
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
            this.btnSendResults = new System.Windows.Forms.Button();
            this.label_status = new System.Windows.Forms.Label();
            this.button_connect = new System.Windows.Forms.Button();
            this.button_sendtest = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSendResults
            // 
            this.btnSendResults.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnSendResults.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSendResults.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSendResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSendResults.Location = new System.Drawing.Point(866, 534);
            this.btnSendResults.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSendResults.Name = "btnSendResults";
            this.btnSendResults.Size = new System.Drawing.Size(274, 60);
            this.btnSendResults.TabIndex = 0;
            this.btnSendResults.Text = "Test abschicken";
            this.btnSendResults.UseVisualStyleBackColor = false;
            // 
            // label_status
            // 
            this.label_status.AutoSize = true;
            this.label_status.Location = new System.Drawing.Point(12, 574);
            this.label_status.Name = "label_status";
            this.label_status.Size = new System.Drawing.Size(0, 20);
            this.label_status.TabIndex = 1;
            // 
            // button_connect
            // 
            this.button_connect.Location = new System.Drawing.Point(583, 366);
            this.button_connect.Name = "button_connect";
            this.button_connect.Size = new System.Drawing.Size(157, 47);
            this.button_connect.TabIndex = 2;
            this.button_connect.Text = "Verbinden/Trennen";
            this.button_connect.UseVisualStyleBackColor = true;
            this.button_connect.Click += new System.EventHandler(this.button_connect_Click);
            // 
            // button_sendtest
            // 
            this.button_sendtest.Location = new System.Drawing.Point(583, 291);
            this.button_sendtest.Name = "button_sendtest";
            this.button_sendtest.Size = new System.Drawing.Size(157, 47);
            this.button_sendtest.TabIndex = 3;
            this.button_sendtest.Text = "Test Nachricht";
            this.button_sendtest.UseVisualStyleBackColor = true;
            this.button_sendtest.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormMultipleChoiceTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1158, 612);
            this.Controls.Add(this.button_sendtest);
            this.Controls.Add(this.button_connect);
            this.Controls.Add(this.label_status);
            this.Controls.Add(this.btnSendResults);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormMultipleChoiceTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Multiple Choice Test";
            this.Load += new System.EventHandler(this.FormMultipleChoiceTest_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSendResults;
        private System.Windows.Forms.Label label_status;
        private System.Windows.Forms.Button button_connect;
        private System.Windows.Forms.Button button_sendtest;
    }
}