using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FrtAfcApiClient;

namespace FrtBoothOfficeMachine
{
    public partial class LoginForm : Form
    {
        private bool _isLoggingIn = false;
        private Font _originalLoginButtonFont; // Store original font
        private Font _loginProgressFont; // Store smaller font for "登陆中…"
        private bool _isPauseMode = false; // Flag to indicate if this is pause mode

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

        // Pause mode constructor
        public static LoginForm CreateForPauseMode()
        {
            var form = new LoginForm();
            form._isPauseMode = true;
            form.SetupPauseMode();
            return form;
        }

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
            
            // Store the original font of the login button
            _originalLoginButtonFont = new Font(LoginButton.Font.FontFamily, LoginButton.Font.Size, LoginButton.Font.Style);
            
            // Create the smaller font for login progress text (create once, reuse multiple times)
            _loginProgressFont = new Font(_originalLoginButtonFont.FontFamily, 10F, _originalLoginButtonFont.Style);
            
            // Set up event handlers
            LoginButton.Click += LoginButton_Click;
            UsernameTextBox.KeyDown += TextBox_KeyDown;
            PasswordTextBox.KeyDown += TextBox_KeyDown;
            
            // Focus on username textbox when form loads
            this.Load += (s, e) => {
                if (_isPauseMode)
                {
                    PasswordTextBox.Focus(); // In pause mode, focus on password since username is pre-filled
                }
                else
                {
                    UsernameTextBox.Focus();
                }
            };
            
            // Hook into the FormClosed event to dispose fonts and clear credentials
            this.FormClosed += (s, e) => {
                DisposeFonts();
                ClearCredentialTextBoxes();
            };
        }

        /// <summary>
        /// Configures the form for pause mode
        /// </summary>
        private void SetupPauseMode()
        {
            // Change the gigantic label to show "暂停售票"
            BigTitleLabel.Text = "暂停售票";
            
            // Pre-fill username with current logged-in user and grey it out
            if (!string.IsNullOrEmpty(GlobalCredentials.Username))
            {
                UsernameTextBox.Text = GlobalCredentials.Username;
            }
            UsernameTextBox.Enabled = false;
            UsernameTextBox.BackColor = SystemColors.Control; // Grey out the username box
            
            // Hide the exit button in pause mode
            ExitButton.Visible = false;
            
            // Change login button text to "解锁"
            LoginButton.Text = "解锁";
            
            // Clear password field
            PasswordTextBox.Text = string.Empty;
        }

        private void DisposeFonts()
        {
            // Dispose of custom fonts when form is closed
            try
            {
                _originalLoginButtonFont?.Dispose();
                _loginProgressFont?.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error disposing fonts: {ex.Message}");
            }
        }

