namespace FrtTicketVendingMachine
{
    partial class TransactionCancelledControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.CancelledTextLabel = new System.Windows.Forms.Label();
            this.RedXPictureBox = new System.Windows.Forms.PictureBox();
            this.FailSoundTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.RedXPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // CancelledTextLabel
            // 
            this.CancelledTextLabel.AutoSize = true;
            this.CancelledTextLabel.Font = new System.Drawing.Font("Microsoft YaHei", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CancelledTextLabel.ForeColor = System.Drawing.Color.Red;
            this.CancelledTextLabel.Location = new System.Drawing.Point(95, 63);
            this.CancelledTextLabel.Name = "CancelledTextLabel";
            this.CancelledTextLabel.Size = new System.Drawing.Size(1110, 128);
            this.CancelledTextLabel.TabIndex = 0;
            this.CancelledTextLabel.Text = "Transaction Cancelled";
            // 
            // RedXPictureBox
            // 
            this.RedXPictureBox.Image = global::FrtTicketVendingMachine.Properties.Resources.XSign;
            this.RedXPictureBox.Location = new System.Drawing.Point(409, 266);
            this.RedXPictureBox.Name = "RedXPictureBox";
            this.RedXPictureBox.Size = new System.Drawing.Size(500, 500);
            this.RedXPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.RedXPictureBox.TabIndex = 1;
            this.RedXPictureBox.TabStop = false;
            // 
            // FailSoundTimer
            // 
            this.FailSoundTimer.Interval = 198;
            this.FailSoundTimer.Tick += new System.EventHandler(this.FailSoundTimer_Tick);
            // 
            // TransactionCancelledControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.RedXPictureBox);
            this.Controls.Add(this.CancelledTextLabel);
            this.Name = "TransactionCancelledControl";
            this.Size = new System.Drawing.Size(1384, 885);
            this.VisibleChanged += new System.EventHandler(this.TransactionCancelledControl_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.RedXPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label CancelledTextLabel;
        private System.Windows.Forms.PictureBox RedXPictureBox;
        private System.Windows.Forms.Timer FailSoundTimer;
    }
}
