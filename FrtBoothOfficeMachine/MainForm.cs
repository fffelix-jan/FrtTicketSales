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

        private void LogoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Disable the close button on the form
        //private const int CP_NOCLOSE_BUTTON = 0x200;
        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams myCp = base.CreateParams;
        //        myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
        //        return myCp;
        //    }
        //}

        public MainForm()
        {
            InitializeComponent();
            UpdateClockDisplay();
            MainPanel.Controls.Add(sellRegularTicketsControl);
            sellRegularTicketsControl.Dock = DockStyle.Fill;
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
    }
}
