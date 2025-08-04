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
    public partial class MainForm : Form
    {
        readonly Color defaultColor = Color.Crimson;
        readonly Color selectedColor = Color.Blue;
        AppText.Language language = AppText.Language.Chinese;
        State kioskState = State.HomeScreen;
        CheckoutControl checkout = new CheckoutControl();
        // Set this to false when processing payments and printing tickets to prevent the machine from swallowing the user's money
        bool canCancel = true;

        // Enables double buffering to avoid lag when refreshing the screen
        // This causes high CPU usage which is why it's not used
        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        // Skip if running over RDP
        //        if (SystemInformation.TerminalServerSession)
        //            return base.CreateParams;

        //        CreateParams cp = base.CreateParams;
        //        cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED (double-buffer all controls)
        //        return cp;
        //    }
        //}

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

            CenterControlHorizontally(ClockRoundedPanel);
            UpdateClockDisplay();
        }

        // Resets the kiosk to the start state if allowed
        public void ResetKiosk()
        {
            if (canCancel)
            {
                //

                // Eject the coins here and wait for them to be ejected without blocking the UI
                // TODO: Implement coin ejection logic
                // TODO: Show a "Cancelling..." message to the user


                // Stage 2 would normally be called by the coin ejection logic
                // But we call it directly here for simplicity
            }
        }

        // After ejecting the coins, the reset logic continues here
        // DO NOT CALL THIS FUNCTION DIRECTLY
        private void ResetKioskStage2()
        {
            // Reset the kiosk state
            kioskState = State.HomeScreen;
            // Hide all panels
            SelectTicketQuantityPanel.Hide();
            SelectPaymentMethodPanel.Hide();
            checkout.Hide();
            MainFrtFullLineMapControl.Hide();
            // Show the welcome panel
            WelcomePanel.Show();
            WelcomePanel.BringToFront();
            // Update the clock display
            UpdateClockDisplay();
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

        private void SelectStation(string stationCode)
        {
            // Set the selected station in the checkout control
            checkout.DestinationText = stationCode;
            // Set the quantity and price to default values
            checkout.QuantityText = "?";
            checkout.PriceText = "?";
            // Transition to the next state
            kioskState = State.WaitSelectTicketQuantity;
            // Show the ticket quantity selection panel
            SelectTicketQuantityPanel.Show();
            SelectTicketQuantityPanel.BringToFront();
            // Hide the welcome panel
            WelcomePanel.Hide();
            // Hide the map
            MainFrtFullLineMapControl.Hide();
            // Show the checkout panel
            checkout.Show();
        }

        private void MainFrtFullLineMapControl_StationSelected(object sender, StationSelectedEventArgs e)
        {
            SelectStation(e.StationCode);
        }
    }
}
