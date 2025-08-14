using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrtBoothOfficeMachine
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            this.TopMost = true;
            SmallTitleLabel.Parent = BackgroundPictureBox;
            BigTitleLabel.Parent = BackgroundPictureBox;
            UsernameLabel.Parent = BackgroundPictureBox;
            PasswordLabel.Parent = BackgroundPictureBox;
            VersionLabel.Parent = BackgroundPictureBox;
            UsernameTextBox.Parent = BackgroundPictureBox;
            PasswordTextBox.Parent = BackgroundPictureBox;
            LoginButton.Parent = BackgroundPictureBox;
            ExitButton.Parent = BackgroundPictureBox;
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
