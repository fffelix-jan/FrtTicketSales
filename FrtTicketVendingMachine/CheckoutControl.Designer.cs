namespace FrtTicketVendingMachine
{
    partial class CheckoutControl
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
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.TotalPriceLabel = new System.Windows.Forms.Label();
            this.QuantityLabel = new System.Windows.Forms.Label();
            this.DestinationLabel = new System.Windows.Forms.Label();
            this.PriceTitleLabel = new System.Windows.Forms.Label();
            this.QuantityTitleLabel = new System.Windows.Forms.Label();
            this.DestinationTitleLabel = new System.Windows.Forms.Label();
            this.InstructionsLabel = new System.Windows.Forms.Label();
            this.InstructionsPictureBox = new System.Windows.Forms.PictureBox();
            this.AnimationTimer = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.InstructionsPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.98137F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52.01863F));
            this.tableLayoutPanel1.Controls.Add(this.TotalPriceLabel, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.QuantityLabel, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.DestinationLabel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.PriceTitleLabel, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.QuantityTitleLabel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.DestinationTitleLabel, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(600, 172);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 113F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(752, 375);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // TotalPriceLabel
            // 
            this.TotalPriceLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.TotalPriceLabel.AutoSize = true;
            this.TotalPriceLabel.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalPriceLabel.ForeColor = System.Drawing.Color.Black;
            this.TotalPriceLabel.Location = new System.Drawing.Point(363, 286);
            this.TotalPriceLabel.Name = "TotalPriceLabel";
            this.TotalPriceLabel.Size = new System.Drawing.Size(269, 65);
            this.TotalPriceLabel.TabIndex = 8;
            this.TotalPriceLabel.Text = "Destination";
            // 
            // QuantityLabel
            // 
            this.QuantityLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.QuantityLabel.AutoSize = true;
            this.QuantityLabel.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QuantityLabel.ForeColor = System.Drawing.Color.Black;
            this.QuantityLabel.Location = new System.Drawing.Point(363, 164);
            this.QuantityLabel.Name = "QuantityLabel";
            this.QuantityLabel.Size = new System.Drawing.Size(269, 65);
            this.QuantityLabel.TabIndex = 7;
            this.QuantityLabel.Text = "Destination";
            // 
            // DestinationLabel
            // 
            this.DestinationLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.DestinationLabel.AutoSize = true;
            this.DestinationLabel.Font = new System.Drawing.Font("Microsoft YaHei", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DestinationLabel.ForeColor = System.Drawing.Color.Black;
            this.DestinationLabel.Location = new System.Drawing.Point(363, 34);
            this.DestinationLabel.Name = "DestinationLabel";
            this.DestinationLabel.Size = new System.Drawing.Size(251, 62);
            this.DestinationLabel.TabIndex = 6;
            this.DestinationLabel.Text = "Xinggang";
            // 
            // PriceTitleLabel
            // 
            this.PriceTitleLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.PriceTitleLabel.AutoSize = true;
            this.PriceTitleLabel.Font = new System.Drawing.Font("Microsoft YaHei", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PriceTitleLabel.ForeColor = System.Drawing.Color.Red;
            this.PriceTitleLabel.Location = new System.Drawing.Point(73, 286);
            this.PriceTitleLabel.Name = "PriceTitleLabel";
            this.PriceTitleLabel.Size = new System.Drawing.Size(284, 64);
            this.PriceTitleLabel.TabIndex = 5;
            this.PriceTitleLabel.Text = "Total Price";
            // 
            // QuantityTitleLabel
            // 
            this.QuantityTitleLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.QuantityTitleLabel.AutoSize = true;
            this.QuantityTitleLabel.Font = new System.Drawing.Font("Microsoft YaHei", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QuantityTitleLabel.ForeColor = System.Drawing.Color.Red;
            this.QuantityTitleLabel.Location = new System.Drawing.Point(119, 164);
            this.QuantityTitleLabel.Name = "QuantityTitleLabel";
            this.QuantityTitleLabel.Size = new System.Drawing.Size(238, 64);
            this.QuantityTitleLabel.TabIndex = 2;
            this.QuantityTitleLabel.Text = "Quantity";
            // 
            // DestinationTitleLabel
            // 
            this.DestinationTitleLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.DestinationTitleLabel.AutoSize = true;
            this.DestinationTitleLabel.Font = new System.Drawing.Font("Microsoft YaHei", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DestinationTitleLabel.ForeColor = System.Drawing.Color.Red;
            this.DestinationTitleLabel.Location = new System.Drawing.Point(50, 33);
            this.DestinationTitleLabel.Name = "DestinationTitleLabel";
            this.DestinationTitleLabel.Size = new System.Drawing.Size(307, 64);
            this.DestinationTitleLabel.TabIndex = 0;
            this.DestinationTitleLabel.Text = "Destination";
            // 
            // InstructionsLabel
            // 
            this.InstructionsLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.InstructionsLabel.AutoSize = true;
            this.InstructionsLabel.Font = new System.Drawing.Font("Microsoft YaHei", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InstructionsLabel.ForeColor = System.Drawing.Color.Red;
            this.InstructionsLabel.Location = new System.Drawing.Point(38, 603);
            this.InstructionsLabel.Name = "InstructionsLabel";
            this.InstructionsLabel.Size = new System.Drawing.Size(390, 64);
            this.InstructionsLabel.TabIndex = 9;
            this.InstructionsLabel.Text = "Your Text Here";
            // 
            // InstructionsPictureBox
            // 
            this.InstructionsPictureBox.Image = global::FrtTicketVendingMachine.Properties.Resources.HandButton1;
            this.InstructionsPictureBox.Location = new System.Drawing.Point(49, 172);
            this.InstructionsPictureBox.Name = "InstructionsPictureBox";
            this.InstructionsPictureBox.Size = new System.Drawing.Size(497, 375);
            this.InstructionsPictureBox.TabIndex = 10;
            this.InstructionsPictureBox.TabStop = false;
            // 
            // AnimationTimer
            // 
            this.AnimationTimer.Interval = 500;
            this.AnimationTimer.Tick += new System.EventHandler(this.AnimationTimer_Tick);
            // 
            // CheckoutControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.InstructionsPictureBox);
            this.Controls.Add(this.InstructionsLabel);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "CheckoutControl";
            this.Size = new System.Drawing.Size(1384, 885);
            this.VisibleChanged += new System.EventHandler(this.CheckoutControl_VisibleChanged);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.InstructionsPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label DestinationTitleLabel;
        private System.Windows.Forms.Label TotalPriceLabel;
        private System.Windows.Forms.Label QuantityLabel;
        private System.Windows.Forms.Label DestinationLabel;
        private System.Windows.Forms.Label PriceTitleLabel;
        private System.Windows.Forms.Label QuantityTitleLabel;
        private System.Windows.Forms.Label InstructionsLabel;
        private System.Windows.Forms.PictureBox InstructionsPictureBox;
        private System.Windows.Forms.Timer AnimationTimer;
    }
}
