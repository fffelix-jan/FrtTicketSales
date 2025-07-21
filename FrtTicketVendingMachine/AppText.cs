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

        public const string AllLinesEnglish = "All Lines";
        public const string AllLinesChinese = "全线网";

        public const string Line1English = "Line 1";
        public const string Line1Chinese = "1号线";

        public const string Line2English = "Line 2";
        public const string Line2Chinese = "2号线";
    }
}
