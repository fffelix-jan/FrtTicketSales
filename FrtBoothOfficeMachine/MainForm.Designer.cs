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
            this.售定期票DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.辅助AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.制票VToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.处理LToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查询UToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.交班XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助BToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DateTimeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ClockUpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.MasterMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MasterMenuStrip
            // 
            this.MasterMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SellTicketsToolStripMenuItem,
            this.辅助AToolStripMenuItem,
            this.制票VToolStripMenuItem,
            this.处理LToolStripMenuItem,
            this.查询UToolStripMenuItem,
            this.交班XToolStripMenuItem,
            this.帮助BToolStripMenuItem,
            this.DateTimeMenuItem});
            this.MasterMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MasterMenuStrip.Name = "MasterMenuStrip";
            this.MasterMenuStrip.Size = new System.Drawing.Size(800, 24);
            this.MasterMenuStrip.TabIndex = 0;
            this.MasterMenuStrip.Text = "主菜单";
            // 
            // SellTicketsToolStripMenuItem
            // 
            this.SellTicketsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SellRegularTicketsToolStripMenuItem,
            this.售定期票DToolStripMenuItem});
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
            // 售定期票DToolStripMenuItem
            // 
            this.售定期票DToolStripMenuItem.Name = "售定期票DToolStripMenuItem";
            this.售定期票DToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.售定期票DToolStripMenuItem.Text = "售定期票 [ &D ]";
            // 
            // 辅助AToolStripMenuItem
            // 
            this.辅助AToolStripMenuItem.Name = "辅助AToolStripMenuItem";
            this.辅助AToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.辅助AToolStripMenuItem.Text = "辅助 [ &A ]";
            // 
            // 制票VToolStripMenuItem
            // 
            this.制票VToolStripMenuItem.Name = "制票VToolStripMenuItem";
            this.制票VToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.制票VToolStripMenuItem.Text = "制票 [ &V ]";
            // 
            // 处理LToolStripMenuItem
            // 
            this.处理LToolStripMenuItem.Name = "处理LToolStripMenuItem";
            this.处理LToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.处理LToolStripMenuItem.Text = "处理 [ &L ]";
            // 
            // 查询UToolStripMenuItem
            // 
            this.查询UToolStripMenuItem.Name = "查询UToolStripMenuItem";
            this.查询UToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.查询UToolStripMenuItem.Text = "查询 [ &U ]";
            // 
            // 交班XToolStripMenuItem
            // 
            this.交班XToolStripMenuItem.Name = "交班XToolStripMenuItem";
            this.交班XToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.交班XToolStripMenuItem.Text = "交班 [ &X ]";
            // 
            // 帮助BToolStripMenuItem
            // 
            this.帮助BToolStripMenuItem.Name = "帮助BToolStripMenuItem";
            this.帮助BToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.帮助BToolStripMenuItem.Text = "帮助 [ &B ]";
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.MasterMenuStrip);
            this.MainMenuStrip = this.MasterMenuStrip;
            this.Name = "MainForm";
            this.Text = "法洛威轨道交通窗口售票机";
            this.MasterMenuStrip.ResumeLayout(false);
            this.MasterMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MasterMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem SellTicketsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SellRegularTicketsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 售定期票DToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 辅助AToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 制票VToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 处理LToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查询UToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 交班XToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助BToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DateTimeMenuItem;
        private System.Windows.Forms.Timer ClockUpdateTimer;
    }
}

