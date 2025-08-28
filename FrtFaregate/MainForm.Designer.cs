namespace FrtFaregate
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        // Handled in the code-behind file

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.MiddleTextLabel = new System.Windows.Forms.Label();
            this.CornerLabel = new System.Windows.Forms.Label();
            this.TicketScanTextBox = new System.Windows.Forms.TextBox();
            this.BottomTextLabel = new System.Windows.Forms.Label();
            this.UserPromptPictureBox = new System.Windows.Forms.PictureBox();
            this.HideUserMessageTimer = new System.Windows.Forms.Timer(this.components);
            this.SecurityTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.UserPromptPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // MiddleTextLabel
            // 
            this.MiddleTextLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.MiddleTextLabel.AutoSize = true;
            this.MiddleTextLabel.Font = new System.Drawing.Font("Microsoft YaHei", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MiddleTextLabel.Location = new System.Drawing.Point(201, 269);
            this.MiddleTextLabel.Name = "MiddleTextLabel";
            this.MiddleTextLabel.Size = new System.Drawing.Size(230, 46);
            this.MiddleTextLabel.TabIndex = 0;
            this.MiddleTextLabel.Text = "请刷卡或扫码";
            this.MiddleTextLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CornerLabel
            // 
            this.CornerLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CornerLabel.AutoSize = true;
            this.CornerLabel.Location = new System.Drawing.Point(12, 419);
            this.CornerLabel.Name = "CornerLabel";
            this.CornerLabel.Size = new System.Drawing.Size(116, 13);
            this.CornerLabel.TabIndex = 2;
            this.CornerLabel.Text = "DateTimeAndOtherInfo";
            // 
            // TicketScanTextBox
            // 
            this.TicketScanTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.TicketScanTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TicketScanTextBox.Location = new System.Drawing.Point(339, 419);
            this.TicketScanTextBox.Name = "TicketScanTextBox";
            this.TicketScanTextBox.Size = new System.Drawing.Size(273, 13);
            this.TicketScanTextBox.TabIndex = 3;
            this.TicketScanTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TicketScanTextBox_KeyDown);
            // 
            // BottomTextLabel
            // 
            this.BottomTextLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.BottomTextLabel.AutoSize = true;
            this.BottomTextLabel.Font = new System.Drawing.Font("Microsoft YaHei", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BottomTextLabel.Location = new System.Drawing.Point(108, 329);
            this.BottomTextLabel.Name = "BottomTextLabel";
            this.BottomTextLabel.Size = new System.Drawing.Size(409, 35);
            this.BottomTextLabel.TabIndex = 4;
            this.BottomTextLabel.Text = "Please Tap Card or Scan Ticket";
            // 
            // UserPromptPictureBox
            // 
            this.UserPromptPictureBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.UserPromptPictureBox.Image = global::FrtFaregate.Properties.Resources.TapCard;
            this.UserPromptPictureBox.Location = new System.Drawing.Point(131, 12);
            this.UserPromptPictureBox.Name = "UserPromptPictureBox";
            this.UserPromptPictureBox.Size = new System.Drawing.Size(370, 254);
            this.UserPromptPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.UserPromptPictureBox.TabIndex = 1;
            this.UserPromptPictureBox.TabStop = false;
            // 
            // HideUserMessageTimer
            // 
            this.HideUserMessageTimer.Interval = 3000;
            this.HideUserMessageTimer.Tick += new System.EventHandler(this.HideUserMessageTimer_Tick);
            // 
            // SecurityTimer
            // 
            this.SecurityTimer.Enabled = true;
            this.SecurityTimer.Interval = 1000;
            this.SecurityTimer.Tick += new System.EventHandler(this.SecurityTimer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.BottomTextLabel);
            this.Controls.Add(this.TicketScanTextBox);
            this.Controls.Add(this.CornerLabel);
            this.Controls.Add(this.UserPromptPictureBox);
            this.Controls.Add(this.MiddleTextLabel);
            this.Name = "MainForm";
            this.Text = "法洛威轨道交通闸机";
            ((System.ComponentModel.ISupportInitialize)(this.UserPromptPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label MiddleTextLabel;
        private System.Windows.Forms.PictureBox UserPromptPictureBox;
        private System.Windows.Forms.Label CornerLabel;
        private System.Windows.Forms.TextBox TicketScanTextBox;
        private System.Windows.Forms.Label BottomTextLabel;
        private System.Windows.Forms.Timer HideUserMessageTimer;
        private System.Windows.Forms.Timer SecurityTimer;
    }
}