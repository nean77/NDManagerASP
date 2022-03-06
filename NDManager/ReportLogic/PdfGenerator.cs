using iText.IO.Font;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using NDManager.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using iText.IO.Font.Constants;

namespace NDManager.ReportLogic
{
    public static class PaymentPdfGenerator
    {
        public static MemoryStream Generate(IList<Payment> payments)
        {
            MemoryStream ms = new MemoryStream();

            PdfWriter pw = new PdfWriter(ms);
            PdfDocument pdfDocument = new PdfDocument(pw);
            Document doc = new Document(pdfDocument, PageSize.A5);
            doc.SetMargins(50,25, 20,25);
            PdfFont font = PdfFontFactory.CreateFont("c:/windows/fonts/arial.ttf", PdfEncodings.IDENTITY_H);
            PdfFont fontBold = PdfFontFactory.CreateFont("c:/windows/fonts/Segoeuib.ttf", PdfEncodings.IDENTITY_H);

            for (int i=0; i < payments.Count(); i++)
            {
                doc.Add(new Paragraph("KARTA PŁATNOŚCI ZA PRZEDSZKOLE").SetTextAlignment(TextAlignment.CENTER).SetFontSize(18).SetFont(fontBold));
                doc.Add(new Paragraph("'NASZ DOMEK'").SetTextAlignment(TextAlignment.CENTER).SetFontSize(22).SetFont(fontBold));
                doc.Add(new LineSeparator(new SolidLine()));
                doc.Add(new Paragraph(""));
                doc.Add(new Paragraph(String.Format("Dziecko: {0}", payments[i].Kid.ToString())).SetFontSize(15).SetFont(fontBold));
                doc.Add(new Paragraph(""));
                doc.Add(new Paragraph(""));
                doc.Add(new Paragraph(String.Format("Płatność za miesiąc {0}", payments[i].MonthNo+"/"+DateTime.Today.Year)).SetFontSize(17).SetFont(font));

                List list = new List(ListNumberingType.DECIMAL);
                list.Add(new ListItem(String.Format("Kwota za wyżywienie: {0} zł", payments[i].Kid.MealDailyRate* payments[i].WorkingDays)));
                list.Add(new ListItem(String.Format("Kwota za godziny: {0} zł", payments[i].Kid.AttendanceDailyRate * payments[i].WorkingDays)));
                doc.Add(list.SetFont(font));
                doc.Add(new Paragraph(""));
                doc.Add(new Paragraph(""));

                doc.Add(new Paragraph("Łączna kwota do zapłaty (po odliczeniach):").SetFontSize(16).SetFont(fontBold));
                doc.Add(new Paragraph(String.Format("{0} zł", payments[i].Value)).SetTextAlignment(TextAlignment.RIGHT).SetFontSize(18).SetFont(fontBold));

                Rectangle pageSize = doc.GetPageEffectiveArea(PageSize.A5);
                float x = pageSize.GetLeft();
                float y = pageSize.GetBottom() + 30;
                string footer = "Przedszkole Nasz Domek, Konopnickiej 42";
                doc.ShowTextAligned(footer, x, y, TextAlignment.LEFT, VerticalAlignment.BOTTOM, 0).SetFontSize(13).SetFont(font);
                y = pageSize.GetBottom() + 12;
                footer = "Telefon: 54 425 16 66, email: naszdomek@opoczta.pl";
                doc.ShowTextAligned(footer, x, y, TextAlignment.LEFT, VerticalAlignment.BOTTOM, 0).SetFontSize(13).SetFont(font);

                if (payments.Count() > 1 && payments.Count() > i+1)
                {
                    doc.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));
                }
            }
            doc.Close();

            byte[] byteStream = ms.ToArray();
            ms = new MemoryStream();
            ms.Write(byteStream, 0 , byteStream.Length);
            ms.Position = 0;

            return ms;
        }
    }
}
