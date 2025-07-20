using System;
using System.Windows.Forms;

namespace TicketPrinterTestProgram
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            // Set current datetime as default
            txtDateTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            btnPrint.Click += BtnPrint_Click;
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < (int)CopyNumericUpDown.Value; ++i)
                {
                    FrtTicketPrinter.TicketGenerator.PrintTicket(
                        txtChineseTicketType.Text,
                        txtEnglishTicketType.Text,
                        txtChineseStation.Text,
                        txtEnglishStation.Text,
                        txtValue.Text,
                        txtPaymentType.Text,
                        txtDateTime.Text,
                        txtQRCode.Text,
                        txtTicketNumber.Text,
                        txtChineseFooter.Text,
                        txtEnglishFooter.Text
                    );
                }

                MessageBox.Show("Ticket printed successfully!",
                              "Success",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                throw;
                /*
                MessageBox.Show($"Error printing ticket: {ex.Message}",
                              "Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error); */
            }
        }
    }
}