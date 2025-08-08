using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrtTicketVendingMachine
{
    public static class AppText
    {
        public enum Language
        {
            English,
            Chinese
        }

        public const string SelectTicketsEnglish = "Select Number of Tickets";
        public const string SelectTicketsChinese = "请选择购票张数";

        public const string SelectPaymentMethodEnglish = "Select Payment Method";
        public const string SelectPaymentMethodChinese = "请选择支付方式";

        public const string CashEnglish = "Cash";
        public const string CashChinese = "现金";

        public const string FakeAlipayWeChatEnglish = "FAlipay/FWeChat";
        public const string FakeAlipayWeChatChinese = "假付宝/伪信支付";

        public const string DestinationEnglish = "Destination:";
        public const string DestinationChinese = "目的地:";

        public const string TotalPriceEnglish = "Total Price:";
        public const string TotalPriceChinese = "总价:";

        public const string PriceEachEnglish = "Price Each:";
        public const string PriceEachChinese = "单价:";

        public const string QuantityEnglish = "Quantity:";
        public const string QuantityChinese = "购票数量:";

        public const string AllLinesEnglish = "All Lines";
        public const string AllLinesChinese = "全线网";

        public const string Line1English = "Line 1";
        public const string Line1Chinese = "1号线";

        public const string Line2English = "Line 2";
        public const string Line2Chinese = "2号线";

        public const string CancelEnglish = "Cancel";
        public const string CancelChinese = "取消";

        public const string PleaseSelectQuantityEnglish = "Please select the number of tickets → → →";
        public const string PleaseSelectQuantityChinese = "请选择购票张数 → → →";

        public const string PleaseSelectPaymentMethodEnglish = "Please select a payment method → → →";
        public const string PleaseSelectPaymentMethodChinese = "请选择支付方式 → → →";

        public const string CancellingEnglish = "Cancelling...";
        public const string CancellingChinese = "正在取消...";

        // Language toggle button text
        public const string EnglishToggleText = "English";
        public const string ChineseToggleText = "中文";

        // Loading and status messages
        public const string LoadingStationInfoEnglish = "Loading station information...";
        public const string LoadingStationInfoChinese = "正在获取车站信息...";

        public const string StationInfoErrorEnglish = "Failed to get station information, please try again later.";
        public const string StationInfoErrorChinese = "获取车站信息失败，请稍后再试。";

        // Quantity display text
        public const string TicketsEnglish = "tickets";
        public const string TicketsChinese = "张";

        // Cash payment messages
        public const string InsertCashEnglish = "Please insert cash...";
        public const string InsertCashChinese = "请投入现金...";

        public const string CashInsertedEnglish = "Inserted: ¥{0:F2}";
        public const string CashInsertedChinese = "已投入: ¥{0:F2}";

        public const string ChangeEjectedEnglish = "Change: ¥{0:F2}";
        public const string ChangeEjectedChinese = "找零: ¥{0:F2}";

        public const string CashReturnedEnglish = "Cash Returned: ¥{0:F2}";
        public const string CashReturnedChinese = "退币: ¥{0:F2}";

        // Payment processing messages
        public const string ProcessingPaymentEnglish = "Processing payment...";
        public const string ProcessingPaymentChinese = "正在处理付款...";

        public const string PrintingTicketsEnglish = "Printing tickets...";
        public const string PrintingTicketsChinese = "正在打印票据...";

        public const string PrintingCompleteEnglish = "Printing complete! Please take your tickets";
        public const string PrintingCompleteChinese = "打印完成！请取票";

        public const string ProcessingFailedEnglish = "Processing failed: {0}";
        public const string ProcessingFailedChinese = "处理失败: {0}";

        // Ticket printing constants
        public const string SingleJourneyTicketEnglish = "Single Journey Ticket";
        public const string SingleJourneyTicketChinese = "单程票";

        public const string CashPaymentMethodEnglish = "现金/Cash";

        public const string ThankYouMessageEnglish = "Thank you for using FRT Transit";
        public const string ThankYouMessageChinese = "感谢您使用FRT轨道交通";
    }
}
