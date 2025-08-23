using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
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

        // Transaction cancelled control
        TransactionCancelledControl transactionCancelledControl = new TransactionCancelledControl();

        // Preloaded beep sound for all clicks
        private readonly SoundPlayer beepSoundPlayer;

        // Preloaded success sound for successful transactions
        private readonly SoundPlayer successSoundPlayer;

        // Cursor visibility state
        private bool isCursorVisible = false;

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

        // Add these fields to the MainForm class
        private Timer qrPaymentPollingTimer = null;
        private string currentQROrderId = null;
        private int qrPaymentTimeoutSeconds = 300; // 5 minutes timeout

        public MainForm()
        {
            InitializeComponent();

            // Hide cursor by default (touchscreen kiosk)
            HideCursor();

            // Preload the beep sound into memory to prevent stuttering during playback
            beepSoundPlayer = new SoundPlayer();
            try
            {
                // Create a new MemoryStream and copy the resource data into it
                var memoryStream = new MemoryStream();
                using (var resourceStream = Properties.Resources.Beep)
                {
                    resourceStream.CopyTo(memoryStream);
                    memoryStream.Position = 0; // Reset position to beginning
                }

                // Create SoundPlayer with the copied data
                beepSoundPlayer = new SoundPlayer(memoryStream);
                beepSoundPlayer.LoadAsync(); // Load the sound asynchronously to avoid blocking the UI
            }
            catch (Exception ex)
            {
                // If sound loading fails, create an empty SoundPlayer to prevent crashes
                Console.WriteLine($"Warning: Failed to load beep sound: {ex.Message}");
                beepSoundPlayer = new SoundPlayer();
            }

            // Preload the success sound similarly if needed
            try
            {
                // Create a new MemoryStream and copy the resource data into it
                var successMemoryStream = new MemoryStream();
                using (var resourceStream = Properties.Resources.RegularTap)
                {
                    resourceStream.CopyTo(successMemoryStream);
                    successMemoryStream.Position = 0; // Reset position to beginning
                }

                // Create SoundPlayer with the copied data
                successSoundPlayer = new SoundPlayer(successMemoryStream);
                successSoundPlayer.LoadAsync(); // Load the sound asynchronously to avoid blocking the UI
            }
            catch (Exception ex)
            {
                // If sound loading fails, create an empty SoundPlayer to prevent crashes
                Console.WriteLine($"Warning: Failed to load success sound: {ex.Message}");
                successSoundPlayer = new SoundPlayer();
            }

            // Hook up click events for all controls recursively
            HookUpClickEvents(this);

            // Initialize API client with authentication
            InitializeApiClient();

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

            // Add the transaction cancelled control to the main panel
            MapSelectionRoundedPanel.Controls.Add(transactionCancelledControl);
            transactionCancelledControl.Dock = DockStyle.Fill;
            transactionCancelledControl.Hide();
            transactionCancelledControl.BringToFront();

            // Set the initial language for the transaction cancelled control
            transactionCancelledControl.Language = this.language;

            // Hide the panels that are not needed at the start (to be replaced with ResetKiosk)
            // TODO: replace with ResetKiosk
            SelectTicketQuantityPanel.Hide();
            SelectPaymentMethodPanel.Hide();

            CancelButton.Hide();

            CenterControlHorizontally(ClockRoundedPanel);
            UpdateClockDisplay();

            // Load current station information after initialization
            LoadCurrentStationInfoAsync();
        }

        /// <summary>
        /// Override WndProc to completely block right-click messages
        /// </summary>
        protected override void WndProc(ref Message m)
        {
            const int WM_RBUTTONDOWN = 0x0204;
            const int WM_RBUTTONUP = 0x0205;
            const int WM_RBUTTONDBLCLK = 0x0206;
            const int WM_CONTEXTMENU = 0x007B;

            // Block right-click related messages
            if (m.Msg == WM_RBUTTONDOWN || m.Msg == WM_RBUTTONUP ||
                m.Msg == WM_RBUTTONDBLCLK || m.Msg == WM_CONTEXTMENU)
            {
                return; // Don't pass the message to the base class
            }

            // Allow all other messages to be processed normally
            base.WndProc(ref m);
        }

        /// <summary>
        /// Hides the mouse cursor
        /// </summary>
        private void HideCursor()
        {
            Cursor.Hide();
            isCursorVisible = false;
        }

        /// <summary>
        /// Shows the mouse cursor
        /// </summary>
        private void ShowCursor()
        {
            Cursor.Show();
            isCursorVisible = true;
        }

        /// <summary>
        /// Toggles the mouse cursor visibility
        /// </summary>
        private void ToggleCursor()
        {
            if (isCursorVisible)
            {
                HideCursor();
            }
            else
            {
                ShowCursor();
            }
        }

        /// <summary>
        /// Handles keyboard input for cursor toggle and other functionality
        /// </summary>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Handle the 'C' key to toggle cursor visibility
            if (keyData == Keys.C)
            {
                ToggleCursor();
                return true; // Indicate that we handled this key
            }

            // Let the base class handle other keys
            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// Loads the current station information and updates the station name labels
        /// </summary>
        private async void LoadCurrentStationInfoAsync()
        {
            try
            {
                // Get the current station code from configuration
                string currentStationCode = SimpleConfig.Get("CURRENT_STATION", "FLZ");

                // Wait for API client to be initialized
                if (GlobalCredentials.ApiClient == null)
                {
                    Console.WriteLine("Warning: API client not initialized yet, using fallback station names");
                    SetFallbackStationNames(currentStationCode);
                    return;
                }

                // Get station information from the API
                var currentStationInfo = await GlobalCredentials.ApiClient.GetStationNameAsync(currentStationCode);

                // Update the station name labels
                ChineseStationNameLabel.Text = currentStationInfo.ChineseName;
                EnglishStationNameLabel.Text = currentStationInfo.EnglishName;

                // Center the labels after updating text
                CenterControlHorizontally(ChineseStationNameLabel);
                CenterControlHorizontally(EnglishStationNameLabel);

                Console.WriteLine($"Station information loaded: {currentStationInfo.ChineseName} ({currentStationInfo.EnglishName})");
            }
            catch (Exception ex)
            {
                // If API call fails, use fallback names
                Console.WriteLine($"Warning: Failed to load current station info: {ex.Message}");
                string currentStationCode = SimpleConfig.Get("CURRENT_STATION", "FLZ");
                SetFallbackStationNames(currentStationCode);
            }
        }

        /// <summary>
        /// Sets fallback station names when API call fails
        /// </summary>
        private void SetFallbackStationNames(string stationCode)
        {
            ChineseStationNameLabel.Text = $"{stationCode}站";
            EnglishStationNameLabel.Text = $"{stationCode} Station";

            // Center the labels after updating text
            CenterControlHorizontally(ChineseStationNameLabel);
            CenterControlHorizontally(EnglishStationNameLabel);
        }

        /// <summary>
        /// Recursively hooks up click events for all controls on the form to play the beep sound
        /// </summary>
        private void HookUpClickEvents(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                // Hook up Click event for clickable controls
                if (control is Button)
                {
                    control.Click += Control_Click;
                }

                // Special handling for controls that might have their own click events
                if (control is CheckoutControl || control is TransactionCancelledControl)
                {
                    control.Click += Control_Click;
                }

                // Recursively hook up events for child controls
                if (control.HasChildren)
                {
                    HookUpClickEvents(control);
                }
            }

            // Also hook up the form's own click event
            if (parent == this)
            {
                this.Click += Control_Click;
            }
        }

        /// <summary>
        /// Event handler that plays the beep sound when any control is clicked
        /// </summary>
        private void Control_Click(object sender, EventArgs e)
        {
            PlayBeepSound();
        }

        /// <summary>
        /// Plays the preloaded beep sound
        /// </summary>
        private void PlayBeepSound()
        {
            try
            {
                beepSoundPlayer?.Play();
            }
            catch (Exception ex)
            {
                // If sound playback fails, log it but don't crash the application
                Console.WriteLine($"Warning: Failed to play beep sound: {ex.Message}");
            }
        }

        /// <summary>
        /// Clean up resources when the form is disposed
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                StopQRPaymentPolling();
                beepSoundPlayer?.Dispose();
                successSoundPlayer?.Dispose();
            }
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Initializes the API client with authentication credentials
        /// </summary>
        private void InitializeApiClient()
        {
            try
            {
                string apiEndpoint = SimpleConfig.Get("API_ENDPOINT");
                string username = SimpleConfig.Get("API_USERNAME", "kiosk");
                string password = SimpleConfig.Get("API_PASSWORD", "kiosk123");

                if (string.IsNullOrEmpty(apiEndpoint))
                {
                    throw new InvalidOperationException("API_ENDPOINT not configured in config file");
                }

                // Create API client
                var apiClient = new FareApiClient(apiEndpoint);

                // Set authentication credentials
                apiClient.SetBasicAuthentication(username, password);

                // Store in GlobalCredentials
                GlobalCredentials.SetCredentials(username, 1, apiClient); // Use ID 1 for kiosk user

                Console.WriteLine($"API client initialized for kiosk user: {username}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to initialize API client: {ex.Message}");
                MessageBox.Show($"Failed to initialize API connection:\n\n{ex.Message}\n\nThe application may not function properly.",
                              "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Resets the kiosk to the start state if allowed
        public void ResetKiosk()
        {
            if (canCancel)
            {
                // Stop QR payment polling if active
                StopQRPaymentPolling();
                currentQROrderId = null;

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
                    return; // EjectionCompleted event will call ShowTransactionCancelledAndReset()
                }

                // If not in cash payment, show the cancelled control immediately
                ShowTransactionCancelledAndReset();
            }
        }

        // Shows the transaction cancelled control for 2 seconds, then completes the reset
        private void ShowTransactionCancelledAndReset()
        {
            // Hide all other panels first
            WelcomePanel.Hide();
            MainFrtFullLineMapControl.Hide();
            SelectTicketQuantityPanel.Hide();
            SelectPaymentMethodPanel.Hide();
            checkout.Hide();

            // Show the transaction cancelled control
            transactionCancelledControl.Show();
            transactionCancelledControl.BringToFront();

            // Start the 2-second timer to complete the reset
            CancelDelayTimer.Start();
        }

        // After ejecting the coins, the reset logic continues here
        // DO NOT CALL THIS FUNCTION DIRECTLY
        private void ResetKioskStage2()
        {
            // Hide the transaction cancelled control first
            transactionCancelledControl.Hide();

            // Set the language back to Chinese
            SetKioskLanguage(AppText.Language.Chinese);

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

            // Update the transaction cancelled control language
            transactionCancelledControl.Language = this.language;

            // Update the clock display
            UpdateClockDisplay();
        }

        private async void SelectStation(string stationCode)
        {
            // Check if we have an authenticated API client
            if (GlobalCredentials.ApiClient == null)
            {
                MessageBox.Show(this.language == AppText.Language.Chinese ?
                    "API客户端未初始化。请检查网络连接。" : "API client not initialized. Please check network connection.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Show loading message while API call is in progress
            checkout.InstructionsText = this.language == AppText.Language.Chinese ?
                AppText.LoadingStationInfoChinese : AppText.LoadingStationInfoEnglish;

            // Disable UI to prevent multiple clicks
            foreach (Control c in LineSelectFlowLayoutPanel.Controls)
            {
                c.Enabled = false;
            }

            try
            {
                // Use GlobalCredentials.ApiClient instead of fareApiClient
                selectedStationInfo = await GlobalCredentials.ApiClient.GetStationNameAsync(stationCode);

                // Get the current station from config (where this kiosk is located)
                string currentStationCode = SimpleConfig.Get("CURRENT_STATION", "FLZ"); // Default to FLZ if not configured

                // Get the fare from current station to selected destination
                selectedFareInfo = await GlobalCredentials.ApiClient.GetFareAsync(currentStationCode, selectedStationInfo.StationCode);

                // Format price each (assuming cents, convert to currency)
                string priceEach = $"¥{selectedFareInfo.FareCents / 100.0:F2}";

                // Set the price each in checkout
                checkout.PriceEachText = priceEach;
            }
            catch (Exception ex)
            {
                // print stack trace for debugging
                Console.WriteLine(ex.ToString());
                // Handle API errors gracefully
                MessageBox.Show(this.language == AppText.Language.Chinese ?
                    AppText.StationInfoErrorChinese : AppText.StationInfoErrorEnglish,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Re-enable UI and return
                foreach (Control c in LineSelectFlowLayoutPanel.Controls)
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

        private void CancelDelayTimer_Tick(object sender, EventArgs e)
        {
            CancelDelayTimer.Stop();
            ResetKioskStage2();
        }

        // Handle cash ejection completion by showing transaction cancelled control
        private void OnCashEjectionCompleted()
        {
            if (currentCashForm != null)
            {
                currentCashForm.Hide();
                currentCashForm.Dispose();
                currentCashForm = null;
            }

            // Show the transaction cancelled control for 2 seconds
            ShowTransactionCancelledAndReset();
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
            // Hide the payment method selection panel
            SelectPaymentMethodPanel.Hide();

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
                OnCashEjectionCompleted();
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

                // Update state and UI for printing
                kioskState = State.PrintingTicket;
                checkout.InstructionsText = this.language == AppText.Language.Chinese
                    ? AppText.PrintingTicketsChinese : AppText.PrintingTicketsEnglish;

                // Use the structured printing approach like TicketPrintDialogForm
                await PrintTicketsAsync(paymentMethod);

                // Show success message
                checkout.InstructionsText = this.language == AppText.Language.Chinese
                    ? AppText.PrintingCompleteChinese : AppText.PrintingCompleteEnglish;

                // Play success sound
                try
                {
                    successSoundPlayer?.Play();
                }
                catch (Exception ex)
                {
                    // If sound playback fails, log it but don't crash the application
                    Console.WriteLine($"Warning: Failed to play success sound: {ex.Message}");
                }

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
                        AppText.PleaseSelectPaymentMethodChinese : AppText.PleaseSelectPaymentMethodEnglish;
                    checkout.SetAnimationType(CheckoutControl.AnimationType.PressButton);
                };
                errorTimer.Start();
            }
        }

        /// <summary>
        /// New structured ticket printing method based on TicketPrintDialogForm approach
        /// </summary>
        private async Task PrintTicketsAsync(string paymentMethod)
        {
            string currentStationCode = SimpleConfig.Get("CURRENT_STATION", "FLZ");
            StationInfo? currentStationInfo = null;

            // Get current station information for printing
            try
            {
                currentStationInfo = await GlobalCredentials.ApiClient.GetStationNameAsync(currentStationCode);
            }
            catch (Exception ex)
            {
                // If we can't get current station info, use fallback
                Console.WriteLine($"Warning: Could not retrieve current station info: {ex.Message}");
            }

            // Print each ticket with progress updates
            for (int i = 0; i < selectedQuantity; i++)
            {
                // Update progress message
                string progressMessage = this.language == AppText.Language.Chinese
                    ? string.Format(AppText.PrintingTicketProgressChinese, i + 1, selectedQuantity)
                    : string.Format(AppText.PrintingTicketProgressEnglish, i + 1, selectedQuantity);

                checkout.InstructionsText = progressMessage;

                // Small delay to show progress
                await Task.Delay(200);

                // Issue ticket through API for each individual ticket
                var ticketData = await GlobalCredentials.ApiClient.IssueTicketAsync(selectedFareInfo.FareCents, currentStationCode, 0);

                // Print the ticket with proper station information and formatting
                await PrintSingleTicketAsync(ticketData, currentStationInfo, paymentMethod, i + 1);

                // Small delay between tickets
                await Task.Delay(300);
            }

            // Final completion message
            checkout.InstructionsText = this.language == AppText.Language.Chinese
                ? AppText.PrintingTicketCompletedChinese : AppText.PrintingTicketCompletedEnglish;
        }

        /// <summary>
        /// Prints a single ticket with proper formatting and station information
        /// </summary>
        private async Task PrintSingleTicketAsync(TicketData ticketData, StationInfo? currentStationInfo, string paymentMethod, int ticketNumber)
        {
            // Use CURRENT station names for ticket printing (where the kiosk is located)
            string chineseStationName, englishStationName;
            if (currentStationInfo.HasValue)
            {
                chineseStationName = currentStationInfo.Value.ChineseName;
                englishStationName = currentStationInfo.Value.EnglishName;
            }
            else
            {
                // Fallback to configured station code if API call failed
                string currentStationCode = SimpleConfig.Get("CURRENT_STATION", "FLZ");
                chineseStationName = currentStationCode;
                englishStationName = currentStationCode;
            }

            // Format price for display
            string priceDisplay = $"¥{selectedFareInfo.FareCents / 100.0:F2}";

            // Format payment method as TicketType/PaymentMethod
            string formattedPaymentMethod = FormatPaymentMethod("全", paymentMethod);

            // Get appropriate ticket type text based on language
            string chineseTicketType = AppText.SingleJourneyTicketChinese;
            string englishTicketType = AppText.SingleJourneyTicketEnglish;

            // Get appropriate footer message based on language
            string chineseFooter = AppText.TicketFooterMessageChinese;
            string englishFooter = AppText.TicketFooterMessageEnglish;

            // Print the ticket
            await Task.Run(() =>
            {
                FrtTicketPrinter.TicketGenerator.PrintTicket(
                    chineseTicketType, englishTicketType,
                    chineseStationName, englishStationName,
                    priceDisplay, formattedPaymentMethod,
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    ticketData.TicketString,
                    $"{ticketData.TicketNumber}",
                    chineseFooter, englishFooter
                );
            });
        }

        /// <summary>
        /// Formats payment method as TicketType/PaymentMethod using single Chinese characters
        /// Based on TicketPrintDialogForm.FormatPaymentMethod
        /// </summary>
        private string FormatPaymentMethod(string ticketTypeCode, string paymentMethod)
        {
            // Map common payment methods to single character codes
            string paymentCode;
            switch (paymentMethod)
            {
                case "现金":
                    paymentCode = "现";
                    break;
                case "银行卡":
                    paymentCode = "卡";
                    break;
                case "银联":
                    paymentCode = "联";
                    break;
                case "支付宝":
                    paymentCode = "支";
                    break;
                case "微信":
                    paymentCode = "微";
                    break;
                default:
                    // Default to first character or 现
                    paymentCode = paymentMethod.Length > 0 ? paymentMethod.Substring(0, 1) : "现";
                    break;
            }

            return $"{ticketTypeCode}/{paymentCode}";
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

        /// <summary>
        /// Starts the QR payment process by creating an order and displaying the QR code
        /// </summary>
        private async void StartQRPaymentAsync()
        {
            try
            {
                // Update checkout display for QR payment creation
                checkout.InstructionsText = this.language == AppText.Language.Chinese ?
                    AppText.CreatingQROrderChinese : AppText.CreatingQROrderEnglish;
                checkout.SetAnimationType(CheckoutControl.AnimationType.PressButton);

                // Create payment order via FakeAlipay API
                var orderResult = await CreateQRPaymentOrderAsync();
                
                if (orderResult.success)
                {
                    currentQROrderId = orderResult.orderId;
                    
                    // Generate and display QR code
                    var qrCodeImage = await GenerateQRCodeImageAsync(orderResult.qrUrl);
                    
                    // Update UI to show QR code
                    checkout.SetAnimationType(CheckoutControl.AnimationType.ScanCode);
                    checkout.QRCodeImage = qrCodeImage;
                    checkout.InstructionsText = this.language == AppText.Language.Chinese ?
                        AppText.ScanQRCodeChinese : AppText.ScanQRCodeEnglish;

                    // Start polling for payment status
                    StartQRPaymentPolling();
                }
                else
                {
                    throw new Exception(orderResult.error ?? "Failed to create payment order");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"QR Payment Error: {ex.Message}");
                
                // Show error message
                checkout.InstructionsText = this.language == AppText.Language.Chinese ?
                    string.Format(AppText.QRPaymentFailedChinese, ex.Message) :
                    string.Format(AppText.QRPaymentFailedEnglish, ex.Message);

                // Return to payment selection after 3 seconds
                var errorTimer = new Timer();
                errorTimer.Interval = 3000;
                errorTimer.Tick += (s, e) => {
                    errorTimer.Stop();
                    errorTimer.Dispose();
                    ReturnToPaymentSelection();
                };
                errorTimer.Start();
            }
        }

        /// <summary>
        /// Starts polling for QR payment status
        /// </summary>
        private void StartQRPaymentPolling()
        {
            qrPaymentPollingTimer = new Timer();
            qrPaymentPollingTimer.Interval = 2000; // Poll every 2 seconds
            qrPaymentPollingTimer.Tick += QRPaymentPollingTimer_Tick;
            qrPaymentPollingTimer.Start();

            // Update instructions
            checkout.InstructionsText = this.language == AppText.Language.Chinese ?
                AppText.WaitingForPaymentChinese : AppText.WaitingForPaymentEnglish;

            // Set timeout
            var timeoutTimer = new Timer();
            timeoutTimer.Interval = qrPaymentTimeoutSeconds * 1000;
            timeoutTimer.Tick += (s, e) => {
                timeoutTimer.Stop();
                timeoutTimer.Dispose();
                HandleQRPaymentTimeout();
            };
            timeoutTimer.Start();
        }

        /// <summary>
        /// Polls the payment status
        /// </summary>
        private async void QRPaymentPollingTimer_Tick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentQROrderId))
            {
                StopQRPaymentPolling();
                return;
            }

            try
            {
                var status = await CheckQRPaymentStatusAsync(currentQROrderId);

                switch (status.status)
                {
                    case "paid":
                        StopQRPaymentPolling();
                        await HandleQRPaymentSuccess();
                        break;

                    case "cancelled":
                        StopQRPaymentPolling();
                        HandleQRPaymentCancelled();
                        break;

                    case "expired":
                        StopQRPaymentPolling();
                        HandleQRPaymentExpired();
                        break;

                    case "pending":
                        // Continue polling
                        break;

                    default:
                        // Unknown status, continue polling
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"QR Payment polling error: {ex.Message}");
                // Continue polling despite errors
            }
        }

        /// <summary>
        /// Creates a payment order via FakeAlipay API
        /// </summary>
        private async Task<(bool success, string orderId, string qrUrl, string error)> CreateQRPaymentOrderAsync()
        {
            try
            {
                string fakeAlipayEndpoint = SimpleConfig.Get("FAKE_ALIPAY_ENDPOINT", "http://localhost:5000/fakealipay");
                
                using (var httpClient = new System.Net.Http.HttpClient())
                {
                    httpClient.Timeout = TimeSpan.FromSeconds(10);
                    
                    var orderData = new
                    {
                        amount_cents = totalPriceCents,
                        description = $"FRT Transit Tickets - {selectedQuantity} tickets to {selectedStationInfo.ChineseName}"
                    };

                    // Use System.Text.Json instead of Newtonsoft.Json
                    var json = System.Text.Json.JsonSerializer.Serialize(orderData);
                    var content = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");
                    
                    var response = await httpClient.PostAsync($"{fakeAlipayEndpoint}/api/order/create", content);
                    var responseContent = await response.Content.ReadAsStringAsync();
                    
                    if (response.IsSuccessStatusCode)
                    {
                        using (var document = System.Text.Json.JsonDocument.Parse(responseContent))
                        {
                            var root = document.RootElement;
                            
                            if (root.TryGetProperty("success", out var successProperty) && successProperty.GetBoolean())
                            {
                                var orderId = root.GetProperty("order_id").GetString();
                                var qrUrl = root.GetProperty("qr_url").GetString();
                                return (true, orderId, qrUrl, null);
                            }
                            else
                            {
                                var error = root.TryGetProperty("error", out var errorProperty) 
                                    ? errorProperty.GetString() : "Unknown error";
                                return (false, null, null, error);
                            }
                        }
                    }
                    else
                    {
                        return (false, null, null, $"HTTP {response.StatusCode}: {responseContent}");
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, null, null, ex.Message);
            }
        }

        /// <summary>
        /// Generates QR code image from URL using QRCoder library
        /// </summary>
        private async Task<Image> GenerateQRCodeImageAsync(string url)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (var qrGenerator = new QRCoder.QRCodeGenerator())
                    {
                        var qrCodeData = qrGenerator.CreateQrCode(url, QRCoder.QRCodeGenerator.ECCLevel.Q);
                        
                        using (var qrCode = new QRCoder.QRCode(qrCodeData))
                        {
                            // Generate QR code as bitmap with good size and quality
                            var qrCodeImage = qrCode.GetGraphic(10, Color.Black, Color.White, true);
                            
                            // Create a larger image with padding and labels
                            var finalImage = new Bitmap(350, 400);
                            using (var graphics = Graphics.FromImage(finalImage))
                            {
                                graphics.Clear(Color.White);
                                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                                graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                                
                                // Draw QR code centered
                                var qrSize = 280;
                                var qrX = (finalImage.Width - qrSize) / 2;
                                var qrY = 30;
                                graphics.DrawImage(qrCodeImage, new Rectangle(qrX, qrY, qrSize, qrSize));
                                
                                //// Add title text
                                //using (var titleFont = new Font("Microsoft Sans Serif", 12, FontStyle.Bold))
                                //{
                                //    var titleText = this.language == AppText.Language.Chinese ? "扫码支付" : "Scan to Pay";
                                //    var titleSize = graphics.MeasureString(titleText, titleFont);
                                //    var titleX = (finalImage.Width - titleSize.Width) / 2;
                                //    graphics.DrawString(titleText, titleFont, Brushes.Black, titleX, 5);
                                //}
                                
                                //// Add amount text
                                //using (var amountFont = new Font("Microsoft Sans Serif", 10, FontStyle.Regular))
                                //{
                                //    var amountText = $"¥{totalPriceCents / 100.0:F2}";
                                //    var amountSize = graphics.MeasureString(amountText, amountFont);
                                //    var amountX = (finalImage.Width - amountSize.Width) / 2;
                                //    graphics.DrawString(amountText, amountFont, Brushes.Black, amountX, 320);
                                //}
                                
                                //// Add instruction text
                                //using (var instructionFont = new Font("Microsoft Sans Serif", 8, FontStyle.Regular))
                                //{
                                //    var instructionText = this.language == AppText.Language.Chinese 
                                //        ? "使用支付宝扫描二维码" : "Scan with Alipay";
                                //    var instructionSize = graphics.MeasureString(instructionText, instructionFont);
                                //    var instructionX = (finalImage.Width - instructionSize.Width) / 2;
                                //    graphics.DrawString(instructionText, instructionFont, Brushes.Gray, instructionX, 345);
                                //}
                            }
                            
                            // Dispose the original QR code image
                            qrCodeImage.Dispose();
                            
                            return finalImage;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"QR Code generation error: {ex.Message}");
                    
                    // Return a fallback error image
                    var errorImage = new Bitmap(350, 400);
                    using (var graphics = Graphics.FromImage(errorImage))
                    {
                        graphics.Clear(Color.LightGray);
                        graphics.DrawRectangle(Pens.Red, 10, 10, errorImage.Width - 20, errorImage.Height - 20);
                        
                        using (var font = new Font("Arial", 12, FontStyle.Bold))
                        {
                            var errorText = "QR Code\nGeneration\nFailed";
                            var textSize = graphics.MeasureString(errorText, font);
                            var textX = (errorImage.Width - textSize.Width) / 2;
                            var textY = (errorImage.Height - textSize.Height) / 2;
                            graphics.DrawString(errorText, font, Brushes.Red, textX, textY);
                        }
                    }
                    return errorImage;
                }
            });
        }

        /// <summary>
        /// Checks QR payment status via API
        /// </summary>
        private async Task<(string status, string error)> CheckQRPaymentStatusAsync(string orderId)
        {
            try
            {
                string fakeAlipayEndpoint = SimpleConfig.Get("FAKE_ALIPAY_ENDPOINT", "http://localhost:5000/fakealipay");
                
                using (var httpClient = new System.Net.Http.HttpClient())
                {
                    httpClient.Timeout = TimeSpan.FromSeconds(5);
                    
                    var response = await httpClient.GetAsync($"{fakeAlipayEndpoint}/api/order/{orderId}/status");
                    var responseContent = await response.Content.ReadAsStringAsync();
                    
                    if (response.IsSuccessStatusCode)
                    {
                        using (var document = System.Text.Json.JsonDocument.Parse(responseContent))
                        {
                            var root = document.RootElement;
                            
                            if (root.TryGetProperty("success", out var successProperty) && successProperty.GetBoolean())
                            {
                                var status = root.GetProperty("status").GetString();
                                return (status, null);
                            }
                            else
                            {
                                var error = root.TryGetProperty("error", out var errorProperty) 
                                    ? errorProperty.GetString() : "Unknown error";
                                return ("error", error);
                            }
                        }
                    }
                    else
                    {
                        return ("error", $"HTTP {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                return ("error", ex.Message);
            }
        }

        /// <summary>
        /// Handles successful QR payment
        /// </summary>
        private async Task HandleQRPaymentSuccess()
        {
            try
            {
                checkout.InstructionsText = this.language == AppText.Language.Chinese ?
                    AppText.QRPaymentSuccessChinese : AppText.QRPaymentSuccessEnglish;

                // Complete payment with 支付宝 payment method
                await CompletePayment(AppText.QRPaymentMethodChinese);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"QR Payment completion error: {ex.Message}");
                checkout.InstructionsText = this.language == AppText.Language.Chinese ?
                    string.Format(AppText.ProcessingFailedChinese, ex.Message) :
                    string.Format(AppText.ProcessingFailedEnglish, ex.Message);
                
                ReturnToPaymentSelection();
            }
            finally
            {
                currentQROrderId = null;
            }
        }

        /// <summary>
        /// Handles QR payment cancellation
        /// </summary>
        private void HandleQRPaymentCancelled()
        {
            checkout.InstructionsText = this.language == AppText.Language.Chinese ?
                AppText.QROrderCancelledChinese : AppText.QROrderCancelledEnglish;
            
            currentQROrderId = null;
            
            // Return to payment selection after 2 seconds
            var timer = new Timer();
            timer.Interval = 2000;
            timer.Tick += (s, e) => {
                timer.Stop();
                timer.Dispose();
                ReturnToPaymentSelection();
            };
            timer.Start();
        }

        /// <summary>
        /// Handles QR payment expiration
        /// </summary>
        private void HandleQRPaymentExpired()
        {
            checkout.InstructionsText = this.language == AppText.Language.Chinese ?
                AppText.QROrderExpiredChinese : AppText.QROrderExpiredEnglish;
            
            currentQROrderId = null;
            
            // Return to payment selection after 3 seconds
            var timer = new Timer();
            timer.Interval = 3000;
            timer.Tick += (s, e) => {
                timer.Stop();
                timer.Dispose();
                ReturnToPaymentSelection();
            };
            timer.Start();
        }

        /// <summary>
        /// Handles QR payment timeout
        /// </summary>
        private void HandleQRPaymentTimeout()
        {
            StopQRPaymentPolling();
            HandleQRPaymentExpired();
        }

        /// <summary>
        /// Stops QR payment polling
        /// </summary>
        private void StopQRPaymentPolling()
        {
            if (qrPaymentPollingTimer != null)
            {
                qrPaymentPollingTimer.Stop();
                qrPaymentPollingTimer.Dispose();
                qrPaymentPollingTimer = null;
            }
        }

        /// <summary>
        /// Returns to payment method selection
        /// </summary>
        private void ReturnToPaymentSelection()
        {
            kioskState = State.WaitSelectPaymentMethod;
            checkout.InstructionsText = this.language == AppText.Language.Chinese ?
                AppText.PleaseSelectPaymentMethodChinese : AppText.PleaseSelectPaymentMethodEnglish;
            checkout.SetAnimationType(CheckoutControl.AnimationType.PressButton);
            checkout.QRCodeImage = null; // Clear QR code
            
            // Show payment method panel
            SelectPaymentMethodPanel.Show();
            SelectPaymentMethodPanel.BringToFront();
        }

        private void QRPayButton_Click(object sender, EventArgs e)
        {
            // Hide the payment method selection panel
            SelectPaymentMethodPanel.Hide();

            // Set state to allow cancellation during payment
            canCancel = true;
            kioskState = State.WaitForPayment;

            // Start QR payment process
            StartQRPaymentAsync();
        }
    }
}