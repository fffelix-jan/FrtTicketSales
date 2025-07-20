using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Reflection;
using Microsoft.Reporting.WinForms;
using QRCoder;

namespace FrtTicketPrinter
{
    public static class TicketGenerator
    {
        public static void PrintTicket(
            string chineseTicketType, string englishTicketType,
            string chineseStationName, string englishStationName,
            string value, string paymentType, string dateTime,
            string qrCodeText, string ticketNumber,
            string chineseFooterMessage, string englishFooterMessage)
        {
            // Generate QR code
            byte[] qrCodeImageBytes = GenerateQRCode(qrCodeText);

            // Set up report parameters
            ReportParameter[] parameters = {
                new ReportParameter("pChineseTicketType", chineseTicketType),
                new ReportParameter("pEnglishTicketType", englishTicketType),
                new ReportParameter("pChineseStationName", chineseStationName),
                new ReportParameter("pEnglishStationName", englishStationName),
                new ReportParameter("pValue", value),
                new ReportParameter("pPaymentType", paymentType),
                new ReportParameter("pDateTime", dateTime),
                new ReportParameter("pQRCodeImage", Convert.ToBase64String(qrCodeImageBytes)),
                new ReportParameter("pTicketNumber", ticketNumber),
                new ReportParameter("pChineseFooterMessage", chineseFooterMessage),
                new ReportParameter("pEnglishFooterMessage", englishFooterMessage)
            };

            // Set up the local report with embedded resource
            LocalReport report = new LocalReport();
            report.ReportEmbeddedResource = "FrtTicketPrinter.FrtTicketReport.rdlc";
            report.SetParameters(parameters);

            // Print!
            report.PrintToPrinter();
        }

        private static byte[] GenerateQRCode(string text)
        {
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q))
            using (QRCode qrCode = new QRCode(qrCodeData))
            {
                // Set quietZone to 0 for no border, or 1 for minimal border
                Bitmap qrCodeImage = qrCode.GetGraphic(
                    pixelsPerModule: 20,
                    darkColor: Color.Black,
                    lightColor: Color.White,
                    drawQuietZones: false // Set to false to completely remove border
                );

                using (MemoryStream ms = new MemoryStream())
                {
                    qrCodeImage.Save(ms, ImageFormat.Png);
                    return ms.ToArray();
                }
            }
        }
    }
}