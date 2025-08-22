using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FrtAfcApiClient;

namespace FrtBoothOfficeMachine
{
    public partial class SellPassesControl : UserControl
    {
        private int dayPassPriceCents = 0;
        private int dayPassQuantity = 0;
        private int totalPriceCents = 0;

        public SellPassesControl()
        {
            InitializeComponent();

            // Set up event handlers
            this.Load += SellPassesControl_Load;

            // Quantity input events
            DayPassQuantityTextBox.KeyPress += QuantityTextBox_KeyPress;
            DayPassQuantityTextBox.KeyDown += DayPassQuantityTextBox_KeyDown;
            DayPassQuantityTextBox.Enter += DayPassQuantityTextBox_Enter;
            DayPassQuantityTextBox.Click += DayPassQuantityTextBox_Click;
            DayPassQuantityTextBox.Leave += DayPassQuantityTextBox_Leave;

            // Cash payment events
            CashPaymentTenderedTextBox.KeyPress += CashPaymentTenderedTextBox_KeyPress;
            CashPaymentTenderedTextBox.KeyDown += CashPaymentTenderedTextBox_KeyDown;
            CashPaymentTenderedTextBox.Leave += CashPaymentTenderedTextBox_Leave;
            CashPaymentTenderedTextBox.TextChanged += CashPaymentTenderedTextBox_TextChanged;

            // Button events
            CancelButton.Click += CancelButton_Click;
            CardPaymentButton.Click += CardPaymentButton_Click;
        }

        private async void SellPassesControl_Load(object sender, EventArgs e)
        {
            await LoadDayPassPriceAsync();
            UpdateDisplayLabels();

            // Set focus to quantity text box after loading
            this.BeginInvoke((Action)(() => {
                DayPassQuantityTextBox.Focus();
            }));
        }

        private async Task LoadDayPassPriceAsync()
        {
            try
            {
                // Check if we have an authenticated API client
                if (GlobalCredentials.ApiClient == null)
                {
                    MessageBox.Show("API客户端未初始化。请重新登录。",
                                  "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Load day pass price from the API
                var priceInfo = await GlobalCredentials.ApiClient.GetDayPassPriceAsync();
                dayPassPriceCents = priceInfo.DayPassPriceCents;

                // Update price display
                DayPassPriceLabel.Text = $"¥{priceInfo.DayPassPriceYuan:F2}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载一日票价格失败：\n\n{ex.Message}",
                              "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Use fallback price
                dayPassPriceCents = 2000; // Default ¥20.00
                DayPassPriceLabel.Text = "¥20.00";
            }
        }

        private void DayPassQuantityTextBox_Enter(object sender, EventArgs e)
        {
            DayPassQuantityTextBox.SelectAll();
        }

        private void DayPassQuantityTextBox_Click(object sender, EventArgs e)
        {
            DayPassQuantityTextBox.SelectAll();
        }

        private void QuantityTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Handle ENTER key press to prevent ding sound
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                return;
            }

            // Only allow digits (0-9) and control characters
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void DayPassQuantityTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ProcessDayPassQuantity();
                CashPaymentTenderedTextBox.Focus();
                e.Handled = true;
            }
        }

        private void DayPassQuantityTextBox_Leave(object sender, EventArgs e)
        {
            ProcessDayPassQuantity();
        }

        private void ProcessDayPassQuantity()
        {
            string input = DayPassQuantityTextBox.Text.Trim();

            if (string.IsNullOrEmpty(input))
            {
                dayPassQuantity = 0;
            }
            else if (int.TryParse(input, out int quantity) && quantity >= 0)
            {
                dayPassQuantity = quantity;
            }
            else
            {
                MessageBox.Show("请输入有效的票数（0或正整数）。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DayPassQuantityTextBox.Focus();
                DayPassQuantityTextBox.SelectAll();
                return;
            }

            CalculateTotalPrice();
            UpdateDisplayLabels();
        }

        private void CashPaymentTenderedTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Handle ENTER key press to prevent ding sound
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                return;
            }

