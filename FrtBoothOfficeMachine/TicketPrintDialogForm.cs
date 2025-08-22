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

namespace FrtBoothOfficeMachine
{
    // This form's sole purpose is to actually print tickets.
    // The various constructors of this form set it up to print different kinds of tickets.
    // When the form is shown, it immediately contacts the server to issue the tickets,
    // prints them, and then closes itself.
    // The form also displays a progress bar to the user while this is happening.
    // The actual blocking print function is run asynchronuly to keep the UI responsive.
    public partial class TicketPrintDialogForm : Form
    {
        // Ticket printing configuration
        private readonly TicketPrintConfig _printConfig;
        private bool _printingStarted = false;

        // Private constructor - use static factory methods instead
        private TicketPrintDialogForm(TicketPrintConfig config)
        {
            InitializeComponent();
            _printConfig = config;
            CenterControlHorizontally(TitleLabel);
            
            // Set up progress bar
            TicketProgressBar.Minimum = 0;
            TicketProgressBar.Maximum = _printConfig.TotalTickets;
            TicketProgressBar.Value = 0;
            
            // Update title
            UpdateTitle(0);
            
            // Handle form shown event to start printing
            this.Shown += TicketPrintDialogForm_Shown;
        }

        private void SetTitleLabelTextAndCenter(string setText)
        {
            TitleLabel.Text = setText;
            CenterControlHorizontally(TitleLabel);
        }

        // Disable the close button on the form
        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        /// <summary>
        /// Constructor for printing regular tickets (full-fare, senior, student)
        /// </summary>
        /// <param name="destinationStationInfo">Destination station information</param>
        /// <param name="fullFareTicketPriceCents">Full fare ticket price in cents</param>
        /// <param name="fullFareCount">Number of full fare tickets</param>
        /// <param name="seniorCount">Number of senior tickets (50% off)</param>
        /// <param name="studentCount">Number of student tickets (25% off)</param>
        /// <param name="paymentMethod">Payment method used</param>
        public static TicketPrintDialogForm CreateForRegularTickets(
            int fullFareTicketPriceCents,
            int fullFareCount, 
            int seniorCount, 
            int studentCount, 
            string paymentMethod = "现金")
        {
            var tickets = new List<TicketInfo>();
            
            // Add full fare tickets
            for (int i = 0; i < fullFareCount; i++)
            {
                tickets.Add(new TicketInfo
                {
                    TicketType = 0, // Full fare
                    ValueCents = fullFareTicketPriceCents,
                    ChineseTicketType = "单程票",
                    EnglishTicketType = "Single Journey Ticket",
                    TicketTypeCode = "全" // Full fare ticket type code
                });
            }
            
            // Add senior tickets (50% off)
            for (int i = 0; i < seniorCount; i++)
            {
                tickets.Add(new TicketInfo
                {
                    TicketType = 2, // Senior
                    ValueCents = fullFareTicketPriceCents / 2,
                    ChineseTicketType = "长者票",
                    EnglishTicketType = "Senior Ticket",
                    TicketTypeCode = "老" // Senior ticket type code
                });
            }
            
            // Add student tickets (25% off)
            for (int i = 0; i < studentCount; i++)
            {
                tickets.Add(new TicketInfo
                {
                    TicketType = 1, // Student
                    ValueCents = (fullFareTicketPriceCents * 75) / 100,
                    ChineseTicketType = "学生票",
                    EnglishTicketType = "Student Ticket",
                    TicketTypeCode = "学" // Student ticket type code
                });
            }

            var config = new TicketPrintConfig
            {
                PrintType = PrintType.RegularTickets,
                Tickets = tickets,
                PaymentMethod = paymentMethod,
                TotalTickets = fullFareCount + seniorCount + studentCount
            };

            return new TicketPrintDialogForm(config);
        }

        /// <summary>
        /// Constructor for printing day pass tickets
        /// </summary>
        /// <param name="quantity">Number of day pass tickets to print</param>
        /// <param name="dayPassPriceCents">Day pass price in cents</param>
        /// <param name="paymentMethod">Payment method used</param>
        public static TicketPrintDialogForm CreateForDayPass(
            int quantity, 
            int dayPassPriceCents, 
            string paymentMethod = "现金")
        {
            var tickets = new List<TicketInfo>();
            
            for (int i = 0; i < quantity; i++)
            {
                tickets.Add(new TicketInfo
                {
                    TicketType = 4, // Day pass
                    ValueCents = dayPassPriceCents,
                    ChineseTicketType = "一日票",
                    EnglishTicketType = "Day Pass",
                    TicketTypeCode = "通" // Day pass ticket type code
                });
            }

            var config = new TicketPrintConfig
            {
                PrintType = PrintType.DayPass,
                Tickets = tickets,
                PaymentMethod = paymentMethod,
                TotalTickets = quantity
            };

            return new TicketPrintDialogForm(config);
        }

        /// <summary>
        /// Constructor for printing test tickets (no server contact)
        /// </summary>
        /// <param name="quantity">Number of test tickets to print</param>
        public static TicketPrintDialogForm CreateForTestTickets(int quantity = 1)
        {
            var tickets = new List<TicketInfo>();
            
            for (int i = 0; i < quantity; i++)
            {
                tickets.Add(new TicketInfo
                {
                    TicketType = 255, // Test ticket type
                    ValueCents = 0,
                    ChineseTicketType = "测试页",
                    EnglishTicketType = "Test Page",
                    TicketTypeCode = "测" // Test ticket type code
                });
            }

            var config = new TicketPrintConfig
            {
                PrintType = PrintType.TestTicket,
                Tickets = tickets,
                PaymentMethod = "测试",
                TotalTickets = quantity
            };

            return new TicketPrintDialogForm(config);
        }

