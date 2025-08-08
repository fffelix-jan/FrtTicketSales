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

namespace FrtTicketVendingMachine
{
    public partial class MainForm : Form
    {
        readonly Color defaultColor = Color.Crimson;
        readonly Color selectedColor = Color.Blue;
        AppText.Language language = AppText.Language.Chinese;
        State kioskState = State.HomeScreen;
        CheckoutControl checkout = new CheckoutControl();

        StationInfo selectedStationInfo;
        FareInfo selectedFareInfo;
        int selectedQuantity;
        int totalPriceCents;

        // Set this to false when processing payments and printing tickets to prevent the machine from swallowing the user's money
        bool canCancel = true;

        // API client
        FareApiClient fareApiClient = new FareApiClient(SimpleConfig.Get("API_ENDPOINT"));

        // Enum for different states of the kiosk
        enum State
        {
            HomeScreen,
            WaitSelectTicketQuantity,
            WaitSelectPaymentMethod,
            WaitForPayment,
            PaymentProcessing,
            PrintingTicket,
        }

        public MainForm()
        {
            InitializeComponent();
            SetKioskLanguage(this.language);

            // Center a bunch of stuff that should be centered
            CenterControlHorizontally(WelcomePanel);
            CenterControlHorizontally(SelectTicketQuantityPanel);
            CenterControlHorizontally(SelectPaymentMethodPanel);
            CenterControlHorizontally(ChineseWelcomeLabel);
            CenterControlHorizontally(EnglishWelcomeLabel);
            CenterControlHorizontally(ChineseStationNameLabel);
            CenterControlHorizontally(EnglishStationNameLabel);

            // Add the checkout control to the station selection panel (so it doesn't overlap in the designer)
            StationSelectionPanel.Controls.Add(checkout);
            checkout.Dock = DockStyle.Fill;

            // Hide the panels that are not needed at the start (to be replaced with ResetKiosk)
            // TODO: replace with ResetKiosk
            SelectTicketQuantityPanel.Hide();
            SelectPaymentMethodPanel.Hide();

            CancelButton.Hide();

            CenterControlHorizontally(ClockRoundedPanel);
            UpdateClockDisplay();
        }

        // Resets the kiosk to the start state if allowed
        public void ResetKiosk()
        {
            if (canCancel)
            {
                // Set canCancel to false to prevent multiple reset attempts
                canCancel = false;

                // Grey out the cancel button to indicate it's not clickable
                CancelButton.Enabled = false;

                // Hide the quantity select and payment select panels
                SelectPaymentMethodPanel.Hide();
                SelectTicketQuantityPanel.Hide();

                // TODO: Eject the coins here and wait for them to be ejected without blocking the UI
                // Show a "Cancelling..." message to the user
                checkout.InstructionsText = this.language == AppText.Language.Chinese ? 
                    AppText.CancellingChinese : AppText.CancellingEnglish;
                // TODO: Start async coin ejection process that calls ResetKioskStage2() when complete

                // For now, start the fake cancel delay timer
                CancelFakeDelayTimer.Start();
            }
        }

        // After ejecting the coins, the reset logic continues here
        // DO NOT CALL THIS FUNCTION DIRECTLY
        private void ResetKioskStage2()
        {
            // Reset the kiosk state to home screen
            kioskState = State.HomeScreen;
            
            // Clear checkout control data
            checkout.DestinationText = "";
            checkout.QuantityText = "";
            checkout.TotalPriceText = "";
            
            // Hide all transaction panels
            SelectTicketQuantityPanel.Hide();
            SelectPaymentMethodPanel.Hide();
            checkout.Hide();
            
            // Show and bring home screen elements to front
            WelcomePanel.Show();
            WelcomePanel.BringToFront();
            MainFrtFullLineMapControl.Show();
            MainFrtFullLineMapControl.BringToFront();
            
            // Reset map to show all lines
            MainFrtFullLineMapControl.ShowLine(0);
            
            // Update the clock display
            UpdateClockDisplay();
            
            // Re-enable cancellation for future transactions
            canCancel = true;

            // Hide the cancel button
            CancelButton.Hide();

            // Show the language toggle button
            LanguageToggleButton.Show();

            // Enable the line buttons
            foreach (Control c in LineSelectFlowLayoutPanel.Controls)
            {
                c.Enabled = true;
            }
        }

