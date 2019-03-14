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
            this.button_sendtest = new System.Windows.Forms.Button();
            this.tabControlExam = new System.Windows.Forms.TabControl();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSendResults
            // 
            this.btnSendResults.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnSendResults.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSendResults.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSendResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSendResults.Location = new System.Drawing.Point(632, 390);
            this.btnSendResults.Margin = new System.Windows.Forms.Padding(4);
            this.btnSendResults.Name = "btnSendResults";
            this.btnSendResults.Size = new System.Drawing.Size(244, 51);
            this.btnSendResults.TabIndex = 0;
            this.btnSendResults.Text = "Test abschicken";
            this.btnSendResults.UseVisualStyleBackColor = false;
            // 
            // label_status
            // 
            this.label_status.AutoSize = true;
            this.label_status.Location = new System.Drawing.Point(11, 488);
            this.label_status.Name = "label_status";
            this.label_status.Size = new System.Drawing.Size(0, 17);
            this.label_status.TabIndex = 1;
            // 
            // button_sendtest
            // 
            this.button_sendtest.Location = new System.Drawing.Point(359, 396);
            this.button_sendtest.Name = "button_sendtest";
            this.button_sendtest.Size = new System.Drawing.Size(140, 41);
            this.button_sendtest.TabIndex = 3;
            this.button_sendtest.Text = "Test Nachricht";
            this.button_sendtest.UseVisualStyleBackColor = true;
            this.button_sendtest.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabControlExam
            // 
            this.tabControlExam.Location = new System.Drawing.Point(10, 10);
            this.tabControlExam.Margin = new System.Windows.Forms.Padding(1);
            this.tabControlExam.Name = "tabControlExam";
            this.tabControlExam.SelectedIndex = 0;
            this.tabControlExam.Size = new System.Drawing.Size(869, 370);
            this.tabControlExam.TabIndex = 4;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(14, 390);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(244, 51);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Test abbrechen";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // FormMultipleChoiceTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(889, 449);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.tabControlExam);
            this.Controls.Add(this.button_sendtest);
            this.Controls.Add(this.label_status);
            this.Controls.Add(this.btnSendResults);
            this.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
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
        private System.Windows.Forms.Button button_sendtest;
        private System.Windows.Forms.TabControl tabControlExam;
        private System.Windows.Forms.Button btnCancel;
    }
}