        /// <summary>
        /// Clears and scrubs the contents of username and password text boxes using the same logic as GlobalCredentials.Clear
        /// </summary>
        private void ClearCredentialTextBoxes()
        {
            try
            {
                // Store original lengths
                int usernameLength = UsernameTextBox.Text?.Length ?? 0;
                int passwordLength = PasswordTextBox.Text?.Length ?? 0;

                // Clear the text boxes initially
                UsernameTextBox.Text = null;
                PasswordTextBox.Text = null;

                GC.Collect();
                GC.Collect();

                // Overwrite the text boxes 10 times with random text (same as GlobalCredentials.Clear)
                Random rand = new Random();
                for (int i = 0; i < 10; i++)
                {
                    UsernameTextBox.Text = new string(Enumerable.Range(0, usernameLength)
                        .Select(_ => (char)rand.Next(32, 127)).ToArray());
                    PasswordTextBox.Text = new string(Enumerable.Range(0, passwordLength)
                        .Select(_ => (char)rand.Next(32, 127)).ToArray());
                    
                    // Force UI update to ensure the random text is actually written to the controls
                    Application.DoEvents();
                    GC.Collect();
                }

                // Then set the text boxes to empty again
                UsernameTextBox.Text = string.Empty;
                PasswordTextBox.Text = string.Empty;
                GC.Collect();
                GC.Collect();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error clearing credential text boxes: {ex.Message}");
                // Fallback: at least clear the text boxes normally
                try
                {
                    UsernameTextBox.Text = string.Empty;
                    PasswordTextBox.Text = string.Empty;
                }
                catch
                {
                    // If even this fails, we can't do much more
                }
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            // Allow login with Enter key
            if (e.KeyCode == Keys.Enter)
            {
                LoginButton.PerformClick();
                e.SuppressKeyPress = true; // Prevent ding sound
            }
        }

        private async void LoginButton_Click(object sender, EventArgs e)
        {
            if (_isLoggingIn) return; // Prevent multiple login attempts
            
            if (_isPauseMode)
            {
                await PerformOfflineLogin();
            }
            else
            {
                await PerformLogin();
            }
        }

        /// <summary>
        /// Performs offline password verification for pause mode
        /// </summary>
        private async Task PerformOfflineLogin()
        {
            _isLoggingIn = true;
            
            try
            {
                // Disable UI during login and change button appearance
                LoginButton.Enabled = false;
                LoginButton.Text = "验证中…";
                LoginButton.Font = _loginProgressFont;
                PasswordTextBox.Enabled = false;

                // Refresh the UI
                Application.DoEvents();

                string enteredPassword = PasswordTextBox.Text;
                
                // Validate input
                if (string.IsNullOrEmpty(enteredPassword))
                {
                    ShowError("请输入密码。");
                    PasswordTextBox.Focus();
                    return;
                }

                // Verify password against stored password
                if (VerifyOfflinePassword(enteredPassword))
                {
                    // Password is correct, close form with success result
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    // Small delay to simulate processing (for better UX)
                    await Task.Delay(500);

                    ShowError("密码错误。");
                    PasswordTextBox.Text = string.Empty;
                    PasswordTextBox.Focus();
                }
            }
            catch (Exception ex)
            {
                ShowVerboseError("验证错误", ex);
                PasswordTextBox.Text = string.Empty;
                PasswordTextBox.Focus();
            }
            finally
            {
                // Re-enable UI and restore original font
                _isLoggingIn = false;
                LoginButton.Enabled = true;
                LoginButton.Text = "解锁";
                LoginButton.Font = _originalLoginButtonFont;
                PasswordTextBox.Enabled = true;
            }
        }

        /// <summary>
        /// Verifies password against the stored password in memory (offline verification)
        /// </summary>
        /// <param name="enteredPassword">Password entered by user</param>
        /// <returns>True if password matches, false otherwise</returns>
        private bool VerifyOfflinePassword(string enteredPassword)
        {
            try
            {
                // Compare with the stored password in GlobalCredentials
                return !string.IsNullOrEmpty(GlobalCredentials.Password) && 
                       GlobalCredentials.Password == enteredPassword;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error verifying offline password: {ex.Message}");
                return false;
            }
        }

        private async Task PerformLogin()
        {
            _isLoggingIn = true;
            FareApiClient apiClient = null;
            
            try
            {
                // Disable UI during login and change button appearance
                LoginButton.Enabled = false;
                LoginButton.Text = "登陆中…";
                
                // Use the pre-created smaller font (no memory allocation here)
                LoginButton.Font = _loginProgressFont;
                
                UsernameTextBox.Enabled = false;
                PasswordTextBox.Enabled = false;

                // Refresh the UI
                Application.DoEvents();

                string username = UsernameTextBox.Text.Trim();
                string password = PasswordTextBox.Text;
                
                // Validate input
                if (string.IsNullOrEmpty(username))
                {
                    ShowError("请输入用户名。");
                    UsernameTextBox.Focus();
                    return;
                }
                
                if (string.IsNullOrEmpty(password))
                {
                    ShowError("请输入密码。");
                    PasswordTextBox.Focus();
                    return;
                }
                
                // Get API endpoint from config
                string apiEndpoint = SimpleConfig.Get("API_ENDPOINT", "https://localhost:5001");
                
                Console.WriteLine($"Attempting to connect to: {apiEndpoint}");
                Console.WriteLine($"Full URL will be: {apiEndpoint}/api/v1/afc/currentdatetime");
                
                // Create API client and set credentials
                apiClient = new FareApiClient(apiEndpoint);
                apiClient.SetBasicAuthentication(username, password);
                
                Console.WriteLine("API client created, testing credentials...");
                
                // Test credentials using GetCurrentDateTimeAsync() 
                var authResponse = await apiClient.GetCurrentDateTimeAsync();
                
                Console.WriteLine($"Authentication successful, received: {authResponse}");
                
                // If we got here without exception, authentication succeeded
                if (authResponse != default(DateTime))
                {
                    // Save credentials to global variables
                    GlobalCredentials.Username = username;
                    GlobalCredentials.Password = password;
                    GlobalCredentials.ApiClient = apiClient;
                    apiClient = null; // Prevent disposal in finally block
                    
                    // Close login form with success result
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    ShowError("服务器响应异常：返回了无效的日期时间。");
                    PasswordTextBox.Text = string.Empty;
                    UsernameTextBox.Focus();
                }
            }
            catch (FrtAfcApiException ex)
            {
                // Check if this is an authentication error first
                if (IsAuthenticationError(ex))
                {
                    ShowError("用户名或密码错误。");
                }
                else
                {
                    ShowVerboseError("API 错误", ex);
                }
                PasswordTextBox.Text = string.Empty;
                UsernameTextBox.Focus();
            }
            catch (System.Net.Http.HttpRequestException ex)
            {
                // Handle direct HTTP errors (if not wrapped in FrtAfcApiException)
                if (IsHttpAuthenticationError(ex))
                {
                    ShowError("用户名或密码错误。");
                }
                else
                {
                    ShowVerboseError("HTTP 请求错误", ex);
                }
                PasswordTextBox.Text = string.Empty;
                UsernameTextBox.Focus();
            }
            catch (SocketException ex)
            {
                string errorMsg = $"网络连接失败（Socket错误）：\n\n" +
                                $"错误代码：{ex.SocketErrorCode}\n" +
                                $"详细信息：{ex.Message}\n\n" +
                                $"可能原因：\n" +
                                $"1. 服务器未运行\n" +
                                $"2. 防火墙阻止连接\n" +
                                $"3. 网络配置问题\n" +
                                $"4. 端口被占用";
                
                Console.WriteLine($"SocketException: {ex.SocketErrorCode} - {ex.Message}");
                ShowError(errorMsg);
                PasswordTextBox.Text = string.Empty;
                UsernameTextBox.Focus();
            }
            catch (WebException ex)
            {
                string errorMsg = $"Web请求异常：\n\n" +
                                $"状态：{ex.Status}\n" +
                                $"详细信息：{ex.Message}\n\n";
                
                if (ex.Response is HttpWebResponse httpResponse)
                {
                    errorMsg += $"HTTP状态码：{httpResponse.StatusCode}\n" +
                              $"状态描述：{httpResponse.StatusDescription}";
                }
                
                Console.WriteLine($"WebException: {ex.Status} - {ex.Message}");
                ShowError(errorMsg);
                PasswordTextBox.Text = string.Empty;
                UsernameTextBox.Focus();
            }
            catch (TaskCanceledException ex)
            {
                string errorMsg = $"请求超时：\n\n" +
                                $"详细信息：{ex.Message}\n\n" +
                                $"可能原因：\n" +
                                $"1. 服务器响应太慢\n" +
                                $"2. 网络延迟过高\n" +
                                $"3. 服务器过载";
                
                Console.WriteLine($"TaskCanceledException (Timeout): {ex.Message}");
                ShowError(errorMsg);
                PasswordTextBox.Text = string.Empty;
                UsernameTextBox.Focus();
            }
            catch (InvalidOperationException ex)
            {
                string errorMsg = $"操作无效：\n\n" +
                                $"详细信息：{ex.Message}\n\n" +
                                $"可能原因：\n" +
                                $"1. HttpClient配置错误\n" +
                                $"2. URL格式无效\n" +
                                $"3. 请求已被取消";
                
                Console.WriteLine($"InvalidOperationException: {ex.Message}");
                ShowError(errorMsg);
                PasswordTextBox.Text = string.Empty;
                UsernameTextBox.Focus();
            }
            catch (UnauthorizedAccessException)
            {
                ShowError("用户名或密码错误。");
                PasswordTextBox.Text = string.Empty;
                UsernameTextBox.Focus();
            }
            catch (Exception ex)
            {
                ShowVerboseError("未知错误", ex);
                PasswordTextBox.Text = string.Empty;
                UsernameTextBox.Focus();
            }
            finally
            {
                // Clean up API client if login failed
                if (apiClient != null)
                {
                    try
                    {
                        apiClient.Dispose();
                    }
                    catch (Exception disposeEx)
                    {
                        Console.WriteLine($"Error disposing API client: {disposeEx.Message}");
                    }
                }
                
                // Re-enable UI and restore original font
                _isLoggingIn = false;
                LoginButton.Enabled = true;
                LoginButton.Text = "登录";
                LoginButton.Font = _originalLoginButtonFont; // Restore original font
                UsernameTextBox.Enabled = true;
                PasswordTextBox.Enabled = true;
            }
        }

        private void ShowVerboseError(string chineseTitle, Exception ex)
        {
            string errorDetails = BuildErrorDetails(ex);
            string errorMsg = $"{chineseTitle}：\n\n{errorDetails}";
            
            Console.WriteLine($"Exception Details:\n{errorDetails}");
            ShowError(errorMsg);
        }

        private string BuildErrorDetails(Exception ex)
        {
            var details = new StringBuilder();
            
            details.AppendLine($"异常类型：{ex.GetType().Name}");
            details.AppendLine($"错误信息：{ex.Message}");
            
            if (ex.InnerException != null)
            {
                details.AppendLine($"\n内部异常：");
                details.AppendLine($"类型：{ex.InnerException.GetType().Name}");
                details.AppendLine($"信息：{ex.InnerException.Message}");
                
                // Check for specific inner exception types
                if (ex.InnerException is SocketException socketEx)
                {
                    details.AppendLine($"Socket错误代码：{socketEx.SocketErrorCode}");
                }
                else if (ex.InnerException is WebException webEx)
                {
                    details.AppendLine($"Web异常状态：{webEx.Status}");
                }
            }
            
            return details.ToString();
        }

        private bool IsAuthenticationError(FrtAfcApiException ex)
        {
            Console.WriteLine($"Checking authentication error for: {ex.Message}");
            Console.WriteLine($"Inner exception: {ex.InnerException?.GetType().Name} - {ex.InnerException?.Message}");

            // Check the main exception message
            if (ex.Message.Contains("401") || ex.Message.Contains("Unauthorized"))
                return true;

            // Check inner exception for HTTP authentication errors
            if (ex.InnerException != null)
            {
                var innerMessage = ex.InnerException.Message;
                if (innerMessage.Contains("401") || 
                    innerMessage.Contains("Unauthorized") ||
                    innerMessage.Contains("401 (Unauthorized)"))
                    return true;

                // Check if inner exception is HttpRequestException with auth error
                if (ex.InnerException is System.Net.Http.HttpRequestException httpEx)
                {
                    return IsHttpAuthenticationError(httpEx);
                }
            }

            return false;
        }

        private bool IsHttpAuthenticationError(System.Net.Http.HttpRequestException ex)
        {
            return ex.Message.Contains("401") || 
                   ex.Message.Contains("Unauthorized") ||
                   ex.Message.Contains("401 (Unauthorized)");
        }

        private void ShowError(string message)
        {
            string title = _isPauseMode ? "解锁错误" : "登录错误";
            MessageBox.Show(message, title, 
                          MessageBoxButtons.OK, MessageBoxIcon.Warning);
            
            // Ensure focus returns to the appropriate textbox after MessageBox is dismissed
            this.BeginInvoke(new Action(() =>
            {
                if (_isPauseMode)
                {
                    PasswordTextBox.Focus();
                }
                else
                {
                    if (string.IsNullOrEmpty(UsernameTextBox.Text.Trim()))
                    {
                        UsernameTextBox.Focus();
                    }
                    else
                    {
                        PasswordTextBox.Focus();
                    }
                }
            }));
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Handle Ctrl+A for text boxes
            if (keyData == (Keys.Control | Keys.A))
            {
                if (this.ActiveControl is TextBox textBox)
                {
                    textBox.SelectAll();
                    return true; // Indicates we handled the key
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