        // Sets the language of the kiosk
        public void SetKioskLanguage(AppText.Language language)
        {
            this.language = language;

            // Set all the text on the controls accordingly
            if (language == AppText.Language.Chinese)
            {
                // Set the checkout control language
                checkout.Language = AppText.Language.Chinese;
                // Set language toggle button text
                LanguageToggleButton.Text = "English";
                // Set select ticket label
                SelectTicketQuantityLabel.Text = AppText.SelectTicketsChinese;
                // Set select payment method label
                SelectPaymentMethodLabel.Text = AppText.SelectPaymentMethodChinese;
                // Set cash button text
                CashButton.Text = AppText.CashChinese;
                // Set fake Alipay/WeChat button text
                QRPayButton.Text = AppText.FakeAlipayWeChatChinese;
                // Set line buttons text
                AllLinesButton.Text = AppText.AllLinesChinese;
                Line1Button.Text = AppText.Line1Chinese;
                Line2Button.Text = AppText.Line2Chinese;
                // Set cancel button text
                CancelButton.Text = AppText.CancelChinese;
            }
            // Else, set it to English
            else
            {
                // Set the checkout control language
                checkout.Language = AppText.Language.English;
                // Set language toggle button text
                LanguageToggleButton.Text = "中文";
                // Set select ticket label
                SelectTicketQuantityLabel.Text = AppText.SelectTicketsEnglish;
                // Set select payment method label
                SelectPaymentMethodLabel.Text = AppText.SelectPaymentMethodEnglish;
                // Set cash button text
                CashButton.Text = AppText.CashEnglish;
                // Set fake Alipay/WeChat button text
                QRPayButton.Text = AppText.FakeAlipayWeChatEnglish;
                // Set line buttons text
                AllLinesButton.Text = AppText.AllLinesEnglish;
                Line1Button.Text = AppText.Line1English;
                Line2Button.Text = AppText.Line2English;
                // Set cancel button text
                CancelButton.Text = AppText.CancelEnglish;
            }

            // Update the clock display
            UpdateClockDisplay();
        }

        // Centers controls horizontally
        public void CenterControlHorizontally(Control c)
        {
            c.Left = (c.Parent.Width - c.Width) / 2;
        }

        // Centers controls vertically
        public void CenterControlVertically(Control c)
        {
            c.Top = (c.Parent.Height - c.Height) / 2;
        }

        private void UpdateClockDisplay()
        {
            // Set based on language
            if (language == AppText.Language.Chinese)
            {
                DateTimeLabel.Text = DateTime.Now.ToString("yyyy年M月d日\r\nHH:mm");
            }
            else
            {
                DateTimeLabel.Text = DateTime.Now.ToString("yyyy-MM-dd\r\nHH:mm");
            }

            // Center the control
            CenterControlHorizontally(DateTimeLabel);
            CenterControlVertically(DateTimeLabel);
        }

        private void ClockUpdateTimer_Tick(object sender, EventArgs e)
        {
            // Update the clock display
            UpdateClockDisplay();
        }

        private void AllLinesButton_Click(object sender, EventArgs e)
        {
            MainFrtFullLineMapControl.ShowLine(0);
        }

        private void Line1Button_Click(object sender, EventArgs e)
        {
            MainFrtFullLineMapControl.ShowLine(1);
        }

        private void Line2Button_Click(object sender, EventArgs e)
        {
            MainFrtFullLineMapControl.ShowLine(2);
        }

        private void LanguageToggleButton_Click(object sender, EventArgs e)
        {
            // Toggle the language
            if (this.language == AppText.Language.Chinese)
            {
                SetKioskLanguage(AppText.Language.English);
            }
            else
            {
                SetKioskLanguage(AppText.Language.Chinese);
            }
        }

