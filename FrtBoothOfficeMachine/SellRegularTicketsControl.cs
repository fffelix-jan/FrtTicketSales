using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FrtAfcApiClient;

namespace FrtBoothOfficeMachine
{
    public partial class SellRegularTicketsControl : UserControl
    {
        private List<StationInfo> _allStations = new List<StationInfo>();
        int selectedStationPriceCents = 0;
        int fullFareTicketCount = 0;
        int seniorTicketCount = 0;
        int studentTicketCount = 0;
        int fullFareTicketPriceCents = 0;
        int seniorTicketPriceCents = 0;
        int studentTicketPriceCents = 0;
        int totalPriceCents = 0;

        public SellRegularTicketsControl()
        {
            InitializeComponent();

            // Some event handlers are defined here to help GitHub Copilot
            // better understand the code during development.

            // Load stations when control is created
            this.Load += SellRegularTicketsControl_Load;

            // Add event handlers for DestinationComboBox
            DestinationComboBox.Leave += DestinationComboBox_Leave;
            DestinationComboBox.KeyPress += DestinationComboBox_KeyPress;
            DestinationComboBox.TextChanged += DestinationComboBox_TextChanged;

            // Add event handlers for quantity text boxes
            FullFareTicketQuantityTextBox.KeyPress += QuantityTextBox_KeyPress;
            FullFareTicketQuantityTextBox.Enter += QuantityTextBox_Enter;
            FullFareTicketQuantityTextBox.Click += QuantityTextBox_Click;
            FullFareTicketQuantityTextBox.Leave += FullFareTicketQuantityTextBox_Leave;
            FullFareTicketQuantityTextBox.KeyDown += FullFareTicketQuantityTextBox_KeyDown;
            
            SeniorTicketQuantityTextBox.KeyPress += QuantityTextBox_KeyPress;
            SeniorTicketQuantityTextBox.Enter += QuantityTextBox_Enter;
            SeniorTicketQuantityTextBox.Click += QuantityTextBox_Click;
            SeniorTicketQuantityTextBox.Leave += SeniorTicketQuantityTextBox_Leave;
            SeniorTicketQuantityTextBox.KeyDown += SeniorTicketQuantityTextBox_KeyDown;
            
            StudentTicketQuantityTextBox.KeyPress += QuantityTextBox_KeyPress;
            StudentTicketQuantityTextBox.Enter += QuantityTextBox_Enter;
            StudentTicketQuantityTextBox.Click += QuantityTextBox_Click;
            StudentTicketQuantityTextBox.Leave += StudentTicketQuantityTextBox_Leave;
            StudentTicketQuantityTextBox.KeyDown += StudentTicketQuantityTextBox_KeyDown;
            
            // Add event handlers for cash payment textbox
            CashPaymentTenderedTextBox.KeyPress += CashPaymentTenderedTextBox_KeyPress;
            CashPaymentTenderedTextBox.KeyDown += CashPaymentTenderedTextBox_KeyDown;
            CashPaymentTenderedTextBox.Leave += CashPaymentTenderedTextBox_Leave;

            // Add event handler for Cancel button
            CancelButton.Click += CancelButton_Click;

            // Add event handler for Swipe Card button
            CardPaymentButton.Click += CardPaymentButton_Click;
        }

        /// <summary>
        /// Cancels the current transaction, clears all fields and resets to default state
        /// </summary>
        private void CancelTransaction()
        {
            // Clear destination selection
            DestinationComboBox.Text = string.Empty;
            DestinationComboBox.SelectedIndex = -1;

            // Clear all quantity text boxes
            FullFareTicketQuantityTextBox.Text = string.Empty;
            SeniorTicketQuantityTextBox.Text = string.Empty;
            StudentTicketQuantityTextBox.Text = string.Empty;

            // Clear cash payment textbox
            CashPaymentTenderedTextBox.Text = string.Empty;

            // Reset all internal variables to default state
            selectedStationPriceCents = 0;
            fullFareTicketCount = 0;
            seniorTicketCount = 0;
            studentTicketCount = 0;
            fullFareTicketPriceCents = 0;
            seniorTicketPriceCents = 0;
            studentTicketPriceCents = 0;
            totalPriceCents = 0;

            // Update all price calculations and display labels
            CalculateFullFarePrice();
            CalculateSeniorPrice();
            CalculateStudentPrice();
            UpdateDisplayLabels();

            // Return focus to the destination selector
            DestinationComboBox.Focus();
        }

