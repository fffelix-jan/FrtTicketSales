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
    public partial class InsertCashForm : Form
    {
        private int amountNeededCents;
        private int amountInsertedCents;
        private int amountEjectedCents;
        private AppText.Language language;
        private bool isEjecting = false; // Track ejection state

        public event EventHandler<CashInsertedEventArgs> CashInserted;
        public event EventHandler<CashEjectedEventArgs> CashEjected;
        public event EventHandler PaymentCompleted;
        public event EventHandler EjectionCompleted; // New event for ejection completion

        public InsertCashForm(int amountNeededCents, AppText.Language language)
        {
            InitializeComponent();
            this.amountNeededCents = amountNeededCents;
            this.amountInsertedCents = 0;
            this.amountEjectedCents = 0;
            this.language = language;

            SetupForm();
            UpdateDisplay();
        }

        // Public method for MainForm to request cash ejection
        public void RequestCashEjection()
        {
            if (!isEjecting)
            {
                isEjecting = true;
                EjectAllCash();
            }
        }

        private void SetupForm()
        {
            // Set form text based on language
            if (language == AppText.Language.Chinese)
            {
                this.Text = "模拟现金接受器";
                titleLabel.Text = "模拟现金接受器";
                oneYuanButton.Text = "投入\r\n¥1";
                fiveYuanButton.Text = "投入\r\n¥5";
                tenYuanButton.Text = "投入\r\n¥10";
                cancelButton.Text = "取消付款";
            }
            else
            {
                this.Text = "Simulated Cash Acceptor";
                titleLabel.Text = "Simulated Cash Acceptor";
                oneYuanButton.Text = "Insert\r\n¥1";
                fiveYuanButton.Text = "Insert\r\n¥5";
                tenYuanButton.Text = "Insert\r\n¥10";
                cancelButton.Text = "Cancel Payment";
            }

            // Position form at bottom left of screen
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, Screen.PrimaryScreen.Bounds.Height - this.Height);
            
            // Disable the control box to remove the red X button
            this.ControlBox = false;
        }

        private void UpdateDisplay()
        {
            decimal amountNeeded = amountNeededCents / 100.0m;
            decimal amountInserted = amountInsertedCents / 100.0m;
            decimal amountRemaining = (amountNeededCents - amountInsertedCents) / 100.0m;

            if (language == AppText.Language.Chinese)
            {
                amountNeededLabel.Text = $"需要金额: ¥{amountNeeded:F2}";
                insertedAmountLabel.Text = $"已投入金额: ¥{amountInserted:F2}";

                if (amountRemaining > 0)
                {
                    remainingAmountLabel.Text = $"剩余金额: ¥{amountRemaining:F2}";
                    remainingAmountLabel.ForeColor = Color.Red;
                    statusLabel.Text = "请继续投入现金...";
                    statusLabel.ForeColor = Color.Gray;
                }
                else
                {
                    remainingAmountLabel.Text = "付款完成！";
                    remainingAmountLabel.ForeColor = Color.Green;
                    statusLabel.Text = "正在处理付款...";
                    statusLabel.ForeColor = Color.Green;
                }
            }
            else
            {
                amountNeededLabel.Text = $"Amount Needed: ¥{amountNeeded:F2}";
                insertedAmountLabel.Text = $"Amount Inserted: ¥{amountInserted:F2}";

                if (amountRemaining > 0)
                {
                    remainingAmountLabel.Text = $"Amount Remaining: ¥{amountRemaining:F2}";
                    remainingAmountLabel.ForeColor = Color.Red;
                    statusLabel.Text = "Insert cash to continue...";
                    statusLabel.ForeColor = Color.Gray;
                }
                else
                {
                    remainingAmountLabel.Text = "Payment Complete!";
                    remainingAmountLabel.ForeColor = Color.Green;
                    statusLabel.Text = "Processing payment...";
                    statusLabel.ForeColor = Color.Green;
                }
            }

            // Disable buttons if payment is complete or overpaid or ejecting
            bool paymentComplete = amountInsertedCents >= amountNeededCents;
            oneYuanButton.Enabled = !paymentComplete && !isEjecting;
            fiveYuanButton.Enabled = !paymentComplete && !isEjecting;
            tenYuanButton.Enabled = !paymentComplete && !isEjecting;
            cancelButton.Enabled = !isEjecting;

            // Complete payment if exact amount or overpaid
            if (paymentComplete && amountInsertedCents > 0 && !isEjecting)
            {
                // Check if change is needed
                int changeCents = amountInsertedCents - amountNeededCents;
                if (changeCents > 0)
                {
                    EjectChange(changeCents);
                }

                // Simulate processing delay then complete
                Timer completeTimer = new Timer();
                completeTimer.Interval = 1500; // 1.5 second delay (longer for change)
                completeTimer.Tick += (s, e) =>
                {
                    completeTimer.Stop();
                    completeTimer.Dispose();
                    PaymentCompleted?.Invoke(this, EventArgs.Empty);
                };
                completeTimer.Start();
            }
        }

        private void EjectChange(int changeCents)
        {
            amountEjectedCents = changeCents;
            decimal changeAmount = changeCents / 100.0m;

            // Show ejected amount
            ejectedAmountLabel.Visible = true;
            if (language == AppText.Language.Chinese)
            {
                ejectedAmountLabel.Text = $"找零: ¥{changeAmount:F2}";
                statusLabel.Text = "正在找零并处理付款...";
            }
            else
            {
                ejectedAmountLabel.Text = $"Change Ejected: ¥{changeAmount:F2}";
                statusLabel.Text = "Ejecting change and processing payment...";
            }

            // Notify parent form about change
            CashEjected?.Invoke(this, new CashEjectedEventArgs(changeCents, CashEjectionReason.Change));
        }

        private void EjectAllCash()
        {
            if (amountInsertedCents > 0)
            {
                amountEjectedCents = amountInsertedCents;
                decimal ejectedAmount = amountEjectedCents / 100.0m;

                // Show ejected amount
                ejectedAmountLabel.Visible = true;
                if (language == AppText.Language.Chinese)
                {
                    ejectedAmountLabel.Text = $"退出现金: ¥{ejectedAmount:F2}";
                    statusLabel.Text = "正在退出现金...";
                }
                else
                {
                    ejectedAmountLabel.Text = $"Cash Ejected: ¥{ejectedAmount:F2}";
                    statusLabel.Text = "Ejecting cash...";
                }

                // Disable all buttons during ejection
                oneYuanButton.Enabled = false;
                fiveYuanButton.Enabled = false;
                tenYuanButton.Enabled = false;
                cancelButton.Enabled = false;

                // Notify parent form about cash ejection
                CashEjected?.Invoke(this, new CashEjectedEventArgs(amountEjectedCents, CashEjectionReason.Cancellation));

                // Simulate ejection delay (coins clinking out)
                Timer ejectTimer = new Timer();
                ejectTimer.Interval = 2000; // 2 second delay to simulate coins falling out
                ejectTimer.Tick += (s, e) =>
                {
                    ejectTimer.Stop();
                    ejectTimer.Dispose();
                    
                    // Update status to show ejection complete
                    if (language == AppText.Language.Chinese)
                    {
                        statusLabel.Text = "现金已退出";
                    }
                    else
                    {
                        statusLabel.Text = "Cash ejection complete";
                    }
                    
                    // Wait another moment then fire the ejection completed event
                    Timer completeTimer = new Timer();
                    completeTimer.Interval = 500;
                    completeTimer.Tick += (s2, e2) =>
                    {
                        completeTimer.Stop();
                        completeTimer.Dispose();
                        EjectionCompleted?.Invoke(this, EventArgs.Empty);
                    };
                    completeTimer.Start();
                };
                ejectTimer.Start();
            }
            else
            {
                // No cash to eject, complete immediately
                EjectionCompleted?.Invoke(this, EventArgs.Empty);
            }
        }

        private void OneYuanButton_Click(object sender, EventArgs e)
        {
            InsertCash(100); // 1 yuan = 100 cents
        }

        private void FiveYuanButton_Click(object sender, EventArgs e)
        {
            InsertCash(500); // 5 yuan = 500 cents
        }

        private void TenYuanButton_Click(object sender, EventArgs e)
        {
            InsertCash(1000); // 10 yuan = 1000 cents
        }

        private void InsertCash(int cents)
        {
            if (!isEjecting)
            {
                amountInsertedCents += cents;
                
                // Notify parent form about cash insertion
                CashInserted?.Invoke(this, new CashInsertedEventArgs(cents, amountInsertedCents));
                
                UpdateDisplay();
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            // Disable cancel button to prevent multiple clicks
            cancelButton.Enabled = false;
            
            // Start ejection process
            isEjecting = true;
            EjectAllCash();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // NEVER allow manual closure - always cancel the close attempt
            e.Cancel = true;
        }
    }

    // Event arguments for cash insertion
    public class CashInsertedEventArgs : EventArgs
    {
        public int AmountInsertedCents { get; }
        public int TotalInsertedCents { get; }

        public CashInsertedEventArgs(int amountInsertedCents, int totalInsertedCents)
        {
            AmountInsertedCents = amountInsertedCents;
            TotalInsertedCents = totalInsertedCents;
        }
    }

    // Event arguments for cash ejection
    public class CashEjectedEventArgs : EventArgs
    {
        public int AmountEjectedCents { get; }
        public CashEjectionReason Reason { get; }

        public CashEjectedEventArgs(int amountEjectedCents, CashEjectionReason reason)
        {
            AmountEjectedCents = amountEjectedCents;
            Reason = reason;
        }
    }

    // Enum for cash ejection reasons
    public enum CashEjectionReason
    {
        Change,
        Cancellation
    }
}