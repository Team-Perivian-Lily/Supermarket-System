namespace SupermarketSoft.Utilities
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Web.Script.Serialization;
    using Supermarket.Models.Reports;

    public static class JsonUtility
    {
        public static void CreateJsonFiles(List<SalesReport> productsSales)
        {
            if (!Directory.Exists(Path.GetTempPath() + "/Json-Reports"))
            {
                Directory.CreateDirectory(Path.GetTempPath() + "/Json-Reports");
            }

            foreach (var productSales in productsSales)
            {
                FileStream fs1 = new FileStream(Path.GetTempPath() + "/Json-Reports/" + productSales.Product.Id + ".json", FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter writer = new StreamWriter(fs1);

                writer.WriteLine(CreateJsonReport(productSales));
                writer.Close();
            }

            Process.Start(Path.GetTempPath() + "/Json-Reports/");
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

