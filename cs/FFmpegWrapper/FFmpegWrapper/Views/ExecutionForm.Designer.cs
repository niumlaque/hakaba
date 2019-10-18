namespace FFmpegWrapper.Views
{
    partial class ExecutionForm
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
            if (disposing && (components != null))
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
            this.label1 = new System.Windows.Forms.Label();
            this.rtBoxCommand = new System.Windows.Forms.RichTextBox();
            this.rtBoxLog = new System.Windows.Forms.RichTextBox();
            this.btnAbort = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "コマンド";
            // 
            // rtBoxCommand
            // 
            this.rtBoxCommand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtBoxCommand.BackColor = System.Drawing.Color.LightGray;
            this.rtBoxCommand.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.rtBoxCommand.Location = new System.Drawing.Point(12, 24);
            this.rtBoxCommand.Name = "rtBoxCommand";
            this.rtBoxCommand.ReadOnly = true;
            this.rtBoxCommand.Size = new System.Drawing.Size(560, 97);
            this.rtBoxCommand.TabIndex = 4;
            this.rtBoxCommand.Text = "";
            // 
            // rtBoxLog
            // 
            this.rtBoxLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtBoxLog.BackColor = System.Drawing.Color.LightGray;
            this.rtBoxLog.Location = new System.Drawing.Point(12, 127);
            this.rtBoxLog.Name = "rtBoxLog";
            this.rtBoxLog.ReadOnly = true;
            this.rtBoxLog.Size = new System.Drawing.Size(560, 293);
            this.rtBoxLog.TabIndex = 6;
            this.rtBoxLog.Text = "";
            // 
            // btnAbort
            // 
            this.btnAbort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAbort.Location = new System.Drawing.Point(497, 426);
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size(75, 23);
            this.btnAbort.TabIndex = 7;
            this.btnAbort.Text = "中断";
            this.btnAbort.UseVisualStyleBackColor = true;
            // 
            // ExecutionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 461);
            this.Controls.Add(this.btnAbort);
            this.Controls.Add(this.rtBoxLog);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rtBoxCommand);
            this.MinimumSize = new System.Drawing.Size(600, 500);
            this.Name = "ExecutionForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Execution";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox rtBoxCommand;
        private System.Windows.Forms.RichTextBox rtBoxLog;
        private System.Windows.Forms.Button btnAbort;
    }
}