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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.DestinationTitleLabel = new System.Windows.Forms.Label();
            this.QuantityTitleLabel = new System.Windows.Forms.Label();
            this.PriceTitleLabel = new System.Windows.Forms.Label();
            this.DestinationLabel = new System.Windows.Forms.Label();
            this.QuantityLabel = new System.Windows.Forms.Label();
            this.TotalPriceLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
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
            this.tableLayoutPanel1.Location = new System.Drawing.Point(708, 172);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 113F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(644, 375);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // DestinationTitleLabel
            // 
            this.DestinationTitleLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.DestinationTitleLabel.AutoSize = true;
            this.DestinationTitleLabel.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DestinationTitleLabel.ForeColor = System.Drawing.Color.Red;
            this.DestinationTitleLabel.Location = new System.Drawing.Point(17, 33);
            this.DestinationTitleLabel.Name = "DestinationTitleLabel";
            this.DestinationTitleLabel.Size = new System.Drawing.Size(289, 65);
            this.DestinationTitleLabel.TabIndex = 0;
            this.DestinationTitleLabel.Text = "Destination";
            // 
            // QuantityTitleLabel
            // 
            this.QuantityTitleLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.QuantityTitleLabel.AutoSize = true;
            this.QuantityTitleLabel.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QuantityTitleLabel.ForeColor = System.Drawing.Color.Red;
            this.QuantityTitleLabel.Location = new System.Drawing.Point(80, 164);
            this.QuantityTitleLabel.Name = "QuantityTitleLabel";
            this.QuantityTitleLabel.Size = new System.Drawing.Size(226, 65);
            this.QuantityTitleLabel.TabIndex = 2;
            this.QuantityTitleLabel.Text = "Quantity";
            // 
            // PriceTitleLabel
            // 
            this.PriceTitleLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.PriceTitleLabel.AutoSize = true;
            this.PriceTitleLabel.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PriceTitleLabel.ForeColor = System.Drawing.Color.Red;
            this.PriceTitleLabel.Location = new System.Drawing.Point(42, 286);
            this.PriceTitleLabel.Name = "PriceTitleLabel";
            this.PriceTitleLabel.Size = new System.Drawing.Size(264, 65);
            this.PriceTitleLabel.TabIndex = 5;
            this.PriceTitleLabel.Text = "Total Price";
            // 
            // DestinationLabel
            // 
            this.DestinationLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.DestinationLabel.AutoSize = true;
            this.DestinationLabel.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DestinationLabel.ForeColor = System.Drawing.Color.Black;
            this.DestinationLabel.Location = new System.Drawing.Point(312, 33);
            this.DestinationLabel.Name = "DestinationLabel";
            this.DestinationLabel.Size = new System.Drawing.Size(230, 65);
            this.DestinationLabel.TabIndex = 6;
            this.DestinationLabel.Text = "Xinggang";
            // 
            // QuantityLabel
            // 
            this.QuantityLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.QuantityLabel.AutoSize = true;
            this.QuantityLabel.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QuantityLabel.ForeColor = System.Drawing.Color.Black;
            this.QuantityLabel.Location = new System.Drawing.Point(312, 164);
            this.QuantityLabel.Name = "QuantityLabel";
            this.QuantityLabel.Size = new System.Drawing.Size(269, 65);
            this.QuantityLabel.TabIndex = 7;
            this.QuantityLabel.Text = "Destination";
            // 
            // TotalPriceLabel
            // 
            this.TotalPriceLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.TotalPriceLabel.AutoSize = true;
            this.TotalPriceLabel.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalPriceLabel.ForeColor = System.Drawing.Color.Black;
            this.TotalPriceLabel.Location = new System.Drawing.Point(312, 286);
            this.TotalPriceLabel.Name = "TotalPriceLabel";
            this.TotalPriceLabel.Size = new System.Drawing.Size(269, 65);
            this.TotalPriceLabel.TabIndex = 8;
            this.TotalPriceLabel.Text = "Destination";
            // 
            // CheckoutControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "CheckoutControl";
            this.Size = new System.Drawing.Size(1384, 885);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label DestinationTitleLabel;
        private System.Windows.Forms.Label TotalPriceLabel;
        private System.Windows.Forms.Label QuantityLabel;
        private System.Windows.Forms.Label DestinationLabel;
        private System.Windows.Forms.Label PriceTitleLabel;
        private System.Windows.Forms.Label QuantityTitleLabel;
    }
}
