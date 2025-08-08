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
    }
}
