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

namespace FrtTicketVendingMachine
{
    public partial class TransactionCancelledControl : UserControl
    {
        // Preload the cancellation sound by loading it into the SoundPlayer during initialization
        private readonly SoundPlayer sp;
        private AppText.Language _language = AppText.Language.Chinese;

        public TransactionCancelledControl()
        {
            InitializeComponent();

            // Preload the sound into memory to prevent stuttering during playback
            try
            {
                // Create a new MemoryStream and copy the resource data into it
                var memoryStream = new MemoryStream();
                using (var resourceStream = Properties.Resources.Fail)
                {
                    resourceStream.CopyTo(memoryStream);
                    memoryStream.Position = 0; // Reset position to beginning
                }

                // Create SoundPlayer with the copied data
                sp = new SoundPlayer(memoryStream);
                sp.LoadAsync(); // Load the sound asynchronously to avoid blocking the UI
            }
            catch (Exception ex)
            {
                // If sound loading fails, create an empty SoundPlayer to prevent crashes
                Console.WriteLine($"Warning: Failed to load cancellation sound: {ex.Message}");
                sp = new SoundPlayer();
            }
        }

        /// <summary>
        /// Gets or sets the language for the control
        /// </summary>
        public AppText.Language Language
        {
            get { return _language; }
            set
            {
                _language = value;
                UpdateLanguage();
            }
        }

        /// <summary>
        /// Updates the control's text based on the current language setting
        /// </summary>
        private void UpdateLanguage()
        {
            if (CancelledTextLabel != null)
            {
                CancelledTextLabel.Text = _language == AppText.Language.Chinese ?
                    AppText.TransactionCancelledChinese : AppText.TransactionCancelledEnglish;
            }
        }

        // Centers controls horizontally
        public void CenterControlHorizontally(Control c)
        {
            c.Left = (c.Parent.Width - c.Width) / 2;
        }

        private void TransactionCancelledControl_VisibleChanged(object sender, EventArgs e)
        {
            // When the control becomes visible, center the picture box and label and play the sound
            if (this.Visible)
            {
                // Update language first in case it changed
                UpdateLanguage();

                CenterControlHorizontally(RedXPictureBox);
                CenterControlHorizontally(CancelledTextLabel);

                // Start the timer to play the sound after a short delay
                FailSoundTimer.Start();
            }
        }

        /// <summary>
        /// Clean up resources when the control is disposed
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                sp?.Dispose();
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        private void FailSoundTimer_Tick(object sender, EventArgs e)
        {
            FailSoundTimer.Stop();
            // Play the preloaded sound - this should be smooth since it's already in memory
            try
            {
                sp.Play();
            }
            catch (Exception ex)
            {
                // If sound playback fails, log it but don't crash the application
                Console.WriteLine($"Warning: Failed to play cancellation sound: {ex.Message}");
            }
        }
    }
}