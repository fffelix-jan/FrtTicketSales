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
            this.测试服务器链接ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.测试打印机PToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ICCardReaderSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.DisplayCalendarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PrintTicketsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打印免费出站票EToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.重打ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ProcessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.开发票FToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退票处理RToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.RefundICCardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.QueryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查询票信息检票CToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.车站信息查询NToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.QueryICCardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ShiftChangeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.暂停售票ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.修改密码并退出登录XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LogoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于本软件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.SellTicketsToolStripMenuItem.Size = new System.Drawing.Size(99, 23);
            this.SellTicketsToolStripMenuItem.Text = "售票 [&G]";
            // 
            // SellRegularTicketsToolStripMenuItem
            // 
            this.SellRegularTicketsToolStripMenuItem.Name = "SellRegularTicketsToolStripMenuItem";
            this.SellRegularTicketsToolStripMenuItem.Size = new System.Drawing.Size(252, 24);
            this.SellRegularTicketsToolStripMenuItem.Text = "售普通票 [&S]";
            // 
            // SellPassesToolStripMenuItem
            // 
            this.SellPassesToolStripMenuItem.Name = "SellPassesToolStripMenuItem";
            this.SellPassesToolStripMenuItem.Size = new System.Drawing.Size(252, 24);
            this.SellPassesToolStripMenuItem.Text = "售定期票 [&D]";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(252, 6);
            // 
            // TopUpICCardToolStripMenuItem
            // 
            this.TopUpICCardToolStripMenuItem.Name = "TopUpICCardToolStripMenuItem";
            this.TopUpICCardToolStripMenuItem.Size = new System.Drawing.Size(252, 24);
            this.TopUpICCardToolStripMenuItem.Text = "IC卡激活及充值 [&C]";
            this.TopUpICCardToolStripMenuItem.Click += new System.EventHandler(this.TopUpICCardToolStripMenuItem_Click);
            // 
            // ExtrasToolStripMenuItem
            // 
            this.ExtrasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.测试服务器链接ToolStripMenuItem,
            this.测试打印机PToolStripMenuItem,
            this.ICCardReaderSettingsToolStripMenuItem,
            this.toolStripSeparator5,
            this.DisplayCalendarToolStripMenuItem});
            this.ExtrasToolStripMenuItem.Name = "ExtrasToolStripMenuItem";
            this.ExtrasToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
            this.ExtrasToolStripMenuItem.Text = "辅助 [&A]";
            // 
            // 测试服务器链接ToolStripMenuItem
            // 
            this.测试服务器链接ToolStripMenuItem.Name = "测试服务器链接ToolStripMenuItem";
            this.测试服务器链接ToolStripMenuItem.Size = new System.Drawing.Size(251, 24);
            this.测试服务器链接ToolStripMenuItem.Text = "测试服务器链接 [&T]";
            // 
            // 测试打印机PToolStripMenuItem
            // 
            this.测试打印机PToolStripMenuItem.Name = "测试打印机PToolStripMenuItem";
            this.测试打印机PToolStripMenuItem.Size = new System.Drawing.Size(251, 24);
            this.测试打印机PToolStripMenuItem.Text = "测试打印机 [&P]";
            // 
            // ICCardReaderSettingsToolStripMenuItem
            // 
            this.ICCardReaderSettingsToolStripMenuItem.Name = "ICCardReaderSettingsToolStripMenuItem";
            this.ICCardReaderSettingsToolStripMenuItem.Size = new System.Drawing.Size(251, 24);
            this.ICCardReaderSettingsToolStripMenuItem.Text = "IC读卡器设置 [&R]";
            this.ICCardReaderSettingsToolStripMenuItem.Click += new System.EventHandler(this.ICCardReaderSettingsToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(255, 6);
            // 
            // DisplayCalendarToolStripMenuItem
            // 
            this.DisplayCalendarToolStripMenuItem.Name = "DisplayCalendarToolStripMenuItem";
            this.DisplayCalendarToolStripMenuItem.Size = new System.Drawing.Size(258, 30);
            this.DisplayCalendarToolStripMenuItem.Text = "日历显示";
            // 
            // PrintTicketsToolStripMenuItem
            // 
            this.PrintTicketsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打印免费出站票EToolStripMenuItem,
            this.重打ToolStripMenuItem});
            this.PrintTicketsToolStripMenuItem.Name = "PrintTicketsToolStripMenuItem";
            this.PrintTicketsToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
            this.PrintTicketsToolStripMenuItem.Text = "制票 [&V]";
            // 
            // 打印免费出站票EToolStripMenuItem
            // 
            this.打印免费出站票EToolStripMenuItem.Name = "打印免费出站票EToolStripMenuItem";
            this.打印免费出站票EToolStripMenuItem.Size = new System.Drawing.Size(251, 24);
            this.打印免费出站票EToolStripMenuItem.Text = "打印免费出站票 [&E]";
            // 
            // 重打ToolStripMenuItem
            // 
            this.重打ToolStripMenuItem.Name = "重打ToolStripMenuItem";
            this.重打ToolStripMenuItem.Size = new System.Drawing.Size(251, 24);
            this.重打ToolStripMenuItem.Text = "重打丢失票 [&R]";
            // 
            // ProcessToolStripMenuItem
            // 
            this.ProcessToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.开发票FToolStripMenuItem,
            this.退票处理RToolStripMenuItem,
            this.toolStripSeparator4,
            this.RefundICCardToolStripMenuItem});
            this.ProcessToolStripMenuItem.Name = "ProcessToolStripMenuItem";
            this.ProcessToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
            this.ProcessToolStripMenuItem.Text = "处理 [&L]";
            // 
            // 开发票FToolStripMenuItem
            // 
            this.开发票FToolStripMenuItem.Name = "开发票FToolStripMenuItem";
            this.开发票FToolStripMenuItem.Size = new System.Drawing.Size(233, 24);
            this.开发票FToolStripMenuItem.Text = "开发票 [&F]";
            // 
            // 退票处理RToolStripMenuItem
            // 
            this.退票处理RToolStripMenuItem.Name = "退票处理RToolStripMenuItem";
            this.退票处理RToolStripMenuItem.Size = new System.Drawing.Size(233, 24);
            this.退票处理RToolStripMenuItem.Text = "退票处理 [&R]";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(232, 6);
            // 
            // RefundICCardToolStripMenuItem
            // 
            this.RefundICCardToolStripMenuItem.Name = "RefundICCardToolStripMenuItem";
            this.RefundICCardToolStripMenuItem.Size = new System.Drawing.Size(233, 24);
            this.RefundICCardToolStripMenuItem.Text = "注销并退IC卡 [&X]";
            this.RefundICCardToolStripMenuItem.Click += new System.EventHandler(this.RefundICCardToolStripMenuItem_Click);
            // 
            // QueryToolStripMenuItem
            // 
            this.QueryToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.查询票信息检票CToolStripMenuItem,
            this.车站信息查询NToolStripMenuItem,
            this.toolStripSeparator2,
            this.QueryICCardToolStripMenuItem});
            this.QueryToolStripMenuItem.Name = "QueryToolStripMenuItem";
            this.QueryToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
            this.QueryToolStripMenuItem.Text = "查询 [&U]";
            // 
            // 查询票信息检票CToolStripMenuItem
            // 
            this.查询票信息检票CToolStripMenuItem.Name = "查询票信息检票CToolStripMenuItem";
            this.查询票信息检票CToolStripMenuItem.Size = new System.Drawing.Size(261, 24);
            this.查询票信息检票CToolStripMenuItem.Text = "查询票信息/检票 [&C]";
            // 
            // 车站信息查询NToolStripMenuItem
            // 
            this.车站信息查询NToolStripMenuItem.Name = "车站信息查询NToolStripMenuItem";
            this.车站信息查询NToolStripMenuItem.Size = new System.Drawing.Size(261, 24);
            this.车站信息查询NToolStripMenuItem.Text = "车站信息查询 [&N]";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(263, 6);
            // 
            // QueryICCardToolStripMenuItem
            // 
            this.QueryICCardToolStripMenuItem.Name = "QueryICCardToolStripMenuItem";
            this.QueryICCardToolStripMenuItem.Size = new System.Drawing.Size(261, 24);
            this.QueryICCardToolStripMenuItem.Text = "查询IC卡信息 [&I]";
            this.QueryICCardToolStripMenuItem.Click += new System.EventHandler(this.QueryICCardToolStripMenuItem_Click);
            // 
            // ShiftChangeToolStripMenuItem
            // 
            this.ShiftChangeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.暂停售票ToolStripMenuItem,
            this.toolStripSeparator1,
            this.修改密码并退出登录XToolStripMenuItem,
            this.LogoutToolStripMenuItem});
            this.ShiftChangeToolStripMenuItem.Name = "ShiftChangeToolStripMenuItem";
            this.ShiftChangeToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
            this.ShiftChangeToolStripMenuItem.Text = "交班 [&X]";
            // 
            // 暂停售票ToolStripMenuItem
            // 
            this.暂停售票ToolStripMenuItem.Name = "暂停售票ToolStripMenuItem";
            this.暂停售票ToolStripMenuItem.Size = new System.Drawing.Size(289, 24);
            this.暂停售票ToolStripMenuItem.Text = "暂停售票 [&Z]";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(293, 6);
            // 
            // 修改密码并退出登录XToolStripMenuItem
            // 
            this.修改密码并退出登录XToolStripMenuItem.Name = "修改密码并退出登录XToolStripMenuItem";
            this.修改密码并退出登录XToolStripMenuItem.Size = new System.Drawing.Size(289, 24);
            this.修改密码并退出登录XToolStripMenuItem.Text = "修改密码并退出登录 [&C]";
            // 
            // LogoutToolStripMenuItem
            // 
            this.LogoutToolStripMenuItem.Name = "LogoutToolStripMenuItem";
            this.LogoutToolStripMenuItem.Size = new System.Drawing.Size(289, 24);
            this.LogoutToolStripMenuItem.Text = "退出登录 [&X]";
            this.LogoutToolStripMenuItem.Click += new System.EventHandler(this.LogoutToolStripMenuItem_Click);
            // 
            // HelpToolStripMenuItem
            // 
            this.HelpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.关于本软件ToolStripMenuItem});
            this.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem";
            this.HelpToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
            this.HelpToolStripMenuItem.Text = "帮助 [&B]";
            // 
            // 关于本软件ToolStripMenuItem
            // 
            this.关于本软件ToolStripMenuItem.Name = "关于本软件ToolStripMenuItem";
            this.关于本软件ToolStripMenuItem.Size = new System.Drawing.Size(192, 30);
            this.关于本软件ToolStripMenuItem.Text = "关于本软件...";
            // 
            // DateTimeMenuItem
            // 
            this.DateTimeMenuItem.Enabled = false;
            this.DateTimeMenuItem.Name = "DateTimeMenuItem";
            this.DateTimeMenuItem.Size = new System.Drawing.Size(169, 29);
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
        private System.Windows.Forms.ToolStripMenuItem 暂停售票ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打印免费出站票EToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 修改密码并退出登录XToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LogoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关于本软件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 开发票FToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 重打ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查询票信息检票CToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 测试服务器链接ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退票处理RToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DisplayCalendarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 车站信息查询NToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem TopUpICCardToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem QueryICCardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 测试打印机PToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem RefundICCardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ICCardReaderSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
    }
}

