namespace FrtBoothOfficeMachine
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
            this.MasterMenuStrip = new System.Windows.Forms.MenuStrip();
            this.SellTicketsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SellRegularTicketsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SellPassesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExtrasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PrintTicketsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ProcessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.QueryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ShiftChangeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DateTimeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ClockUpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.MasterMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MasterMenuStrip
            // 
            this.MasterMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SellTicketsToolStripMenuItem,
            this.ExtrasToolStripMenuItem,
            this.PrintTicketsToolStripMenuItem,
            this.ProcessToolStripMenuItem,
            this.QueryToolStripMenuItem,
            this.ShiftChangeToolStripMenuItem,
            this.HelpToolStripMenuItem,
            this.DateTimeMenuItem});
            this.MasterMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MasterMenuStrip.Name = "MasterMenuStrip";
            this.MasterMenuStrip.Size = new System.Drawing.Size(784, 24);
            this.MasterMenuStrip.TabIndex = 0;
            this.MasterMenuStrip.Text = "主菜单";
            // 
            // SellTicketsToolStripMenuItem
            // 
            this.SellTicketsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SellRegularTicketsToolStripMenuItem,
            this.SellPassesToolStripMenuItem});
            this.SellTicketsToolStripMenuItem.Name = "SellTicketsToolStripMenuItem";
            this.SellTicketsToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.SellTicketsToolStripMenuItem.Text = "售票 [ &G ]";
            // 
            // SellRegularTicketsToolStripMenuItem
            // 
            this.SellRegularTicketsToolStripMenuItem.Name = "SellRegularTicketsToolStripMenuItem";
            this.SellRegularTicketsToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.SellRegularTicketsToolStripMenuItem.Text = "售普通票 [ &S ]";
            // 
            // SellPassesToolStripMenuItem
            // 
            this.SellPassesToolStripMenuItem.Name = "SellPassesToolStripMenuItem";
            this.SellPassesToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.SellPassesToolStripMenuItem.Text = "售定期票 [ &D ]";
            // 
            // ExtrasToolStripMenuItem
            // 
            this.ExtrasToolStripMenuItem.Name = "ExtrasToolStripMenuItem";
            this.ExtrasToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.ExtrasToolStripMenuItem.Text = "辅助 [ &A ]";
            // 
            // PrintTicketsToolStripMenuItem
            // 
            this.PrintTicketsToolStripMenuItem.Name = "PrintTicketsToolStripMenuItem";
            this.PrintTicketsToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.PrintTicketsToolStripMenuItem.Text = "制票 [ &V ]";
            // 
            // ProcessToolStripMenuItem
            // 
            this.ProcessToolStripMenuItem.Name = "ProcessToolStripMenuItem";
            this.ProcessToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.ProcessToolStripMenuItem.Text = "处理 [ &L ]";
            // 
            // QueryToolStripMenuItem
            // 
            this.QueryToolStripMenuItem.Name = "QueryToolStripMenuItem";
            this.QueryToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.QueryToolStripMenuItem.Text = "查询 [ &U ]";
            // 
            // ShiftChangeToolStripMenuItem
            // 
            this.ShiftChangeToolStripMenuItem.Name = "ShiftChangeToolStripMenuItem";
            this.ShiftChangeToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.ShiftChangeToolStripMenuItem.Text = "交班 [ &X ]";
            // 
            // HelpToolStripMenuItem
            // 
            this.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem";
            this.HelpToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.HelpToolStripMenuItem.Text = "帮助 [ &B ]";
            // 
            // DateTimeMenuItem
            // 
            this.DateTimeMenuItem.Enabled = false;
            this.DateTimeMenuItem.Name = "DateTimeMenuItem";
            this.DateTimeMenuItem.Size = new System.Drawing.Size(107, 20);
            this.DateTimeMenuItem.Text = "1969-04-20 16:20";
            // 
            // ClockUpdateTimer
            // 
            this.ClockUpdateTimer.Enabled = true;
            this.ClockUpdateTimer.Interval = 500;
            this.ClockUpdateTimer.Tick += new System.EventHandler(this.ClockUpdateTimer_Tick);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(784, 537);
            this.panel1.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.MasterMenuStrip);
            this.MainMenuStrip = this.MasterMenuStrip;
            this.Name = "MainForm";
            this.Text = "法洛威轨道交通窗口售票机";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.MasterMenuStrip.ResumeLayout(false);
            this.MasterMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MasterMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem SellTicketsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SellRegularTicketsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SellPassesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExtrasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PrintTicketsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ProcessToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem QueryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ShiftChangeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DateTimeMenuItem;
        private System.Windows.Forms.Timer ClockUpdateTimer;
        private System.Windows.Forms.Panel panel1;
    }
}

