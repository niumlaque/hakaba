namespace FFmpegWrapper.Views
{
    partial class OptionsForm
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
            this.txtFFmpegPath = new System.Windows.Forms.TextBox();
            this.btnFFmpegPath = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFFprobePath = new System.Windows.Forms.TextBox();
            this.btnFFprobePath = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "FFmpeg パス";
            // 
            // txtFFmpegPath
            // 
            this.txtFFmpegPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFFmpegPath.Location = new System.Drawing.Point(87, 14);
            this.txtFFmpegPath.Name = "txtFFmpegPath";
            this.txtFFmpegPath.Size = new System.Drawing.Size(200, 19);
            this.txtFFmpegPath.TabIndex = 1;
            // 
            // btnFFmpegPath
            // 
            this.btnFFmpegPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFFmpegPath.Location = new System.Drawing.Point(293, 12);
            this.btnFFmpegPath.Name = "btnFFmpegPath";
            this.btnFFmpegPath.Size = new System.Drawing.Size(33, 23);
            this.btnFFmpegPath.TabIndex = 2;
            this.btnFFmpegPath.Text = "...";
            this.btnFFmpegPath.UseVisualStyleBackColor = true;
            this.btnFFmpegPath.Click += new System.EventHandler(this.OnFFmpegPathButtonClicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "FFprobe パス";
            // 
            // txtFFprobePath
            // 
            this.txtFFprobePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFFprobePath.Location = new System.Drawing.Point(87, 43);
            this.txtFFprobePath.Name = "txtFFprobePath";
            this.txtFFprobePath.Size = new System.Drawing.Size(200, 19);
            this.txtFFprobePath.TabIndex = 4;
            // 
            // btnFFprobePath
            // 
            this.btnFFprobePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFFprobePath.Location = new System.Drawing.Point(293, 41);
            this.btnFFprobePath.Name = "btnFFprobePath";
            this.btnFFprobePath.Size = new System.Drawing.Size(33, 23);
            this.btnFFprobePath.TabIndex = 5;
            this.btnFFprobePath.Text = "...";
            this.btnFFprobePath.UseVisualStyleBackColor = true;
            this.btnFFprobePath.Click += new System.EventHandler(this.OnFFprobePathButtonClicked);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(170, 77);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.OKButtonClicked);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(251, 77);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "キャンセル";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.OnCancelButtonClicked);
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(338, 112);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnFFprobePath);
            this.Controls.Add(this.txtFFprobePath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnFFmpegPath);
            this.Controls.Add(this.txtFFmpegPath);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Options";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFFmpegPath;
        private System.Windows.Forms.Button btnFFmpegPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFFprobePath;
        private System.Windows.Forms.Button btnFFprobePath;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}