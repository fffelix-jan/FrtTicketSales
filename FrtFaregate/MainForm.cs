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
using System.Runtime.InteropServices;

namespace FrtFaregate
{
    public partial class MainForm : Form
    {
        // Unmanaged code to hide the blinking cursor in the text box
        [DllImport("user32.dll")]
        private static extern bool HideCaret(IntPtr hWnd);

        const string DefaultMiddleText = "请刷卡或扫码";
        const string DefaultBottomText = "Please Tap Card or Scan Ticket";

        // Cache animation images to prevent RAM growth
        private Image tapCardImage;
        private Image successImage;
        private Image failImage;

        // Cache sound effects to prevent RAM growth
        private SoundPlayer successSound;
        private SoundPlayer seniorTicketSound;
        private SoundPlayer studentTicketSound;
        private SoundPlayer failSound;

        enum DisplayType
        {
            RegularTap,
            SeniorTicket,
            StudentTicket,
            Fail
        }

        public MainForm()
        {
            InitializeComponent();
            CenterControlHorizontally(UserPromptPictureBox);
            CenterControlHorizontally(MiddleTextLabel);
            CenterControlHorizontally(BottomTextLabel);

            // Load images in RAM
            tapCardImage = Properties.Resources.TapCard;
            successImage = Properties.Resources.CheckSign;
            failImage = Properties.Resources.XSign;

            // Preload sound effects into memory to prevent stuttering during playback
            successSound = LoadSoundFromResource(Properties.Resources.RegularTap);
            seniorTicketSound = LoadSoundFromResource(Properties.Resources.SeniorTap);
            studentTicketSound = LoadSoundFromResource(Properties.Resources.StudentTap);
            failSound = LoadSoundFromResource(Properties.Resources.Fail);

            // Set the text
            MiddleTextLabel.Text = DefaultMiddleText;
            BottomTextLabel.Text = DefaultBottomText;

            // Create the event that hides the caret when the text box gains focus
            TicketScanTextBox.GotFocus += (s, e) => HideCaret(TicketScanTextBox.Handle);
        }

        private SoundPlayer LoadSoundFromResource(Stream resourceStream)
        {
            try
            {
                var memoryStream = new MemoryStream();
                resourceStream.CopyTo(memoryStream);
                memoryStream.Position = 0;
                var player = new SoundPlayer(memoryStream);
                player.LoadAsync();
                return player;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Warning: Failed to load sound: {ex.Message}");
                return new SoundPlayer();
            }
        }

        // Centers controls horizontally
        public void CenterControlHorizontally(Control c)
        {
            c.Left = (c.Parent.Width - c.Width) / 2;
        }

        private void DisplayScanResult(DisplayType type, string middleText, string bottomText)
        {
            // Reset the hiding timer
            HideUserMessageTimer.Stop();

            // Switch the text
            MiddleTextLabel.Text = middleText;
            BottomTextLabel.Text = bottomText;

            // Switch the icon based on success or fail
            switch (type)
            {
                case DisplayType.RegularTap:
                    UserPromptPictureBox.Image = successImage;
                    successSound.Play();
                    break;
                case DisplayType.SeniorTicket:
                    UserPromptPictureBox.Image = successImage;
                    seniorTicketSound.Play();
                    break;
                case DisplayType.StudentTicket:
                    UserPromptPictureBox.Image = successImage;
                    studentTicketSound.Play();
                    break;
                case DisplayType.Fail:
                    UserPromptPictureBox.Image = failImage;
                    failSound.Play();
                    break;
            }

            // Start the hiding timer
            HideUserMessageTimer.Start();
        }

        // This timer keeps the text box selected
        private void SecurityTimer_Tick(object sender, EventArgs e)
        {
            TicketScanTextBox.Focus();
        }
    }
}
