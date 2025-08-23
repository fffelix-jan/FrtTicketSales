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
using Microsoft.VisualBasic; // For Interaction.InputBox

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
            this.Text = $"{GlobalConstants.ApplicationName} - 当前用户: {GlobalCredentials.Username}";

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
                        MessageBox.Show("免费出站票打印完成！\n\n请将车票交给需要出站的乘客，\n并记录发放原因以备查询。",
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

        private async void ReprintDamagedTicketToolStripMenuItem_Click(object sender, EventArgs e)
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

                // Get ticket number from user using VB.NET InputBox
                string ticketNumber = Interaction.InputBox(
                    "请输入需要重印的损坏车票编号：",
                    "重印损坏车票",
                    "");

                // Check if user cancelled or entered empty string
                if (string.IsNullOrWhiteSpace(ticketNumber))
                {
                    return; // User cancelled
                }

                // Trim whitespace
                ticketNumber = ticketNumber.Trim();

                // Basic validation of ticket number format
                if (!System.Text.RegularExpressions.Regex.IsMatch(ticketNumber, @"^\d+$"))
                {
                    MessageBox.Show("车票编号格式无效。请输入有效的数字车票编号。",
                                  "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Get current station for reissuance
                string currentStationCode = SimpleConfig.Get("CURRENT_STATION", "FLZ");

                // Show confirmation dialog with warning
                var confirmResult = MessageBox.Show(
                    $"确定要重印车票编号 {ticketNumber} 吗？\n\n" +
                    $"警告：\n" +
                    $"• 原车票将被系统自动作废\n" +
                    $"• 新车票将在 {currentStationCode} 站签发\n" +
                    $"• 此操作无法撤销\n\n" +
                    $"请确认原车票确实已损坏且无法使用。",
                    "确认重印损坏车票",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2); // Default to "No" for safety

                if (confirmResult != DialogResult.Yes)
                {
                    return; // User cancelled
                }

                // Show progress message
                this.Cursor = Cursors.WaitCursor;

                try
                {
                    // Call the API to reprint the ticket
                    var reissueResponse = await GlobalCredentials.ApiClient.ReprintTicketAsync(
                        ticketNumber,
                        currentStationCode);

                    // Get original issuing station information for printing
                    StationInfo? originalStationInfo = null;
                    try
                    {
                        originalStationInfo = await GlobalCredentials.ApiClient.GetStationNameAsync(reissueResponse.OriginalIssuingStation);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Warning: Could not retrieve original issuing station info for {reissueResponse.OriginalIssuingStation}: {ex.Message}");
                        // We'll continue with printing even if we can't get station name
                    }

                    // Create the appropriate ticket for printing based on ticket type
                    using (var printDialog = CreatePrintDialogForReplacementTicket(reissueResponse, originalStationInfo))
                    {
                        var printResult = printDialog.ShowDialog(this);

                        if (printResult == DialogResult.OK)
                        {
                            // Success - show confirmation and details including printing success
                            string successMessage = $"车票重印和打印成功！\n\n" +
                                                  $"原车票编号：{reissueResponse.OriginalTicket}\n" +
                                                  $"新车票编号：{reissueResponse.ReplacementTicket.TicketNumber}\n" +
                                                  $"车票类型：{GetTicketTypeDescription(reissueResponse.ReplacementTicketType)}\n" +
                                                  $"原签发站：{(originalStationInfo?.ChineseName ?? reissueResponse.OriginalIssuingStation)}\n" +
                                                  $"重印时间：{reissueResponse.ReissueTime:yyyy-MM-dd HH:mm:ss}\n" +
                                                  $"操作员：{reissueResponse.ReissuedBy}\n\n" +
                                                  $"原车票已自动作废，新车票已打印完成，\n请将新车票交给乘客。";

                            MessageBox.Show(successMessage,
                                          "重印和打印成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            // Printing failed but reissue was successful
                            MessageBox.Show($"车票重印成功，但打印失败！\n\n" +
                                          $"原车票编号：{reissueResponse.OriginalTicket}\n" +
                                          $"新车票编号：{reissueResponse.ReplacementTicket.TicketNumber}\n" +
                                          $"车票类型：{GetTicketTypeDescription(reissueResponse.ReplacementTicketType)}\n\n" +
                                          $"原车票已作废，但新车票打印失败。\n" +
                                          $"请检查打印机状态，可能需要手动处理此车票。",
                                          "打印失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (TicketNotFoundException ex)
                {
                    MessageBox.Show($"车票不存在：\n\n{ex.Message}\n\n请检查车票编号是否正确。",
                                  "车票未找到", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (FrtAfcApiException ex) when (ex.Message.Contains("cannot be reissued"))
                {
                    MessageBox.Show($"车票无法重印：\n\n{ex.Message}\n\n" +
                                  $"可能原因：\n" +
                                  $"• 车票已被使用或作废\n" +
                                  $"• 车票状态不允许重印\n" +
                                  $"• 车票已过期",
                                  "无法重印", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (FrtAfcApiException ex) when (ex.Message.Contains("Insufficient permissions"))
                {
                    MessageBox.Show("权限不足：您没有重印车票的权限。\n\n请联系系统管理员。",
                                  "权限错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (FrtAfcApiException ex)
                {
                    MessageBox.Show($"服务器错误：\n\n{ex.Message}\n\n请检查网络连接和服务器状态。",
                                  "服务器错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"重印车票时发生未知错误：\n\n{ex.Message}\n\n请联系技术支持。",
                                  "未知错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"执行重印操作时发生错误：\n\n{ex.Message}",
                              "操作错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Creates the appropriate TicketPrintDialogForm for the replacement ticket based on ticket type
        /// </summary>
        /// <param name="reissueResponse">The reissue response from the server</param>
        /// <param name="originalStationInfo">Original issuing station information (nullable)</param>
        /// <returns>Configured TicketPrintDialogForm for the replacement ticket</returns>
        private TicketPrintDialogForm CreatePrintDialogForReplacementTicket(ReissueTicketResponse reissueResponse, StationInfo? originalStationInfo)
        {
            // Use the actual replacement ticket value from the server response
            int ticketValueCents = GetTicketValueFromReissueResponse(reissueResponse);

            // Use the new reprinted ticket factory method with actual server data
            return TicketPrintDialogForm.CreateForReprintedTicket(
                reissueResponse.ReplacementTicket,
                reissueResponse.ReplacementTicketType,
                ticketValueCents, // Now using actual value from server
                originalStationInfo);
        }

        /// <summary>
        /// Gets the ticket value from the reissue response - no longer needs calculation
        /// </summary>
        /// <param name="reissueResponse">The reissue response from the server</param>
        /// <returns>Actual ticket value in cents from the server</returns>
        private int GetTicketValueFromReissueResponse(ReissueTicketResponse reissueResponse)
        {
            // Use the actual replacement ticket value from the server response
            return reissueResponse.ReplacementValueCents;
        }

        /// <summary>
        /// Gets a human-readable description of the ticket type
        /// </summary>
        /// <param name="ticketType">Ticket type code</param>
        /// <returns>Chinese description of the ticket type</returns>
        private string GetTicketTypeDescription(byte ticketType)
        {
            switch (ticketType)
            {
                case 0:
                    return "单程票（全价）";
                case 1:
                    return "单程票（学生）";
                case 2:
                    return "单程票（长者）";
                case 3:
                    return "免费出站票";
                case 4:
                    return "一日票";
                case 255:
                    return "调试票";
                default:
                    return $"未知类型（{ticketType}）";
            }
        }

        private async void IssueFapiaoToolStripMenuItem_Click(object sender, EventArgs e)
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

                // Get ticket number or QR code from user using VB.NET InputBox
                string ticketInput = Interaction.InputBox(
                    "请输入车票编号或扫描车票二维码：\n\n• 可直接输入车票编号\n• 或使用扫描枪扫描车票二维码",
                    "开具发票",
                    "");

                // Check if user cancelled or entered empty string
                if (string.IsNullOrWhiteSpace(ticketInput))
                {
                    return; // User cancelled
                }

                // Trim whitespace
                ticketInput = ticketInput.Trim();

                // Basic validation - either numeric ticket number or longer QR code
                if (ticketInput.Length < 3)
                {
                    MessageBox.Show("输入无效。请输入有效的车票编号或扫描车票二维码。",
                                  "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Show confirmation dialog
                string inputType = ticketInput.Length > 20 ? "二维码" : "车票编号";
                var confirmResult = MessageBox.Show(
                    $"确定要为{inputType} {(ticketInput.Length > 20 ? "[已扫描]" : ticketInput)} 开具发票吗？\n\n" +
                    $"注意：\n" +
                    $"• 每张车票只能开具一次发票\n" +
                    $"• 免费车票无法开具发票\n" +
                    $"• 此操作无法撤销",
                    "确认开具发票",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2); // Default to "No" for safety

                if (confirmResult != DialogResult.Yes)
                {
                    return; // User cancelled
                }

                // Show progress message
                this.Cursor = Cursors.WaitCursor;

                try
                {
                    // Call the API to issue invoice
                    var invoiceResponse = await GlobalCredentials.ApiClient.IssueInvoiceAsync(ticketInput);

                    // Success - show confirmation and details
                    string successMessage = $"发票开具成功！\n\n" +
                                          $"车票编号：{invoiceResponse.TicketNumber}\n" +
                                          $"车票类型：{GetTicketTypeDescription(invoiceResponse.TicketType)}\n" +
                                          $"车票金额：¥{invoiceResponse.ValueYuan:F2}\n" +
                                          $"签发站：{invoiceResponse.IssuingStation}\n" +
                                          $"开票时间：{invoiceResponse.InvoiceTime:yyyy-MM-dd HH:mm:ss}\n" +
                                          $"开票员：{invoiceResponse.InvoicedBy}\n\n" +
                                          $"请按照相关规定为乘客提供发票服务。";

                    MessageBox.Show(successMessage,
                                  "发票开具成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (TicketNotFoundException ex)
                {
                    MessageBox.Show($"车票不存在：\n\n{ex.Message}\n\n请检查输入的车票编号或二维码是否正确。",
                                  "车票未找到", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (InvoiceException ex)
                {
                    // Check if this is specifically about already invoiced ticket
                    if (ex.Message.Contains("already been invoiced") ||
                        (ex.InnerException != null && ex.InnerException.Message.Contains("already been invoiced")))
                    {
                        // Special message for already invoiced tickets
                        MessageBox.Show($"该车票已开具发票！\n\n" +
                                      $"系统检测到此车票已经开具过发票。\n" +
                                      $"每张车票只能开具一次发票。\n\n" +
                                      $"如果您认为这是错误，请：\n" +
                                      $"• 检查车票编号或二维码是否正确\n" +
                                      $"• 核实该车票是否确实未开票\n" +
                                      $"• 如有疑问请联系管理员",
                                      "车票已开票", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        // Regular invoice exception handling - safely access message
                        string errorDetails = ex.Message;
                        if (ex.InnerException != null)
                        {
                            errorDetails = ex.InnerException.Message;
                        }

                        MessageBox.Show($"无法开具发票：\n\n{errorDetails}\n\n" +
                                      $"可能原因：\n" +
                                      $"• 该车票已开具发票\n" +
                                      $"• 免费车票无法开票\n" +
                                      $"• 车票状态异常",
                                      "发票开具失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (FrtAfcApiException ex) when (ex.Message.Contains("Insufficient permissions"))
                {
                    MessageBox.Show("权限不足：您没有开具发票的权限。\n\n请联系系统管理员。",
                                  "权限错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (FrtAfcApiException ex)
                {
                    // Also check for "already been invoiced" in general API exceptions
                    if (ex.Message.Contains("already been invoiced") ||
                        (ex.InnerException != null && ex.InnerException.Message.Contains("already been invoiced")))
                    {
                        // Special message for already invoiced tickets from API exception
                        MessageBox.Show($"该车票已开具发票！\n\n" +
                                      $"系统检测到此车票已经开具过发票。\n" +
                                      $"每张车票只能开具一次发票。\n\n" +
                                      $"如果您认为这是错误，请：\n" +
                                      $"• 检查车票编号或二维码是否正确\n" +
                                      $"• 核实该车票是否确实未开票\n" +
                                      $"• 如有疑问请联系管理员",
                                      "车票已开票", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show($"服务器错误：\n\n{ex.Message}\n\n请检查网络连接和服务器状态。",
                                      "服务器错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    // Also check for "already been invoiced" in general exceptions
                    if (ex.Message.Contains("already been invoiced") ||
                        (ex.InnerException != null && ex.InnerException.Message.Contains("already been invoiced")))
                    {
                        // Special message for already invoiced tickets from general exception
                        MessageBox.Show($"该车票已开具发票！\n\n" +
                                      $"系统检测到此车票已经开具过发票。\n" +
                                      $"每张车票只能开具一次发票。\n\n" +
                                      $"如果您认为这是错误，请：\n" +
                                      $"• 检查车票编号或二维码是否正确\n" +
                                      $"• 核实该车票是否确实未开票\n" +
                                      $"• 如有疑问请联系管理员",
                                      "车票已开票", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        // Safely access exception details
                        string errorDetails = ex.Message;
                        if (ex.InnerException != null)
                        {
                            errorDetails = ex.InnerException.Message;
                        }

                        MessageBox.Show($"开具发票时发生未知错误：\n\n{errorDetails}\n\n请联系技术支持。",
                                      "未知错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                // Safely access exception details for outer catch
                string errorDetails = ex.Message;
                if (ex.InnerException != null)
                {
                    errorDetails = ex.InnerException.Message;
                }

                MessageBox.Show($"执行发票开具操作时发生错误：\n\n{errorDetails}",
                              "操作错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private async void RefundTicketToolStripMenuItem_Click(object sender, EventArgs e)
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

                // Get ticket number or QR code from user using VB.NET InputBox
                string ticketInput = Interaction.InputBox(
                    "请输入车票编号或扫描车票二维码：\n\n• 可直接输入车票编号\n• 或使用扫描枪扫描车票二维码",
                    "退票",
                    "");

                // Check if user cancelled or entered empty string
                if (string.IsNullOrWhiteSpace(ticketInput))
                {
                    return; // User cancelled
                }

                // Trim whitespace
                ticketInput = ticketInput.Trim();

                // Basic validation - either numeric ticket number or longer QR code
                if (ticketInput.Length < 3)
                {
                    MessageBox.Show("输入无效。请输入有效的车票编号或扫描车票二维码。",
                                  "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Show confirmation dialog
                string inputType = ticketInput.Length > 20 ? "二维码" : "车票编号";
                var confirmResult = MessageBox.Show(
                    $"确定要退票{inputType} {(ticketInput.Length > 20 ? "[已扫描]" : ticketInput)} 吗？\n\n" +
                    $"注意：\n" +
                    $"• 只有未使用且未开票的车票可以退票\n" +
                    $"• 已开具发票的车票无法退票\n" +
                    $"• 免费车票无法退票\n" +
                    $"• 此操作无法撤销",
                    "确认退票",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2); // Default to "No" for safety

                if (confirmResult != DialogResult.Yes)
                {
                    return; // User cancelled
                }

                // Show progress message
                this.Cursor = Cursors.WaitCursor;

                try
                {
                    // Call the API to refund ticket
                    var refundResponse = await GlobalCredentials.ApiClient.RefundTicketAsync(ticketInput);

                    // Success - show confirmation and details
                    string successMessage = $"退票成功！\n\n" +
                                          $"车票编号：{refundResponse.TicketNumber}\n" +
                                          $"车票类型：{GetTicketTypeDescription(refundResponse.TicketType)}\n" +
                                          $"退款金额：¥{refundResponse.ValueYuan:F2}\n" +
                                          $"签发站：{refundResponse.IssuingStation}\n" +
                                          $"退票时间：{refundResponse.RefundTime:yyyy-MM-dd HH:mm:ss}\n" +
                                          $"操作员：{refundResponse.RefundedBy}\n\n" +
                                          $"请按照相关规定为乘客办理退款手续。";

                    MessageBox.Show(successMessage,
                                  "退票成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (TicketNotFoundException ex)
                {
                    MessageBox.Show($"车票不存在：\n\n{ex.Message}\n\n请检查输入的车票编号或二维码是否正确。",
                                  "车票未找到", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (RefundException ex)
                {
                    // Check for specific refund error scenarios based on server error messages
                    string errorMessage = ex.Message.ToLower();

                    if (errorMessage.Contains("cannot be refunded") && errorMessage.Contains("paid"))
                    {
                        // Special message for tickets not in Paid state
                        MessageBox.Show($"该车票无法退票！\n\n" +
                                      $"只有处于\"已支付\"状态的车票才能退票。\n" +
                                      $"该车票可能已被使用或处于其他状态。\n\n" +
                                      $"如果您认为这是错误，请：\n" +
                                      $"• 检查车票编号或二维码是否正确\n" +
                                      $"• 核实该车票是否确实未使用\n" +
                                      $"• 如有疑问请联系管理员",
                                      "车票状态不符", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (errorMessage.Contains("already been invoiced"))
                    {
                        // Special message for already invoiced tickets
                        MessageBox.Show($"该车票无法退票！\n\n" +
                                      $"该车票已开具发票，无法办理退票。\n" +
                                      $"已开具发票的车票不能退票。\n\n" +
                                      $"如果需要退票，请：\n" +
                                      $"• 先按相关规定处理发票\n" +
                                      $"• 或联系管理员协助处理\n" +
                                      $"• 如有疑问请联系财务部门",
                                      "车票已开票", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (errorMessage.Contains("cannot refund free tickets"))
                    {
                        // Special message for free tickets
                        MessageBox.Show($"无法退票！\n\n" +
                                      $"免费车票无法办理退票。\n" +
                                      $"只有有价车票才能办理退票业务。\n\n" +
                                      $"如有疑问请联系管理员。",
                                      "免费车票", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Regular refund exception handling - safely access message
                        string errorDetails = ex.Message;
                        if (ex.InnerException != null)
                        {
                            errorDetails = ex.InnerException.Message;
                        }

                        MessageBox.Show($"无法退票：\n\n{errorDetails}\n\n" +
                                      $"可能原因：\n" +
                                      $"• 车票已被使用或作废\n" +
                                      $"• 车票已开具发票\n" +
                                      $"• 免费车票无法退票\n" +
                                      $"• 车票状态异常",
                                      "退票失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (FrtAfcApiException ex) when (ex.Message.Contains("Insufficient permissions"))
                {
                    MessageBox.Show("权限不足：您没有退票的权限。\n\n请联系系统管理员。",
                                  "权限错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (FrtAfcApiException ex)
                {
                    // Check for specific error messages in general API exceptions
                    string errorMessage = ex.Message.ToLower();

                    if (errorMessage.Contains("cannot be refunded") && errorMessage.Contains("paid"))
                    {
                        MessageBox.Show($"该车票无法退票！\n\n" +
                                      $"只有处于\"已支付\"状态的车票才能退票。\n" +
                                      $"该车票可能已被使用或处于其他状态。\n\n" +
                                      $"如果您认为这是错误，请：\n" +
                                      $"• 检查车票编号或二维码是否正确\n" +
                                      $"• 核实该车票是否确实未使用\n" +
                                      $"• 如有疑问请联系管理员",
                                      "车票状态不符", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (errorMessage.Contains("already been invoiced"))
                    {
                        MessageBox.Show($"该车票无法退票！\n\n" +
                                      $"该车票已开具发票，无法办理退票。\n" +
                                      $"已开具发票的车票不能退票。\n\n" +
                                      $"如果需要退票，请：\n" +
                                      $"• 先按相关规定处理发票\n" +
                                      $"• 或联系管理员协助处理\n" +
                                      $"• 如有疑问请联系财务部门",
                                      "车票已开票", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (errorMessage.Contains("cannot refund free tickets"))
                    {
                        MessageBox.Show($"无法退票！\n\n" +
                                      $"免费车票无法办理退票。\n" +
                                      $"只有有价车票才能办理退票业务。\n\n" +
                                      $"如有疑问请联系管理员。",
                                      "免费车票", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show($"服务器错误：\n\n{ex.Message}\n\n请检查网络连接和服务器状态。",
                                      "服务器错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    // Check for specific error messages in general exceptions
                    string errorMessage = ex.Message.ToLower();

                    if (errorMessage.Contains("cannot be refunded") && errorMessage.Contains("paid"))
                    {
                        MessageBox.Show($"该车票无法退票！\n\n" +
                                      $"只有处于\"已支付\"状态的车票才能退票。\n" +
                                      $"该车票可能已被使用或处于其他状态。\n\n" +
                                      $"如果您认为这是错误，请：\n" +
                                      $"• 检查车票编号或二维码是否正确\n" +
                                      $"• 核实该车票是否确实未使用\n" +
                                      $"• 如有疑问请联系管理员",
                                      "车票状态不符", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (errorMessage.Contains("already been invoiced"))
                    {
                        MessageBox.Show($"该车票无法退票！\n\n" +
                                      $"该车票已开具发票，无法办理退票。\n" +
                                      $"已开具发票的车票不能退票。\n\n" +
                                      $"如果需要退票，请：\n" +
                                      $"• 先按相关规定处理发票\n" +
                                      $"• 或联系管理员协助处理\n" +
                                      $"• 如有疑问请联系财务部门",
                                      "车票已开票", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (errorMessage.Contains("cannot refund free tickets"))
                    {
                        MessageBox.Show($"无法退票！\n\n" +
                                      $"免费车票无法办理退票。\n" +
                                      $"只有有价车票才能办理退票业务。\n\n" +
                                      $"如有疑问请联系管理员。",
                                      "免费车票", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Safely access exception details
                        string errorDetails = ex.Message;
                        if (ex.InnerException != null)
                        {
                            errorDetails = ex.InnerException.Message;
                        }

                        MessageBox.Show($"退票时发生未知错误：\n\n{errorDetails}\n\n请联系技术支持。",
                                      "未知错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                // Safely access exception details for outer catch
                string errorDetails = ex.Message;
                if (ex.InnerException != null)
                {
                    errorDetails = ex.InnerException.Message;
                }

                MessageBox.Show($"执行退票操作时发生错误：\n\n{errorDetails}",
                              "操作错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private async void QueryTicketInfoToolStripMenuItem_Click(object sender, EventArgs e)
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

                // Get ticket number or QR code from user using VB.NET InputBox
                string ticketInput = Interaction.InputBox(
                    "请输入车票编号或扫描车票二维码：\n\n• 可直接输入车票编号\n• 或使用扫描枪扫描车票二维码",
                    "查询车票信息",
                    "");

                // Check if user cancelled or entered empty string
                if (string.IsNullOrWhiteSpace(ticketInput))
                {
                    return; // User cancelled
                }

                // Trim whitespace
                ticketInput = ticketInput.Trim();

                // Basic validation - either numeric ticket number or longer QR code
                if (ticketInput.Length < 3)
                {
                    MessageBox.Show("输入无效。请输入有效的车票编号或扫描车票二维码。",
                                  "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Show progress message
                this.Cursor = Cursors.WaitCursor;

                try
                {
                    // Call the API to get ticket info - explicitly use FrtAfcApiClient.TicketInfo
                    var ticketInfo = await GlobalCredentials.ApiClient.GetTicketInfoAsync(ticketInput);

                    // Success - show detailed ticket information
                    string inputType = ticketInput.Length > 20 ? "二维码" : "车票编号";

                    // Get station name for better display
                    string stationDisplayName = ticketInfo.IssuingStation;
                    try
                    {
                        var stationInfo = await GlobalCredentials.ApiClient.GetStationNameAsync(ticketInfo.IssuingStation);
                        stationDisplayName = $"{stationInfo.ChineseName} ({stationInfo.StationCode})";
                    }
                    catch
                    {
                        // If we can't get station name, just use the code
                        stationDisplayName = ticketInfo.IssuingStation;
                    }

                    // Format ticket state description
                    string ticketStateDescription = GetTicketStateDescription(ticketInfo.TicketState);

                    // Build the information message
                    string infoMessage = $"车票信息查询成功！\n\n" +
                                       $"查询方式：{inputType}\n" +
                                       $"车票编号：{ticketInfo.TicketNumber}\n" +
                                       $"车票类型：{GetTicketTypeDescription(ticketInfo.TicketType)}\n" +
                                       $"车票状态：{ticketStateDescription}\n" +
                                       $"车票金额：¥{ticketInfo.ValueYuan:F2}\n" +
                                       $"签发站：{stationDisplayName}\n" +
                                       $"签发时间：{ticketInfo.IssueDateTime:yyyy-MM-dd HH:mm:ss}\n" +
                                       $"发票状态：{(ticketInfo.IsInvoiced ? "已开票" : "未开票")}\n" +
                                       $"输入方式：{(ticketInfo.InputMethod == "qr_code" ? "二维码扫描" : "手动输入")}";


                    MessageBox.Show(infoMessage,
                                  "车票信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (TicketNotFoundException ex)
                {
                    string inputType = ticketInput.Length > 20 ? "二维码" : "车票编号";
                    MessageBox.Show($"车票不存在！\n\n" +
                                  $"系统中找不到{inputType}对应的车票记录。\n\n" +
                                  $"请确认：\n" +
                                  $"• {inputType}是否输入正确\n" +
                                  $"• 车票编号是否有效\n" +
                                  $"• 车票是否已被系统删除\n\n" +
                                  $"如有疑问，请联系工作人员。",
                                  "车票未找到", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (TicketValidationException ex)
                {
                    string inputType = ticketInput.Length > 20 ? "二维码" : "车票编号";
                    
                    if (ex.Message.Contains("Invalid ticket number format"))
                    {
                        MessageBox.Show($"车票编号格式错误！\n\n" +
                                      $"车票编号必须是纯数字格式。\n\n" +
                                      $"请检查：\n" +
                                      $"• 车票编号是否包含字母或特殊字符\n" +
                                      $"• 是否输入了完整的车票编号\n" +
                                      $"• 车票编号是否正确\n\n",
                                      "格式错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (ex.Message.Contains("Invalid QR code"))
                    {
                        MessageBox.Show($"二维码无效！\n\n" +
                                      $"扫描的二维码不是有效的车票码。\n\n" +
                                      $"请确认：\n" +
                                      $"• 二维码是否为车票上的二维码\n" +
                                      $"• 二维码是否清晰完整\n" +
                                      $"• 扫描设备是否正常工作\n" +
                                      $"• 是否扫描了正确的二维码\n\n" +
                                      $"建议：尝试手动输入车票编号",
                                      "二维码无效", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (ex.Message.Contains("QR code decoding failed"))
                    {
                        MessageBox.Show($"二维码解析失败！\n\n" +
                                      $"二维码可能已损坏或无法识别。\n\n" +
                                      $"可能原因：\n" +
                                      $"• 二维码印刷不清晰或污损\n" +
                                      $"• 二维码部分缺失或损坏\n" +
                                      $"• 车票表面有污渍或划痕\n" +
                                      $"• 二维码格式已过期\n\n" +
                                      $"解决方案：请手动输入车票编号",
                                      "解析失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show($"{inputType}验证失败！\n\n" +
                                      $"{ex.Message}\n\n" +
                                      $"请检查输入内容是否正确，或联系工作人员。",
                                      "验证失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (FrtAfcApiException ex) when (ex.Message.Contains("Insufficient permissions"))
                {
                    MessageBox.Show("权限不足：您没有查询车票信息的权限。\n\n请联系系统管理员。",
                                  "权限错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (FrtAfcApiException ex)
                {
                    string errorDetails = ex.Message;
                    if (ex.InnerException != null)
                    {
                        errorDetails = ex.InnerException.Message;
                    }

                    MessageBox.Show($"服务器错误：\n\n{errorDetails}\n\n请检查网络连接和服务器状态。",
                                  "服务器错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    // Safely access exception details
                    string errorDetails = ex.Message;
                    if (ex.InnerException != null)
                    {
                        errorDetails = ex.InnerException.Message;
                    }

                    MessageBox.Show($"查询车票信息时发生未知错误：\n\n{errorDetails}\n\n请联系技术支持。",
                                  "未知错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Safely access exception details for outer catch
                string errorDetails = ex.Message;
                if (ex.InnerException != null)
                {
                    errorDetails = ex.InnerException.Message;
                }

                MessageBox.Show($"执行车票查询操作时发生错误：\n\n{errorDetails}",
                              "操作错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Gets a human-readable description of the ticket state
        /// </summary>
        /// <param name="ticketState">Ticket state code</param>
        /// <returns>Chinese description of the ticket state</returns>
        private string GetTicketStateDescription(byte ticketState)
        {
            switch (ticketState)
            {
                case 0:
                    return "未使用（新发）";
                case 1:
                    return "已支付（待使用）";
                case 2:
                    return "已入站";
                case 3:
                    return "已出站（已使用）";
                case 4:
                    return "已作废（重印）";
                case 5:
                    return "已退票";
                default:
                    return $"未知状态（{ticketState}）";
            }
        }

        private void QueryStationInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Show the station information form as a dialog
            using (var stationInfoForm = new ViewStationInformationForm())
            {
                stationInfoForm.ShowDialog(this);
            }
        }
    }
}