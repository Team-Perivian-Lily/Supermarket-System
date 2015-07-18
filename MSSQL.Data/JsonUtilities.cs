using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSSQL.Data
{
    using System.Diagnostics;
    using System.IO;
    using System.Web.Script.Serialization;

    public static class JsonUtilities
    {
        public static void CreateJsonFiles(DateTime startDate, DateTime endDate, List<Product> products)
        {
  
                //var productsInRange = context.Products.Where(p => p.)
                if (!Directory.Exists(System.IO.Path.GetTempPath() + "/Json-Reports"))
                {
                    Directory.CreateDirectory(System.IO.Path.GetTempPath() + "/Json-Reports");
                }
               
                foreach (var product in products)
                {
                    FileStream fs1 = new FileStream(System.IO.Path.GetTempPath() + "/Json-Reports/" + product.Id + ".json", FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter writer = new StreamWriter(fs1);

                    writer.WriteLine(CreateJsonReport(product));
                    writer.Close();
                }
                Process.Start(System.IO.Path.GetTempPath() + "/Json-Reports/");
        }

        public static string CreateJsonReport(Product product)
        {
            var jsonReport =
                new JavaScriptSerializer().Serialize(
                    new Dictionary<string, string>()
                        {
                                { "product-id", product.Id.ToString() },
                                { "product-name", product.ProductName },
                                { "vendor-name", product.Vendor.VendorName },
                                { "total-quantity-sold", "47" },
                                { "total-incomes", "135.70" }
                        });
              
            return jsonReport;
        } 
    }
}
