namespace FrtBoothOfficeMachine
{
    partial class CardPaymentDialogForm
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
            this.TitleLabel = new System.Windows.Forms.Label();
            this.PaymentTypeLabel = new System.Windows.Forms.Label();
            this.PaymentTypeComboBox = new System.Windows.Forms.ComboBox();
            this.AmountLabel = new System.Windows.Forms.Label();
            this.AmountDisplayLabel = new System.Windows.Forms.Label();
            this.InstructionLabel = new System.Windows.Forms.Label();
            this.UserCancelButton = new System.Windows.Forms.Button();
            this.ProcessPaymentButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Font = new System.Drawing.Font("SimSun", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TitleLabel.Location = new System.Drawing.Point(12, 9);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(141, 27);
            this.TitleLabel.TabIndex = 0;
            this.TitleLabel.Text = "POS机支付";
            // 
            // PaymentTypeLabel
            // 
            this.PaymentTypeLabel.AutoSize = true;
            this.PaymentTypeLabel.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PaymentTypeLabel.Location = new System.Drawing.Point(79, 65);
            this.PaymentTypeLabel.Name = "PaymentTypeLabel";
            this.PaymentTypeLabel.Size = new System.Drawing.Size(85, 19);
            this.PaymentTypeLabel.TabIndex = 1;
            this.PaymentTypeLabel.Text = "支付方式";
            // 
            // PaymentTypeComboBox
            // 
            this.PaymentTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PaymentTypeComboBox.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PaymentTypeComboBox.FormattingEnabled = true;
            this.PaymentTypeComboBox.Items.AddRange(new object[] {
            "伪信支付",
            "假付宝",
            "银联"});
            this.PaymentTypeComboBox.Location = new System.Drawing.Point(180, 62);
            this.PaymentTypeComboBox.Name = "PaymentTypeComboBox";
            this.PaymentTypeComboBox.Size = new System.Drawing.Size(280, 27);
            this.PaymentTypeComboBox.TabIndex = 2;
            // 
            // AmountLabel
            // 
            this.AmountLabel.AutoSize = true;
            this.AmountLabel.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AmountLabel.Location = new System.Drawing.Point(79, 110);
            this.AmountLabel.Name = "AmountLabel";
            this.AmountLabel.Size = new System.Drawing.Size(85, 19);
            this.AmountLabel.TabIndex = 3;
            this.AmountLabel.Text = "支付金额";
            // 
            // AmountDisplayLabel
            // 
            this.AmountDisplayLabel.AutoSize = true;
            this.AmountDisplayLabel.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AmountDisplayLabel.ForeColor = System.Drawing.Color.DarkBlue;
            this.AmountDisplayLabel.Location = new System.Drawing.Point(175, 103);
            this.AmountDisplayLabel.Name = "AmountDisplayLabel";
            this.AmountDisplayLabel.Size = new System.Drawing.Size(72, 29);
            this.AmountDisplayLabel.TabIndex = 4;
            this.AmountDisplayLabel.Text = "¥0.00";
            // 
            // InstructionLabel
            // 
            this.InstructionLabel.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.InstructionLabel.ForeColor = System.Drawing.Color.DarkRed;
            this.InstructionLabel.Location = new System.Drawing.Point(17, 150);
            this.InstructionLabel.Name = "InstructionLabel";
            this.InstructionLabel.Size = new System.Drawing.Size(460, 40);
            this.InstructionLabel.TabIndex = 5;
            this.InstructionLabel.Text = "请选择支付方式，然后点击\"进行POS机支付\"按钮。\r\n收款员将使用POS机处理顾客的电子支付。";
            this.InstructionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UserCancelButton
            // 
            this.UserCancelButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.UserCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.UserCancelButton.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.UserCancelButton.Location = new System.Drawing.Point(50, 210);
            this.UserCancelButton.Name = "UserCancelButton";
            this.UserCancelButton.Size = new System.Drawing.Size(120, 35);
            this.UserCancelButton.TabIndex = 6;
            this.UserCancelButton.Text = "取消";
            this.UserCancelButton.UseVisualStyleBackColor = false;
            // 
            // ProcessPaymentButton
            // 
            this.ProcessPaymentButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ProcessPaymentButton.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ProcessPaymentButton.Location = new System.Drawing.Point(280, 210);
            this.ProcessPaymentButton.Name = "ProcessPaymentButton";
            this.ProcessPaymentButton.Size = new System.Drawing.Size(180, 35);
            this.ProcessPaymentButton.TabIndex = 7;
            this.ProcessPaymentButton.Text = "进行POS机支付";
            this.ProcessPaymentButton.UseVisualStyleBackColor = false;
            this.ProcessPaymentButton.Click += new System.EventHandler(this.ProcessPaymentButton_Click);
            // 
            // CardPaymentDialogForm
            // 
            this.AcceptButton = this.ProcessPaymentButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(199)))), ((int)(((byte)(199)))));
            this.ClientSize = new System.Drawing.Size(500, 270);
            this.Controls.Add(this.ProcessPaymentButton);
            this.Controls.Add(this.UserCancelButton);
            this.Controls.Add(this.InstructionLabel);
            this.Controls.Add(this.AmountDisplayLabel);
            this.Controls.Add(this.AmountLabel);
            this.Controls.Add(this.PaymentTypeComboBox);
            this.Controls.Add(this.PaymentTypeLabel);
            this.Controls.Add(this.TitleLabel);
            this.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CardPaymentDialogForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "POS机支付";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.Label PaymentTypeLabel;
        private System.Windows.Forms.ComboBox PaymentTypeComboBox;
        private System.Windows.Forms.Label AmountLabel;
        private System.Windows.Forms.Label AmountDisplayLabel;
        private System.Windows.Forms.Label InstructionLabel;
        private System.Windows.Forms.Button UserCancelButton;
        private System.Windows.Forms.Button ProcessPaymentButton;
    }
}