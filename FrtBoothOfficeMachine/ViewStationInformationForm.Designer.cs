namespace FrtBoothOfficeMachine
{
    partial class ViewStationInformationForm
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
            this.StationsDataGridView = new System.Windows.Forms.DataGridView();
            this.StationCodeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChineseNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EnglishNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ZoneIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsActiveColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.StationCountLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.StationsDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // StationsDataGridView
            // 
            this.StationsDataGridView.AllowUserToAddRows = false;
            this.StationsDataGridView.AllowUserToDeleteRows = false;
            this.StationsDataGridView.AllowUserToResizeRows = false;
            this.StationsDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.StationsDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.StationsDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.StationsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.StationsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.StationCodeColumn,
            this.ChineseNameColumn,
            this.EnglishNameColumn,
            this.ZoneIdColumn,
            this.IsActiveColumn});
            this.StationsDataGridView.Location = new System.Drawing.Point(12, 70);
            this.StationsDataGridView.MultiSelect = false;
            this.StationsDataGridView.Name = "StationsDataGridView";
            this.StationsDataGridView.ReadOnly = true;
            this.StationsDataGridView.RowHeadersVisible = false;
            this.StationsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.StationsDataGridView.Size = new System.Drawing.Size(776, 368);
            this.StationsDataGridView.TabIndex = 1;
            // 
            // StationCodeColumn
            // 
            this.StationCodeColumn.DataPropertyName = "StationCode";
            this.StationCodeColumn.FillWeight = 80F;
            this.StationCodeColumn.HeaderText = "站点代码";
            this.StationCodeColumn.Name = "StationCodeColumn";
            this.StationCodeColumn.ReadOnly = true;
            this.StationCodeColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // ChineseNameColumn
            // 
            this.ChineseNameColumn.DataPropertyName = "ChineseName";
            this.ChineseNameColumn.FillWeight = 150F;
            this.ChineseNameColumn.HeaderText = "中文名称";
            this.ChineseNameColumn.Name = "ChineseNameColumn";
            this.ChineseNameColumn.ReadOnly = true;
            this.ChineseNameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // EnglishNameColumn
            // 
            this.EnglishNameColumn.DataPropertyName = "EnglishName";
            this.EnglishNameColumn.FillWeight = 150F;
            this.EnglishNameColumn.HeaderText = "英文名称";
            this.EnglishNameColumn.Name = "EnglishNameColumn";
            this.EnglishNameColumn.ReadOnly = true;
            this.EnglishNameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // ZoneIdColumn
            // 
            this.ZoneIdColumn.DataPropertyName = "ZoneId";
            this.ZoneIdColumn.FillWeight = 60F;
            this.ZoneIdColumn.HeaderText = "区域";
            this.ZoneIdColumn.Name = "ZoneIdColumn";
            this.ZoneIdColumn.ReadOnly = true;
            this.ZoneIdColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // IsActiveColumn
            // 
            this.IsActiveColumn.DataPropertyName = "StatusText";
            this.IsActiveColumn.FillWeight = 80F;
            this.IsActiveColumn.HeaderText = "状态";
            this.IsActiveColumn.Name = "IsActiveColumn";
            this.IsActiveColumn.ReadOnly = true;
            this.IsActiveColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // StatusLabel
            // 
            this.StatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.StatusLabel.Location = new System.Drawing.Point(12, 450);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(53, 12);
            this.StatusLabel.TabIndex = 2;
            this.StatusLabel.Text = "准备就绪";
            // 
            // RefreshButton
            // 
            this.RefreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.RefreshButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.RefreshButton.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.RefreshButton.Location = new System.Drawing.Point(623, 445);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(75, 25);
            this.RefreshButton.TabIndex = 3;
            this.RefreshButton.Text = "刷新(&R)";
            this.RefreshButton.UseVisualStyleBackColor = false;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.CloseButton.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CloseButton.Location = new System.Drawing.Point(713, 445);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 25);
            this.CloseButton.TabIndex = 4;
            this.CloseButton.Text = "关闭(&C)";
            this.CloseButton.UseVisualStyleBackColor = false;
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TitleLabel.Location = new System.Drawing.Point(12, 15);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(109, 16);
            this.TitleLabel.TabIndex = 5;
            this.TitleLabel.Text = "站点信息列表";
            // 
            // StationCountLabel
            // 
            this.StationCountLabel.AutoSize = true;
            this.StationCountLabel.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.StationCountLabel.Location = new System.Drawing.Point(12, 45);
            this.StationCountLabel.Name = "StationCountLabel";
            this.StationCountLabel.Size = new System.Drawing.Size(71, 12);
            this.StationCountLabel.TabIndex = 6;
            this.StationCountLabel.Text = "共 0 个站点";
            // 
            // ViewStationInformationForm
            // 
            this.AcceptButton = this.CloseButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(160)))), ((int)(((byte)(194)))));
            this.CancelButton = this.CloseButton;
            this.ClientSize = new System.Drawing.Size(800, 480);
            this.Controls.Add(this.StationCountLabel);
            this.Controls.Add(this.TitleLabel);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.RefreshButton);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.StationsDataGridView);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(650, 400);
            this.Name = "ViewStationInformationForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "站点信息";
            this.Load += new System.EventHandler(this.ViewStationInformationForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ViewStationInformationForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.StationsDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView StationsDataGridView;
        private System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.Label StationCountLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn StationCodeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChineseNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn EnglishNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ZoneIdColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsActiveColumn;
    }
}