using System;
using System.Collections.Generic;
using System.Linq;
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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // dummy logic, the login logic still needs to be programmed
            // TODO: program login logic
            Application.Run(new LoginForm());
            Application.Run(new MainForm());
        }
    }
}
