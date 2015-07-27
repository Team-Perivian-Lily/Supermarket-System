namespace SupermarketSoft.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using iTextSharp.text;
    using iTextSharp.text.pdf;
    using Supermarket.Models.Reports;

    public static class PdfUtility
    {
        public static void CreatePdfFile(List<DateSalesReports> dataGroups)
        {
            if (!Directory.Exists(Path.GetTempPath() + "/Pdf-Report"))
            {
                Directory.CreateDirectory(Path.GetTempPath() + "/Pdf-Report");
            }

            var pdfReport = new Document(PageSize.A4, 50f, 50f, 50f, 50f);
            PdfWriter.GetInstance(
                pdfReport,
                new FileStream(Path.GetTempPath() + "/Pdf-Report" + "/Sales-Report-Test.pdf", FileMode.Create));

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

            return table;
        }

        private static void AddContentMainHeader(PdfPTable table)
        {
            Paragraph headerParagraph = new Paragraph("Aggregated Sales Report");
            headerParagraph.Font.SetStyle("bold");
            headerParagraph.Alignment = 1;

            PdfPCell headerCell = new PdfPCell();
            headerCell.Colspan = 5;
            headerCell.AddElement(headerParagraph);

            table.AddCell(headerCell);
        }

        private static void AddSecondaryHeaderCells(PdfPTable table, DateTime dataGroupTotalSumOfSales)
        {
            Paragraph headerMainParagraph = new Paragraph("Date: " + dataGroupTotalSumOfSales.ToShortDateString());
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
                Paragraph headerSecondaryParagraph = new Paragraph(text);
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
                contentCells[0] = new PdfPCell(new Paragraph(groupItem.Product.ProductName));
                contentCells[1] = new PdfPCell(new Paragraph(groupItem.Quantity));
                contentCells[2] = new PdfPCell(new Paragraph(groupItem.Product.Price.ToString("F")));
                contentCells[3] = new PdfPCell(new Paragraph(groupItem.Location.Name));
                contentCells[4] = new PdfPCell(new Paragraph(groupItem.SumOfSale.ToString("F")));

                foreach (var contentCell in contentCells)
                {
                    table.AddCell(contentCell);
                }
            }

            var footerLabelCell = new PdfPCell();
            footerLabelCell.Colspan = 4;

            var footerLabelContent = new Paragraph("Total sum for " + dataGroup.Key.ToShortDateString());
            footerLabelContent.Alignment = Element.ALIGN_RIGHT;

            var footerContentCell = new PdfPCell();
            footerContentCell.Colspan = 1;

            var footerContent = new Paragraph(dataGroup.TotalSumOfSales.ToString("F"));

            footerLabelCell.AddElement(footerLabelContent);
            footerContentCell.AddElement(footerContent);

            table.AddCell(footerLabelCell);
            table.AddCell(footerContentCell);
        }
    }
}