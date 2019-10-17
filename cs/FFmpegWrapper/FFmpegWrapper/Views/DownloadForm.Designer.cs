namespace FFmpegWrapper.Views
{
    partial class DownloadForm
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
            this.lblFM = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // lblFM
            // 
            this.lblFM.AutoSize = true;
            this.lblFM.Location = new System.Drawing.Point(69, 28);
            this.lblFM.Name = "lblFM";
            this.lblFM.Size = new System.Drawing.Size(130, 12);
            this.lblFM.TabIndex = 1;
            this.lblFM.TabStop = true;
            this.lblFM.Text = "https://www.ffmpeg.org/";
            this.lblFM.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblFM.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnFMLinkLabelClicked);
            // 
            // DownloadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(272, 66);
            this.Controls.Add(this.lblFM);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DownloadForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Download";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel lblFM;
    }
}