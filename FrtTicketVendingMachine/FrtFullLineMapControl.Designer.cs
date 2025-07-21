namespace FrtTicketVendingMachine
{
    partial class FrtFullLineMapControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.FullMapPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.FullMapPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // FullMapPictureBox
            // 
            this.FullMapPictureBox.Image = global::FrtTicketVendingMachine.Properties.Resources.FrtTvmAllLinesMap;
            this.FullMapPictureBox.Location = new System.Drawing.Point(0, 0);
            this.FullMapPictureBox.Name = "FullMapPictureBox";
            this.FullMapPictureBox.Size = new System.Drawing.Size(1384, 885);
            this.FullMapPictureBox.TabIndex = 0;
            this.FullMapPictureBox.TabStop = false;
            // 
            // FrtFullLineMapControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.FullMapPictureBox);
            this.Name = "FrtFullLineMapControl";
            this.Size = new System.Drawing.Size(1384, 885);
            ((System.ComponentModel.ISupportInitialize)(this.FullMapPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox FullMapPictureBox;
    }
}
