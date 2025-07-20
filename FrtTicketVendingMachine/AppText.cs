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

        public const string CashEnglish = "Cash";
        public const string CashChinese = "现金";

        public const string FakeAlipayWeChatEnglish = "FakeAlipay/FakeWeChatPay";
        public const string FakeAlipayWeChatChinese = "假付宝/伪信支付";
    }
}
