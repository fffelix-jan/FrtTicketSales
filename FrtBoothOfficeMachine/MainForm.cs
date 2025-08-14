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

        public MainForm()
        {
            InitializeComponent();
            UpdateClockDisplay();
            MainPanel.Controls.Add(sellRegularTicketsControl);
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
    }
}
