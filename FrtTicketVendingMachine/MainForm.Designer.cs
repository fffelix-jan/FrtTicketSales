namespace FrtTicketVendingMachine
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
            this.components = new System.ComponentModel.Container();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.ClockUpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.RightHandSelectionsRoundedPanel = new FrtTicketVendingMachine.RoundedPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SelectTicketQuantityLabel = new System.Windows.Forms.Label();
            this.QuantitySelectFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.OneTicketButton = new FrtTicketVendingMachine.RoundedButton();
            this.TwoTicketButton = new FrtTicketVendingMachine.RoundedButton();
            this.ThreeTicketButton = new FrtTicketVendingMachine.RoundedButton();
            this.FourTicketButton = new FrtTicketVendingMachine.RoundedButton();
            this.FiveTicketButton = new FrtTicketVendingMachine.RoundedButton();
            this.SixTicketButton = new FrtTicketVendingMachine.RoundedButton();
            this.ClockRoundedPanel = new FrtTicketVendingMachine.RoundedPanel();
            this.DateTimeLabel = new System.Windows.Forms.Label();
            this.LogoPictureBox = new System.Windows.Forms.PictureBox();
            this.MapSelectionRoundedPanel = new FrtTicketVendingMachine.RoundedPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.MainPanel.SuspendLayout();
            this.RightHandSelectionsRoundedPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.QuantitySelectFlowLayoutPanel.SuspendLayout();
            this.ClockRoundedPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LogoPictureBox)).BeginInit();
            this.MapSelectionRoundedPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.MainPanel.Controls.Add(this.RightHandSelectionsRoundedPanel);
            this.MainPanel.Controls.Add(this.MapSelectionRoundedPanel);
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Margin = new System.Windows.Forms.Padding(10);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(1920, 1080);
            this.MainPanel.TabIndex = 0;
            // 
            // ClockUpdateTimer
            // 
            this.ClockUpdateTimer.Enabled = true;
            this.ClockUpdateTimer.Interval = 500;
            this.ClockUpdateTimer.Tick += new System.EventHandler(this.ClockUpdateTimer_Tick);
            // 
            // RightHandSelectionsRoundedPanel
            // 
            this.RightHandSelectionsRoundedPanel.BackColor = System.Drawing.Color.DodgerBlue;
            this.RightHandSelectionsRoundedPanel.BorderColor = System.Drawing.Color.White;
            this.RightHandSelectionsRoundedPanel.Controls.Add(this.panel1);
            this.RightHandSelectionsRoundedPanel.Controls.Add(this.ClockRoundedPanel);
            this.RightHandSelectionsRoundedPanel.Controls.Add(this.LogoPictureBox);
            this.RightHandSelectionsRoundedPanel.Location = new System.Drawing.Point(1459, 30);
            this.RightHandSelectionsRoundedPanel.Name = "RightHandSelectionsRoundedPanel";
            this.RightHandSelectionsRoundedPanel.Radius = 50;
            this.RightHandSelectionsRoundedPanel.Size = new System.Drawing.Size(418, 1009);
            this.RightHandSelectionsRoundedPanel.TabIndex = 1;
            this.RightHandSelectionsRoundedPanel.Thickness = 5F;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.SelectTicketQuantityLabel);
            this.panel1.Controls.Add(this.QuantitySelectFlowLayoutPanel);
            this.panel1.Location = new System.Drawing.Point(23, 296);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(367, 285);
            this.panel1.TabIndex = 4;
            // 
            // SelectTicketQuantityLabel
            // 
            this.SelectTicketQuantityLabel.AutoSize = true;
            this.SelectTicketQuantityLabel.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectTicketQuantityLabel.ForeColor = System.Drawing.Color.White;
            this.SelectTicketQuantityLabel.Location = new System.Drawing.Point(12, 0);
            this.SelectTicketQuantityLabel.Name = "SelectTicketQuantityLabel";
            this.SelectTicketQuantityLabel.Size = new System.Drawing.Size(337, 37);
            this.SelectTicketQuantityLabel.TabIndex = 3;
            this.SelectTicketQuantityLabel.Text = "Select Number of Tickets";
            // 
            // QuantitySelectFlowLayoutPanel
            // 
            this.QuantitySelectFlowLayoutPanel.Controls.Add(this.OneTicketButton);
            this.QuantitySelectFlowLayoutPanel.Controls.Add(this.TwoTicketButton);
            this.QuantitySelectFlowLayoutPanel.Controls.Add(this.ThreeTicketButton);
            this.QuantitySelectFlowLayoutPanel.Controls.Add(this.FourTicketButton);
            this.QuantitySelectFlowLayoutPanel.Controls.Add(this.FiveTicketButton);
            this.QuantitySelectFlowLayoutPanel.Controls.Add(this.SixTicketButton);
            this.QuantitySelectFlowLayoutPanel.Location = new System.Drawing.Point(19, 44);
            this.QuantitySelectFlowLayoutPanel.Name = "QuantitySelectFlowLayoutPanel";
            this.QuantitySelectFlowLayoutPanel.Size = new System.Drawing.Size(321, 223);
            this.QuantitySelectFlowLayoutPanel.TabIndex = 2;
            // 
            // OneTicketButton
            // 
            this.OneTicketButton.BackColor = System.Drawing.Color.Crimson;
            this.OneTicketButton.BorderColor = System.Drawing.Color.Transparent;
            this.OneTicketButton.FlatAppearance.BorderSize = 0;
            this.OneTicketButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OneTicketButton.Font = new System.Drawing.Font("Segoe UI", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OneTicketButton.ForeColor = System.Drawing.Color.White;
            this.OneTicketButton.Location = new System.Drawing.Point(3, 3);
            this.OneTicketButton.Name = "OneTicketButton";
            this.OneTicketButton.Radius = 25;
            this.OneTicketButton.Size = new System.Drawing.Size(100, 100);
            this.OneTicketButton.TabIndex = 0;
            this.OneTicketButton.TabStop = false;
            this.OneTicketButton.Text = "1";
            this.OneTicketButton.Thickness = 3F;
            this.OneTicketButton.UseVisualStyleBackColor = false;
            // 
            // TwoTicketButton
            // 
            this.TwoTicketButton.BackColor = System.Drawing.Color.Crimson;
            this.TwoTicketButton.BorderColor = System.Drawing.Color.Transparent;
            this.TwoTicketButton.FlatAppearance.BorderSize = 0;
            this.TwoTicketButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TwoTicketButton.Font = new System.Drawing.Font("Segoe UI", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TwoTicketButton.ForeColor = System.Drawing.Color.White;
            this.TwoTicketButton.Location = new System.Drawing.Point(109, 3);
            this.TwoTicketButton.Name = "TwoTicketButton";
            this.TwoTicketButton.Radius = 25;
            this.TwoTicketButton.Size = new System.Drawing.Size(100, 100);
            this.TwoTicketButton.TabIndex = 1;
            this.TwoTicketButton.TabStop = false;
            this.TwoTicketButton.Text = "2";
            this.TwoTicketButton.Thickness = 3F;
            this.TwoTicketButton.UseVisualStyleBackColor = false;
            // 
            // ThreeTicketButton
            // 
            this.ThreeTicketButton.BackColor = System.Drawing.Color.Crimson;
            this.ThreeTicketButton.BorderColor = System.Drawing.Color.Transparent;
            this.ThreeTicketButton.FlatAppearance.BorderSize = 0;
            this.ThreeTicketButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ThreeTicketButton.Font = new System.Drawing.Font("Segoe UI", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ThreeTicketButton.ForeColor = System.Drawing.Color.White;
            this.ThreeTicketButton.Location = new System.Drawing.Point(215, 3);
            this.ThreeTicketButton.Name = "ThreeTicketButton";
            this.ThreeTicketButton.Radius = 25;
            this.ThreeTicketButton.Size = new System.Drawing.Size(100, 100);
            this.ThreeTicketButton.TabIndex = 2;
            this.ThreeTicketButton.TabStop = false;
            this.ThreeTicketButton.Text = "3";
            this.ThreeTicketButton.Thickness = 3F;
            this.ThreeTicketButton.UseVisualStyleBackColor = false;
            // 
            // FourTicketButton
            // 
            this.FourTicketButton.BackColor = System.Drawing.Color.Crimson;
            this.FourTicketButton.BorderColor = System.Drawing.Color.Transparent;
            this.FourTicketButton.FlatAppearance.BorderSize = 0;
            this.FourTicketButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FourTicketButton.Font = new System.Drawing.Font("Segoe UI", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FourTicketButton.ForeColor = System.Drawing.Color.White;
            this.FourTicketButton.Location = new System.Drawing.Point(3, 109);
            this.FourTicketButton.Name = "FourTicketButton";
            this.FourTicketButton.Radius = 25;
            this.FourTicketButton.Size = new System.Drawing.Size(100, 100);
            this.FourTicketButton.TabIndex = 3;
            this.FourTicketButton.TabStop = false;
            this.FourTicketButton.Text = "4";
            this.FourTicketButton.Thickness = 3F;
            this.FourTicketButton.UseVisualStyleBackColor = false;
            // 
            // FiveTicketButton
            // 
            this.FiveTicketButton.BackColor = System.Drawing.Color.Crimson;
            this.FiveTicketButton.BorderColor = System.Drawing.Color.Transparent;
            this.FiveTicketButton.FlatAppearance.BorderSize = 0;
            this.FiveTicketButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FiveTicketButton.Font = new System.Drawing.Font("Segoe UI", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FiveTicketButton.ForeColor = System.Drawing.Color.White;
            this.FiveTicketButton.Location = new System.Drawing.Point(109, 109);
            this.FiveTicketButton.Name = "FiveTicketButton";
            this.FiveTicketButton.Radius = 25;
            this.FiveTicketButton.Size = new System.Drawing.Size(100, 100);
            this.FiveTicketButton.TabIndex = 4;
            this.FiveTicketButton.TabStop = false;
            this.FiveTicketButton.Text = "5";
            this.FiveTicketButton.Thickness = 3F;
            this.FiveTicketButton.UseVisualStyleBackColor = false;
            // 
            // SixTicketButton
            // 
            this.SixTicketButton.BackColor = System.Drawing.Color.Crimson;
            this.SixTicketButton.BorderColor = System.Drawing.Color.Transparent;
            this.SixTicketButton.FlatAppearance.BorderSize = 0;
            this.SixTicketButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SixTicketButton.Font = new System.Drawing.Font("Segoe UI", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SixTicketButton.ForeColor = System.Drawing.Color.White;
            this.SixTicketButton.Location = new System.Drawing.Point(215, 109);
            this.SixTicketButton.Name = "SixTicketButton";
            this.SixTicketButton.Radius = 25;
            this.SixTicketButton.Size = new System.Drawing.Size(100, 100);
            this.SixTicketButton.TabIndex = 5;
            this.SixTicketButton.TabStop = false;
            this.SixTicketButton.Text = "6";
            this.SixTicketButton.Thickness = 3F;
            this.SixTicketButton.UseVisualStyleBackColor = false;
            // 
            // ClockRoundedPanel
            // 
            this.ClockRoundedPanel.BackColor = System.Drawing.Color.DarkTurquoise;
            this.ClockRoundedPanel.BorderColor = System.Drawing.Color.White;
            this.ClockRoundedPanel.Controls.Add(this.DateTimeLabel);
            this.ClockRoundedPanel.Location = new System.Drawing.Point(49, 162);
            this.ClockRoundedPanel.Name = "ClockRoundedPanel";
            this.ClockRoundedPanel.Radius = 20;
            this.ClockRoundedPanel.Size = new System.Drawing.Size(316, 128);
            this.ClockRoundedPanel.TabIndex = 1;
            this.ClockRoundedPanel.Thickness = 5F;
            // 
            // DateTimeLabel
            // 
            this.DateTimeLabel.AutoSize = true;
            this.DateTimeLabel.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateTimeLabel.ForeColor = System.Drawing.Color.White;
            this.DateTimeLabel.Location = new System.Drawing.Point(6, 13);
            this.DateTimeLabel.Name = "DateTimeLabel";
            this.DateTimeLabel.Size = new System.Drawing.Size(304, 100);
            this.DateTimeLabel.TabIndex = 0;
            this.DateTimeLabel.Text = "1919年08月10日\r\n04:20";
            this.DateTimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LogoPictureBox
            // 
            this.LogoPictureBox.Image = global::FrtTicketVendingMachine.Properties.Resources.rsz_frt_logo;
            this.LogoPictureBox.Location = new System.Drawing.Point(-9, -3);
            this.LogoPictureBox.Name = "LogoPictureBox";
            this.LogoPictureBox.Size = new System.Drawing.Size(427, 134);
            this.LogoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.LogoPictureBox.TabIndex = 0;
            this.LogoPictureBox.TabStop = false;
            // 
            // MapSelectionRoundedPanel
            // 
            this.MapSelectionRoundedPanel.BackColor = System.Drawing.Color.White;
            this.MapSelectionRoundedPanel.BorderColor = System.Drawing.Color.White;
            this.MapSelectionRoundedPanel.Controls.Add(this.panel2);
            this.MapSelectionRoundedPanel.Location = new System.Drawing.Point(30, 30);
            this.MapSelectionRoundedPanel.Name = "MapSelectionRoundedPanel";
            this.MapSelectionRoundedPanel.Radius = 50;
            this.MapSelectionRoundedPanel.Size = new System.Drawing.Size(1384, 804);
            this.MapSelectionRoundedPanel.TabIndex = 0;
            this.MapSelectionRoundedPanel.Thickness = 5F;
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(0, 20);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1384, 765);
            this.panel2.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.Controls.Add(this.MainPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FRT Ticket Vending Machine Main Window";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.MainPanel.ResumeLayout(false);
            this.RightHandSelectionsRoundedPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.QuantitySelectFlowLayoutPanel.ResumeLayout(false);
            this.ClockRoundedPanel.ResumeLayout(false);
            this.ClockRoundedPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LogoPictureBox)).EndInit();
            this.MapSelectionRoundedPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel MainPanel;
        private RoundedPanel MapSelectionRoundedPanel;
        private RoundedPanel RightHandSelectionsRoundedPanel;
        private System.Windows.Forms.PictureBox LogoPictureBox;
        private RoundedPanel ClockRoundedPanel;
        private System.Windows.Forms.Label DateTimeLabel;
        private System.Windows.Forms.FlowLayoutPanel QuantitySelectFlowLayoutPanel;
        private System.Windows.Forms.Label SelectTicketQuantityLabel;
        private RoundedButton OneTicketButton;
        private RoundedButton TwoTicketButton;
        private RoundedButton ThreeTicketButton;
        private RoundedButton FourTicketButton;
        private RoundedButton FiveTicketButton;
        private RoundedButton SixTicketButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Timer ClockUpdateTimer;
        private System.Windows.Forms.Panel panel2;
    }
}

