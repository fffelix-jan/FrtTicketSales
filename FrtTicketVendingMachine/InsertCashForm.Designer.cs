namespace FrtTicketVendingMachine
{
    partial class InsertCashForm
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
            this.titleLabel = new System.Windows.Forms.Label();
            this.amountNeededLabel = new System.Windows.Forms.Label();
            this.insertedAmountLabel = new System.Windows.Forms.Label();
            this.remainingAmountLabel = new System.Windows.Forms.Label();
            this.ejectedAmountLabel = new System.Windows.Forms.Label();
            this.oneYuanButton = new System.Windows.Forms.Button();
            this.fiveYuanButton = new System.Windows.Forms.Button();
            this.tenYuanButton = new System.Windows.Forms.Button();
            this.statusLabel = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.Location = new System.Drawing.Point(12, 15);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(193, 22);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Simulated Cash Acceptor";
            // 
            // amountNeededLabel
            // 
            this.amountNeededLabel.AutoSize = true;
            this.amountNeededLabel.Font = new System.Drawing.Font("Microsoft YaHei", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.amountNeededLabel.Location = new System.Drawing.Point(12, 50);
            this.amountNeededLabel.Name = "amountNeededLabel";
            this.amountNeededLabel.Size = new System.Drawing.Size(115, 20);
            this.amountNeededLabel.TabIndex = 1;
            this.amountNeededLabel.Text = "Amount Needed:";
            // 
            // insertedAmountLabel
            // 
            this.insertedAmountLabel.AutoSize = true;
            this.insertedAmountLabel.Font = new System.Drawing.Font("Microsoft YaHei", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.insertedAmountLabel.Location = new System.Drawing.Point(12, 75);
            this.insertedAmountLabel.Name = "insertedAmountLabel";
            this.insertedAmountLabel.Size = new System.Drawing.Size(132, 20);
            this.insertedAmountLabel.TabIndex = 2;
            this.insertedAmountLabel.Text = "Amount Inserted: ¥0";
            // 
            // remainingAmountLabel
            // 
            this.remainingAmountLabel.AutoSize = true;
            this.remainingAmountLabel.Font = new System.Drawing.Font("Microsoft YaHei", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.remainingAmountLabel.ForeColor = System.Drawing.Color.Red;
            this.remainingAmountLabel.Location = new System.Drawing.Point(12, 100);
            this.remainingAmountLabel.Name = "remainingAmountLabel";
            this.remainingAmountLabel.Size = new System.Drawing.Size(148, 19);
            this.remainingAmountLabel.TabIndex = 3;
            this.remainingAmountLabel.Text = "Amount Remaining:";
            // 
            // ejectedAmountLabel
            // 
            this.ejectedAmountLabel.AutoSize = true;
            this.ejectedAmountLabel.Font = new System.Drawing.Font("Microsoft YaHei", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ejectedAmountLabel.ForeColor = System.Drawing.Color.Orange;
            this.ejectedAmountLabel.Location = new System.Drawing.Point(12, 125);
            this.ejectedAmountLabel.Name = "ejectedAmountLabel";
            this.ejectedAmountLabel.Size = new System.Drawing.Size(0, 19);
            this.ejectedAmountLabel.TabIndex = 4;
            this.ejectedAmountLabel.Visible = false;
            // 
            // oneYuanButton
            // 
            this.oneYuanButton.BackColor = System.Drawing.Color.LightGreen;
            this.oneYuanButton.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.oneYuanButton.Location = new System.Drawing.Point(300, 50);
            this.oneYuanButton.Name = "oneYuanButton";
            this.oneYuanButton.Size = new System.Drawing.Size(80, 60);
            this.oneYuanButton.TabIndex = 5;
            this.oneYuanButton.Text = "Insert\r\n¥1";
            this.oneYuanButton.UseVisualStyleBackColor = false;
            this.oneYuanButton.Click += new System.EventHandler(this.OneYuanButton_Click);
            // 
            // fiveYuanButton
            // 
            this.fiveYuanButton.BackColor = System.Drawing.Color.LightBlue;
            this.fiveYuanButton.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fiveYuanButton.Location = new System.Drawing.Point(390, 50);
            this.fiveYuanButton.Name = "fiveYuanButton";
            this.fiveYuanButton.Size = new System.Drawing.Size(80, 60);
            this.fiveYuanButton.TabIndex = 6;
            this.fiveYuanButton.Text = "Insert\r\n¥5";
            this.fiveYuanButton.UseVisualStyleBackColor = false;
            this.fiveYuanButton.Click += new System.EventHandler(this.FiveYuanButton_Click);
            // 
            // tenYuanButton
            // 
            this.tenYuanButton.BackColor = System.Drawing.Color.LightCoral;
            this.tenYuanButton.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tenYuanButton.Location = new System.Drawing.Point(480, 50);
            this.tenYuanButton.Name = "tenYuanButton";
            this.tenYuanButton.Size = new System.Drawing.Size(80, 60);
            this.tenYuanButton.TabIndex = 7;
            this.tenYuanButton.Text = "Insert\r\n¥10";
            this.tenYuanButton.UseVisualStyleBackColor = false;
            this.tenYuanButton.Click += new System.EventHandler(this.TenYuanButton_Click);
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusLabel.ForeColor = System.Drawing.Color.Gray;
            this.statusLabel.Location = new System.Drawing.Point(12, 150);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(150, 17);
            this.statusLabel.TabIndex = 8;
            this.statusLabel.Text = "Insert cash to continue...";
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.Color.LightGray;
            this.cancelButton.Font = new System.Drawing.Font("Microsoft YaHei", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.Location = new System.Drawing.Point(300, 145);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(260, 25);
            this.cancelButton.TabIndex = 9;
            this.cancelButton.Text = "Cancel Payment";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // InsertCashForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(580, 185);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.tenYuanButton);
            this.Controls.Add(this.fiveYuanButton);
            this.Controls.Add(this.oneYuanButton);
            this.Controls.Add(this.ejectedAmountLabel);
            this.Controls.Add(this.remainingAmountLabel);
            this.Controls.Add(this.insertedAmountLabel);
            this.Controls.Add(this.amountNeededLabel);
            this.Controls.Add(this.titleLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InsertCashForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Simulated Cash Acceptor";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label amountNeededLabel;
        private System.Windows.Forms.Label insertedAmountLabel;
        private System.Windows.Forms.Label remainingAmountLabel;
        private System.Windows.Forms.Label ejectedAmountLabel;
        private System.Windows.Forms.Button oneYuanButton;
        private System.Windows.Forms.Button fiveYuanButton;
        private System.Windows.Forms.Button tenYuanButton;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Button cancelButton;
    }
}