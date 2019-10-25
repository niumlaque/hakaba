namespace FFmpegWrapper.Views
{
    partial class ParametersForm
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
            this.chkYes = new System.Windows.Forms.CheckBox();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSourceRef = new System.Windows.Forms.Button();
            this.txtDestination = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbnDestRef = new System.Windows.Forms.Button();
            this.gBoxVideo = new System.Windows.Forms.GroupBox();
            this.gBoxVideoFilter = new System.Windows.Forms.GroupBox();
            this.txtPostProcessing = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtVideoScale = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.chkUseBFrame = new System.Windows.Forms.CheckBox();
            this.gBoxBFrame = new System.Windows.Forms.GroupBox();
            this.nudRefs = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.chkBFrameStrategy = new System.Windows.Forms.CheckBox();
            this.nudMaxBFrame = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbVideoCodec = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.gBoxAudio = new System.Windows.Forms.GroupBox();
            this.gBoxVideo.SuspendLayout();
            this.gBoxVideoFilter.SuspendLayout();
            this.gBoxBFrame.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRefs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxBFrame)).BeginInit();
            this.SuspendLayout();
            // 
            // chkYes
            // 
            this.chkYes.AutoSize = true;
            this.chkYes.Checked = true;
            this.chkYes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkYes.Location = new System.Drawing.Point(79, 62);
            this.chkYes.Name = "chkYes";
            this.chkYes.Size = new System.Drawing.Size(135, 16);
            this.chkYes.TabIndex = 6;
            this.chkYes.Text = "同名のファイルは上書き";
            this.chkYes.UseVisualStyleBackColor = true;
            // 
            // txtSource
            // 
            this.txtSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSource.Location = new System.Drawing.Point(79, 12);
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(671, 19);
            this.txtSource.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "入力ファイル";
            // 
            // btnSourceRef
            // 
            this.btnSourceRef.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSourceRef.Location = new System.Drawing.Point(756, 10);
            this.btnSourceRef.Name = "btnSourceRef";
            this.btnSourceRef.Size = new System.Drawing.Size(32, 23);
            this.btnSourceRef.TabIndex = 2;
            this.btnSourceRef.Text = "...";
            this.btnSourceRef.UseVisualStyleBackColor = true;
            this.btnSourceRef.Click += new System.EventHandler(this.OnSourceRefButtonClicked);
            // 
            // txtDestination
            // 
            this.txtDestination.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDestination.Location = new System.Drawing.Point(79, 37);
            this.txtDestination.Name = "txtDestination";
            this.txtDestination.Size = new System.Drawing.Size(671, 19);
            this.txtDestination.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "出力ファイル";
            // 
            // tbnDestRef
            // 
            this.tbnDestRef.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbnDestRef.Location = new System.Drawing.Point(756, 35);
            this.tbnDestRef.Name = "tbnDestRef";
            this.tbnDestRef.Size = new System.Drawing.Size(32, 23);
            this.tbnDestRef.TabIndex = 5;
            this.tbnDestRef.Text = "...";
            this.tbnDestRef.UseVisualStyleBackColor = true;
            this.tbnDestRef.Click += new System.EventHandler(this.OnDestRefButtonClicked);
            // 
            // gBoxVideo
            // 
            this.gBoxVideo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gBoxVideo.Controls.Add(this.gBoxVideoFilter);
            this.gBoxVideo.Controls.Add(this.chkUseBFrame);
            this.gBoxVideo.Controls.Add(this.gBoxBFrame);
            this.gBoxVideo.Controls.Add(this.cmbVideoCodec);
            this.gBoxVideo.Controls.Add(this.label3);
            this.gBoxVideo.Location = new System.Drawing.Point(12, 84);
            this.gBoxVideo.Name = "gBoxVideo";
            this.gBoxVideo.Size = new System.Drawing.Size(776, 254);
            this.gBoxVideo.TabIndex = 7;
            this.gBoxVideo.TabStop = false;
            this.gBoxVideo.Text = "映像オプション";
            // 
            // gBoxVideoFilter
            // 
            this.gBoxVideoFilter.Controls.Add(this.txtPostProcessing);
            this.gBoxVideoFilter.Controls.Add(this.label7);
            this.gBoxVideoFilter.Controls.Add(this.txtVideoScale);
            this.gBoxVideoFilter.Controls.Add(this.label6);
            this.gBoxVideoFilter.Location = new System.Drawing.Point(26, 156);
            this.gBoxVideoFilter.Name = "gBoxVideoFilter";
            this.gBoxVideoFilter.Size = new System.Drawing.Size(471, 89);
            this.gBoxVideoFilter.TabIndex = 9;
            this.gBoxVideoFilter.TabStop = false;
            this.gBoxVideoFilter.Text = "ビデオフィルタ";
            // 
            // txtPostProcessing
            // 
            this.txtPostProcessing.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtPostProcessing.Location = new System.Drawing.Point(97, 54);
            this.txtPostProcessing.Name = "txtPostProcessing";
            this.txtPostProcessing.Size = new System.Drawing.Size(100, 19);
            this.txtPostProcessing.TabIndex = 5;
            this.txtPostProcessing.Text = "ac";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 57);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 12);
            this.label7.TabIndex = 4;
            this.label7.Text = "PostProcessing";
            // 
            // txtVideoScale
            // 
            this.txtVideoScale.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtVideoScale.Location = new System.Drawing.Point(54, 25);
            this.txtVideoScale.Name = "txtVideoScale";
            this.txtVideoScale.Size = new System.Drawing.Size(100, 19);
            this.txtVideoScale.TabIndex = 3;
            this.txtVideoScale.Text = "1920:1080";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 2;
            this.label6.Text = "リサイズ";
            // 
            // chkUseBFrame
            // 
            this.chkUseBFrame.AutoSize = true;
            this.chkUseBFrame.Checked = true;
            this.chkUseBFrame.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUseBFrame.Location = new System.Drawing.Point(35, 55);
            this.chkUseBFrame.Name = "chkUseBFrame";
            this.chkUseBFrame.Size = new System.Drawing.Size(125, 16);
            this.chkUseBFrame.TabIndex = 7;
            this.chkUseBFrame.Text = "B フレームを使用する";
            this.chkUseBFrame.UseVisualStyleBackColor = true;
            this.chkUseBFrame.CheckedChanged += new System.EventHandler(this.OnUseBFrameCheckedChanged);
            // 
            // gBoxBFrame
            // 
            this.gBoxBFrame.Controls.Add(this.nudRefs);
            this.gBoxBFrame.Controls.Add(this.label5);
            this.gBoxBFrame.Controls.Add(this.chkBFrameStrategy);
            this.gBoxBFrame.Controls.Add(this.nudMaxBFrame);
            this.gBoxBFrame.Controls.Add(this.label4);
            this.gBoxBFrame.Location = new System.Drawing.Point(26, 56);
            this.gBoxBFrame.Name = "gBoxBFrame";
            this.gBoxBFrame.Size = new System.Drawing.Size(471, 85);
            this.gBoxBFrame.TabIndex = 8;
            this.gBoxBFrame.TabStop = false;
            // 
            // nudRefs
            // 
            this.nudRefs.Location = new System.Drawing.Point(129, 50);
            this.nudRefs.Maximum = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this.nudRefs.Name = "nudRefs";
            this.nudRefs.Size = new System.Drawing.Size(55, 19);
            this.nudRefs.TabIndex = 10;
            this.nudRefs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudRefs.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "参照フレーム上限数";
            // 
            // chkBFrameStrategy
            // 
            this.chkBFrameStrategy.AutoSize = true;
            this.chkBFrameStrategy.Checked = true;
            this.chkBFrameStrategy.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBFrameStrategy.Location = new System.Drawing.Point(190, 26);
            this.chkBFrameStrategy.Name = "chkBFrameStrategy";
            this.chkBFrameStrategy.Size = new System.Drawing.Size(228, 16);
            this.chkBFrameStrategy.TabIndex = 8;
            this.chkBFrameStrategy.Text = "B フレームの挿入位置を適応的に判断する";
            this.chkBFrameStrategy.UseVisualStyleBackColor = true;
            // 
            // nudMaxBFrame
            // 
            this.nudMaxBFrame.Location = new System.Drawing.Point(129, 25);
            this.nudMaxBFrame.Maximum = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this.nudMaxBFrame.Name = "nudMaxBFrame";
            this.nudMaxBFrame.Size = new System.Drawing.Size(55, 19);
            this.nudMaxBFrame.TabIndex = 2;
            this.nudMaxBFrame.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudMaxBFrame.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "最大 B フレーム数";
            // 
            // cmbVideoCodec
            // 
            this.cmbVideoCodec.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVideoCodec.FormattingEnabled = true;
            this.cmbVideoCodec.Location = new System.Drawing.Point(80, 22);
            this.cmbVideoCodec.Name = "cmbVideoCodec";
            this.cmbVideoCodec.Size = new System.Drawing.Size(207, 20);
            this.cmbVideoCodec.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "コーデック";
            // 
            // gBoxAudio
            // 
            this.gBoxAudio.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gBoxAudio.Location = new System.Drawing.Point(14, 348);
            this.gBoxAudio.Name = "gBoxAudio";
            this.gBoxAudio.Size = new System.Drawing.Size(774, 100);
            this.gBoxAudio.TabIndex = 8;
            this.gBoxAudio.TabStop = false;
            this.gBoxAudio.Text = "音声オプション";
            // 
            // ParametersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 460);
            this.Controls.Add(this.gBoxAudio);
            this.Controls.Add(this.gBoxVideo);
            this.Controls.Add(this.tbnDestRef);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDestination);
            this.Controls.Add(this.btnSourceRef);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSource);
            this.Controls.Add(this.chkYes);
            this.Name = "ParametersForm";
            this.Text = "ParametersForm";
            this.gBoxVideo.ResumeLayout(false);
            this.gBoxVideo.PerformLayout();
            this.gBoxVideoFilter.ResumeLayout(false);
            this.gBoxVideoFilter.PerformLayout();
            this.gBoxBFrame.ResumeLayout(false);
            this.gBoxBFrame.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRefs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxBFrame)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkYes;
        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSourceRef;
        private System.Windows.Forms.TextBox txtDestination;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button tbnDestRef;
        private System.Windows.Forms.GroupBox gBoxVideo;
        private System.Windows.Forms.GroupBox gBoxAudio;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbVideoCodec;
        private System.Windows.Forms.CheckBox chkUseBFrame;
        private System.Windows.Forms.GroupBox gBoxBFrame;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudMaxBFrame;
        private System.Windows.Forms.CheckBox chkBFrameStrategy;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudRefs;
        private System.Windows.Forms.GroupBox gBoxVideoFilter;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtVideoScale;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPostProcessing;
    }
}