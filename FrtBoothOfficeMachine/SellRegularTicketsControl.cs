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
            FullFareTicketQuantityTextBox.Leave += FullFareTicketQuantityTextBox_Leave;
            FullFareTicketQuantityTextBox.KeyDown += FullFareTicketQuantityTextBox_KeyDown;
            
            SeniorTicketQuantityTextBox.KeyPress += QuantityTextBox_KeyPress;
            SeniorTicketQuantityTextBox.Leave += SeniorTicketQuantityTextBox_Leave;
            SeniorTicketQuantityTextBox.KeyDown += SeniorTicketQuantityTextBox_KeyDown;
            
            StudentTicketQuantityTextBox.KeyPress += QuantityTextBox_KeyPress;
            StudentTicketQuantityTextBox.Leave += StudentTicketQuantityTextBox_Leave;
            StudentTicketQuantityTextBox.KeyDown += StudentTicketQuantityTextBox_KeyDown;
            
            // Add event handlers for cash payment textbox
            CashPaymentTenderedTextBox.KeyPress += CashPaymentTenderedTextBox_KeyPress;
            CashPaymentTenderedTextBox.KeyDown += CashPaymentTenderedTextBox_KeyDown;
            CashPaymentTenderedTextBox.Leave += CashPaymentTenderedTextBox_Leave;
        }

        private async void SellRegularTicketsControl_Load(object sender, EventArgs e)
        {
            await LoadStationsAsync();
            UpdateDisplayLabels(); // Initialize the display labels
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

        private void DestinationComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Handle ENTER key press
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true; // Prevent the beep sound
                if (ProcessDestinationInput())
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

        private void DestinationComboBox_TextChanged(object sender, EventArgs e)
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
        }

        private void DestinationComboBox_Leave(object sender, EventArgs e)
        {
            // Process input when the user leaves the combo box
            ProcessDestinationInput();
        }

        private void QuantityTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
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
            // TODO: Implement payment processing and ticket printing
            MessageBox.Show("支付处理和票据打印功能将在稍后实现。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            TotalQuantityAndPriceLabel.Text = $"{totalTickets} x {totalYuan:F2}";
        }

        // Our specical StationFind technology finds the station you want FAST!!! :D
        private bool ProcessDestinationInput()
        {
            string input = DestinationComboBox.Text.Trim();

            // If input is empty, reset price to 0
            if (string.IsNullOrEmpty(input))
            {
                selectedStationPriceCents = 0;
                DestinationComboBox.SelectedIndex = -1;
                UpdateAllPrices();
                return true; // Empty input is valid
            }

            // 1. Check if input exactly matches one of the dropdown items (case-insensitive)
            for (int i = 0; i < DestinationComboBox.Items.Count; i++)
            {
                if (DestinationComboBox.Items[i].ToString().Equals(input, StringComparison.OrdinalIgnoreCase))
                {
                    DestinationComboBox.SelectedIndex = i;
                    selectedStationPriceCents = 0; // Reset price since we selected a station
                    UpdateAllPrices();
                    return true;
                }
            }

            // 2. Try to parse as a decimal price (yuan)
            if (decimal.TryParse(input, out decimal priceYuan))
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
                UpdateAllPrices();
                return true;
            }

            // 3. Check if input matches a Chinese station name exactly (case-insensitive)
            for (int i = 0; i < _allStations.Count; i++)
            {
                if (_allStations[i].ChineseName.Equals(input, StringComparison.OrdinalIgnoreCase))
                {
                    DestinationComboBox.SelectedIndex = i;
                    selectedStationPriceCents = 0; // Reset price since we selected a station
                    UpdateAllPrices();
                    return true;
                }
            }

            // 4. Check if input matches an English station name exactly (case-insensitive)
            for (int i = 0; i < _allStations.Count; i++)
            {
                if (_allStations[i].EnglishName.Equals(input, StringComparison.OrdinalIgnoreCase))
                {
                    DestinationComboBox.SelectedIndex = i;
                    selectedStationPriceCents = 0; // Reset price since we selected a station
                    UpdateAllPrices();
                    return true;
                }
            }

            // 5. Check if input is a 3-letter station code (case-insensitive)
            if (input.Length == 3 && input.All(c => char.IsLetter(c)))
            {
                // Find matching station by code (case-insensitive)
                for (int i = 0; i < _allStations.Count; i++)
                {
                    if (_allStations[i].StationCode.Equals(input, StringComparison.OrdinalIgnoreCase))
                    {
                        DestinationComboBox.SelectedIndex = i;
                        selectedStationPriceCents = 0; // Reset price since we selected a station
                        UpdateAllPrices();
                        return true;
                    }
                }

                // Invalid station code - show error message and return focus
                MessageBox.Show("无效的车站代码。请输入有效的三字母车站代码。",
                              "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DestinationComboBox.Focus();
                DestinationComboBox.SelectAll();
                return false;
            }

            // If we get here, the input doesn't match any expected format
            MessageBox.Show("请输入有效的车站代码（3个字母）、车站中文名称、英文名称或票价金额。",
                          "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            DestinationComboBox.Focus();
            DestinationComboBox.SelectAll();
            return false;
        }

        private void UpdateAllPrices()
        {
            CalculateFullFarePrice();
            CalculateSeniorPrice();
            CalculateStudentPrice();
            UpdateDisplayLabels();
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

            // Handle Ctrl+A for DestinationComboBox
            if (keyData == (Keys.Control | Keys.A) && DestinationComboBox.Focused)
            {
                DestinationComboBox.SelectAll();
                return true; // Indicates we handled the key
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