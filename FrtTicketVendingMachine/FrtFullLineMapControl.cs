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
    public partial class FrtFullLineMapControl : UserControl
    {
        List<Button> line1Buttons;
        List<Button> line2Buttons;

        public FrtFullLineMapControl()
        {
            InitializeComponent();

            // Fill the lists with the buttons for each line
            line1Buttons = new List<Button>();
            line2Buttons = new List<Button>();

            // Line 1 buttons
            line1Buttons.Add(XinggangButton);
            line1Buttons.Add(HongqiAvenueButton);
            line1Buttons.Add(GanshuiRoadButton);
            line1Buttons.Add(YananRoadButton);
            line1Buttons.Add(FajunStreetButton);
            line1Buttons.Add(FagangAvenueButton);
            line1Buttons.Add(YuguRoadButton);

            // Line 2 buttons
            line2Buttons.Add(MingtingButton);
            line2Buttons.Add(PortDupontButton);
            line2Buttons.Add(ShuangyongRoadButton1);
            line2Buttons.Add(ShuangyongRoadButton2);
            line2Buttons.Add(JiefangRoadButton);
            line2Buttons.Add(ZhoupuButton);
            line2Buttons.Add(XuanzhicunButton);
            line2Buttons.Add(JunlinRoadButton);
            line2Buttons.Add(XianmuAvenueButton);

            // Uncomment to see what the buttons look like
            // FullMapPictureBox.Hide();
        }

        // Show a specific line on the full map
        // 0 - All
        // 1 - Line 1
        // 2 - Line 2
        public void ShowLine(int lineNumber)
        {
            // Switch the picture in the picture box based on the line number
            switch (lineNumber)
            {
                case 1:
                    FullMapPictureBox.Image = Properties.Resources.FrtTvmLine1Map;
                    foreach (Button b in line1Buttons)
                    {
                        b.Show();
                    }
                    foreach (Button b in line2Buttons)
                    {
                        b.Hide();
                    }
                    break;
                case 2:
                    FullMapPictureBox.Image = Properties.Resources.FrtTvmLine2Map;
                    foreach (Button b in line2Buttons)
                    {
                        b.Show();
                    }
                    foreach (Button b in line1Buttons)
                    {
                        b.Hide();
                    }
                    break;
                default:
                    FullMapPictureBox.Image = Properties.Resources.FrtTvmAllLinesMap;
                    foreach (Button b in line1Buttons)
                    {
                        b.Show();
                    }
                    foreach (Button b in line2Buttons)
                    {
                        b.Show();
                    }
                    break;
            }
        }

        private void StationClick(string stationCode)
        {
            // TODO: Call the payment selection screen

            // But for now, let's show a message box
            MessageBox.Show($"You clicked on station: {stationCode}", "Station Clicked", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Line 2 buttons
        private void MingtingButton_Click(object sender, EventArgs e)
        {
            StationClick("MTI");
        }

        private void PortDupontButton_Click(object sender, EventArgs e)
        {
            StationClick("DBG");
        }

        private void ShuangyongRoadButton1_Click(object sender, EventArgs e)
        {
            StationClick("SYL");
        }

        private void ShuangyongRoadButton2_Click(object sender, EventArgs e)
        {
            StationClick("SYL");
        }

        private void JiefangRoadButton_Click(object sender, EventArgs e)
        {
            StationClick("JFL");
        }

        private void ZhoupuButton_Click(object sender, EventArgs e)
        {
            StationClick("ZPU");
        }

        private void FallowayRailwayStationButton_Click(object sender, EventArgs e)
        {
            StationClick("FLZ");
        }

        private void XuanzhicunButton_Click(object sender, EventArgs e)
        {
            StationClick("XZC");
        }

        private void JunlinRoadButton_Click(object sender, EventArgs e)
        {
            StationClick("JLL");
        }

        private void XianmuAvenueButton_Click(object sender, EventArgs e)
        {
            StationClick("XMD");
        }


        // Line 1 buttons
        private void XinggangButton_Click(object sender, EventArgs e)
        {
            StationClick("XGA");
        }

        // Falloway Railway Station is shared between Line 1 and Line 2

        private void HongqiAvenueButton_Click(object sender, EventArgs e)
        {
            StationClick("HQJ");
        }

        private void GanshuiRoadButton_Click(object sender, EventArgs e)
        {
            StationClick("GSL");
        }

        private void YananRoadButton_Click(object sender, EventArgs e)
        {
            StationClick("YAL");
        }

        private void FajunStreetButton_Click(object sender, EventArgs e)
        {
            StationClick("FJJ");
        }

        private void FagangAvenueButton_Click(object sender, EventArgs e)
        {
            StationClick("FGD");
        }

        private void YuguRoadButton_Click(object sender, EventArgs e)
        {
            StationClick("YGL");
        }
    }
}
