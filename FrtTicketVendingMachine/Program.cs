using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrtTicketVendingMachine
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                // Load configuration from user's home folder
                string configPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "ticket_machine_config.txt");
                
                SimpleConfig.Load(configPath);
                
                // Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            catch (FileNotFoundException)
            {
                string userHome = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                MessageBox.Show($"Configuration file not found!\n\n" +
                              $"Please create 'ticket_machine_config.txt' in:\n{userHome}\n\n" +
                              $"Example content:\n" +
                              $"API_ENDPOINT=https://api.example.com\n" +
                              $"API_USERNAME=your_username\n" +
                              $"API_PASSWORD=your_password",
                              "Configuration Missing",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Warning);
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show("User home directory not found. Please check your system configuration.",
                              "Directory Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Access denied when reading configuration file. Please check file permissions.",
                              "Access Denied",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error starting application:\n\n{ex.Message}",
                              "Startup Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
            }
        }
    }
}
