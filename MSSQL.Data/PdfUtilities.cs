namespace MSSQL.Data
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using iTextSharp.text;
    using iTextSharp.text.pdf;
    using Supermarket.Models;

    public class PdfUtilities
    {
        public void CreateReportFile()
        {
            var context = new MSSQLSupermarketEntities();

            if (!Directory.Exists(Path.GetTempPath() + "/Pdf-Report"))
            {
                Directory.CreateDirectory(Path.GetTempPath() + "/Pdf-Report");
            }

            var pdfReport = new Document(PageSize.A4, 50f, 50f, 50f, 50f);
            PdfWriter.GetInstance(
                pdfReport,
                new FileStream(Path.GetTempPath() + "/Pdf-Report" + "/Sales-Report.pdf", FileMode.Create));

            pdfReport.Open();
            pdfReport.Add(GenerateReportTable(context));
            pdfReport.Close();

            Process.Start(Path.GetTempPath() + "/Pdf-Report/");
        }

        private PdfPTable GenerateReportTable(MSSQLSupermarketEntities context)
        {
            var salesGroupsByDate = context.Sales
                .GroupBy(s => s.Date);

            PdfPTable salesTable = new PdfPTable(5);
            salesTable.TotalWidth = 490f;
            salesTable.LockedWidth = true;

            AddSalesTableMainHeader(salesTable);

            foreach (var salesGroup in salesGroupsByDate)
            {
                AddGroupHeaderCells(salesTable, salesGroup);
                AddGroupContentCells(salesTable, salesGroup);
            }

            return salesTable;
        }

        private void AddGroupContentCells(PdfPTable salesTable, IGrouping<DateTime, Sale> salesGroup)
        {
            PdfPCell[] contentCells = new PdfPCell[5];

            foreach (var sale in salesGroup)
            {
                var productName = sale.Product.ProductName;
                var quantity = sale.Quantity + " " + sale.Product.Measure.MeasureName;
                var unitPrice = sale.Product.Price.ToString("F");
                var location = sale.Location.Name;
                var sumOfSale = (sale.Product.Price*sale.Quantity).ToString("F");

                contentCells[0] = new PdfPCell(new Paragraph(productName));
                contentCells[1] = new PdfPCell(new Paragraph(quantity));
                contentCells[2] = new PdfPCell(new Paragraph(unitPrice));
                contentCells[3] = new PdfPCell(new Paragraph(location));
                contentCells[4] = new PdfPCell(new Paragraph(sumOfSale));

                foreach (var contentCell in contentCells)
                {
                    salesTable.AddCell(contentCell);
                }
            }

            var totalDateSumLabel = new PdfPCell();
            totalDateSumLabel.Colspan = 4;

            var preLastParagraph = new Paragraph("Total sum for " + salesGroup.Key.ToShortDateString());
            preLastParagraph.Alignment = Element.ALIGN_RIGHT;

            var totalDateSum = new PdfPCell();
            totalDateSum.Colspan = 1;

            var lastParagraph = new Paragraph(salesGroup.Sum(sg => sg.Quantity*sg.Product.Price).ToString("F"));

            totalDateSumLabel.AddElement(preLastParagraph);
            totalDateSum.AddElement(lastParagraph);

            salesTable.AddCell(totalDateSumLabel);
            salesTable.AddCell(totalDateSum);
        }

        private void AddSalesTableMainHeader(PdfPTable salesTable)
        {
            Paragraph headerParagraph = new Paragraph("Aggregated Sales Report");
            headerParagraph.Font.SetStyle("bold");
            headerParagraph.Alignment = 1;

            PdfPCell headerCell = new PdfPCell();
            headerCell.Colspan = 5;
            headerCell.AddElement(headerParagraph);

            salesTable.AddCell(headerCell);
        }

        private void AddGroupHeaderCells(PdfPTable salesTable, IGrouping<DateTime, Sale> salesGroup)
        {
            Paragraph dateParagraph = new Paragraph("Date: " + salesGroup.Key.ToShortDateString());
            dateParagraph.Font.SetStyle("bold");
            dateParagraph.Alignment = 0;

            PdfPCell dateCell = new PdfPCell();
            dateCell.Colspan = salesTable.NumberOfColumns;
            dateCell.AddElement(dateParagraph);
            dateCell.BackgroundColor = BaseColor.LIGHT_GRAY;

            salesTable.AddCell(dateCell);

            string[] headerParagraphs = {"Product", "Quantity", "Unit Price", "Location", "Sum"};
            foreach (var text in headerParagraphs)
            {
                Paragraph headerParagraph = new Paragraph(text);
                headerParagraph.Alignment = 0;

                PdfPCell headerCell = new PdfPCell();
                headerCell.Colspan = 1;
                headerCell.AddElement(headerParagraph);
                headerCell.BackgroundColor = BaseColor.LIGHT_GRAY;

                salesTable.AddCell(headerCell);
            }
        }
    }
}