        /// <summary>
        /// Handles the Cancel button click event
        /// </summary>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            CancelTransaction();
        }

        private async void SellRegularTicketsControl_Load(object sender, EventArgs e)
        {
            await LoadStationsAsync();
            UpdateDisplayLabels(); // Initialize the display labels

            // Set focus to the destination combo box after loading is complete
            // Use BeginInvoke to ensure the control is fully visible and ready to receive focus
            this.BeginInvoke((Action)(() => {
                DestinationComboBox.Focus();
            }));
        }

        private async Task LoadStationsAsync()
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

                // Load all stations from the API
                _allStations = await GlobalCredentials.ApiClient.GetAllStationsAsync();

                // Sort stations by Chinese name using Chinese culture for proper Pinyin ordering
                var chineseCulture = CultureInfo.CreateSpecificCulture("zh-CN");
                _allStations = _allStations
                    .Where(s => s.IsActive) // Only include active stations
                    .OrderBy(s => s.ChineseName, StringComparer.Create(chineseCulture, true))
                    .ToList();

                // Clear existing items
                DestinationComboBox.Items.Clear();

                // Add stations to combo box in the format: "{chineseName} - {stationCode}"
                foreach (var station in _allStations)
                {
                    string displayText = $"{station.ChineseName} - {station.StationCode}";
                    DestinationComboBox.Items.Add(displayText);
                }

