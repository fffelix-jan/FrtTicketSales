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
            this.LineSelectFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.ClockUpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.MapSelectionRoundedPanel = new FrtTicketVendingMachine.RoundedPanel();
            this.StationSelectionPanel = new System.Windows.Forms.Panel();
            this.MainFrtFullLineMapControl = new FrtTicketVendingMachine.FrtFullLineMapControl();
            this.RightHandSelectionsRoundedPanel = new FrtTicketVendingMachine.RoundedPanel();
            this.LanguageToggleButton = new FrtTicketVendingMachine.RoundedButton();
            this.WelcomePanel = new System.Windows.Forms.Panel();
            this.ChineseWelcomeLabel = new System.Windows.Forms.Label();
            this.EnglishStationNameLabel = new System.Windows.Forms.Label();
            this.ChineseStationNameLabel = new System.Windows.Forms.Label();
            this.EnglishWelcomeLabel = new System.Windows.Forms.Label();
            this.SelectPaymentMethodPanel = new System.Windows.Forms.Panel();
            this.SelectPaymentMethodLabel = new System.Windows.Forms.Label();
            this.PaymentSelectFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.CashButton = new FrtTicketVendingMachine.RoundedButton();
            this.QRPayButton = new FrtTicketVendingMachine.RoundedButton();
            this.SelectTicketQuantityPanel = new System.Windows.Forms.Panel();
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
            this.AllLinesButton = new FrtTicketVendingMachine.RoundedButton();
            this.Line1Button = new FrtTicketVendingMachine.RoundedButton();
            this.Line2Button = new FrtTicketVendingMachine.RoundedButton();
            this.MainPanel.SuspendLayout();
            this.LineSelectFlowLayoutPanel.SuspendLayout();
            this.MapSelectionRoundedPanel.SuspendLayout();
            this.StationSelectionPanel.SuspendLayout();
            this.RightHandSelectionsRoundedPanel.SuspendLayout();
            this.WelcomePanel.SuspendLayout();
            this.SelectPaymentMethodPanel.SuspendLayout();
            this.PaymentSelectFlowLayoutPanel.SuspendLayout();
            this.SelectTicketQuantityPanel.SuspendLayout();
            this.QuantitySelectFlowLayoutPanel.SuspendLayout();
            this.ClockRoundedPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LogoPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.MainPanel.Controls.Add(this.MapSelectionRoundedPanel);
            this.MainPanel.Controls.Add(this.RightHandSelectionsRoundedPanel);
            this.MainPanel.Controls.Add(this.LineSelectFlowLayoutPanel);
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Margin = new System.Windows.Forms.Padding(10);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(1920, 1080);
            this.MainPanel.TabIndex = 0;
            // 
            // LineSelectFlowLayoutPanel
            // 
            this.LineSelectFlowLayoutPanel.Controls.Add(this.AllLinesButton);
            this.LineSelectFlowLayoutPanel.Controls.Add(this.Line1Button);
            this.LineSelectFlowLayoutPanel.Controls.Add(this.Line2Button);
            this.LineSelectFlowLayoutPanel.Location = new System.Drawing.Point(44, 941);
            this.LineSelectFlowLayoutPanel.Name = "LineSelectFlowLayoutPanel";
            this.LineSelectFlowLayoutPanel.Size = new System.Drawing.Size(1370, 110);
            this.LineSelectFlowLayoutPanel.TabIndex = 2;
            // 
            // ClockUpdateTimer
            // 
            this.ClockUpdateTimer.Enabled = true;
            this.ClockUpdateTimer.Interval = 500;
            this.ClockUpdateTimer.Tick += new System.EventHandler(this.ClockUpdateTimer_Tick);
            // 
            // MapSelectionRoundedPanel
            // 
            this.MapSelectionRoundedPanel.BackColor = System.Drawing.Color.White;
            this.MapSelectionRoundedPanel.BorderColor = System.Drawing.Color.White;
            this.MapSelectionRoundedPanel.Controls.Add(this.StationSelectionPanel);
            this.MapSelectionRoundedPanel.Location = new System.Drawing.Point(30, 30);
            this.MapSelectionRoundedPanel.Name = "MapSelectionRoundedPanel";
            this.MapSelectionRoundedPanel.Radius = 50;
            this.MapSelectionRoundedPanel.Size = new System.Drawing.Size(1384, 930);
            this.MapSelectionRoundedPanel.TabIndex = 0;
            this.MapSelectionRoundedPanel.Thickness = 5F;
            // 
            // StationSelectionPanel
            // 
            this.StationSelectionPanel.Controls.Add(this.MainFrtFullLineMapControl);
            this.StationSelectionPanel.Location = new System.Drawing.Point(0, 20);
            this.StationSelectionPanel.Name = "StationSelectionPanel";
            this.StationSelectionPanel.Size = new System.Drawing.Size(1384, 885);
            this.StationSelectionPanel.TabIndex = 0;
            // 
            // MainFrtFullLineMapControl
            // 
            this.MainFrtFullLineMapControl.Location = new System.Drawing.Point(0, 0);
            this.MainFrtFullLineMapControl.Name = "MainFrtFullLineMapControl";
            this.MainFrtFullLineMapControl.Size = new System.Drawing.Size(1384, 885);
            this.MainFrtFullLineMapControl.TabIndex = 0;
            this.MainFrtFullLineMapControl.StationSelected += new System.EventHandler<FrtTicketVendingMachine.StationSelectedEventArgs>(this.MainFrtFullLineMapControl_StationSelected);
            // 
            // RightHandSelectionsRoundedPanel
            // 
            this.RightHandSelectionsRoundedPanel.BackColor = System.Drawing.Color.DodgerBlue;
            this.RightHandSelectionsRoundedPanel.BorderColor = System.Drawing.Color.White;
            this.RightHandSelectionsRoundedPanel.Controls.Add(this.LanguageToggleButton);
            this.RightHandSelectionsRoundedPanel.Controls.Add(this.WelcomePanel);
            this.RightHandSelectionsRoundedPanel.Controls.Add(this.SelectPaymentMethodPanel);
            this.RightHandSelectionsRoundedPanel.Controls.Add(this.SelectTicketQuantityPanel);
            this.RightHandSelectionsRoundedPanel.Controls.Add(this.ClockRoundedPanel);
            this.RightHandSelectionsRoundedPanel.Controls.Add(this.LogoPictureBox);
            this.RightHandSelectionsRoundedPanel.Location = new System.Drawing.Point(1459, 30);
            this.RightHandSelectionsRoundedPanel.Name = "RightHandSelectionsRoundedPanel";
            this.RightHandSelectionsRoundedPanel.Radius = 50;
            this.RightHandSelectionsRoundedPanel.Size = new System.Drawing.Size(418, 1009);
            this.RightHandSelectionsRoundedPanel.TabIndex = 1;
            this.RightHandSelectionsRoundedPanel.Thickness = 5F;
            // 
            // LanguageToggleButton
            // 
            this.LanguageToggleButton.BackColor = System.Drawing.Color.ForestGreen;
            this.LanguageToggleButton.BorderColor = System.Drawing.Color.Transparent;
            this.LanguageToggleButton.FlatAppearance.BorderSize = 0;
            this.LanguageToggleButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LanguageToggleButton.Font = new System.Drawing.Font("Segoe UI", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LanguageToggleButton.ForeColor = System.Drawing.Color.White;
            this.LanguageToggleButton.Location = new System.Drawing.Point(257, 937);
            this.LanguageToggleButton.Name = "LanguageToggleButton";
            this.LanguageToggleButton.Radius = 25;
            this.LanguageToggleButton.Size = new System.Drawing.Size(144, 57);
            this.LanguageToggleButton.TabIndex = 7;
            this.LanguageToggleButton.TabStop = false;
            this.LanguageToggleButton.Text = "English";
            this.LanguageToggleButton.Thickness = 3F;
            this.LanguageToggleButton.UseVisualStyleBackColor = false;
            this.LanguageToggleButton.Click += new System.EventHandler(this.LanguageToggleButton_Click);
            // 
            // WelcomePanel
            // 
            this.WelcomePanel.Controls.Add(this.ChineseWelcomeLabel);
            this.WelcomePanel.Controls.Add(this.EnglishStationNameLabel);
            this.WelcomePanel.Controls.Add(this.ChineseStationNameLabel);
            this.WelcomePanel.Controls.Add(this.EnglishWelcomeLabel);
            this.WelcomePanel.Location = new System.Drawing.Point(23, 400);
            this.WelcomePanel.Name = "WelcomePanel";
            this.WelcomePanel.Size = new System.Drawing.Size(365, 231);
            this.WelcomePanel.TabIndex = 6;
            // 
            // ChineseWelcomeLabel
            // 
            this.ChineseWelcomeLabel.AutoSize = true;
            this.ChineseWelcomeLabel.Font = new System.Drawing.Font("SimHei", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChineseWelcomeLabel.ForeColor = System.Drawing.Color.White;
            this.ChineseWelcomeLabel.Location = new System.Drawing.Point(120, 26);
            this.ChineseWelcomeLabel.Name = "ChineseWelcomeLabel";
            this.ChineseWelcomeLabel.Size = new System.Drawing.Size(128, 27);
            this.ChineseWelcomeLabel.TabIndex = 7;
            this.ChineseWelcomeLabel.Text = "欢迎光临";
            // 
            // EnglishStationNameLabel
            // 
            this.EnglishStationNameLabel.AutoSize = true;
            this.EnglishStationNameLabel.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EnglishStationNameLabel.ForeColor = System.Drawing.Color.White;
            this.EnglishStationNameLabel.Location = new System.Drawing.Point(14, 165);
            this.EnglishStationNameLabel.Name = "EnglishStationNameLabel";
            this.EnglishStationNameLabel.Size = new System.Drawing.Size(337, 37);
            this.EnglishStationNameLabel.TabIndex = 6;
            this.EnglishStationNameLabel.Text = "Falloway Railway Station";
            // 
            // ChineseStationNameLabel
            // 
            this.ChineseStationNameLabel.AutoSize = true;
            this.ChineseStationNameLabel.Font = new System.Drawing.Font("SimHei", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChineseStationNameLabel.ForeColor = System.Drawing.Color.White;
            this.ChineseStationNameLabel.Location = new System.Drawing.Point(28, 102);
            this.ChineseStationNameLabel.Name = "ChineseStationNameLabel";
            this.ChineseStationNameLabel.Size = new System.Drawing.Size(314, 48);
            this.ChineseStationNameLabel.TabIndex = 5;
            this.ChineseStationNameLabel.Text = "法洛威火车站";
            // 
            // EnglishWelcomeLabel
            // 
            this.EnglishWelcomeLabel.AutoSize = true;
            this.EnglishWelcomeLabel.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EnglishWelcomeLabel.ForeColor = System.Drawing.Color.White;
            this.EnglishWelcomeLabel.Location = new System.Drawing.Point(134, 63);
            this.EnglishWelcomeLabel.Name = "EnglishWelcomeLabel";
            this.EnglishWelcomeLabel.Size = new System.Drawing.Size(93, 25);
            this.EnglishWelcomeLabel.TabIndex = 4;
            this.EnglishWelcomeLabel.Text = "Welcome";
            // 
            // SelectPaymentMethodPanel
            // 
            this.SelectPaymentMethodPanel.Controls.Add(this.SelectPaymentMethodLabel);
            this.SelectPaymentMethodPanel.Controls.Add(this.PaymentSelectFlowLayoutPanel);
            this.SelectPaymentMethodPanel.Location = new System.Drawing.Point(23, 400);
            this.SelectPaymentMethodPanel.Name = "SelectPaymentMethodPanel";
            this.SelectPaymentMethodPanel.Size = new System.Drawing.Size(367, 285);
            this.SelectPaymentMethodPanel.TabIndex = 5;
            // 
            // SelectPaymentMethodLabel
            // 
            this.SelectPaymentMethodLabel.AutoSize = true;
            this.SelectPaymentMethodLabel.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectPaymentMethodLabel.ForeColor = System.Drawing.Color.White;
            this.SelectPaymentMethodLabel.Location = new System.Drawing.Point(12, 0);
            this.SelectPaymentMethodLabel.Name = "SelectPaymentMethodLabel";
            this.SelectPaymentMethodLabel.Size = new System.Drawing.Size(323, 37);
            this.SelectPaymentMethodLabel.TabIndex = 3;
            this.SelectPaymentMethodLabel.Text = "Select Payment Method";
            // 
            // PaymentSelectFlowLayoutPanel
            // 
            this.PaymentSelectFlowLayoutPanel.Controls.Add(this.CashButton);
            this.PaymentSelectFlowLayoutPanel.Controls.Add(this.QRPayButton);
            this.PaymentSelectFlowLayoutPanel.Location = new System.Drawing.Point(19, 44);
            this.PaymentSelectFlowLayoutPanel.Name = "PaymentSelectFlowLayoutPanel";
            this.PaymentSelectFlowLayoutPanel.Size = new System.Drawing.Size(321, 223);
            this.PaymentSelectFlowLayoutPanel.TabIndex = 2;
            // 
            // CashButton
            // 
            this.CashButton.BackColor = System.Drawing.Color.ForestGreen;
            this.CashButton.BorderColor = System.Drawing.Color.Transparent;
            this.CashButton.FlatAppearance.BorderSize = 0;
            this.CashButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CashButton.Font = new System.Drawing.Font("Segoe UI", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CashButton.ForeColor = System.Drawing.Color.White;
            this.CashButton.Location = new System.Drawing.Point(3, 3);
            this.CashButton.Name = "CashButton";
            this.CashButton.Radius = 25;
            this.CashButton.Size = new System.Drawing.Size(314, 100);
            this.CashButton.TabIndex = 4;
            this.CashButton.TabStop = false;
            this.CashButton.Text = "Cash";
            this.CashButton.Thickness = 3F;
            this.CashButton.UseVisualStyleBackColor = false;
            // 
            // QRPayButton
            // 
            this.QRPayButton.BackColor = System.Drawing.Color.ForestGreen;
            this.QRPayButton.BorderColor = System.Drawing.Color.Transparent;
            this.QRPayButton.FlatAppearance.BorderSize = 0;
            this.QRPayButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.QRPayButton.Font = new System.Drawing.Font("Segoe UI", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QRPayButton.ForeColor = System.Drawing.Color.White;
            this.QRPayButton.Location = new System.Drawing.Point(3, 109);
            this.QRPayButton.Name = "QRPayButton";
            this.QRPayButton.Radius = 25;
            this.QRPayButton.Size = new System.Drawing.Size(314, 100);
            this.QRPayButton.TabIndex = 5;
            this.QRPayButton.TabStop = false;
            this.QRPayButton.Text = "FAlipay/FWeChat";
            this.QRPayButton.Thickness = 3F;
            this.QRPayButton.UseVisualStyleBackColor = false;
            // 
            // SelectTicketQuantityPanel
            // 
            this.SelectTicketQuantityPanel.Controls.Add(this.SelectTicketQuantityLabel);
            this.SelectTicketQuantityPanel.Controls.Add(this.QuantitySelectFlowLayoutPanel);
            this.SelectTicketQuantityPanel.Location = new System.Drawing.Point(23, 400);
            this.SelectTicketQuantityPanel.Name = "SelectTicketQuantityPanel";
            this.SelectTicketQuantityPanel.Size = new System.Drawing.Size(367, 285);
            this.SelectTicketQuantityPanel.TabIndex = 4;
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
            // AllLinesButton
            // 
            this.AllLinesButton.BackColor = System.Drawing.Color.Blue;
            this.AllLinesButton.BorderColor = System.Drawing.Color.Transparent;
            this.AllLinesButton.FlatAppearance.BorderSize = 0;
            this.AllLinesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AllLinesButton.Font = new System.Drawing.Font("Segoe UI", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AllLinesButton.ForeColor = System.Drawing.Color.White;
            this.AllLinesButton.Location = new System.Drawing.Point(3, 3);
            this.AllLinesButton.Name = "AllLinesButton";
            this.AllLinesButton.Radius = 25;
            this.AllLinesButton.Size = new System.Drawing.Size(160, 103);
            this.AllLinesButton.TabIndex = 6;
            this.AllLinesButton.TabStop = false;
            this.AllLinesButton.Text = "All Lines";
            this.AllLinesButton.Thickness = 3F;
            this.AllLinesButton.UseVisualStyleBackColor = false;
            this.AllLinesButton.Click += new System.EventHandler(this.AllLinesButton_Click);
            // 
            // Line1Button
            // 
            this.Line1Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(23)))), ((int)(((byte)(21)))));
            this.Line1Button.BorderColor = System.Drawing.Color.Transparent;
            this.Line1Button.FlatAppearance.BorderSize = 0;
            this.Line1Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Line1Button.Font = new System.Drawing.Font("Segoe UI", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Line1Button.ForeColor = System.Drawing.Color.White;
            this.Line1Button.Location = new System.Drawing.Point(169, 3);
            this.Line1Button.Name = "Line1Button";
            this.Line1Button.Radius = 25;
            this.Line1Button.Size = new System.Drawing.Size(160, 103);
            this.Line1Button.TabIndex = 7;
            this.Line1Button.TabStop = false;
            this.Line1Button.Text = "Line 1";
            this.Line1Button.Thickness = 3F;
            this.Line1Button.UseVisualStyleBackColor = false;
            this.Line1Button.Click += new System.EventHandler(this.Line1Button_Click);
            // 
            // Line2Button
            // 
            this.Line2Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(177)))), ((int)(((byte)(106)))));
            this.Line2Button.BorderColor = System.Drawing.Color.Transparent;
            this.Line2Button.FlatAppearance.BorderSize = 0;
            this.Line2Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Line2Button.Font = new System.Drawing.Font("Segoe UI", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Line2Button.ForeColor = System.Drawing.Color.White;
            this.Line2Button.Location = new System.Drawing.Point(335, 3);
            this.Line2Button.Name = "Line2Button";
            this.Line2Button.Radius = 25;
            this.Line2Button.Size = new System.Drawing.Size(160, 103);
            this.Line2Button.TabIndex = 8;
            this.Line2Button.TabStop = false;
            this.Line2Button.Text = "Line 2";
            this.Line2Button.Thickness = 3F;
            this.Line2Button.UseVisualStyleBackColor = false;
            this.Line2Button.Click += new System.EventHandler(this.Line2Button_Click);
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
            this.LineSelectFlowLayoutPanel.ResumeLayout(false);
            this.MapSelectionRoundedPanel.ResumeLayout(false);
            this.StationSelectionPanel.ResumeLayout(false);
            this.RightHandSelectionsRoundedPanel.ResumeLayout(false);
            this.WelcomePanel.ResumeLayout(false);
            this.WelcomePanel.PerformLayout();
            this.SelectPaymentMethodPanel.ResumeLayout(false);
            this.SelectPaymentMethodPanel.PerformLayout();
            this.PaymentSelectFlowLayoutPanel.ResumeLayout(false);
            this.SelectTicketQuantityPanel.ResumeLayout(false);
            this.SelectTicketQuantityPanel.PerformLayout();
            this.QuantitySelectFlowLayoutPanel.ResumeLayout(false);
            this.ClockRoundedPanel.ResumeLayout(false);
            this.ClockRoundedPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LogoPictureBox)).EndInit();
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
        private System.Windows.Forms.Panel SelectTicketQuantityPanel;
        private System.Windows.Forms.Timer ClockUpdateTimer;
        private System.Windows.Forms.Panel StationSelectionPanel;
        private System.Windows.Forms.Panel SelectPaymentMethodPanel;
        private System.Windows.Forms.Label SelectPaymentMethodLabel;
        private System.Windows.Forms.FlowLayoutPanel PaymentSelectFlowLayoutPanel;
        private RoundedButton CashButton;
        private RoundedButton QRPayButton;
        private System.Windows.Forms.Panel WelcomePanel;
        private System.Windows.Forms.Label ChineseWelcomeLabel;
        private System.Windows.Forms.Label EnglishStationNameLabel;
        private System.Windows.Forms.Label ChineseStationNameLabel;
        private System.Windows.Forms.Label EnglishWelcomeLabel;
        private System.Windows.Forms.FlowLayoutPanel LineSelectFlowLayoutPanel;
        private RoundedButton AllLinesButton;
        private RoundedButton Line1Button;
        private RoundedButton Line2Button;
        private FrtFullLineMapControl MainFrtFullLineMapControl;
        private RoundedButton LanguageToggleButton;
    }
}

