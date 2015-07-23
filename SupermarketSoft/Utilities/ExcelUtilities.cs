using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using Supermarket.Models;
using SQLLite.Data;


namespace SupermarketSoft.Utilities
{
    using System.IO.Compression;
    using System.Windows.Forms;

    using Excel;

    public static class ExcelUtilities
    {
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

            var ctx = new SQLiteEntities();

            var taxes = ctx.Taxes.ToList();

            var colNames = typeof(Tax).GetProperties().Select(p => p.Name).ToList();
            FileInfo newFile = new FileInfo(path + @"\ProductTaxes.xlsx");

            if (newFile.Exists)
            {
                newFile.Delete();  // ensures we create a new workbook
                newFile = new FileInfo(path + @"\ProductTaxes.xlsx");
            }
            using (ExcelPackage package = new ExcelPackage(newFile))
            {
                var currentRow = 1;
                // add a new worksheet to the empty workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Product Taxes");
                //Add the headers
                for (int i = 1; i <= colNames.Count; i++)
                {
                    worksheet.Cells[currentRow, i].Value = colNames[i - 1];
                }

                currentRow++;
                for (int i = currentRow; i < taxes.Count + currentRow; i++)
                {
                    worksheet.Cells[currentRow, 1].Value = taxes[i - currentRow].Id;
                    worksheet.Cells[currentRow, 2].Value = taxes[i - currentRow].Name;
                    worksheet.Cells[currentRow, 3].Value = taxes[i - currentRow].Tax1;
                }



                //var cell = worksheet.Cells[products.Count + 2, 1];

                //cell.Formula = "Sum(" + worksheet.Cells[2, 1] + ":" + worksheet.Cells[products.Count + 1, 1] + ")";

                //worksheet.Cells["A3"].Value = 12002;
                //worksheet.Cells["B3"].Value = "Hammer";
                //worksheet.Cells["C3"].Value = 5;
                //worksheet.Cells["D3"].Value = 12.10;

                //worksheet.Cells["A4"].Value = 12003;
                //worksheet.Cells["B4"].Value = "Saw";
                //worksheet.Cells["C4"].Value = 12;
                //worksheet.Cells["D4"].Value = 15.37;

                //Add a formula for the value-column
                //worksheet.Cells["E2:E4"].Formula = "C2*D2";

                ////Ok now format the values;
                //using (var range = worksheet.Cells[1, 1, 1, 5])
                //{
                //    range.Style.Font.Bold = true;
                //    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                //    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                //    range.Style.Font.Color.SetColor(Color.White);
                //}

                //worksheet.Cells["A5:E5"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                //worksheet.Cells["A5:E5"].Style.Font.Bold = true;

                //worksheet.Cells[5, 3, 5, 5].Formula = string.Format("SUBTOTAL(9,{0})", new ExcelAddress(2, 3, 4, 3).Address);
                //worksheet.Cells["C2:C5"].Style.Numberformat.Format = "#,##0";
                //worksheet.Cells["D2:E5"].Style.Numberformat.Format = "#,##0.00";

                ////Create an autofilter for the range
                //worksheet.Cells["A1:E4"].AutoFilter = true;

                //worksheet.Cells["A2:A4"].Style.Numberformat.Format = "@";   //Format as text

                ////There is actually no need to calculate, Excel will do it for you, but in some cases it might be useful. 
                ////For example if you link to this workbook from another workbook or you will open the workbook in a program that hasn't a calculation engine or 
                ////you want to use the result of a formula in your program.
                //worksheet.Calculate();

                worksheet.Cells.AutoFitColumns(0);  //Autofit columns for all cells

                // lets set the header text 
                //worksheet.HeaderFooter.OddHeader.CenteredText = "&24&U&\"Arial,Regular Bold\" Inventory";
                //// add the page number to the footer plus the total number of pages
                //worksheet.HeaderFooter.OddFooter.RightAlignedText =
                //    string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);
                //// add the sheet name to the footer
                //worksheet.HeaderFooter.OddFooter.CenteredText = ExcelHeaderFooter.SheetName;
                //// add the file path to the footer
                //worksheet.HeaderFooter.OddFooter.LeftAlignedText = ExcelHeaderFooter.FilePath + ExcelHeaderFooter.FileName;

                //worksheet.PrinterSettings.RepeatRows = worksheet.Cells["1:2"];
                //worksheet.PrinterSettings.RepeatColumns = worksheet.Cells["A:G"];

                //// Change the sheet view to show it in page layout mode
                //worksheet.View.PageLayoutView = true;

                // set some document properties
                //package.Workbook.Properties.Title = "Invertory";
                //package.Workbook.Properties.Author = "Jan Kдllman";
                //package.Workbook.Properties.Comments = "This sample demonstrates how to create an Excel 2007 workbook using EPPlus";

                //// set some extended property values
                //package.Workbook.Properties.Company = "AdventureWorks Inc.";

                //// set some custom property values
                //package.Workbook.Properties.SetCustomPropertyValue("Checked by", "Jan Kдllman");
                //package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", "EPPlus");
                // save our new workbook and we are done!
                package.Save();

            }

            return newFile.FullName;
        }

        public static void ReadExcelData(ZipArchiveEntry entry, string[] salesData)
        {
            var ms = new MemoryStream();
            CopyStream(entry.Open(), ms);
            var excelReader = ExcelReaderFactory.CreateBinaryReader(ms);
            var dataSet = excelReader.AsDataSet();
            string shop = dataSet.Tables[0].Rows[1][1].ToString();
            string result = string.Empty;
            for (int i = 3; i < dataSet.Tables[0].Rows.Count - 1; i++)
            {
                var row = dataSet.Tables[0].Rows[i];

                result +=
                    "\n\nDate: " + salesData[0]
                    + "\nShop: " + shop
                    + "\nProduct: " + row[1]
                    + "\nQuantity: " + row[2];
            }
            MessageBox.Show(result);
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
