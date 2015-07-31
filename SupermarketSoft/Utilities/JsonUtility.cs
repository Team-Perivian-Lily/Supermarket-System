namespace SupermarketSoft.Utilities
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Web.Script.Serialization;
    using Supermarket.Models.Reports;

    public static class JsonUtility
    {
        private const string JsonReportFilePath = @"..\..\..\Exported-Files\Json";

        public static void CreateJsonFiles(List<SalesReport> productsSales)
        {
            if (!Directory.Exists(JsonReportFilePath))
            {
                Directory.CreateDirectory(JsonReportFilePath);
            }

            foreach (var productSales in productsSales)
            {
                FileStream fs1 = new FileStream(JsonReportFilePath + @"\" + productSales.Product.Id + ".json", FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter writer = new StreamWriter(fs1);

                writer.WriteLine(CreateJsonReport(productSales));
                writer.Close();
            }

            Process.Start(JsonReportFilePath);
        }

        public static string CreateJsonReport(SalesReport salesReport)
        {
            return
                new JavaScriptSerializer().Serialize(
                    new Dictionary<string, string>()
                        {
                            { "product-id", salesReport.ProductId.ToString() },
                            { "product-name", salesReport.Product.ProductName},
                            { "vendor-name", salesReport.Product.Vendor.VendorName },
                            { "total-quantity-sold", salesReport.TotalQuantitySold.ToString() },
                            { "total-incomes", salesReport.TotalIncomes.ToString() }
                        });
        }
    }
}

