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

        // Store reference to cash form for cancellation handling
        private InsertCashForm currentCashForm = null;

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

                // Show a "Cancelling..." message to the user
                checkout.InstructionsText = this.language == AppText.Language.Chinese ? 
                    AppText.CancellingChinese : AppText.CancellingEnglish;

                // Check if we're in cash payment state and there's a cash form open
                if (kioskState == State.WaitForPayment && currentCashForm != null)
                {
                    // Request cash ejection from the cash form and wait for completion
                    currentCashForm.RequestCashEjection();
                    return; // EjectionCompleted event will call ResetKioskStage2()
                }

                // If not in cash payment, use the fake delay timer as before
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
                LanguageToggleButton.Text = AppText.EnglishToggleText;
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
                LanguageToggleButton.Text = AppText.ChineseToggleText;
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

        private async void SelectStation(string stationCode)
        {
            // Show loading message while API call is in progress
            checkout.InstructionsText = this.language == AppText.Language.Chinese ? 
                AppText.LoadingStationInfoChinese : AppText.LoadingStationInfoEnglish;
            
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
                    AppText.StationInfoErrorChinese : AppText.StationInfoErrorEnglish, 
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
                $"{quantity} {AppText.TicketsChinese}" : $"{quantity} {AppText.TicketsEnglish}";

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

        private void CashButton_Click(object sender, EventArgs e)
        {
            // Set state to allow cancellation during payment
            canCancel = true;
            kioskState = State.WaitForPayment;
            
            // Update checkout display for cash payment
            checkout.InstructionsText = this.language == AppText.Language.Chinese ? 
                AppText.InsertCashChinese : AppText.InsertCashEnglish;
            checkout.SetAnimationType(CheckoutControl.AnimationType.InsertMoney);
            
            // Create and show cash insertion form
            currentCashForm = new InsertCashForm(totalPriceCents, this.language);
            
            // Handle cash insertion events
            currentCashForm.CashInserted += (s, insertedArgs) => {
                // Update main form display with inserted amount
                decimal insertedAmount = insertedArgs.TotalInsertedCents / 100.0m;
                checkout.InstructionsText = this.language == AppText.Language.Chinese 
                    ? string.Format(AppText.CashInsertedChinese, insertedAmount)
                    : string.Format(AppText.CashInsertedEnglish, insertedAmount);
            };
            
            // Handle cash ejection events (change or cancellation)
            currentCashForm.CashEjected += (s, ejectedArgs) => {
                decimal ejectedAmount = ejectedArgs.AmountEjectedCents / 100.0m;
                if (ejectedArgs.Reason == CashEjectionReason.Change)
                {
                    checkout.InstructionsText = this.language == AppText.Language.Chinese 
                        ? string.Format(AppText.ChangeEjectedChinese, ejectedAmount)
                        : string.Format(AppText.ChangeEjectedEnglish, ejectedAmount);
                }
                else
                {
                    checkout.InstructionsText = this.language == AppText.Language.Chinese 
                        ? string.Format(AppText.CashReturnedChinese, ejectedAmount)
                        : string.Format(AppText.CashReturnedEnglish, ejectedAmount);
                }
            };
            
            // Handle ejection completion (for cancellation)
            currentCashForm.EjectionCompleted += (s, completedArgs) => {
                currentCashForm.Hide();
                currentCashForm.Dispose();
                currentCashForm = null;
                
                // Continue with the normal reset process
                ResetKioskStage2();
            };
            
            // Handle payment completion
            currentCashForm.PaymentCompleted += async (s, completedArgs) => {
                currentCashForm.Hide();
                await CompletePayment(AppText.CashPaymentMethodEnglish);
                currentCashForm.Dispose();
                currentCashForm = null;
            };
            
            // Show the cash form
            currentCashForm.Show(this);
        }

        private async Task CompletePayment(string paymentMethod)
        {
            try
            {
                // Set state to processing
                canCancel = false;
                kioskState = State.PaymentProcessing;

                // Update UI to show processing
                checkout.InstructionsText = this.language == AppText.Language.Chinese
                    ? AppText.ProcessingPaymentChinese : AppText.ProcessingPaymentEnglish;
                checkout.SetAnimationType(CheckoutControl.AnimationType.TicketPrinting);

                // Get current station for ticket issuing
                string currentStationCode = SimpleConfig.Get("CURRENT_STATION", "FLZ");

                // Issue tickets through API
                var ticketData = await fareApiClient.IssueTicketAsync(selectedFareInfo.FareCents, currentStationCode, 0);

                // Update state and UI for printing
                kioskState = State.PrintingTicket;
                checkout.InstructionsText = this.language == AppText.Language.Chinese
                    ? AppText.PrintingTicketsChinese : AppText.PrintingTicketsEnglish;

                // Get station names for printing
                string chineseStationName = selectedStationInfo.ChineseName;
                string englishStationName = selectedStationInfo.EnglishName;
                string priceEachDisplay = $"¥{selectedFareInfo.FareCents / 100.0:F2}";

                // Print each ticket
                for (int i = 0; i < selectedQuantity; i++)
                {
                    FrtTicketPrinter.TicketGenerator.PrintTicket(
                        AppText.SingleJourneyTicketChinese, AppText.SingleJourneyTicketEnglish,
                        chineseStationName, englishStationName,
                        priceEachDisplay, paymentMethod,
                        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        ticketData.TicketString,
                        $"{ticketData.TicketNumber}-{i + 1:D2}",
                        AppText.ThankYouMessageChinese, AppText.ThankYouMessageEnglish
                    );
                }

                // Show success message
                checkout.InstructionsText = this.language == AppText.Language.Chinese
                    ? AppText.PrintingCompleteChinese : AppText.PrintingCompleteEnglish;

                // Auto-reset after 3 seconds
                var resetTimer = new Timer();
                resetTimer.Interval = 3000;
                resetTimer.Tick += (s, e) => {
                    resetTimer.Stop();
                    resetTimer.Dispose();
                    ResetKioskStage2();
                };
                resetTimer.Start();
            }
            catch (Exception ex)
            {
                // Handle errors
                checkout.InstructionsText = this.language == AppText.Language.Chinese
                    ? string.Format(AppText.ProcessingFailedChinese, ex.Message)
                    : string.Format(AppText.ProcessingFailedEnglish, ex.Message);

                // Return to payment selection after error
                canCancel = true;
                kioskState = State.WaitSelectPaymentMethod;

                // Show error for 3 seconds then return to payment selection
                var errorTimer = new Timer();
                errorTimer.Interval = 3000;
                errorTimer.Tick += (s, e) => {
                    errorTimer.Stop();
                    errorTimer.Dispose();
                    checkout.InstructionsText = this.language == AppText.Language.Chinese ?
                        AppText.SelectPaymentMethodChinese : AppText.SelectPaymentMethodEnglish;
                    checkout.SetAnimationType(CheckoutControl.AnimationType.PressButton);
                };
                errorTimer.Start();
            }
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
