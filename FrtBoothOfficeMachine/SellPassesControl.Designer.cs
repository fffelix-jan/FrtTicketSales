namespace FrtBoothOfficeMachine
{
    partial class SellPassesControl
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
            this.TitleLabel = new System.Windows.Forms.Label();
            this.MainLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.QuantitySelectionPanel = new System.Windows.Forms.Panel();
            this.F1Label = new System.Windows.Forms.Label();
            this.DayPassQuantityTextBox = new System.Windows.Forms.TextBox();
            this.DayPassInfoPanel = new System.Windows.Forms.Panel();
            this.DayPassPriceLabel = new System.Windows.Forms.Label();
            this.DayPassDescriptionLabel = new System.Windows.Forms.Label();
            this.PriceSummaryPanel = new System.Windows.Forms.Panel();
            this.TotalPriceLabel = new System.Windows.Forms.Label();
            this.QuantityPriceLabel = new System.Windows.Forms.Label();
            this.PaymentButtonsPanel = new System.Windows.Forms.Panel();
            this.CancelButton = new System.Windows.Forms.Button();
            this.CardPaymentButton = new System.Windows.Forms.Button();
            this.CashPaymentGroupBox = new System.Windows.Forms.GroupBox();
            this.ChangeLabel = new System.Windows.Forms.Label();
            this.CashPaymentTenderedTextBox = new System.Windows.Forms.TextBox();
            this.MainLayoutPanel.SuspendLayout();
            this.QuantitySelectionPanel.SuspendLayout();
            this.DayPassInfoPanel.SuspendLayout();
            this.PriceSummaryPanel.SuspendLayout();
            this.PaymentButtonsPanel.SuspendLayout();
            this.CashPaymentGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Font = new System.Drawing.Font("SimSun", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleLabel.Location = new System.Drawing.Point(20, 20);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(147, 33);
            this.TitleLabel.TabIndex = 0;
            this.TitleLabel.Text = "售定期票";
            // 
            // MainLayoutPanel
            // 
            this.MainLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainLayoutPanel.ColumnCount = 2;
            this.MainLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.MainLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.MainLayoutPanel.Controls.Add(this.QuantitySelectionPanel, 0, 0);
            this.MainLayoutPanel.Controls.Add(this.DayPassInfoPanel, 1, 0);
            this.MainLayoutPanel.Location = new System.Drawing.Point(25, 70);
            this.MainLayoutPanel.Name = "MainLayoutPanel";
            this.MainLayoutPanel.RowCount = 1;
            this.MainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainLayoutPanel.Size = new System.Drawing.Size(734, 280);
            this.MainLayoutPanel.TabIndex = 1;
            // 
            // QuantitySelectionPanel
            // 
            this.QuantitySelectionPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.QuantitySelectionPanel.Controls.Add(this.F1Label);
            this.QuantitySelectionPanel.Controls.Add(this.DayPassQuantityTextBox);
            this.QuantitySelectionPanel.Location = new System.Drawing.Point(3, 3);
            this.QuantitySelectionPanel.Name = "QuantitySelectionPanel";
            this.QuantitySelectionPanel.Size = new System.Drawing.Size(434, 274);
            this.QuantitySelectionPanel.TabIndex = 0;
            // 
            // F1Label
            // 
            this.F1Label.AutoSize = true;
            this.F1Label.Font = new System.Drawing.Font("SimSun", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.F1Label.Location = new System.Drawing.Point(20, 30);
            this.F1Label.Name = "F1Label";
            this.F1Label.Size = new System.Drawing.Size(187, 24);
            this.F1Label.TabIndex = 0;
            this.F1Label.Text = "F1. 一日票数量";
            // 
            // DayPassQuantityTextBox
            // 
            this.DayPassQuantityTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DayPassQuantityTextBox.Font = new System.Drawing.Font("Arial", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DayPassQuantityTextBox.Location = new System.Drawing.Point(20, 80);
            this.DayPassQuantityTextBox.MaxLength = 2;
            this.DayPassQuantityTextBox.Name = "DayPassQuantityTextBox";
            this.DayPassQuantityTextBox.Size = new System.Drawing.Size(394, 81);
            this.DayPassQuantityTextBox.TabIndex = 0;
            this.DayPassQuantityTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // DayPassInfoPanel
            // 
            this.DayPassInfoPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DayPassInfoPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(250)))));
            this.DayPassInfoPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DayPassInfoPanel.Controls.Add(this.DayPassPriceLabel);
            this.DayPassInfoPanel.Controls.Add(this.DayPassDescriptionLabel);
            this.DayPassInfoPanel.Location = new System.Drawing.Point(443, 3);
            this.DayPassInfoPanel.Name = "DayPassInfoPanel";
            this.DayPassInfoPanel.Size = new System.Drawing.Size(288, 274);
            this.DayPassInfoPanel.TabIndex = 1;
            // 
            // DayPassPriceLabel
            // 
            this.DayPassPriceLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DayPassPriceLabel.Font = new System.Drawing.Font("Arial", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DayPassPriceLabel.ForeColor = System.Drawing.Color.DarkBlue;
            this.DayPassPriceLabel.Location = new System.Drawing.Point(10, 30);
            this.DayPassPriceLabel.Name = "DayPassPriceLabel";
            this.DayPassPriceLabel.Size = new System.Drawing.Size(266, 50);
            this.DayPassPriceLabel.TabIndex = 0;
            this.DayPassPriceLabel.Text = "¥20.00";
            this.DayPassPriceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DayPassDescriptionLabel
            // 
            this.DayPassDescriptionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DayPassDescriptionLabel.Font = new System.Drawing.Font("SimSun", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DayPassDescriptionLabel.Location = new System.Drawing.Point(10, 90);
            this.DayPassDescriptionLabel.Name = "DayPassDescriptionLabel";
            this.DayPassDescriptionLabel.Size = new System.Drawing.Size(266, 170);
            this.DayPassDescriptionLabel.TabIndex = 1;
            this.DayPassDescriptionLabel.Text = "一日票\r\n\r\n• 当日有效\r\n• 不限乘车次数\r\n• 全线通用\r\n• 限本人使用\r\n\r\n适用于观光旅游\r\n和多次出行";
            // 
            // PriceSummaryPanel
            // 
            this.PriceSummaryPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.PriceSummaryPanel.Controls.Add(this.TotalPriceLabel);
            this.PriceSummaryPanel.Controls.Add(this.QuantityPriceLabel);
            this.PriceSummaryPanel.Location = new System.Drawing.Point(470, 365);
            this.PriceSummaryPanel.Name = "PriceSummaryPanel";
            this.PriceSummaryPanel.Size = new System.Drawing.Size(289, 80);
            this.PriceSummaryPanel.TabIndex = 2;
            // 
            // TotalPriceLabel
            // 
            this.TotalPriceLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TotalPriceLabel.AutoSize = true;
            this.TotalPriceLabel.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalPriceLabel.ForeColor = System.Drawing.Color.DarkGreen;
            this.TotalPriceLabel.Location = new System.Drawing.Point(150, 40);
            this.TotalPriceLabel.Name = "TotalPriceLabel";
            this.TotalPriceLabel.Size = new System.Drawing.Size(98, 37);
            this.TotalPriceLabel.TabIndex = 1;
            this.TotalPriceLabel.Text = "¥0.00";
            // 
            // QuantityPriceLabel
            // 
            this.QuantityPriceLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.QuantityPriceLabel.AutoSize = true;
            this.QuantityPriceLabel.Font = new System.Drawing.Font("SimSun", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QuantityPriceLabel.Location = new System.Drawing.Point(150, 10);
            this.QuantityPriceLabel.Name = "QuantityPriceLabel";
            this.QuantityPriceLabel.Size = new System.Drawing.Size(120, 22);
            this.QuantityPriceLabel.TabIndex = 0;
            this.QuantityPriceLabel.Text = "0 x ¥20.00";
            // 
            // PaymentButtonsPanel
            // 
            this.PaymentButtonsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.PaymentButtonsPanel.Controls.Add(this.CancelButton);
            this.PaymentButtonsPanel.Controls.Add(this.CardPaymentButton);
            this.PaymentButtonsPanel.Location = new System.Drawing.Point(25, 455);
            this.PaymentButtonsPanel.Name = "PaymentButtonsPanel";
            this.PaymentButtonsPanel.Size = new System.Drawing.Size(340, 75);
            this.PaymentButtonsPanel.TabIndex = 3;
            // 
            // CancelButton
            // 
            this.CancelButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.CancelButton.Font = new System.Drawing.Font("SimSun", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelButton.ForeColor = System.Drawing.Color.Red;
            this.CancelButton.Location = new System.Drawing.Point(0, 0);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(120, 75);
            this.CancelButton.TabIndex = 0;
            this.CancelButton.Text = "取消\r\n[Alt+E]";
            this.CancelButton.UseVisualStyleBackColor = false;
            // 
            // CardPaymentButton
            // 
            this.CardPaymentButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.CardPaymentButton.Font = new System.Drawing.Font("SimSun", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CardPaymentButton.Location = new System.Drawing.Point(130, 0);
            this.CardPaymentButton.Name = "CardPaymentButton";
            this.CardPaymentButton.Size = new System.Drawing.Size(140, 75);
            this.CardPaymentButton.TabIndex = 1;
            this.CardPaymentButton.Text = "刷卡\r\n[Ctrl+4]";
            this.CardPaymentButton.UseVisualStyleBackColor = false;
            // 
            // CashPaymentGroupBox
            // 
            this.CashPaymentGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CashPaymentGroupBox.Controls.Add(this.ChangeLabel);
            this.CashPaymentGroupBox.Controls.Add(this.CashPaymentTenderedTextBox);
            this.CashPaymentGroupBox.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CashPaymentGroupBox.Location = new System.Drawing.Point(380, 455);
            this.CashPaymentGroupBox.Name = "CashPaymentGroupBox";
            this.CashPaymentGroupBox.Size = new System.Drawing.Size(379, 75);
            this.CashPaymentGroupBox.TabIndex = 4;
            this.CashPaymentGroupBox.TabStop = false;
            this.CashPaymentGroupBox.Text = "现金支付 [Ctrl+1]";
            // 
            // ChangeLabel
            // 
            this.ChangeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ChangeLabel.AutoSize = true;
            this.ChangeLabel.Location = new System.Drawing.Point(250, 35);
            this.ChangeLabel.Name = "ChangeLabel";
            this.ChangeLabel.Size = new System.Drawing.Size(126, 19);
            this.ChangeLabel.TabIndex = 1;
            this.ChangeLabel.Text = "找零: ¥0.00";
            // 
            // CashPaymentTenderedTextBox
            // 
            this.CashPaymentTenderedTextBox.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CashPaymentTenderedTextBox.Location = new System.Drawing.Point(10, 30);
            this.CashPaymentTenderedTextBox.Name = "CashPaymentTenderedTextBox";
            this.CashPaymentTenderedTextBox.Size = new System.Drawing.Size(230, 38);
            this.CashPaymentTenderedTextBox.TabIndex = 0;
            // 
            // SellPassesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(199)))), ((int)(((byte)(199)))));
            this.Controls.Add(this.CashPaymentGroupBox);
            this.Controls.Add(this.PaymentButtonsPanel);
            this.Controls.Add(this.PriceSummaryPanel);
            this.Controls.Add(this.MainLayoutPanel);
            this.Controls.Add(this.TitleLabel);
            this.Name = "SellPassesControl";
            this.Size = new System.Drawing.Size(784, 550);
            this.VisibleChanged += new System.EventHandler(this.SellPassesControl_VisibleChanged);
            this.MainLayoutPanel.ResumeLayout(false);
            this.QuantitySelectionPanel.ResumeLayout(false);
            this.QuantitySelectionPanel.PerformLayout();
            this.DayPassInfoPanel.ResumeLayout(false);
            this.PriceSummaryPanel.ResumeLayout(false);
            this.PriceSummaryPanel.PerformLayout();
            this.PaymentButtonsPanel.ResumeLayout(false);
            this.CashPaymentGroupBox.ResumeLayout(false);
            this.CashPaymentGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.TableLayoutPanel MainLayoutPanel;
        private System.Windows.Forms.Panel QuantitySelectionPanel;
        private System.Windows.Forms.Label F1Label;
        private System.Windows.Forms.TextBox DayPassQuantityTextBox;
        private System.Windows.Forms.Panel DayPassInfoPanel;
        private System.Windows.Forms.Label DayPassPriceLabel;
        private System.Windows.Forms.Label DayPassDescriptionLabel;
        private System.Windows.Forms.Panel PriceSummaryPanel;
        private System.Windows.Forms.Label TotalPriceLabel;
        private System.Windows.Forms.Label QuantityPriceLabel;
        private System.Windows.Forms.Panel PaymentButtonsPanel;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button CardPaymentButton;
        private System.Windows.Forms.GroupBox CashPaymentGroupBox;
        private System.Windows.Forms.Label ChangeLabel;
        private System.Windows.Forms.TextBox CashPaymentTenderedTextBox;
    }
}