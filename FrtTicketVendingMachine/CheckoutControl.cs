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

        // Public field to change the text of the price label
        public string PriceText
        {
            get { return TotalPriceLabel.Text; }
            set { TotalPriceLabel.Text = value; }
        }

        // Public field to change the text of the quantity label
        public string QuantityText
        {
            get { return QuantityLabel.Text; }
            set { QuantityLabel.Text = value; }
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
                        PriceTitleLabel.Text = AppText.TotalPriceEnglish;
                        QuantityTitleLabel.Text = AppText.QuantityEnglish;
                        // TODO: get the updated station name from the server
                        break;
                    case AppText.Language.Chinese:
                        DestinationTitleLabel.Text = AppText.DestinationChinese;
                        PriceTitleLabel.Text = AppText.TotalPriceChinese;
                        QuantityTitleLabel.Text = AppText.QuantityChinese;
                        // TODO: get the updated station name from the server
                        break;
                }
            }
        }

        public void SetStationNameFromStationCode(string stationCode)
        {
            // TODO: Get the station name from the server in the selected language
        }

        public CheckoutControl()
        {
            InitializeComponent();
        }
    }
}
