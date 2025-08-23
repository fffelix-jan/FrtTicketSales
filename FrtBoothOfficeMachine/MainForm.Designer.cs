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
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.TopUpICCardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExtrasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TestServerConnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TestPrinterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ICCardReaderSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.DisplayCalendarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PrintTicketsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PrintFreeExitTicketToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ReprintDamagedTicketToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ProcessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.IssueFapiaoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RefundTicketToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.RefundICCardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.QueryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.QueryTicketInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.QueryStationInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.QueryICCardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ShiftChangeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PauseSellingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ChangePasswordAndLogOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LogoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DateTimeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ClockUpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.MainPanel = new System.Windows.Forms.Panel();
            this.MasterMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MasterMenuStrip
            // 
            this.MasterMenuStrip.Font = new System.Drawing.Font("SimSun", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.SellPassesToolStripMenuItem,
            this.toolStripSeparator3,
            this.TopUpICCardToolStripMenuItem});
            this.SellTicketsToolStripMenuItem.Name = "SellTicketsToolStripMenuItem";
            this.SellTicketsToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
            this.SellTicketsToolStripMenuItem.Text = "售票 [&G]";
            // 
            // SellRegularTicketsToolStripMenuItem
            // 
            this.SellRegularTicketsToolStripMenuItem.Name = "SellRegularTicketsToolStripMenuItem";
            this.SellRegularTicketsToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.SellRegularTicketsToolStripMenuItem.Text = "售单程票 [&S]";
            this.SellRegularTicketsToolStripMenuItem.Click += new System.EventHandler(this.SellRegularTicketsToolStripMenuItem_Click);
            // 
            // SellPassesToolStripMenuItem
            // 
            this.SellPassesToolStripMenuItem.Name = "SellPassesToolStripMenuItem";
            this.SellPassesToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.SellPassesToolStripMenuItem.Text = "售定期票 [&D]";
            this.SellPassesToolStripMenuItem.Click += new System.EventHandler(this.SellPassesToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(209, 6);
            // 
            // TopUpICCardToolStripMenuItem
            // 
            this.TopUpICCardToolStripMenuItem.Name = "TopUpICCardToolStripMenuItem";
            this.TopUpICCardToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.TopUpICCardToolStripMenuItem.Text = "IC卡激活及充值 [&C]";
            this.TopUpICCardToolStripMenuItem.Click += new System.EventHandler(this.TopUpICCardToolStripMenuItem_Click);
            // 
            // ExtrasToolStripMenuItem
            // 
            this.ExtrasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TestServerConnectionToolStripMenuItem,
            this.TestPrinterToolStripMenuItem,
            this.ICCardReaderSettingsToolStripMenuItem,
            this.toolStripSeparator5,
            this.DisplayCalendarToolStripMenuItem});
            this.ExtrasToolStripMenuItem.Name = "ExtrasToolStripMenuItem";
            this.ExtrasToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
            this.ExtrasToolStripMenuItem.Text = "辅助 [&A]";
            // 
            // TestServerConnectionToolStripMenuItem
            // 
            this.TestServerConnectionToolStripMenuItem.Name = "TestServerConnectionToolStripMenuItem";
            this.TestServerConnectionToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.TestServerConnectionToolStripMenuItem.Text = "测试服务器链接 [&T]";
            this.TestServerConnectionToolStripMenuItem.Click += new System.EventHandler(this.TestServerConnectionToolStripMenuItem_Click);
            // 
            // TestPrinterToolStripMenuItem
            // 
            this.TestPrinterToolStripMenuItem.Name = "TestPrinterToolStripMenuItem";
            this.TestPrinterToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.TestPrinterToolStripMenuItem.Text = "测试打印机 [&P]";
            this.TestPrinterToolStripMenuItem.Click += new System.EventHandler(this.TestPrinterToolStripMenuItem_Click);
            // 
            // ICCardReaderSettingsToolStripMenuItem
            // 
            this.ICCardReaderSettingsToolStripMenuItem.Name = "ICCardReaderSettingsToolStripMenuItem";
            this.ICCardReaderSettingsToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.ICCardReaderSettingsToolStripMenuItem.Text = "IC读卡器设置 [&R]";
            this.ICCardReaderSettingsToolStripMenuItem.Click += new System.EventHandler(this.ICCardReaderSettingsToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(208, 6);
            // 
            // DisplayCalendarToolStripMenuItem
            // 
            this.DisplayCalendarToolStripMenuItem.Name = "DisplayCalendarToolStripMenuItem";
            this.DisplayCalendarToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.DisplayCalendarToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.DisplayCalendarToolStripMenuItem.Text = "日历显示";
            this.DisplayCalendarToolStripMenuItem.Click += new System.EventHandler(this.DisplayCalendarToolStripMenuItem_Click);
            // 
            // PrintTicketsToolStripMenuItem
            // 
            this.PrintTicketsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PrintFreeExitTicketToolStripMenuItem,
            this.ReprintDamagedTicketToolStripMenuItem});
            this.PrintTicketsToolStripMenuItem.Name = "PrintTicketsToolStripMenuItem";
            this.PrintTicketsToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
            this.PrintTicketsToolStripMenuItem.Text = "制票 [&V]";
            // 
            // PrintFreeExitTicketToolStripMenuItem
            // 
            this.PrintFreeExitTicketToolStripMenuItem.Name = "PrintFreeExitTicketToolStripMenuItem";
            this.PrintFreeExitTicketToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.PrintFreeExitTicketToolStripMenuItem.Text = "打印免费出站票 [&E]";
            this.PrintFreeExitTicketToolStripMenuItem.Click += new System.EventHandler(this.PrintFreeExitTicketToolStripMenuItem_Click);
            // 
            // ReprintDamagedTicketToolStripMenuItem
            // 
            this.ReprintDamagedTicketToolStripMenuItem.Name = "ReprintDamagedTicketToolStripMenuItem";
            this.ReprintDamagedTicketToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.ReprintDamagedTicketToolStripMenuItem.Text = "重印破损车票 [&R]";
            this.ReprintDamagedTicketToolStripMenuItem.Click += new System.EventHandler(this.ReprintDamagedTicketToolStripMenuItem_Click);
            // 
            // ProcessToolStripMenuItem
            // 
            this.ProcessToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.IssueFapiaoToolStripMenuItem,
            this.RefundTicketToolStripMenuItem,
            this.toolStripSeparator4,
            this.RefundICCardToolStripMenuItem});
            this.ProcessToolStripMenuItem.Name = "ProcessToolStripMenuItem";
            this.ProcessToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
            this.ProcessToolStripMenuItem.Text = "处理 [&L]";
            // 
            // IssueFapiaoToolStripMenuItem
            // 
            this.IssueFapiaoToolStripMenuItem.Name = "IssueFapiaoToolStripMenuItem";
            this.IssueFapiaoToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.IssueFapiaoToolStripMenuItem.Text = "开发票 [&F]";
            this.IssueFapiaoToolStripMenuItem.Click += new System.EventHandler(this.IssueFapiaoToolStripMenuItem_Click);
            // 
            // RefundTicketToolStripMenuItem
            // 
            this.RefundTicketToolStripMenuItem.Name = "RefundTicketToolStripMenuItem";
            this.RefundTicketToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.RefundTicketToolStripMenuItem.Text = "退票处理 [&R]";
            this.RefundTicketToolStripMenuItem.Click += new System.EventHandler(this.RefundTicketToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(194, 6);
            // 
            // RefundICCardToolStripMenuItem
            // 
            this.RefundICCardToolStripMenuItem.Name = "RefundICCardToolStripMenuItem";
            this.RefundICCardToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.RefundICCardToolStripMenuItem.Text = "注销并退IC卡 [&X]";
            this.RefundICCardToolStripMenuItem.Click += new System.EventHandler(this.RefundICCardToolStripMenuItem_Click);
            // 
            // QueryToolStripMenuItem
            // 
            this.QueryToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.QueryTicketInfoToolStripMenuItem,
            this.QueryStationInfoToolStripMenuItem,
            this.toolStripSeparator2,
            this.QueryICCardToolStripMenuItem});
            this.QueryToolStripMenuItem.Name = "QueryToolStripMenuItem";
            this.QueryToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
            this.QueryToolStripMenuItem.Text = "查询 [&U]";
            // 
            // QueryTicketInfoToolStripMenuItem
            // 
            this.QueryTicketInfoToolStripMenuItem.Name = "QueryTicketInfoToolStripMenuItem";
            this.QueryTicketInfoToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.QueryTicketInfoToolStripMenuItem.Text = "车票信息查询 [&C]";
            this.QueryTicketInfoToolStripMenuItem.Click += new System.EventHandler(this.QueryTicketInfoToolStripMenuItem_Click);
            // 
            // QueryStationInfoToolStripMenuItem
            // 
            this.QueryStationInfoToolStripMenuItem.Name = "QueryStationInfoToolStripMenuItem";
            this.QueryStationInfoToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.QueryStationInfoToolStripMenuItem.Text = "车站信息查询 [&N]";
            this.QueryStationInfoToolStripMenuItem.Click += new System.EventHandler(this.QueryStationInfoToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(194, 6);
            // 
            // QueryICCardToolStripMenuItem
            // 
            this.QueryICCardToolStripMenuItem.Name = "QueryICCardToolStripMenuItem";
            this.QueryICCardToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.QueryICCardToolStripMenuItem.Text = "查询IC卡信息 [&I]";
            this.QueryICCardToolStripMenuItem.Click += new System.EventHandler(this.QueryICCardToolStripMenuItem_Click);
            // 
            // ShiftChangeToolStripMenuItem
            // 
            this.ShiftChangeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PauseSellingToolStripMenuItem,
            this.toolStripSeparator1,
            this.ChangePasswordAndLogOutToolStripMenuItem,
            this.LogoutToolStripMenuItem});
            this.ShiftChangeToolStripMenuItem.Name = "ShiftChangeToolStripMenuItem";
            this.ShiftChangeToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
            this.ShiftChangeToolStripMenuItem.Text = "交班 [&X]";
            // 
            // PauseSellingToolStripMenuItem
            // 
            this.PauseSellingToolStripMenuItem.Name = "PauseSellingToolStripMenuItem";
            this.PauseSellingToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
            this.PauseSellingToolStripMenuItem.Text = "暂停售票 [&Z]";
            this.PauseSellingToolStripMenuItem.Click += new System.EventHandler(this.PauseSellingToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(238, 6);
            // 
            // ChangePasswordAndLogOutToolStripMenuItem
            // 
            this.ChangePasswordAndLogOutToolStripMenuItem.Name = "ChangePasswordAndLogOutToolStripMenuItem";
            this.ChangePasswordAndLogOutToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
            this.ChangePasswordAndLogOutToolStripMenuItem.Text = "修改密码并退出登录 [&C]";
            this.ChangePasswordAndLogOutToolStripMenuItem.Click += new System.EventHandler(this.ChangePasswordAndLogOutToolStripMenuItem_Click);
            // 
            // LogoutToolStripMenuItem
            // 
            this.LogoutToolStripMenuItem.Name = "LogoutToolStripMenuItem";
            this.LogoutToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
            this.LogoutToolStripMenuItem.Text = "退出登录 [&X]";
            this.LogoutToolStripMenuItem.Click += new System.EventHandler(this.LogoutToolStripMenuItem_Click);
            // 
            // HelpToolStripMenuItem
            // 
            this.HelpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AboutToolStripMenuItem});
            this.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem";
            this.HelpToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
            this.HelpToolStripMenuItem.Text = "帮助 [&B]";
            // 
            // AboutToolStripMenuItem
            // 
            this.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
            this.AboutToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.AboutToolStripMenuItem.Text = "关于本软件...";
            this.AboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // DateTimeMenuItem
            // 
            this.DateTimeMenuItem.Enabled = false;
            this.DateTimeMenuItem.Name = "DateTimeMenuItem";
            this.DateTimeMenuItem.Size = new System.Drawing.Size(147, 20);
            this.DateTimeMenuItem.Text = "1969-04-20 16:20";
            // 
            // ClockUpdateTimer
            // 
            this.ClockUpdateTimer.Enabled = true;
            this.ClockUpdateTimer.Interval = 500;
            this.ClockUpdateTimer.Tick += new System.EventHandler(this.ClockUpdateTimer_Tick);
            // 
            // MainPanel
            // 
            this.MainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(199)))), ((int)(((byte)(199)))));
            this.MainPanel.Location = new System.Drawing.Point(0, 27);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(784, 534);
            this.MainPanel.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.MasterMenuStrip);
            this.MainMenuStrip = this.MasterMenuStrip;
            this.MinimumSize = new System.Drawing.Size(800, 600);
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
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.ToolStripMenuItem PauseSellingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PrintFreeExitTicketToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ChangePasswordAndLogOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LogoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem IssueFapiaoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ReprintDamagedTicketToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem QueryTicketInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TestServerConnectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RefundTicketToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DisplayCalendarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem QueryStationInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem TopUpICCardToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem QueryICCardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TestPrinterToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem RefundICCardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ICCardReaderSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
    }
}

