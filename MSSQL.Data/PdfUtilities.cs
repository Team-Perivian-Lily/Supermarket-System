namespace MSSQL.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using iTextSharp.text;
    using iTextSharp.text.pdf;

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
            pdfReport.Add(this.GenerateReportTable(context, pdfReport));
            pdfReport.Close();
        }

        private PdfPTable GenerateReportTable(MSSQLSupermarketEntities context, Document pdfReport)
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

        private void AddGroupContentCells(PdfPTable salesTable, IEnumerable<Sale> salesGroup)
        {
            foreach (var sale in salesGroup)
            {
                PdfPCell[] contentCells = new[] { 
                    new PdfPCell { Colspan = 1 },
                    new PdfPCell { Colspan = 1 }, 
                    new PdfPCell { Colspan = 1 }, 
                    new PdfPCell { Colspan = 1 }, 
                    new PdfPCell { Colspan = 1 }
                };

                contentCells[0].AddElement(new Paragraph(sale.Product.ProductName));
                contentCells[1].AddElement(new Paragraph(sale.Quantity + " " + sale.Product.Measure.MeasureName));
                contentCells[2].AddElement(new Paragraph(sale.Product.Price.ToString("F")));
                contentCells[3].AddElement(new Paragraph(sale.Location.Name));
                contentCells[4].AddElement(new Paragraph((sale.Product.Price * sale.Quantity).ToString("F")));

                foreach (var contentCell in contentCells)
                {
                    salesTable.AddCell(contentCell);
                }
            }
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

            string[] headerParagraphs = { "Product", "Quantity", "Unit Price", "Location", "Sum" };
            foreach (string text in headerParagraphs)
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
