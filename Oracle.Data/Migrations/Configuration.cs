using System.Linq;
using System.Security.Cryptography.X509Certificates;
using ClassLibrary1;

namespace Oracle.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using System.IO;
    //using Supermarket.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<OracleDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "Oracle.Data.OracleDbContext";
        }

        protected override void Seed(OracleDbContext context)
        {
            FillVendors(context);
            FillMeasures(context);
            FillProducts(context);
        }

        public void FillVendors(OracleDbContext context)
        {
           
            if (!context.VENDORS.Any())
            {
                string[] vendors = new string[]
                {
                    "Nestle Sofia Corp.",
                    "Zagorka Corp.",
                    "Targovishte Bottling Company Ltd.",
                    "Coca Cola Corp.",
                    "Heineken International",
                    "Carlsberg International",
                    "Alpina Distribution",
                    "Aladin OOD",
                    "Bella Bulgaria AD",
                    "Victory OOD"
                };

                for (int i = 0; i < vendors.Length; i++)
                {
                    context.VENDORS.Add(new Vendor()
                    {
                        VendorName = vendors[i]
                    });
                }
            }
            context.SaveChanges();
        }

        public void FillMeasures(OracleDbContext context)
        {
            if (!context.MEASURES.Any())
            {
                string[] measures = new string[]
                {
                    "lieters",
                    "pieces",
                    "kg"
                };

                for (int i = 0; i < measures.Length; i++)
                {
                    context.MEASURES.Add(new Measure()
                    {
                        Id = i * 100,
                       MeasureName = measures[i]
                    });
                }
            }
            context.SaveChanges();
        }

        public void FillProducts(OracleDbContext context)
        {

            if (!context.PRODUCTS.Any())
            {
                string[] products = new string[]
                {
                    "2,Beer \"Zagorka\",2,0.86",
                    "3,Vodka \"Targovishte\",2,7.56",
                    "2,Beer \"Beck's\",2,1.03",
                    "1,Chocolate \"Milka\",3,2.80",
                    "10,Victory White,4,4.8",
                    "3,Vodka FLIRT,2,7.00",
                    "10,Victory Blue,4,4.80",
                    "3,Vodka Absolute,2,10.00",
                    "9,Lotto Pizza,4,0.90",
                    "10,Victory Deluxe,4,2.80",
                    "9,Bruscetti MArreti,4,1.20",
                    "10,Victory Exclusive,4,4.80",
                    "3,Black Ram,2,9.00",
                    "9,7 Days,4,1.10",
                    "9,Mura ChocoDreams,4,0.70"
                };

                for (int i = 0; i < products.Length; i++)
                {
                    string[] currProduct = products[i].Split(',');
                    int vendorID = int.Parse(currProduct[0]);
                    string productName = currProduct[1];
                    int measureId = int.Parse(currProduct[2]);
                    double price = double.Parse(currProduct[3]);
                    context.PRODUCTS.Add(new Product()
                    {
                        VendorID = vendorID,
                        ProductName = productName,
                        MeasureID = measureId,
                        Price = price
                    });
                }
            }
            context.SaveChanges();
        }
    }
}