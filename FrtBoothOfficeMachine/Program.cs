using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrtBoothOfficeMachine
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Force entire application to use Chinese culture
            CultureInfo chineseCulture = new CultureInfo("zh-CN");
            Thread.CurrentThread.CurrentCulture = chineseCulture;
            Thread.CurrentThread.CurrentUICulture = chineseCulture;

            // Set default culture for all new threads
            CultureInfo.DefaultThreadCurrentCulture = chineseCulture;
            CultureInfo.DefaultThreadCurrentUICulture = chineseCulture;

            // Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // Load configuration
            try
            {
                string configPath = System.IO.Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), 
                    "bom_config.txt");
                SimpleConfig.Load(configPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"配置文件加载失败：\nConfiguration load failed:\n\n{ex.Message}", 
                              "配置错误 / Configuration Error", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
            // Main application loop - keep running until user chooses to exit
            bool continueRunning = true;
            
            while (continueRunning)
            {
                // Show login form
                using (var loginForm = new LoginForm())
                {
                    DialogResult loginResult = loginForm.ShowDialog();
                    
                    if (loginResult == DialogResult.OK && GlobalCredentials.IsAuthenticated)
                    {
                        // User successfully logged in, show main form
                        try
                        {
                            using (var mainForm = new MainForm())
                            {
                                Application.Run(mainForm);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"主程序运行时发生错误：\nMain application error:\n\n{ex.Message}", 
                                          "应用程序错误 / Application Error", 
                                          MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            // Clear credentials when main form exits
                            GlobalCredentials.Clear();
                        }
                        
                        // Optional: After main form closes, ask if user wants to login again
                        //DialogResult continueResult = MessageBox.Show(
                        //    "是否重新登录？\nLogin again?", 
                        //    "退出登录 / Logout", 
                        //    MessageBoxButtons.YesNo, 
                        //    MessageBoxIcon.Question);
                            
                        //continueRunning = (continueResult == DialogResult.Yes);
                    }
                    else
                    {
                        // User cancelled login or login failed, exit application
                        continueRunning = false;
                    }
                }
            }
        }
    }
}
