namespace SupermarketSoft.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using iTextSharp.text;
    using iTextSharp.text.pdf;
    using Supermarket.Models.Reports;
    using Font = iTextSharp.text.Font;

    public static class PdfUtility
    {
        private static readonly Font MainHeaderFont = new Font(Font.FontFamily.HELVETICA, 14, (int)FontStyle.Bold);
        private static readonly Font HeaderFont = new Font(Font.FontFamily.HELVETICA, 13, (int)FontStyle.Bold);
        private static readonly Font FooterFont = new Font(Font.FontFamily.HELVETICA, 13, (int)FontStyle.Bold);
        private static readonly Font ContentFont = new Font(Font.FontFamily.HELVETICA, 12, 0);

        public static void CreatePdfFile(List<DateSalesReports> dataGroups)
        {
            if (!Directory.Exists(Path.GetTempPath() + "/Pdf-Report"))
            {
                Directory.CreateDirectory(Path.GetTempPath() + "/Pdf-Report");
            }

            var pdfReport = new Document(PageSize.A4, 50f, 50f, 50f, 50f);
            PdfWriter.GetInstance(
                pdfReport,
                new FileStream(Path.GetTempPath() + "/Pdf-Report" + "/Sales-Report.pdf", FileMode.Create));

            pdfReport.Open();
            pdfReport.Add(PopulatePdfFile(dataGroups));
            pdfReport.Close();

            Process.Start(Path.GetTempPath() + "/Pdf-Report/");
        }

        private static PdfPTable PopulatePdfFile(List<DateSalesReports> dataGroups)
        {
            PdfPTable table = new PdfPTable(5);
            table.TotalWidth = 490f;
            table.LockedWidth = true;

            AddContentMainHeader(table);

            foreach (var dataGroup in dataGroups)
            {
                AddSecondaryHeaderCells(table, dataGroup.Key);
                AddContentCells(table, dataGroup);
            }

            var grandTotal = dataGroups.Sum(dg => dg.TotalSumOfSales).ToString("F");
            AddFooterRow(table, "Grand Total: ", grandTotal);

            return table;
        }

        private static void AddContentMainHeader(PdfPTable table)
        {
            Paragraph headerParagraph = new Paragraph("Aggregated Sales Report", MainHeaderFont);
            headerParagraph.Font.SetStyle("bold");
            headerParagraph.Alignment = 1;

            PdfPCell headerCell = new PdfPCell();
            headerCell.Colspan = 5;
            headerCell.AddElement(headerParagraph);

            table.AddCell(headerCell);
        }

        private static void AddSecondaryHeaderCells(PdfPTable table, DateTime secondaryHeaderContent)
        {
            Paragraph headerMainParagraph = new Paragraph("Date: " + secondaryHeaderContent.ToShortDateString(), HeaderFont);
            headerMainParagraph.Font.SetStyle("bold");
            headerMainParagraph.Alignment = 0;

            PdfPCell headerMainCell = new PdfPCell();
            headerMainCell.Colspan = table.NumberOfColumns;
            headerMainCell.AddElement(headerMainParagraph);
            headerMainCell.BackgroundColor = BaseColor.LIGHT_GRAY;

            table.AddCell(headerMainCell);

            string[] headerParagraphs = { "Product", "Quantity", "Unit Price", "Location", "Sum" };
            foreach (var text in headerParagraphs)
            {
                Paragraph headerSecondaryParagraph = new Paragraph(text, HeaderFont);
                headerSecondaryParagraph.Alignment = 0;

                PdfPCell headerSecondaryCell = new PdfPCell();
                headerSecondaryCell.Colspan = 1;
                headerSecondaryCell.AddElement(headerSecondaryParagraph);
                headerSecondaryCell.BackgroundColor = BaseColor.LIGHT_GRAY;

                table.AddCell(headerSecondaryCell);
            }
        }

        private static void AddContentCells(PdfPTable table, DateSalesReports dataGroup)
        {
            PdfPCell[] contentCells = new PdfPCell[5];

            foreach (var groupItem in dataGroup.ProductReports)
            {
                contentCells[0] = new PdfPCell(new Paragraph(groupItem.Product.ProductName, ContentFont));
                contentCells[1] = new PdfPCell(new Paragraph(groupItem.Quantity, ContentFont));
                contentCells[2] = new PdfPCell(new Paragraph(groupItem.Product.Price.ToString("F"), ContentFont));
                contentCells[3] = new PdfPCell(new Paragraph(groupItem.Location.Name, ContentFont));
                contentCells[4] = new PdfPCell(new Paragraph(groupItem.SumOfSale.ToString("F"), ContentFont));

                foreach (var contentCell in contentCells)
                {
                    table.AddCell(contentCell);
                }
            }

            var footerLabelText = "Total sum for  " + dataGroup.Key.ToShortDateString();
            var footerContent = dataGroup.TotalSumOfSales.ToString("F");

            AddFooterRow(table, footerLabelText, footerContent);
        }

        private static void AddFooterRow(PdfPTable table, string footerLabelText, string footerValue)
        {
            var footerLabelCell = new PdfPCell();
            footerLabelCell.Colspan = 4;

            var footerLabelContent = new Paragraph(footerLabelText, FooterFont);
            footerLabelContent.Alignment = Element.ALIGN_RIGHT;

            var footerContentCell = new PdfPCell();
            footerContentCell.Colspan = 1;

            var footerContent = new Paragraph(footerValue, FooterFont);

            footerLabelCell.AddElement(footerLabelContent);
            footerContentCell.AddElement(footerContent);

            table.AddCell(footerLabelCell);
            table.AddCell(footerContentCell);
        }
    }
}