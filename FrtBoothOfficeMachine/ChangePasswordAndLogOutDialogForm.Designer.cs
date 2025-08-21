namespace FrtBoothOfficeMachine
{
    partial class ChangePasswordAndLogOutDialogForm
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
            this.OldPasswordLabel = new System.Windows.Forms.Label();
            this.NewPasswordLabel = new System.Windows.Forms.Label();
            this.ConfirmPasswordLabel = new System.Windows.Forms.Label();
            this.OldPasswordTextBox = new System.Windows.Forms.TextBox();
            this.NewPasswordTextBox = new System.Windows.Forms.TextBox();
            this.ConfirmPasswordTextBox = new System.Windows.Forms.TextBox();
            this.UserCancelButton = new System.Windows.Forms.Button();
            this.ChangePasswordAndLogOutButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Font = new System.Drawing.Font("SimSun", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TitleLabel.Location = new System.Drawing.Point(12, 9);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(264, 27);
            this.TitleLabel.TabIndex = 0;
            this.TitleLabel.Text = "修改密码并退出登录";
            // 
            // OldPasswordLabel
            // 
            this.OldPasswordLabel.AutoSize = true;
            this.OldPasswordLabel.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.OldPasswordLabel.Location = new System.Drawing.Point(79, 56);
            this.OldPasswordLabel.Name = "OldPasswordLabel";
            this.OldPasswordLabel.Size = new System.Drawing.Size(66, 19);
            this.OldPasswordLabel.TabIndex = 1;
            this.OldPasswordLabel.Text = "原密码";
            // 
            // NewPasswordLabel
            // 
            this.NewPasswordLabel.AutoSize = true;
            this.NewPasswordLabel.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.NewPasswordLabel.Location = new System.Drawing.Point(79, 101);
            this.NewPasswordLabel.Name = "NewPasswordLabel";
            this.NewPasswordLabel.Size = new System.Drawing.Size(66, 19);
            this.NewPasswordLabel.TabIndex = 3;
            this.NewPasswordLabel.Text = "新密码";
            // 
            // ConfirmPasswordLabel
            // 
            this.ConfirmPasswordLabel.AutoSize = true;
            this.ConfirmPasswordLabel.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ConfirmPasswordLabel.Location = new System.Drawing.Point(59, 146);
            this.ConfirmPasswordLabel.Name = "ConfirmPasswordLabel";
            this.ConfirmPasswordLabel.Size = new System.Drawing.Size(85, 19);
            this.ConfirmPasswordLabel.TabIndex = 5;
            this.ConfirmPasswordLabel.Text = "确认密码";
            // 
            // OldPasswordTextBox
            // 
            this.OldPasswordTextBox.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.OldPasswordTextBox.Location = new System.Drawing.Point(169, 53);
            this.OldPasswordTextBox.Name = "OldPasswordTextBox";
            this.OldPasswordTextBox.PasswordChar = '*';
            this.OldPasswordTextBox.Size = new System.Drawing.Size(300, 29);
            this.OldPasswordTextBox.TabIndex = 2;
            // 
            // NewPasswordTextBox
            // 
            this.NewPasswordTextBox.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.NewPasswordTextBox.Location = new System.Drawing.Point(169, 98);
            this.NewPasswordTextBox.Name = "NewPasswordTextBox";
            this.NewPasswordTextBox.PasswordChar = '*';
            this.NewPasswordTextBox.Size = new System.Drawing.Size(300, 29);
            this.NewPasswordTextBox.TabIndex = 4;
            // 
            // ConfirmPasswordTextBox
            // 
            this.ConfirmPasswordTextBox.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ConfirmPasswordTextBox.Location = new System.Drawing.Point(169, 143);
            this.ConfirmPasswordTextBox.Name = "ConfirmPasswordTextBox";
            this.ConfirmPasswordTextBox.PasswordChar = '*';
            this.ConfirmPasswordTextBox.Size = new System.Drawing.Size(300, 29);
            this.ConfirmPasswordTextBox.TabIndex = 6;
            // 
            // CancelButton
            // 
            this.UserCancelButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.UserCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.UserCancelButton.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.UserCancelButton.Location = new System.Drawing.Point(36, 196);
            this.UserCancelButton.Name = "CancelButton";
            this.UserCancelButton.Size = new System.Drawing.Size(120, 35);
            this.UserCancelButton.TabIndex = 7;
            this.UserCancelButton.Text = "取消";
            this.UserCancelButton.UseVisualStyleBackColor = false;
            // 
            // ChangePasswordAndLogOutButton
            // 
            this.ChangePasswordAndLogOutButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ChangePasswordAndLogOutButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ChangePasswordAndLogOutButton.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ChangePasswordAndLogOutButton.Location = new System.Drawing.Point(271, 196);
            this.ChangePasswordAndLogOutButton.Name = "ChangePasswordAndLogOutButton";
            this.ChangePasswordAndLogOutButton.Size = new System.Drawing.Size(198, 35);
            this.ChangePasswordAndLogOutButton.TabIndex = 8;
            this.ChangePasswordAndLogOutButton.Text = "修改密码并退出登录";
            this.ChangePasswordAndLogOutButton.UseVisualStyleBackColor = false;
            // 
            // ChangePasswordAndLogOutDialogForm
            // 
            this.AcceptButton = this.ChangePasswordAndLogOutButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(199)))), ((int)(((byte)(199)))));
            this.ClientSize = new System.Drawing.Size(520, 255);
            this.Controls.Add(this.ChangePasswordAndLogOutButton);
            this.Controls.Add(this.UserCancelButton);
            this.Controls.Add(this.ConfirmPasswordTextBox);
            this.Controls.Add(this.NewPasswordTextBox);
            this.Controls.Add(this.OldPasswordTextBox);
            this.Controls.Add(this.ConfirmPasswordLabel);
            this.Controls.Add(this.NewPasswordLabel);
            this.Controls.Add(this.OldPasswordLabel);
            this.Controls.Add(this.TitleLabel);
            this.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChangePasswordAndLogOutDialogForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "修改密码并退出登录";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChangePasswordAndLogOutDialogForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.Label OldPasswordLabel;
        private System.Windows.Forms.Label NewPasswordLabel;
        private System.Windows.Forms.Label ConfirmPasswordLabel;
        private System.Windows.Forms.TextBox OldPasswordTextBox;
        private System.Windows.Forms.TextBox NewPasswordTextBox;
        private System.Windows.Forms.TextBox ConfirmPasswordTextBox;
        private System.Windows.Forms.Button UserCancelButton;
        private System.Windows.Forms.Button ChangePasswordAndLogOutButton;
    }
}