                // Set combo box properties for better user experience
                DestinationComboBox.Sorted = false; // We've already sorted manually
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载车站信息失败：\n\n{ex.Message}",
                              "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void DestinationComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Handle ENTER key press
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true; // Prevent the beep sound
                if (await ProcessDestinationInputAsync())
                {
                    // If input was valid, advance focus to FullFareTicketQuantityTextBox
                    FullFareTicketQuantityTextBox.Focus();
                }
            }
            // Auto-capitalize English letters A-Z
            else if (char.IsLetter(e.KeyChar))
            {
                e.KeyChar = char.ToUpper(e.KeyChar);
            }
        }

        private async void DestinationComboBox_Leave(object sender, EventArgs e)
        {
            // Process input when the user leaves the combo box
            await ProcessDestinationInputAsync();
        }

        private async void DestinationComboBox_TextChanged(object sender, EventArgs e)
        {
            // Auto-capitalize any lowercase letters in the current text
            ComboBox comboBox = sender as ComboBox;
            int selectionStart = comboBox.SelectionStart;
            int selectionLength = comboBox.SelectionLength;
            string text = comboBox.Text;
            string upperText = text.ToUpper();

            if (text != upperText)
            {
                comboBox.Text = upperText;
                // Restore the selection instead of just the cursor position
                comboBox.SelectionStart = selectionStart;
                comboBox.SelectionLength = selectionLength;
            }

            // Set the full fare ticket quantity to 1 if quantites haven't been selected yet
            if (FullFareTicketQuantityTextBox.Text.Length == 0 &&
                SeniorTicketQuantityTextBox.Text.Length == 0 &&
                StudentTicketQuantityTextBox.Text.Length == 0)
            {
                FullFareTicketQuantityTextBox.Text = "1";
            }

            // Calculate the fare from the server
            await ProcessDestinationInputAsync();
            ProcessFullFareTicketQuantity();
            ProcessStudentTicketQuantity();
            ProcessSeniorTicketQuantity();
        }

        private void QuantityTextBox_Enter(object sender, EventArgs e)
        {
            // Select all text when entering the quantity text box
            TextBox textBox = sender as TextBox;
            textBox.SelectAll();
        }

        private void QuantityTextBox_Click(object sender, EventArgs e)
        {
            // Select all text when clicking the quantity text box
            TextBox textBox = sender as TextBox;
            textBox.SelectAll();
        }

        private void QuantityTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Handle ENTER key press to prevent ding sound
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true; // Prevent the beep sound
                return; // Let the KeyDown event handle the actual logic
            }

            // Only allow digits (0-9) and control characters (backspace, delete, etc.)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Block the character
            }
        }

        private void FullFareTicketQuantityTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ProcessFullFareTicketQuantity();
                SeniorTicketQuantityTextBox.Focus();
                e.Handled = true;
            }
        }

        private void FullFareTicketQuantityTextBox_Leave(object sender, EventArgs e)
        {
            ProcessFullFareTicketQuantity();
        }

        private void SeniorTicketQuantityTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ProcessSeniorTicketQuantity();
                StudentTicketQuantityTextBox.Focus();
                e.Handled = true;
            }
        }

        private void SeniorTicketQuantityTextBox_Leave(object sender, EventArgs e)
        {
            ProcessSeniorTicketQuantity();
        }

        private void StudentTicketQuantityTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ProcessStudentTicketQuantity();
                CashPaymentTenderedTextBox.Focus();
                e.Handled = true;
            }
        }

        private void StudentTicketQuantityTextBox_Leave(object sender, EventArgs e)
        {
            ProcessStudentTicketQuantity();
        }

        private void CashPaymentTenderedTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Handle ENTER key press to prevent ding sound
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true; // Prevent the beep sound
                return; // Let the KeyDown event handle the actual logic
            }

            // Allow digits, decimal point, and control characters
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Block the character
            }

            // Only allow one decimal point
            TextBox textBox = sender as TextBox;
            if (e.KeyChar == '.' && textBox.Text.Contains('.'))
            {
                e.Handled = true; // Block additional decimal points
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
            ProcessCashTendered();
        }

        private void ProcessFullFareTicketQuantity()
        {
            string input = FullFareTicketQuantityTextBox.Text.Trim();
            
            if (string.IsNullOrEmpty(input))
            {
                fullFareTicketCount = 0;
            }
            else if (int.TryParse(input, out int quantity) && quantity >= 0)
            {
                fullFareTicketCount = quantity;
            }
            else
            {
                MessageBox.Show("请输入有效的票数（0或正整数）。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                FullFareTicketQuantityTextBox.Focus();
                FullFareTicketQuantityTextBox.SelectAll();
                return;
            }
            
            CalculateFullFarePrice();
            UpdateDisplayLabels();
        }

        private void ProcessSeniorTicketQuantity()
        {
            string input = SeniorTicketQuantityTextBox.Text.Trim();
            
            if (string.IsNullOrEmpty(input))
            {
                seniorTicketCount = 0;
            }
            else if (int.TryParse(input, out int quantity) && quantity >= 0)
            {
                seniorTicketCount = quantity;
            }
            else
            {
                MessageBox.Show("请输入有效的票数（0或正整数）。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                SeniorTicketQuantityTextBox.Focus();
                SeniorTicketQuantityTextBox.SelectAll();
                return;
            }
            
            CalculateSeniorPrice();
            UpdateDisplayLabels();
        }

        private void ProcessStudentTicketQuantity()
        {
            string input = StudentTicketQuantityTextBox.Text.Trim();
            
            if (string.IsNullOrEmpty(input))
            {
                studentTicketCount = 0;
            }
            else if (int.TryParse(input, out int quantity) && quantity >= 0)
            {
                studentTicketCount = quantity;
            }
            else
            {
                MessageBox.Show("请输入有效的票数（0或正整数）。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                StudentTicketQuantityTextBox.Focus();
                StudentTicketQuantityTextBox.SelectAll();
                return;
            }
            
            CalculateStudentPrice();
            UpdateDisplayLabels();
        }

        private void ProcessCashTendered()
        {
            // Update change calculation when tender amount changes
            CalculateChange();
        }

        private void ProcessCashPayment()
        {
            // Check if total price is zero
            if (totalPriceCents <= 0)
            {
                MessageBox.Show("请先选择目的地和票数。",
                              "无法完成支付", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DestinationComboBox.Focus();
                return;
            }

            // Get the tendered amount
            string tenderText = CashPaymentTenderedTextBox.Text.Trim();
            if (string.IsNullOrEmpty(tenderText))
            {
                //MessageBox.Show("请输入支付金额。",
                //              "支付金额无效", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                using (var printDialog = TicketPrintDialogForm.CreateForRegularTickets(
                    selectedStationPriceCents,     // Remove: destinationStation,
                    fullFareTicketCount,
                    seniorTicketCount,
                    studentTicketCount,
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
                MessageBox.Show("请先选择目的地和票数。",
                              "无法完成支付", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DestinationComboBox.Focus();
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

                        // Get selected station for printing (if any)
                        var selectedStation = GetSelectedStation();
                        StationInfo destinationStation;

                        if (selectedStation.HasValue)
                        {
                            destinationStation = selectedStation.Value;
                        }
                        else
                        {
                            // If no station selected but price was manually entered, create a dummy station
                            destinationStation = new StationInfo
                            {
                                StationCode = "UNK",
                                ChineseName = "自定义票价",
                                EnglishName = "Custom Fare",
                                ZoneId = 0,
                                IsActive = true
                            };
                        }

                        // Create and show ticket printing dialog
                        using (var printDialog = TicketPrintDialogForm.CreateForRegularTickets(
                            selectedStationPriceCents,
                            fullFareTicketCount,
                            seniorTicketCount,
                            studentTicketCount,
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
                    // If result is DialogResult.Cancel or payment failed, do nothing
                    // User can try again or choose different payment method
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"处理电子支付时发生错误：\n\n{ex.Message}",
                              "支付错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalculateFullFarePrice()
        {
            if (selectedStationPriceCents > 0)
            {
                fullFareTicketPriceCents = selectedStationPriceCents * fullFareTicketCount;
            }
            else
            {
                fullFareTicketPriceCents = 0;
            }
            CalculateTotalPrice();
        }

        private void CalculateSeniorPrice()
        {
            if (selectedStationPriceCents > 0)
            {
                // Senior tickets are typically 50% off
                int seniorPriceCents = selectedStationPriceCents / 2;
                seniorTicketPriceCents = seniorPriceCents * seniorTicketCount;
            }
            else
            {
                seniorTicketPriceCents = 0;
            }
            CalculateTotalPrice();
        }

        private void CalculateStudentPrice()
        {
            if (selectedStationPriceCents > 0)
            {
                // Student tickets are typically 25% off
                int studentPriceCents = (selectedStationPriceCents * 75) / 100;
                studentTicketPriceCents = studentPriceCents * studentTicketCount;
            }
            else
            {
                studentTicketPriceCents = 0;
            }
            CalculateTotalPrice();
        }

        private void CalculateTotalPrice()
        {
            totalPriceCents = fullFareTicketPriceCents + seniorTicketPriceCents + studentTicketPriceCents;
            CalculateChange();
        }

        private void CalculateChange()
        {
            string tenderText = CashPaymentTenderedTextBox.Text.Trim();
            if (!string.IsNullOrEmpty(tenderText) && decimal.TryParse(tenderText, out decimal tenderAmount))
            {
                int tenderCents = (int)(tenderAmount * 100);
                int changeCents = tenderCents - totalPriceCents;
                decimal changeAmount = changeCents / 100.0m;
                ChangeLabel.Text = $"找零: {changeAmount:F2}";
            }
            else
            {
                ChangeLabel.Text = "找零: 0.00";
            }
        }

        private void UpdateDisplayLabels()
        {
            // Update full fare label
            if (selectedStationPriceCents > 0)
            {
                decimal priceYuan = selectedStationPriceCents / 100.0m;
                FullFareTicketQuantityAndPriceLabel.Text = $"{fullFareTicketCount} x {priceYuan:F2}";
                
                // Calculate discounted prices for senior and student tickets
                decimal seniorPriceYuan = (selectedStationPriceCents / 2) / 100.0m;
                decimal studentPriceYuan = ((selectedStationPriceCents * 75) / 100) / 100.0m;
                
                SeniorTicketQuantityAndPriceLabel.Text = $"{seniorTicketCount} x {seniorPriceYuan:F2}";
                StudentTicketQuantityAndPriceLabel.Text = $"{studentTicketCount} x {studentPriceYuan:F2}";
            }
            else
            {
                FullFareTicketQuantityAndPriceLabel.Text = $"{fullFareTicketCount} x 0.00";
                SeniorTicketQuantityAndPriceLabel.Text = $"{seniorTicketCount} x 0.00";
                StudentTicketQuantityAndPriceLabel.Text = $"{studentTicketCount} x 0.00";
            }
            
            // Update total label
            int totalTickets = fullFareTicketCount + seniorTicketCount + studentTicketCount;
            decimal totalYuan = totalPriceCents / 100.0m;
            TotalQuantityAndPriceLabel.Text = $"{totalTickets} @ {totalYuan:F2}";
        }

        // Our specical StationFind technology finds the station you want FAST!!! :D
        private async Task<bool> ProcessDestinationInputAsync()
        {
            string input = DestinationComboBox.Text.Trim();

            // If input is empty, reset price to 0
            if (string.IsNullOrEmpty(input))
            {
                selectedStationPriceCents = 0;
                DestinationComboBox.SelectedIndex = -1;
                CalculateFullFarePrice();
                CalculateSeniorPrice();
                CalculateStudentPrice();
                UpdateDisplayLabels();
                return true; // Empty input is valid
            }

            bool stationSelected = false;

            // 1. Check if input exactly matches one of the dropdown items (case-insensitive)
            for (int i = 0; i < DestinationComboBox.Items.Count; i++)
            {
                if (DestinationComboBox.Items[i].ToString().Equals(input, StringComparison.OrdinalIgnoreCase))
                {
                    DestinationComboBox.SelectedIndex = i;
                    await FetchFareFromServerAsync(i); // Fetch actual fare from server
                    stationSelected = true;
                    break;
                }
            }

            // 2. Try to parse as a decimal price (yuan)
            if (!stationSelected && decimal.TryParse(input, out decimal priceYuan))
            {
                // Check if price is positive (greater than 0)
                if (priceYuan <= 0)
                {
                    MessageBox.Show("票价必须大于0元。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    DestinationComboBox.Focus();
                    DestinationComboBox.SelectAll();
                    return false;
                }

                // Convert yuan to cents (multiply by 100)
                selectedStationPriceCents = (int)(priceYuan * 100);
                // Clear any selection in the combo box since user entered a price directly
                DestinationComboBox.SelectedIndex = -1;
                // Set the text back to the entered price for clarity
                DestinationComboBox.Text = priceYuan.ToString("F2");
                stationSelected = true;
            }

            // 3. Check if input matches a Chinese station name exactly (case-insensitive)
            if (!stationSelected)
            {
                for (int i = 0; i < _allStations.Count; i++)
                {
                    if (_allStations[i].ChineseName.Equals(input, StringComparison.OrdinalIgnoreCase))
                    {
                        DestinationComboBox.SelectedIndex = i;
                        await FetchFareFromServerAsync(i); // Fetch actual fare from server
                        stationSelected = true;
                        break;
                    }
                }
            }

            // 4. Check if input matches an English station name exactly (case-insensitive)
            if (!stationSelected)
            {
                for (int i = 0; i < _allStations.Count; i++)
                {
                    if (_allStations[i].EnglishName.Equals(input, StringComparison.OrdinalIgnoreCase))
                    {
                        DestinationComboBox.SelectedIndex = i;
                        await FetchFareFromServerAsync(i); // Fetch actual fare from server
                        stationSelected = true;
                        break;
                    }
                }
            }

            // 5. Check if input is a 3-letter station code (case-insensitive)
            if (!stationSelected && input.Length == 3 && input.All(c => char.IsLetter(c)))
            {
                // Find matching station by code (case-insensitive)
                for (int i = 0; i < _allStations.Count; i++)
                {
                    if (_allStations[i].StationCode.Equals(input, StringComparison.OrdinalIgnoreCase))
                    {
                        DestinationComboBox.SelectedIndex = i;
                        await FetchFareFromServerAsync(i); // Fetch actual fare from server
                        stationSelected = true;
                        break;
                    }
                }

                if (!stationSelected)
                {
                    // Invalid station code - show error message and return focus
                    MessageBox.Show("无效的车站代码。请输入有效的三字母车站代码。",
                                  "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    DestinationComboBox.Focus();
                    DestinationComboBox.SelectAll();
                    return false;
                }
            }

            if (stationSelected)
            {
                // Check if all ticket quantities are empty (no tickets specified yet)
                bool allQuantitiesEmpty = string.IsNullOrWhiteSpace(FullFareTicketQuantityTextBox.Text) &&
                                         string.IsNullOrWhiteSpace(SeniorTicketQuantityTextBox.Text) &&
                                         string.IsNullOrWhiteSpace(StudentTicketQuantityTextBox.Text);

                // If all quantities are empty, prefill full-fare ticket quantity as 1
                if (allQuantitiesEmpty)
                {
                    FullFareTicketQuantityTextBox.Text = "1";
                    ProcessFullFareTicketQuantity(); // Process the prefilled quantity
                }

                // Recalculate prices and update display
                CalculateFullFarePrice();
                CalculateSeniorPrice();
                CalculateStudentPrice();
                UpdateDisplayLabels();
                return true;
            }

            // If we get here, the input doesn't match any expected format
            MessageBox.Show("请输入有效的车站代码（3个字母）、车站中文名称、英文名称或票价金额。",
                          "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            DestinationComboBox.Focus();
            DestinationComboBox.SelectAll();
            return false;
        }

        /// <summary>
        /// Fetches the actual fare from the server for the selected destination station
        /// </summary>
        /// <param name="stationIndex">Index of the selected station in _allStations list</param>
        private async Task FetchFareFromServerAsync(int stationIndex)
        {
            try
            {
                // Check if we have an authenticated API client
                if (GlobalCredentials.ApiClient == null)
                {
                    MessageBox.Show("API客户端未初始化。请重新登录。",
                                  "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    selectedStationPriceCents = 0;
                    return;
                }

                // Get the current station from config (where this booth is located)
                string currentStationCode = SimpleConfig.Get("CURRENT_STATION", "FLZ"); // Default to FLZ if not configured

                // Get the destination station code
                string destinationStationCode = _allStations[stationIndex].StationCode;

                // Don't fetch fare if destination is the same as origin
                if (currentStationCode.Equals(destinationStationCode, StringComparison.OrdinalIgnoreCase))
                {
                    selectedStationPriceCents = 0;
                    return;
                }

                // Fetch fare information from the server
                var fareInfo = await GlobalCredentials.ApiClient.GetFareAsync(currentStationCode, destinationStationCode);
                
                // Update the selected station price with the fetched fare
                selectedStationPriceCents = fareInfo.FareCents;
            }
            catch (Exception ex)
            {
                // Handle API errors gracefully
                MessageBox.Show($"获取票价信息失败：\n\n{ex.Message}",
                              "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                
                // Reset price to 0 on error
                selectedStationPriceCents = 0;
            }
        }

        // Update the synchronous wrapper to call the async version
        private bool ProcessDestinationInput()
        {
            // For compatibility with existing event handlers, we need to run the async method synchronously
            // This is not ideal but necessary for the current event handler structure
            try
            {
                return ProcessDestinationInputAsync().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"处理目的地输入时发生错误：\n\n{ex.Message}",
                              "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Gets the selected station info from the combo box
        /// </summary>
        /// <returns>Selected StationInfo or null if nothing selected</returns>
        public StationInfo? GetSelectedStation()
        {
            if (DestinationComboBox.SelectedIndex >= 0 && DestinationComboBox.SelectedIndex < _allStations.Count)
            {
                return _allStations[DestinationComboBox.SelectedIndex];
            }
            return null;
        }

        /// <summary>
        /// Gets the selected ticket price in cents
        /// </summary>
        /// <returns>Price in cents (0 if no price set)</returns>
        public int GetSelectedPriceCents()
        {
            return selectedStationPriceCents;
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
                    return true; // Indicates we handled the key
                }
            }

            if (keyData == (Keys.Control | Keys.D1))
            {
                CashPaymentTenderedTextBox.Focus();
                return true;
            }

            if (keyData == (Keys.Control | Keys.D4))
            {
                CardPaymentButton.PerformClick();
                return true;
            }

            // F1 hotkey to focus on the destination combo box
            if (keyData == Keys.F1)
            {
                DestinationComboBox.Focus();
                return true; // Indicates we handled the key
            }

            // F2 hotkey to focus on the full fare ticket quantity text box
            if (keyData == Keys.F2)
            {
                FullFareTicketQuantityTextBox.Focus();
                return true; // Indicates we handled the key
            }

            // F3 hotkey to focus on the senior ticket quantity text box
            if (keyData == Keys.F3)
            {
                SeniorTicketQuantityTextBox.Focus();
                return true; // Indicates we handled the key
            }

            // F4 hotkey to focus on the student ticket quantity text box
            if (keyData == Keys.F4)
            {
                StudentTicketQuantityTextBox.Focus();
                return true; // Indicates we handled the key
            }

            // Alt+E hotkey to cancel the transaction
            if (keyData == (Keys.Alt | Keys.E))
            {
                CancelTransaction();
                return true; // Indicates we handled the key
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}