            // Allow digits, decimal point, and control characters
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }

            // Only allow one decimal point
            TextBox textBox = sender as TextBox;
            if (e.KeyChar == '.' && textBox.Text.Contains('.'))
            {
                e.Handled = true;
            }
        }

        private void CashPaymentTenderedTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ProcessCashPayment();
                e.Handled = true;
            }
        }

        private void CashPaymentTenderedTextBox_Leave(object sender, EventArgs e)
        {
            CalculateChange();
        }

        private void CalculateTotalPrice()
        {
            totalPriceCents = dayPassPriceCents * dayPassQuantity;
            CalculateChange();
        }

        private void CashPaymentTenderedTextBox_TextChanged(object sender, EventArgs e)
        {
            // Calculate change in real-time as user types
            CalculateChange();
        }

        private void CalculateChange()
        {
            string tenderText = CashPaymentTenderedTextBox.Text.Trim();
            if (!string.IsNullOrEmpty(tenderText) && decimal.TryParse(tenderText, out decimal tenderAmount))
            {
                int tenderCents = (int)(tenderAmount * 100);
                int changeCents = tenderCents - totalPriceCents;
                decimal changeAmount = Math.Abs(changeCents) / 100.0m; // Always show positive amount

                if (changeCents < 0)
                {
                    // Customer hasn't paid enough - show how much more is needed
                    ChangeLabel.Text = $"还差: ¥{changeAmount:F2}";
                }
                else
                {
                    // Customer paid enough - show change due
                    ChangeLabel.Text = $"找零: ¥{changeAmount:F2}";
                }
            }
            else
            {
                ChangeLabel.Text = "找零: ¥0.00";
            }
        }

        private void UpdateDisplayLabels()
        {
            decimal dayPassPriceYuan = dayPassPriceCents / 100.0m;
            decimal totalYuan = totalPriceCents / 100.0m;

            QuantityPriceLabel.Text = $"{dayPassQuantity} x ¥{dayPassPriceYuan:F2}";
            TotalPriceLabel.Text = $"¥{totalYuan:F2}";
        }

        private void ProcessCashPayment()
        {
            // Check if total price is zero
            if (totalPriceCents <= 0)
            {
                MessageBox.Show("请先输入一日票数量。",
                              "无法完成支付", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DayPassQuantityTextBox.Focus();
                return;
            }

            // Get the tendered amount
            string tenderText = CashPaymentTenderedTextBox.Text.Trim();
            if (string.IsNullOrEmpty(tenderText))
            {
                CashPaymentTenderedTextBox.Focus();
                return;
            }

            if (!decimal.TryParse(tenderText, out decimal tenderAmount) || tenderAmount < 0)
            {
                MessageBox.Show("请输入有效的支付金额。",
                              "支付金额无效", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CashPaymentTenderedTextBox.Focus();
                CashPaymentTenderedTextBox.SelectAll();
                return;
            }

            // Convert to cents for precise calculation
            int tenderCents = (int)(tenderAmount * 100);
            int changeCents = tenderCents - totalPriceCents;

            // Check if amount is sufficient
            if (changeCents < 0)
            {
                decimal shortfallAmount = Math.Abs(changeCents) / 100.0m;
                decimal totalAmount = totalPriceCents / 100.0m;
                MessageBox.Show($"支付金额不足。\n\n应付金额：¥{totalAmount:F2}\n已付金额：¥{tenderAmount:F2}\n还需支付：¥{shortfallAmount:F2}",
                              "支付金额不足", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CashPaymentTenderedTextBox.Focus();
                CashPaymentTenderedTextBox.SelectAll();
                return;
            }

            // Payment is sufficient, update change label
            decimal changeAmount = changeCents / 100.0m;
            ChangeLabel.Text = $"找零: ¥{changeAmount:F2}";

            try
            {
                // Create and show ticket printing dialog
                using (var printDialog = TicketPrintDialogForm.CreateForDayPass(
                    dayPassQuantity,
                    dayPassPriceCents,
                    "现金"))
                {
                    var result = printDialog.ShowDialog(this);

                    if (result == DialogResult.OK)
                    {
                        // Show success message with change amount
                        string successMessage;
                        if (changeAmount > 0)
                        {
                            successMessage = $"支付成功！票据已打印。\n\n找零：¥{changeAmount:F2}";
                        }
                        else
                        {
                            successMessage = "支付成功！票据已打印。\n\n无需找零。";
                        }

                        MessageBox.Show(successMessage,
                                      "支付成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Clear the transaction after successful payment
                        CancelTransaction();
                    }
                    else
                    {
                        // Printing failed or was cancelled
                        MessageBox.Show("票据打印失败或被取消。请重试。",
                                      "打印失败", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        // Reset change label since payment didn't complete
                        CalculateChange();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"处理支付时发生错误：\n\n{ex.Message}",
                              "支付错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Reset change label since payment didn't complete
                CalculateChange();
            }
        }

        private void CardPaymentButton_Click(object sender, EventArgs e)
        {
            ProcessCardPayment();
        }

        private void ProcessCardPayment()
        {
            // Check if total price is zero
            if (totalPriceCents <= 0)
            {
                MessageBox.Show("请先输入一日票数量。",
                              "无法完成支付", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DayPassQuantityTextBox.Focus();
                return;
            }

            try
            {
                // Convert total price to decimal for display
                decimal totalAmount = totalPriceCents / 100.0m;

                // Show card payment dialog
                using (var cardPaymentDialog = new CardPaymentDialogForm(totalAmount))
                {
                    var result = cardPaymentDialog.ShowDialog(this);

                    if (result == DialogResult.OK && cardPaymentDialog.PaymentSuccessful)
                    {
                        // Payment successful, proceed with ticket printing
                        string paymentMethod = cardPaymentDialog.SelectedPaymentMethod;

                        // Create and show ticket printing dialog
                        using (var printDialog = TicketPrintDialogForm.CreateForDayPass(
                            dayPassQuantity,
                            dayPassPriceCents,
                            paymentMethod))
                        {
                            var printResult = printDialog.ShowDialog(this);

                            if (printResult == DialogResult.OK)
                            {
                                // Show success message
                                string successMessage = $"支付成功！票据已打印。\n\n支付方式：{paymentMethod}";

                                MessageBox.Show(successMessage,
                                              "支付成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // Clear the transaction after successful payment
                                CancelTransaction();
                            }
                            else
                            {
                                // Printing failed
                                MessageBox.Show("票据打印失败。请联系技术支持。",
                                              "打印失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"处理电子支付时发生错误：\n\n{ex.Message}",
                              "支付错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            CancelTransaction();
        }

        public void CancelTransaction()
        {
            // Clear quantity input
            DayPassQuantityTextBox.Text = string.Empty;

            // Clear cash payment
            CashPaymentTenderedTextBox.Text = string.Empty;

            // Reset internal variables
            dayPassQuantity = 0;
            totalPriceCents = 0;

            // Update display
            UpdateDisplayLabels();
            CalculateChange();

            // Return focus to quantity input
            DayPassQuantityTextBox.Focus();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Don't process keys if the control is hidden
            if (!this.Visible)
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }

            // Handle Ctrl+A for text boxes
            if (keyData == (Keys.Control | Keys.A))
            {
                if (this.ActiveControl is TextBox textBox)
                {
                    textBox.SelectAll();
                    return true;
                }
            }

            // F1 hotkey to focus on quantity textbox
            if (keyData == Keys.F1)
            {
                DayPassQuantityTextBox.Focus();
                DayPassQuantityTextBox.SelectAll();
                return true;
            }

            // Alt+E hotkey to cancel the transaction
            if (keyData == (Keys.Alt | Keys.E))
            {
                CancelTransaction();
                return true;
            }

            // Ctrl+1 for cash payment
            if (keyData == (Keys.Control | Keys.D1))
            {
                CashPaymentTenderedTextBox.Focus();
                return true;
            }

            // Ctrl+4 for card payment
            if (keyData == (Keys.Control | Keys.D4))
            {
                ProcessCardPayment();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void SellPassesControl_VisibleChanged(object sender, EventArgs e)
        {
            // Focus on the day pass quantity text box when the control becomes visible
            if (this.Visible && this.IsHandleCreated)
            {
                try
                {
                    // Use BeginInvoke to ensure the control is fully visible and ready to receive focus
                    this.BeginInvoke((Action)(() => {
                        DayPassQuantityTextBox.Focus();
                        DayPassQuantityTextBox.SelectAll(); // Select all text for better user experience
                    }));
                }
                catch (Exception ex)
                {
                    // Log error but don't crash
                    Console.WriteLine($"Error focusing DayPassQuantityTextBox on visible: {ex.Message}");
                }
            }
        }
    }
}