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
            this.Load += (s, e) => UsernameTextBox.Focus();
            
            // Hook into the FormClosed event to dispose fonts
            this.FormClosed += (s, e) => DisposeFonts();
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

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            // Allow login with Enter key
            if (e.KeyCode == Keys.Enter)
            {
                LoginButton_Click(sender, e);
            }
        }

        private async void LoginButton_Click(object sender, EventArgs e)
        {
            if (_isLoggingIn) return; // Prevent multiple login attempts
            
            await PerformLogin();
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
            MessageBox.Show(message, "登录错误", 
                          MessageBoxButtons.OK, MessageBoxIcon.Warning);
            
            // Ensure focus returns to the appropriate textbox after MessageBox is dismissed
            this.BeginInvoke(new Action(() =>
            {
                if (string.IsNullOrEmpty(UsernameTextBox.Text.Trim()))
                {
                    UsernameTextBox.Focus();
                }
                else
                {
                    PasswordTextBox.Focus();
                }
            }));
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
