using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrtTicketVendingMachine
{
    public partial class FrtFullLineMapControl : UserControl
    {
        public FrtFullLineMapControl()
        {
            InitializeComponent();
        }

        private void MingtingButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Mingting button clicked!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
