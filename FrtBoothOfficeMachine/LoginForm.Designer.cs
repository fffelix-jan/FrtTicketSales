namespace FrtBoothOfficeMachine
{
    partial class LoginForm
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
            this.BackgroundPictureBox = new System.Windows.Forms.PictureBox();
            this.SmallTitleLabel = new System.Windows.Forms.Label();
            this.UsernameLabel = new System.Windows.Forms.Label();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.UsernameTextBox = new System.Windows.Forms.TextBox();
            this.PasswordTextBox = new System.Windows.Forms.TextBox();
            this.BigTitleLabel = new System.Windows.Forms.Label();
            this.LoginButton = new System.Windows.Forms.Button();
            this.ExitButton = new System.Windows.Forms.Button();
            this.VersionLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.BackgroundPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // BackgroundPictureBox
            // 
            this.BackgroundPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BackgroundPictureBox.Image = global::FrtBoothOfficeMachine.Properties.Resources.LoginBackgroundImage;
            this.BackgroundPictureBox.Location = new System.Drawing.Point(0, 0);
            this.BackgroundPictureBox.Name = "BackgroundPictureBox";
            this.BackgroundPictureBox.Size = new System.Drawing.Size(786, 563);
            this.BackgroundPictureBox.TabIndex = 0;
            this.BackgroundPictureBox.TabStop = false;
            // 
            // SmallTitleLabel
            // 
            this.SmallTitleLabel.AutoSize = true;
            this.SmallTitleLabel.BackColor = System.Drawing.Color.Transparent;
            this.SmallTitleLabel.Font = new System.Drawing.Font("SimSun", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SmallTitleLabel.ForeColor = System.Drawing.Color.Yellow;
            this.SmallTitleLabel.Location = new System.Drawing.Point(12, 9);
            this.SmallTitleLabel.Name = "SmallTitleLabel";
            this.SmallTitleLabel.Size = new System.Drawing.Size(608, 48);
            this.SmallTitleLabel.TabIndex = 0;
            this.SmallTitleLabel.Text = "法洛威轨道交通窗口售票机";
            // 
            // UsernameLabel
            // 
            this.UsernameLabel.AutoSize = true;
            this.UsernameLabel.BackColor = System.Drawing.Color.Transparent;
            this.UsernameLabel.Font = new System.Drawing.Font("SimSun", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UsernameLabel.ForeColor = System.Drawing.Color.Yellow;
            this.UsernameLabel.Location = new System.Drawing.Point(387, 430);
            this.UsernameLabel.Name = "UsernameLabel";
            this.UsernameLabel.Size = new System.Drawing.Size(96, 27);
            this.UsernameLabel.TabIndex = 2;
            this.UsernameLabel.Text = "用户名";
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.AutoSize = true;
            this.PasswordLabel.BackColor = System.Drawing.Color.Transparent;
            this.PasswordLabel.Font = new System.Drawing.Font("SimSun", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PasswordLabel.ForeColor = System.Drawing.Color.Yellow;
            this.PasswordLabel.Location = new System.Drawing.Point(415, 473);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(68, 27);
            this.PasswordLabel.TabIndex = 4;
            this.PasswordLabel.Text = "密码";
            // 
            // UsernameTextBox
            // 
            this.UsernameTextBox.Font = new System.Drawing.Font("SimSun", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UsernameTextBox.Location = new System.Drawing.Point(502, 426);
            this.UsernameTextBox.Name = "UsernameTextBox";
            this.UsernameTextBox.Size = new System.Drawing.Size(270, 31);
            this.UsernameTextBox.TabIndex = 3;
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.Font = new System.Drawing.Font("SimSun", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PasswordTextBox.Location = new System.Drawing.Point(502, 469);
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.PasswordChar = '*';
            this.PasswordTextBox.Size = new System.Drawing.Size(270, 31);
            this.PasswordTextBox.TabIndex = 5;
            // 
            // BigTitleLabel
            // 
            this.BigTitleLabel.AutoSize = true;
            this.BigTitleLabel.BackColor = System.Drawing.Color.Transparent;
            this.BigTitleLabel.Font = new System.Drawing.Font("SimSun", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BigTitleLabel.ForeColor = System.Drawing.Color.Yellow;
            this.BigTitleLabel.Location = new System.Drawing.Point(12, 83);
            this.BigTitleLabel.Name = "BigTitleLabel";
            this.BigTitleLabel.Size = new System.Drawing.Size(236, 97);
            this.BigTitleLabel.TabIndex = 1;
            this.BigTitleLabel.Text = "登录";
            // 
            // LoginButton
            // 
            this.LoginButton.Font = new System.Drawing.Font("SimSun", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoginButton.Location = new System.Drawing.Point(697, 516);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(75, 33);
            this.LoginButton.TabIndex = 6;
            this.LoginButton.Text = "登录";
            this.LoginButton.UseVisualStyleBackColor = true;
            // 
            // ExitButton
            // 
            this.ExitButton.Font = new System.Drawing.Font("SimSun", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExitButton.ForeColor = System.Drawing.Color.Red;
            this.ExitButton.Location = new System.Drawing.Point(502, 516);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(75, 33);
            this.ExitButton.TabIndex = 7;
            this.ExitButton.Text = "退出";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // VersionLabel
            // 
            this.VersionLabel.AutoSize = true;
            this.VersionLabel.BackColor = System.Drawing.Color.Transparent;
            this.VersionLabel.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.VersionLabel.ForeColor = System.Drawing.Color.Yellow;
            this.VersionLabel.Location = new System.Drawing.Point(12, 511);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(343, 42);
            this.VersionLabel.TabIndex = 8;
            this.VersionLabel.Text = "v1.0.0\r\n© 1996-2025 法洛威铁道科学研究院 版权所有";
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.VersionLabel);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.LoginButton);
            this.Controls.Add(this.BigTitleLabel);
            this.Controls.Add(this.PasswordTextBox);
            this.Controls.Add(this.UsernameTextBox);
            this.Controls.Add(this.PasswordLabel);
            this.Controls.Add(this.UsernameLabel);
            this.Controls.Add(this.SmallTitleLabel);
            this.Controls.Add(this.BackgroundPictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "法洛威轨道交通窗口售票机 - 登录";
            ((System.ComponentModel.ISupportInitialize)(this.BackgroundPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox BackgroundPictureBox;
        private System.Windows.Forms.Label SmallTitleLabel;
        private System.Windows.Forms.Label UsernameLabel;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.TextBox UsernameTextBox;
        private System.Windows.Forms.TextBox PasswordTextBox;
        private System.Windows.Forms.Label BigTitleLabel;
        private System.Windows.Forms.Button LoginButton;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.Label VersionLabel;
    }
}