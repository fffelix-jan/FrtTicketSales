using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FrtAfcApiClient;

namespace FrtBoothOfficeMachine
{
    public partial class ChangePasswordAndLogOutDialogForm : Form
    {
        public bool PasswordChangedSuccessfully { get; private set; } = false;

        public ChangePasswordAndLogOutDialogForm()
        {
            InitializeComponent();

            // Wire up event handlers
            ChangePasswordAndLogOutButton.Click += ChangePasswordAndLogOutButton_Click;
            UserCancelButton.Click += CancelButton_Click;

            // Add KeyDown event handlers for Enter key support
            OldPasswordTextBox.KeyDown += PasswordTextBox_KeyDown;
            NewPasswordTextBox.KeyDown += PasswordTextBox_KeyDown;
            ConfirmPasswordTextBox.KeyDown += PasswordTextBox_KeyDown;

            // Set initial focus
            this.Shown += (s, e) => OldPasswordTextBox.Focus();
        }

        private void PasswordTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;

                if (sender == OldPasswordTextBox)
                {
                    NewPasswordTextBox.Focus();
                }
                else if (sender == NewPasswordTextBox)
                {
                    ConfirmPasswordTextBox.Focus();
                }
                else if (sender == ConfirmPasswordTextBox)
                {
                    // Trigger password change when Enter is pressed on confirm password
                    ChangePasswordAndLogOutButton_Click(sender, e);
                }
            }
        }

        private async void ChangePasswordAndLogOutButton_Click(object sender, EventArgs e)
        {
            // Validate input fields
            if (!ValidateInput())
            {
                return;
            }

            // Disable controls during processing
            SetControlsEnabled(false);

            try
            {
                // Check if we have an authenticated API client
                if (GlobalCredentials.ApiClient == null)
                {
                    MessageBox.Show("API客户端未初始化。请重新登录。",
                                  "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Create password change request
                var request = new ChangePasswordRequest
                {
                    CurrentPassword = OldPasswordTextBox.Text.Trim(),
                    NewPassword = NewPasswordTextBox.Text.Trim()
                };

                // Call the API to change password
                var response = await GlobalCredentials.ApiClient.ChangePasswordAsync(request);

                // If we get here, the password change was successful
                PasswordChangedSuccessfully = true;

                MessageBox.Show("密码修改成功。系统将退出登录。",
                              "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear credentials from global storage
                GlobalCredentials.Clear();

                // Close dialog with OK result
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (PasswordChangeException ex)
            {
                // Handle password change specific errors
                if (ex.Message.ToLower().Contains("password is incorrect"))
                {
                    // Special case: current password is incorrect, show a custom dialog
                    MessageBox.Show("原密码不正确。请重新输入。",
                                  "密码错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show($"密码修改失败：\n\n{ex.Message}",
                                  "密码修改错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                SetControlsEnabled(true);

                // Focus back to old password field for retry
                OldPasswordTextBox.Focus();
                OldPasswordTextBox.SelectAll();
            }
            catch (FrtAfcApiException ex)
            {
                // Handle general API errors
                MessageBox.Show($"与服务器通信失败：\n\n{ex.Message}",
                              "通信错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetControlsEnabled(true);
                ChangePasswordAndLogOutButton.Focus();
            }
            catch (Exception ex)
            {
                // Handle unexpected errors
                MessageBox.Show($"发生未知错误：\n\n{ex.Message}",
                              "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetControlsEnabled(true);
                ChangePasswordAndLogOutButton.Focus();
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            // Close dialog with Cancel result
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private bool ValidateInput()
        {
            // Validate old password
            if (string.IsNullOrWhiteSpace(OldPasswordTextBox.Text))
            {
                MessageBox.Show("请输入原密码。", "验证错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                OldPasswordTextBox.Focus();
                return false;
            }

            // Validate new password
            if (string.IsNullOrWhiteSpace(NewPasswordTextBox.Text))
            {
                MessageBox.Show("请输入新密码。", "验证错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                NewPasswordTextBox.Focus();
                return false;
            }

            // Check minimum password length (server requires 8 characters)
            if (NewPasswordTextBox.Text.Trim().Length < 8)
            {
                MessageBox.Show("新密码长度至少需要8个字符。", "验证错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                NewPasswordTextBox.Focus();
                NewPasswordTextBox.SelectAll();
                return false;
            }

            // Validate confirm password
            if (string.IsNullOrWhiteSpace(ConfirmPasswordTextBox.Text))
            {
                MessageBox.Show("请确认新密码。", "验证错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ConfirmPasswordTextBox.Focus();
                return false;
            }

            // Check if new password and confirm password match
            if (NewPasswordTextBox.Text.Trim() != ConfirmPasswordTextBox.Text.Trim())
            {
                MessageBox.Show("新密码和确认密码不匹配。", "验证错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ConfirmPasswordTextBox.Focus();
                ConfirmPasswordTextBox.SelectAll();
                return false;
            }

            // Check if new password is different from old password
            if (OldPasswordTextBox.Text.Trim() == NewPasswordTextBox.Text.Trim())
            {
                MessageBox.Show("新密码不能与原密码相同。", "验证错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                NewPasswordTextBox.Focus();
                NewPasswordTextBox.SelectAll();
                return false;
            }

            return true;
        }

        private void SetControlsEnabled(bool enabled)
        {
            // Enable/disable all input controls and buttons
            OldPasswordTextBox.Enabled = enabled;
            NewPasswordTextBox.Enabled = enabled;
            ConfirmPasswordTextBox.Enabled = enabled;
            ChangePasswordAndLogOutButton.Enabled = enabled;
            UserCancelButton.Enabled = enabled;

            // Update cursor to show processing state
            this.Cursor = enabled ? Cursors.Default : Cursors.WaitCursor;

            // Update button text to show processing state
            if (enabled)
            {
                ChangePasswordAndLogOutButton.Text = "修改密码并退出登录";
            }
            else
            {
                ChangePasswordAndLogOutButton.Text = "正在处理...";
            }

            // Refresh the UI
            Application.DoEvents();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Handle Escape key to cancel
            if (keyData == Keys.Escape)
            {
                CancelButton_Click(this, EventArgs.Empty);
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void ChangePasswordAndLogOutDialogForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Suppress the closure of the form after the MessageBox is dismissed
            // (likely a bug in WinForms)
            if (e.CloseReason == CloseReason.None)
            {
                e.Cancel = true;
                return;
            }
        }
    }
}