        private async void SelectStation(string stationCode)
        {
            // Show loading message while API call is in progress
            checkout.InstructionsText = this.language == AppText.Language.Chinese ? 
                "正在获取车站信息..." : "Loading station information...";
            
            // Disable UI to prevent multiple clicks
            foreach(Control c in LineSelectFlowLayoutPanel.Controls)
            {
                c.Enabled = false;
            }

            try
            {
                // Use await instead of .Result to avoid blocking the UI thread
                selectedStationInfo = await fareApiClient.GetStationNameAsync(stationCode);
                
                // Get the current station from config (where this kiosk is located)
                string currentStationCode = SimpleConfig.Get("CURRENT_STATION", "FLZ"); // Default to FLZ if not configured
                
                // Get the fare from current station to selected destination
                selectedFareInfo = await fareApiClient.GetFareAsync(currentStationCode, selectedStationInfo.StationCode);
                
                // Format price each (assuming cents, convert to currency)
                string priceEach = $"¥{selectedFareInfo.FareCents / 100.0:F2}";

                // Set the price each in checkout
                checkout.PriceEachText = priceEach;
            }
            catch (Exception ex)
            {
                // Handle API errors gracefully
                MessageBox.Show(this.language == AppText.Language.Chinese ? 
                    "获取车站信息失败，请稍后再试。" : "Failed to get station information, please try again later.", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                // Re-enable UI and return
                foreach(Control c in LineSelectFlowLayoutPanel.Controls)
                {
                    c.Enabled = true;
                }
                return;
            }

            // Set the selected station info with proper name
            string stationDisplayName = this.language == AppText.Language.Chinese ? 
                selectedStationInfo.ChineseName : 
                selectedStationInfo.EnglishName;

            // Enable and show the cancel button
            CancelButton.Enabled = true;
            CancelButton.Show();
            // Hide the language toggle button
            LanguageToggleButton.Hide();
            
            // Set the selected station in the checkout control
            checkout.DestinationText = stationDisplayName; // Use the proper name, not the code
            // Set the quantity and price to default values
            checkout.QuantityText = "?";
            // Transition to the next state
            kioskState = State.WaitSelectTicketQuantity;
            // Set the instructions text accordingly
            checkout.InstructionsText = this.language == AppText.Language.Chinese ? 
                AppText.PleaseSelectQuantityChinese : AppText.PleaseSelectQuantityEnglish;
            // Show the ticket quantity selection panel
            SelectTicketQuantityPanel.Show();
            SelectTicketQuantityPanel.BringToFront();
            // Hide the welcome panel
            WelcomePanel.Hide();
            // Hide the map
            MainFrtFullLineMapControl.Hide();
            // Set the animation
            checkout.SetAnimationType(CheckoutControl.AnimationType.PressButton);
            // Show the checkout panel
            checkout.Show();
        }

        private void MainFrtFullLineMapControl_StationSelected(object sender, StationSelectedEventArgs e)
        {
            SelectStation(e.StationCode);
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            ResetKiosk();
        }

        private void CancelFakeDelayTimer_Tick(object sender, EventArgs e)
        {
            CancelFakeDelayTimer.Stop();
            ResetKioskStage2();
        }

        private async void SelectTicketQuantity(int quantity)
        {
            // Set the selected quantity in the checkout control
            selectedQuantity = quantity;
            checkout.QuantityText = this.language == AppText.Language.Chinese ? 
                $"{quantity} 张" : $"{quantity} tickets";

            // Calculate total price in cents directly from selectedFareInfo (integer math - faster)
            totalPriceCents = selectedFareInfo.FareCents * quantity;
            
            // Convert to display format for checkout
            string totalPriceDisplay = $"¥{totalPriceCents / 100.0:F2}";
            checkout.TotalPriceText = totalPriceDisplay;
            
            // Transition to payment method selection state
            kioskState = State.WaitSelectPaymentMethod;
            
            // Update instructions for payment method selection
            checkout.InstructionsText = this.language == AppText.Language.Chinese ? 
                AppText.PleaseSelectPaymentMethodChinese : AppText.PleaseSelectPaymentMethodEnglish;
            
            // Hide the quantity selection panel
            SelectTicketQuantityPanel.Hide();
            
            // Show the payment method selection panel
            SelectPaymentMethodPanel.Show();
            SelectPaymentMethodPanel.BringToFront();
            
            // Update animation type for payment selection
            checkout.SetAnimationType(CheckoutControl.AnimationType.PressButton);
        }

        private void OneTicketButton_Click(object sender, EventArgs e)
        {
            SelectTicketQuantity(1);
        }

        private void TwoTicketButton_Click(object sender, EventArgs e)
        {
            SelectTicketQuantity(2);
        }

        private void ThreeTicketButton_Click(object sender, EventArgs e)
        {
            SelectTicketQuantity(3);
        }

        private void FourTicketButton_Click(object sender, EventArgs e)
        {
            SelectTicketQuantity(4);
        }

        private void FiveTicketButton_Click(object sender, EventArgs e)
        {
            SelectTicketQuantity(5);
        }

        private void SixTicketButton_Click(object sender, EventArgs e)
        {
            SelectTicketQuantity(6);
        }
    }
}
