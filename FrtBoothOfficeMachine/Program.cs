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
            // dummy logic, the login logic still needs to be programmed
            // TODO: program login logic
            Application.Run(new LoginForm());
            Application.Run(new MainForm());
        }
    }
}