        private async void TicketPrintDialogForm_Shown(object sender, EventArgs e)
        {
            if (_printingStarted) return;
            _printingStarted = true;

            try
            {
                await StartPrintingAsync();
                
                // Close form after successful printing
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"打印失败：\n\n{ex.Message}", 
                              "打印错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private async Task StartPrintingAsync()
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
            
            int ticketsPrinted = 0;

            foreach (var ticket in _printConfig.Tickets)
            {
                // Update progress
                UpdateTitle(ticketsPrinted);
                TicketProgressBar.Value = ticketsPrinted;
                Application.DoEvents();

                if (_printConfig.PrintType == PrintType.TestTicket)
                {
                    // Print test ticket without server contact
                    await PrintTestTicketAsync(ticket, ticketsPrinted + 1);
                }
                else
                {
                    // Contact server to issue ticket, then print
                    await PrintRealTicketAsync(ticket, currentStationCode, currentStationInfo, ticketsPrinted + 1);
                }

                ticketsPrinted++;
                
                // Small delay to show progress
                await Task.Delay(100);
            }

            // Final update
            UpdateTitle(ticketsPrinted);
            TicketProgressBar.Value = ticketsPrinted;
            
            // Show completion message briefly
            SetTitleLabelTextAndCenter("打印完成！");
            await Task.Delay(500);
        }

        private async Task PrintRealTicketAsync(TicketInfo ticket, string currentStationCode, StationInfo? currentStationInfo, int ticketNumber)
        {
            // Issue ticket through API
            var ticketData = await GlobalCredentials.ApiClient.IssueTicketAsync(
                ticket.ValueCents, currentStationCode, ticket.TicketType);

            // Use CURRENT station names (not destination)
            string chineseStationName, englishStationName;
            if (currentStationInfo.HasValue)
            {
                chineseStationName = currentStationInfo.Value.ChineseName;
                englishStationName = currentStationInfo.Value.EnglishName;
            }
            else
            {
                // Fallback to configured station code if API call failed
                chineseStationName = currentStationCode;
                englishStationName = currentStationCode;
            }

            // Format price for display
            string priceDisplay = $"¥{ticket.ValueCents / 100.0:F2}";

            // Format payment method as TicketType/PaymentMethod
            string formattedPaymentMethod = FormatPaymentMethod(ticket.TicketTypeCode, _printConfig.PaymentMethod);

            // Print the ticket
            await Task.Run(() =>
            {
                FrtTicketPrinter.TicketGenerator.PrintTicket(
                    ticket.ChineseTicketType, ticket.EnglishTicketType,
                    chineseStationName, englishStationName,
                    priceDisplay, formattedPaymentMethod,
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    ticketData.TicketString,
                    $"{ticketData.TicketNumber}-{ticketNumber:D2}",
                    "请勿损坏车票", "Please do not damage this ticket"
                );
            });
        }

        private async Task PrintTestTicketAsync(TicketInfo ticket, int ticketNumber)
        {
            // Format payment method as TicketType/PaymentMethod for test tickets
            string formattedPaymentMethod = FormatPaymentMethod(ticket.TicketTypeCode, _printConfig.PaymentMethod);

            // Print test ticket without server contact
            await Task.Run(() =>
            {
                FrtTicketPrinter.TicketGenerator.PrintTicket(
                    ticket.ChineseTicketType, ticket.EnglishTicketType,
                    "测试站", "Test Station",
                    "¥0.00", formattedPaymentMethod,
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    "TEST-TICKET-QR-CODE",
                    $"TEST-{DateTime.Now:yyyyMMdd}-{ticketNumber:D3}",
                    "仅供测试，请勿损坏", "For testing only, do not damage"
                );
            });
        }

        /// <summary>
        /// Formats payment method as TicketType/PaymentMethod using single Chinese characters
        /// </summary>
        /// <param name="ticketTypeCode">Single character ticket type code (全/老/学/通/测)</param>
        /// <param name="paymentMethod">Payment method string</param>
        /// <returns>Formatted payment method string</returns>
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
                case "测试":
                    paymentCode = "测";
                    break;
                default:
                    // Default to first character or 现
                    paymentCode = paymentMethod.Length > 0 ? paymentMethod.Substring(0, 1) : "现";
                    break;
            }

            return $"{ticketTypeCode}/{paymentCode}";
        }

        private void UpdateTitle(int currentTicket)
        {
            SetTitleLabelTextAndCenter($"正在打印车票... （第{currentTicket}张，共{_printConfig.TotalTickets}张）");
        }

        // Centers controls horizontally
        public void CenterControlHorizontally(Control c)
        {
            c.Left = (c.Parent.Width - c.Width) / 2;
        }

        // Configuration classes
        private class TicketPrintConfig
        {
            public PrintType PrintType { get; set; }
            public List<TicketInfo> Tickets { get; set; } = new List<TicketInfo>();
            public string PaymentMethod { get; set; } = "现金";
            public int TotalTickets { get; set; }
        }

        private class TicketInfo
        {
            public int TicketType { get; set; }
            public int ValueCents { get; set; }
            public string ChineseTicketType { get; set; } = "";
            public string EnglishTicketType { get; set; } = "";
            public string TicketTypeCode { get; set; } = ""; // Single character ticket type code
        }

        private enum PrintType
        {
            RegularTickets,
            DayPass,
            TestTicket
        }
    }
}
