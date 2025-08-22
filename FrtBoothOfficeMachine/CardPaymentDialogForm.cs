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
    public partial class CardPaymentDialogForm : Form
    {
        public string SelectedPaymentMethod { get; private set; } = "";
        public bool PaymentSuccessful { get; private set; } = false;

        private decimal _paymentAmount = 0;

        // Mapping from parody names (displayed) to real payment method names (used for printing)
        private readonly Dictionary<string, string> _paymentMethodMapping = new Dictionary<string, string>
        {
            { "银联", "银联" },           // Real name, no change needed
            { "伪信支付", "微信支付" },   // Parody → Real WeChat Pay
            { "假付宝", "支付宝" }        // Parody → Real Alipay
        };

        public CardPaymentDialogForm(decimal paymentAmount)
        {
            InitializeComponent();
            _paymentAmount = paymentAmount;

            // Set up the form
            SetupForm();
        }

        private void SetupForm()
        {
            // Display the payment amount
            AmountDisplayLabel.Text = $"¥{_paymentAmount:F2}";

            // Set default selection to first payment method
            if (PaymentTypeComboBox.Items.Count > 0)
            {
                PaymentTypeComboBox.SelectedIndex = 0;
            }

            // Set up event handlers
            UserCancelButton.Click += CancelButton_Click;

            // Focus on the payment type combo box
            this.Load += (s, e) => PaymentTypeComboBox.Focus();
        }

        private void ProcessPaymentButton_Click(object sender, EventArgs e)
        {
            // Validate that a payment method is selected
            if (PaymentTypeComboBox.SelectedIndex < 0)
            {
                MessageBox.Show("请选择支付方式。", "支付方式未选择",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                PaymentTypeComboBox.Focus();
                return;
            }

            // Get the selected parody payment method name from the dropdown
            string selectedParodyMethod = PaymentTypeComboBox.SelectedItem.ToString();

            // Map the parody name to the real payment method name
            string realPaymentMethod = GetRealPaymentMethodName(selectedParodyMethod);

            // Show confirmation dialog to cashier using the parody name
            string confirmationMessage = $"请使用POS机处理{selectedParodyMethod}支付。\n\n" +
                                       $"支付金额：¥{_paymentAmount:F2}\n\n" +
                                       $"支付是否成功？";

            var result = MessageBox.Show(confirmationMessage, "POS机支付确认",
                                       MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Payment successful - store the REAL payment method name for ticket printing
                SelectedPaymentMethod = realPaymentMethod;
                PaymentSuccessful = true;

                // Close form with OK result
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                // Payment failed, stay on form to allow retry
                // Focus back to payment type for potential retry
                PaymentTypeComboBox.Focus();
            }
        }

        /// <summary>
        /// Maps parody payment method names to real payment method names
        /// </summary>
        /// <param name="parodyName">The parody name displayed in the dropdown</param>
        /// <returns>The real payment method name for ticket printing</returns>
        private string GetRealPaymentMethodName(string parodyName)
        {
            if (_paymentMethodMapping.TryGetValue(parodyName, out string realName))
            {
                return realName;
            }

            // Fallback: return the parody name if mapping not found
            // This shouldn't happen with the predefined dropdown items
            return parodyName;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            // User cancelled payment
            SelectedPaymentMethod = "";
            PaymentSuccessful = false;

            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Handle Escape key to cancel
            if (keyData == Keys.Escape)
            {
                CancelButton_Click(this, EventArgs.Empty);
                return true;
            }

            // Handle Enter key to process payment
            if (keyData == Keys.Enter)
            {
                ProcessPaymentButton_Click(this, EventArgs.Empty);
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}