namespace SupermarketSoft.Utilities
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.IO.Compression;
    using System.Linq;
    using Excel;
    using OfficeOpenXml;
    using OfficeOpenXml.Style;
    using Supermarket.Models;

    public static class ExcelUtility
    {
        private const int ExcelColumns = 5;
        private const string ExcelReportFilePath = @"..\..\..\Exported-Files\Excel";
        private const string ExcelReportFileName = @"\Product-Taxes.xlsx";

        private const string WorkSheetVendorColumn = "Vendor";
        private const string WorkSheetExpensesColumn = "Expenses";
        private const string WorkSheetIncomesColumn = "Incomes";
        private const string WorkSheetTotalTaxesColumn = "Total Taxes";
        private const string WorkSheetResultColumn = "Financial Result";

        public static void GenerateExcelReportFile(Dictionary<string, double?> taxes, List<Vendor> vendors)
        {
            var newFile = CreateFile();

            using (ExcelPackage package = new ExcelPackage(newFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Product Taxes");

                worksheet.Cells[1, 1].Value = WorkSheetVendorColumn;
                worksheet.Cells[1, 2].Value = WorkSheetIncomesColumn;
                worksheet.Cells[1, 3].Value = WorkSheetExpensesColumn;
                worksheet.Cells[1, 4].Value = WorkSheetTotalTaxesColumn;
                worksheet.Cells[1, 5].Value = WorkSheetResultColumn;

                var currentRow = 2;
                for (int i = 0; i < vendors.Count(); i++, currentRow++)
                {
                    worksheet.Cells[currentRow, 1].Value = vendors[i].VendorName;
                    var incomes = 0.0;
                    foreach (var product in vendors[i].Products)
                    {
                        foreach (var sale in product.Sales)
                        {
                            incomes += sale.Quantity * product.Price;
                        }
                    }

                    var totalTax = CalculateTaxes(vendors, i, taxes);

                    worksheet.Cells[currentRow, 2].Value = incomes;
                    worksheet.Cells[currentRow, 3].Value = vendors[i].Expenses.Sum(e => e.Value);
                    worksheet.Cells[currentRow, 4].Value = totalTax;

                    CalculateFinancialResult(worksheet, currentRow, vendors);
                }

                worksheet.Calculate();
                worksheet.Cells.AutoFitColumns(0);
                worksheet.View.PageLayoutView = true;
                worksheet.HeaderFooter.FirstHeader.CenteredText = "Баси данъците чуек";

                package.Workbook.Properties.Title = "Product Taxes";
                package.Workbook.Properties.Author = "Kor";
                package.Workbook.Properties.Comments = "Fok diz sh*t";

                package.Save();
            }

            Process.Start(ExcelReportFilePath);
        }

        public static List<List<string>> ReadSalesReportData(ZipArchive zip)
        {
            List<List<string>> sales = new List<List<string>>();

            foreach (var entry in zip.Entries)
            {
                string[] salesHeaders = entry.FullName.Split('/');

                if (entry.FullName.EndsWith(".xls"))
                {
                    using (var ms = new MemoryStream())
                    {
                        CopyStream(entry.Open(), ms);
                        var excelReader = ExcelReaderFactory.CreateBinaryReader(ms);
                        var dataSet = excelReader.AsDataSet();
                        string location = dataSet.Tables[0].Rows[1][1].ToString().Replace("“", "\"").Replace("”", "\"").Replace("’", "'");

                        for (int i = 3; i < dataSet.Tables[0].Rows.Count - 1; i++)
                        {
                            var row = dataSet.Tables[0].Rows[i];
                            var date = salesHeaders[0];
                            var productName = row[1].ToString().Replace("“", "\"").Replace("”", "\"").Replace("’", "'");
                            var quantity = row[2].ToString();
                            List<string> saleStrings = new List<string>() { date, location, productName, quantity };

                            sales.Add(saleStrings);
                        }
                    }
                }
            }

            return sales;
        }

        private static FileInfo CreateFile()
        {
            if (!Directory.Exists(ExcelReportFilePath))
            {
                Directory.CreateDirectory(ExcelReportFilePath);
            }

            FileInfo newFile = new FileInfo(ExcelReportFilePath + ExcelReportFileName);

            if (newFile.Exists)
            {
                newFile.Delete();
                newFile = new FileInfo(ExcelReportFilePath + ExcelReportFileName);
            }

            return newFile;
        }

        private static double? CalculateTaxes(List<Vendor> vendors, int i, Dictionary<string, double?> productTaxesData)
        {
            double? totalTax = 0.0;

            foreach (var product in vendors[i].Products)
            {
                var price = product.Price;
                var quantity = product.Sales.Sum(sale => sale.Quantity);
                var tax = productTaxesData.FirstOrDefault(t => t.Key.Equals(product.ProductName)).Value;

                totalTax += (price * quantity * tax) / 100;
            }

            return totalTax;
        }

        private static void CalculateFinancialResult(ExcelWorksheet worksheet, int currentRow, List<Vendor> vendors)
        {
            var cell = worksheet.Cells[currentRow, 5];
            cell.Formula = worksheet.Cells[currentRow, 2] + "-"
                   + worksheet.Cells[currentRow, 3] + "-" + worksheet.Cells[currentRow, 4];

            using (var range = worksheet.Cells[1, 1, 1, ExcelColumns])
            {
                range.Style.Font.Bold = true;
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(Color.DarkGray);
                range.Style.Font.Color.SetColor(Color.Black);
            }

            for (int row = 1; row <= vendors.Count + 1; row++)
            {
                for (int col = 1; col <= 5; col++)
                {
                    worksheet.Cells[row, col].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells["B2:E100"].Style.Numberformat.Format = "0.00";
                }
            }
        }

        private static void CopyStream(Stream input, Stream output)
        {
            var buffer = new byte[16 * 1024];
            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, read);
            }
        }
    }
}
