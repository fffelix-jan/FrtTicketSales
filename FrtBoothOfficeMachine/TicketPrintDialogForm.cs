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
    // This form's sole purpose is to actually print tickets.
    // The various constructors of this form set it up to print different kinds of tickets.
    // When the form is shown, it immediately contacts the server to issue the tickets,
    // prints them, and then closes itself.
    // The form also displays a progress bar to the user while this is happening.
    // The actual blocking print function is run asynchronuly to keep the UI responsive.
    public partial class TicketPrintDialogForm : Form
    {
        public TicketPrintDialogForm()
        {
            InitializeComponent();
            CenterControlHorizontally(TitleLabel);
        }

        // Centers controls horizontally
        public void CenterControlHorizontally(Control c)
        {
            c.Left = (c.Parent.Width - c.Width) / 2;
        }

        private void TitleLabel_TextChanged(object sender, EventArgs e)
        {
            CenterControlHorizontally(TitleLabel);
        }
    }
}
