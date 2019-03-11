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
            this.SuspendLayout();
            // 
            // btnSendResults
            // 
            this.btnSendResults.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnSendResults.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSendResults.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSendResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSendResults.Location = new System.Drawing.Point(577, 347);
            this.btnSendResults.Name = "btnSendResults";
            this.btnSendResults.Size = new System.Drawing.Size(183, 39);
            this.btnSendResults.TabIndex = 0;
            this.btnSendResults.Text = "Test abschicken";
            this.btnSendResults.UseVisualStyleBackColor = false;
            // 
            // FormMultipleChoiceTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(772, 398);
            this.Controls.Add(this.btnSendResults);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormMultipleChoiceTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Multiple Choice Test";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSendResults;
    }
}