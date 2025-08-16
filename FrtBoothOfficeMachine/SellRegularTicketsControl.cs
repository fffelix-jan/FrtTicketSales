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
    public partial class SellRegularTicketsControl : UserControl
    {
        public SellRegularTicketsControl()
        {
            InitializeComponent();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Don't process keys if the control is hidden
            if (!this.Visible)
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }

            // F1 hotkey to focus on the destination combo box
            if (keyData == Keys.F1)
            {
                // Your F1 functionality here
                DestinationComboBox.Focus();
                return true; // Indicates we handled the key
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
