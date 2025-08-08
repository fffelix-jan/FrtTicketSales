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
    public partial class CheckoutControl : UserControl
    {
        // Private variable for language
        private AppText.Language _language;

        // Private variable for animation state and type
        private int animationState = 1;
        private AnimationType currentAnimationType;

        // Cache animation images to prevent RAM growth
        private Image handButton1Image;
        private Image handButton2Image;
        private Image insertCoin1Image;
        private Image insertCoin2Image;
        private Image insertCoin3Image;
        private Image insertCoin4Image;

        // Track which price mode we're in
        private bool showingTotalPrice = true;

        // Enum for animation types
        public enum AnimationType
        {
            PressButton,
            InsertMoney,
            ScanCode,
            TicketPrinting,
        }

        public void SetAnimationType(AnimationType type)
        {
            currentAnimationType = type;
            switch (type)
            {
                case AnimationType.PressButton:
                    // Load images once if not already loaded
                    if (handButton1Image == null)
                    {
                        handButton1Image = Properties.Resources.HandButton1;
                    }
                    if (handButton2Image == null)
                    {
                        handButton2Image = Properties.Resources.HandButton2;
                    }
                    InstructionsPictureBox.Image = handButton1Image;
                    break;
                case AnimationType.InsertMoney:
                    // Load images once if not already loaded
                    if (insertCoin1Image == null)
                    {
                        insertCoin1Image = Properties.Resources.InsertCoin1;
                    }
                    if (insertCoin2Image == null)
                    {
                        insertCoin2Image = Properties.Resources.InsertCoin2;
                    }
                    if (insertCoin3Image == null)
                    {
                        insertCoin3Image = Properties.Resources.InsertCoin3;
                    }
                    if (insertCoin4Image == null)
                    {
                        insertCoin4Image = Properties.Resources.InsertCoin4;
                    }
                    InstructionsPictureBox.Image = insertCoin1Image;
                    break;
                default:
                    break;
            }
            animationState = 1;
            AnimationTimer.Start();
        }

        // Public field to change the text of the destination label
        public string DestinationText
        {
            get { return DestinationLabel.Text; }
            set
            {
                // Set the font size based on the length of the value, if greater than 12 characters, 28pt, otherwise 36pt
                if (value.Length > 12)
                {
                    DestinationLabel.Font = new Font(DestinationLabel.Font.FontFamily, 28);
                }
                else
                {
                    DestinationLabel.Font = new Font(DestinationLabel.Font.FontFamily, 36);
                }

                // Set the text of the label
                DestinationLabel.Text = value;
            }
        }

        // Public field to change the text of the total price label
        public string TotalPriceText
        {
            get { return TotalPriceLabel.Text; }
            set 
            { 
                TotalPriceLabel.Text = value;
                showingTotalPrice = true;
                UpdatePriceTitleLabel();
            }
        }

        // Public field to change the text of the price each label
        public string PriceEachText
        {
            get { return TotalPriceLabel.Text; }
            set 
            { 
                TotalPriceLabel.Text = value;
                showingTotalPrice = false;
                UpdatePriceTitleLabel();
            }
        }

        // Public field to change the text of the quantity label
        public string QuantityText
        {
            get { return QuantityLabel.Text; }
            set { QuantityLabel.Text = value; }
        }

        // Public field to change the text of the instructions label
        public string InstructionsText
        {
            get { return InstructionsLabel.Text; }
            set { InstructionsLabel.Text = value; }
        }

        // Helper method to update the price title label based on current mode and language
        private void UpdatePriceTitleLabel()
        {
            if (showingTotalPrice)
            {
                PriceTitleLabel.Text = _language == AppText.Language.Chinese ? 
                    AppText.TotalPriceChinese : AppText.TotalPriceEnglish;
            }
            else
            {
                PriceTitleLabel.Text = _language == AppText.Language.Chinese ? 
                    AppText.PriceEachChinese : AppText.PriceEachEnglish;
            }
        }

        // Public field to change the language
        public AppText.Language Language
        {
            get { return _language; }
            set
            {
                _language = value;
                // Update the text of the labels based on the selected language
                switch (_language)
                {
                    case AppText.Language.English:
                        DestinationTitleLabel.Text = AppText.DestinationEnglish;
                        QuantityTitleLabel.Text = AppText.QuantityEnglish;
                        break;
                    case AppText.Language.Chinese:
                        DestinationTitleLabel.Text = AppText.DestinationChinese;
                        QuantityTitleLabel.Text = AppText.QuantityChinese;
                        break;
                }
                // Update the price title label based on current mode
                UpdatePriceTitleLabel();
            }
        }

        public CheckoutControl()
        {
            InitializeComponent();
        }

        private void CheckoutControl_VisibleChanged(object sender, EventArgs e)
        {
            // Stop the animation timer
            if (!this.Visible)
            {
                AnimationTimer.Stop();
            }
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            // Hand animation logic based on the value of animation
            if (currentAnimationType == AnimationType.PressButton)
            {
                if (animationState == 1)
                {
                    InstructionsPictureBox.Image = handButton2Image;
                    animationState = 2;
                }
                else
                {
                    InstructionsPictureBox.Image = handButton1Image;
                    animationState = 1;
                }
            }
            else if (currentAnimationType == AnimationType.InsertMoney)
            {
                if (animationState == 1)
                {
                    InstructionsPictureBox.Image = insertCoin2Image;
                    animationState = 2;
                }
                else if (animationState == 2)
                {
                    InstructionsPictureBox.Image = insertCoin3Image;
                    animationState = 3;
                }
                else if (animationState == 3)
                {
                    InstructionsPictureBox.Image = insertCoin4Image;
                    animationState = 4;
                }
                else
                {
                    InstructionsPictureBox.Image = insertCoin1Image;
                    animationState = 1;
                }
            }
        }

        // Dispose of cached images when control is disposed
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                handButton1Image?.Dispose();
                handButton2Image?.Dispose();
                insertCoin1Image?.Dispose();
                insertCoin2Image?.Dispose();
                insertCoin3Image?.Dispose();
                insertCoin4Image?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
