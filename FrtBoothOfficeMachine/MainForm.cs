using FrtAfcApiClient;
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
        private SellRegularTicketsControl sellRegularTicketsControl = new SellRegularTicketsControl();
        private SellPassesControl sellPassesControl = new SellPassesControl();
        private UserControl currentControl = null;

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
            this.Text = $"{GlobalConstants.ApplicationName} - 当前用户：{GlobalCredentials.Username}";

            // Initialize both controls but don't add them yet
            InitializeControls();

            // Show the regular tickets control by default
            ShowSellRegularTicketsControl();
            sellPassesControl.Hide();
        }

        /// <summary>
        /// Initializes both control instances
        /// </summary>
        private void InitializeControls()
        {
            // Set dock style for both controls
            sellRegularTicketsControl.Dock = DockStyle.Fill;
            sellPassesControl.Dock = DockStyle.Fill;
        }

        /// <summary>
        /// Switches to the sell regular tickets view
        /// </summary>
        private void ShowSellRegularTicketsControl()
        {
            sellPassesControl.CancelTransaction();
            SwitchToControl(sellRegularTicketsControl);
        }

        /// <summary>
        /// Switches to the sell passes view
        /// </summary>
        private void ShowSellPassesControl()
        {
            sellRegularTicketsControl.CancelTransaction();
            SwitchToControl(sellPassesControl);
        }

        /// <summary>
        /// Helper method to switch between controls in the MainPanel
        /// </summary>
        /// <param name="newControl">The control to switch to</param>
        private void SwitchToControl(UserControl newControl)
        {
            if (currentControl == newControl)
            {
                return; // Already showing this control
            }

            try
            {
                // Suspend layout to prevent flickering
                MainPanel.SuspendLayout();

                // Remove current control if exists
                if (currentControl != null)
                {
                    currentControl.Hide();
                    MainPanel.Controls.Remove(currentControl);
                }

                // Add and show new control
                MainPanel.Controls.Add(newControl);
                newControl.Dock = DockStyle.Fill;
                newControl.BringToFront();

                // Update current control reference
                currentControl = newControl;

                // Show the control
                newControl.Show();
            }
            finally
            {
                // Resume layout
                MainPanel.ResumeLayout(true);
            }
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

        /// <summary>
        /// Event handler for the Sell Regular Tickets menu item
        /// </summary>
        private void SellRegularTicketsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowSellRegularTicketsControl();
        }

        /// <summary>
        /// Event handler for the Sell Passes menu item
        /// </summary>
        private void SellPassesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowSellPassesControl();
        }

        private async void TestServerConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Disable the menu item during testing to prevent multiple concurrent tests
            var menuItem = sender as ToolStripMenuItem;
            if (menuItem != null)
            {
                menuItem.Enabled = false;
                menuItem.Text = "测试中...";
            }

            try
            {
                string testResult;
                MessageBoxIcon resultIcon;
                string resultTitle;

                // Check if we have an API client
                if (GlobalCredentials.ApiClient == null)
                {
                    testResult = "测试失败：API客户端未初始化。\n\n请先登录系统。";
                    resultIcon = MessageBoxIcon.Error;
                    resultTitle = "连接测试失败";
                }
                else
                {
                    try
                    {
                        // Test connection using GetCurrentDateTimeAsync
                        var startTime = DateTime.Now;
                        var serverDateTime = await GlobalCredentials.ApiClient.GetCurrentDateTimeAsync();
                        var endTime = DateTime.Now;
                        var responseTime = (endTime - startTime).TotalMilliseconds;

                        // Get API endpoint from config for display
                        string apiEndpoint = SimpleConfig.Get("API_ENDPOINT", "未配置");

                        // Connection successful
                        testResult = $"服务器连接测试成功！\n\n" +
                                   $"服务器地址：{apiEndpoint}\n" +
                                   $"服务器时间：{serverDateTime:yyyy-MM-dd HH:mm:ss}\n" +
                                   $"本地时间：{DateTime.Now:yyyy-MM-dd HH:mm:ss}\n" +
                                   $"响应时间：{responseTime:F0} 毫秒\n" +
                                   $"当前用户：{GlobalCredentials.Username}";

                        resultIcon = MessageBoxIcon.Information;
                        resultTitle = "连接测试成功";
                    }
                    catch (FrtAfcApiException ex)
                    {
                        // API-specific error
                        string apiEndpoint = SimpleConfig.Get("API_ENDPOINT", "未配置");
                        testResult = $"服务器连接测试失败！\n\n" +
                                   $"服务器地址：{apiEndpoint}\n" +
                                   $"错误类型：API错误\n" +
                                   $"错误信息：{ex.Message}\n\n";

                        if (ex.InnerException != null)
                        {
                            testResult += $"详细信息：{ex.InnerException.Message}";
                        }

                        resultIcon = MessageBoxIcon.Error;
                        resultTitle = "连接测试失败";
                    }
                    catch (System.Net.Http.HttpRequestException ex)
                    {
                        // HTTP-specific error
                        string apiEndpoint = SimpleConfig.Get("API_ENDPOINT", "未配置");
                        testResult = $"服务器连接测试失败！\n\n" +
                                   $"服务器地址：{apiEndpoint}\n" +
                                   $"错误类型：HTTP请求错误\n" +
                                   $"错误信息：{ex.Message}\n\n" +
                                   $"可能原因：\n" +
                                   $"• 服务器未运行\n" +
                                   $"• 网络连接问题\n" +
                                   $"• 服务器地址配置错误\n" +
                                   $"• 防火墙阻止连接";

                        resultIcon = MessageBoxIcon.Error;
                        resultTitle = "连接测试失败";
                    }
                    catch (TaskCanceledException ex)
                    {
                        // Timeout error
                        string apiEndpoint = SimpleConfig.Get("API_ENDPOINT", "未配置");
                        testResult = $"服务器连接测试超时！\n\n" +
                                   $"服务器地址：{apiEndpoint}\n" +
                                   $"错误信息：{ex.Message}\n\n" +
                                   $"可能原因：\n" +
                                   $"• 服务器响应过慢\n" +
                                   $"• 网络延迟过高\n" +
                                   $"• 服务器过载";

                        resultIcon = MessageBoxIcon.Warning;
                        resultTitle = "连接测试超时";
                    }
                    catch (Exception ex)
                    {
                        // General error
                        string apiEndpoint = SimpleConfig.Get("API_ENDPOINT", "未配置");
                        testResult = $"服务器连接测试失败！\n\n" +
                                   $"服务器地址：{apiEndpoint}\n" +
                                   $"错误类型：{ex.GetType().Name}\n" +
                                   $"错误信息：{ex.Message}";

                        resultIcon = MessageBoxIcon.Error;
                        resultTitle = "连接测试失败";
                    }
                }

                // Show result to user
                MessageBox.Show(testResult, resultTitle, MessageBoxButtons.OK, resultIcon);
            }
            catch (Exception ex)
            {
                // Handle any unexpected errors in the test process itself
                MessageBox.Show($"执行连接测试时发生错误：\n\n{ex.Message}",
                              "测试错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Re-enable the menu item
                if (menuItem != null)
                {
                    menuItem.Enabled = true;
                    menuItem.Text = "测试服务器链接 [&T]";
                }
            }
        }

        private void TestPrinterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // Create and show ticket printing dialog for test tickets
                using (var printDialog = TicketPrintDialogForm.CreateForTestTickets(1))
                {
                    var result = printDialog.ShowDialog(this);

                    if (result == DialogResult.OK)
                    {
                        // Printing completed successfully
                        MessageBox.Show("打印机测试完成！\n\n请检查打印机是否正常出纸，\n测试页内容是否清晰可读。",
                                      "打印机测试", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Printing failed or was cancelled
                        MessageBox.Show("打印机测试失败！\n\n请检查：\n• 打印机是否正常连接\n• 纸张是否充足\n• 打印机驱动是否正确安装",
                                      "打印机测试失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any unexpected errors during the test process
                MessageBox.Show($"执行打印机测试时发生错误：\n\n{ex.Message}\n\n请检查打印机连接和配置。",
                              "打印机测试错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintFreeExitTicketToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if we have an authenticated API client
                if (GlobalCredentials.ApiClient == null)
                {
                    MessageBox.Show("API客户端未初始化。请先登录系统。",
                                  "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Ask user for confirmation since this is a special ticket type
                var confirmResult = MessageBox.Show(
                    "确定要打印免费出站票吗？\n\n免费出站票通常用于：\n• 设备故障时的应急出站\n• 特殊情况下的客户服务\n• 系统维护期间的临时出站",
                    "确认打印免费出站票",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmResult != DialogResult.Yes)
                {
                    return; // User cancelled
                }

                // Create and show ticket printing dialog for free exit tickets
                using (var printDialog = TicketPrintDialogForm.CreateForFreeExitTickets(1))
                {
                    var result = printDialog.ShowDialog(this);

                    if (result == DialogResult.OK)
                    {
                        // Printing completed successfully
                        MessageBox.Show("免费出站票打印完成！\n\n请将票据交给需要出站的乘客，\n并记录发放原因以备查询。",
                                      "免费出站票打印完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Printing failed or was cancelled
                        MessageBox.Show("免费出站票打印失败！\n\n请检查：\n• 打印机是否正常连接\n• 纸张是否充足\n• 服务器连接是否正常",
                                      "打印失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (FrtAfcApiException ex)
            {
                MessageBox.Show($"服务器错误：\n\n{ex.Message}\n\n请检查网络连接和服务器状态。",
                              "服务器错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Handle any unexpected errors during the process
                MessageBox.Show($"打印免费出站票时发生错误：\n\n{ex.Message}\n\n请联系技术支持。",
                              "打印错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}