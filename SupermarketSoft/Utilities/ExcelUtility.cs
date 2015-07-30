namespace SupermarketSoft.Utilities
{
    using System.Diagnostics;
    using System.Windows.Forms;
    using SQLLite.Data;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Compression;
    using System.Linq;
    using System.Drawing;
    using MySQL.DataSupermarket;
    using OfficeOpenXml;
    using OfficeOpenXml.Style;
    using Excel;

    public static class ExcelUtility
    {
        private const int ExcelColumns = 5;
        public static string GenerateFile(DirectoryInfo outputDir = null)
        {
            var path = "";
            if (outputDir == null)
            {
                path = @"..\..\..";
            }
            else
            {
                path = outputDir.FullName;
            }

            var vendors = MySQLRepository.GetAllData();

            var context = new SQLiteEntities();
            var taxes = context.Taxes.ToList();

            FileInfo newFile = new FileInfo(path + @"\ProductTaxes.xlsx");

            if (newFile.Exists)
            {
                newFile.Delete();  // ensures we create a new workbook
                newFile = new FileInfo(path + @"\ProductTaxes.xlsx");
            }
            using (ExcelPackage package = new ExcelPackage(newFile))
            {
                // add a new worksheet to the empty workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Product Taxes");

                // Add the headers
                worksheet.Cells[1, 1].Value = "Vendor";
                worksheet.Cells[1, 2].Value = "Incomes";
                worksheet.Cells[1, 3].Value = "Expenses";
                worksheet.Cells[1, 4].Value = "Total Taxes";
                worksheet.Cells[1, 5].Value = "Financial Result";

                var currentRow = 2;
                for (int i = 0; i < vendors.Count(); i++, currentRow++)
                {
                    // Vendors
                    worksheet.Cells[currentRow, 1].Value = vendors[i].VendorName;
                    var incomes = 0.0;
                    foreach (var product in vendors[i].Products)
                    {
                        foreach (var sale in product.Sales)
                        {
                            incomes += sale.Quantity * product.Price;
                        }
                    }

                    // Incomes
                    worksheet.Cells[currentRow, 2].Value = incomes;

                    // Expenses
                    worksheet.Cells[currentRow, 3].Value = vendors[i].Expenses.Sum(e => e.Value);

                    // Calculate taxes
                    var totalTax = 0.0;

                    foreach (var product in vendors[i].Products)
                    {
                        var tax = 20;
                        var price = product.Price;
                        var quantity = product.Sales.Sum(sale => sale.Quantity);

                        var temporaryTax = price * quantity * tax / 100;
                        totalTax += (double)temporaryTax;
                    }

                    worksheet.Cells[currentRow, 4].Value = totalTax;

                    // Calculate financial result
                    var cell = worksheet.Cells[currentRow, 5];
                    cell.Formula = worksheet.Cells[currentRow, 2] + "-" + worksheet.Cells[currentRow, 3] + "-" + worksheet.Cells[currentRow, 4];

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

                worksheet.Calculate();

                worksheet.Cells.AutoFitColumns(0);  //Autofit columns for all cells

                // Pade layout
                worksheet.View.PageLayoutView = true;

                // Header
                worksheet.HeaderFooter.FirstHeader.CenteredText = "Баси данъците чуек";

                // set some document properties
                package.Workbook.Properties.Title = "Product Taxs";
                package.Workbook.Properties.Author = "Kor";
                package.Workbook.Properties.Comments = "Fok diz sh*t";

                package.Save();
            }

            GenerateMessageBox(path);
            return newFile.FullName;
        }

        private static void GenerateMessageBox(string path)
        {
            var msgBox = MessageBox.Show("Excel report created successfully. Do you want to open the directory?", "Confirm",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (msgBox == DialogResult.OK)
            {
                Process.Start(path);
            }
        }

        public static List<List<string>> ReadSaleData(ZipArchive zip)
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

        public static void CopyStream(Stream input, Stream output)
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
