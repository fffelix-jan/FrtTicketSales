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
    public partial class MainForm : Form
    {
        SellRegularTicketsControl sellRegularTicketsControl = new SellRegularTicketsControl();

        private void LogoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Disable the close button on the form
        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        public MainForm()
        {
            InitializeComponent();
            UpdateClockDisplay();
            MainPanel.Controls.Add(sellRegularTicketsControl);
            sellRegularTicketsControl.Dock = DockStyle.Fill;
        }

        private void UpdateClockDisplay()
        {
            // Update the clock display with the current date and time
            DateTimeMenuItem.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
        }

        private void ClockUpdateTimer_Tick(object sender, EventArgs e)
        {
            UpdateClockDisplay();
        }

        // Message box for unimplemented IC card (transit card) functionality
        private void ICCardPlaceholder()
        {
            MessageBox.Show("IC卡（交通卡）功能尚未启用。", "功能未启用", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void TopUpICCardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ICCardPlaceholder();
        }

        private void ICCardReaderSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ICCardPlaceholder();
        }

        private void RefundICCardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ICCardPlaceholder();
        }

        private void QueryICCardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ICCardPlaceholder();
        }

        private void DisplayCalendarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (CalendarForm calendarForm = new CalendarForm())
            {
                calendarForm.ShowDialog();
            } // calendarForm is automatically disposed here
        }

        private void ChangePasswordAndLogOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var changePasswordDialog = new ChangePasswordAndLogOutDialogForm())
            {
                changePasswordDialog.ShowDialog(this);

                if (changePasswordDialog.PasswordChangedSuccessfully)
                {
                    // Password was changed successfully, close the application
                    this.Close();
                }
                // If result is DialogResult.Cancel or password change failed, do nothing
                // The user remains logged in and can continue using the application
            }
        }

        /// <summary>
        /// Event handler for the Pause Selling menu item
        /// </summary>
        private void PauseSellingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Check if user is currently authenticated
            if (!GlobalCredentials.IsAuthenticated)
            {
                MessageBox.Show("当前未登录，无法暂停售票。", "暂停售票", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Hide the main form
                this.Hide();

                // Create and show the login form in pause mode
                using (var pauseLoginForm = LoginForm.CreateForPauseMode())
                {
                    var result = pauseLoginForm.ShowDialog(this);

                    // Show the main form again regardless of the result
                    this.Show();
                    this.BringToFront();
                    this.Activate();

                    if (result == DialogResult.OK)
                    {
                        // Successfully unlocked, continue normal operation
                        Console.WriteLine("Successfully unlocked from pause mode");
                    }
                    else
                    {
                        // User cancelled or failed to unlock
                        // For security, we might want to log this event
                        Console.WriteLine("Pause mode unlock was cancelled or failed");
                    }
                }
            }
            catch (Exception ex)
            {
                // Ensure main form is visible even if an error occurs
                this.Show();
                this.BringToFront();
                this.Activate();

                MessageBox.Show($"进入暂停模式时发生错误：\n\n{ex.Message}",
                              "暂停售票错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"{GlobalConstants.ApplicationName}\n{GlobalConstants.VersionName}\n{GlobalConstants.CopyrightText}",
                          "关于", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
