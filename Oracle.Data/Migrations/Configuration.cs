namespace Oracle.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;

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
                    context.VENDORS.Add(new VendorDTO()
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
                    context.MEASURES.Add(new MeasureDTO()
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
                    "2,Beer \"Zagorka\",1,0.86",
                    "3,Vodka \"Targovishte\",1,7.56",
                    "2,Beer \"Beck's\",1,1.03",
                    "1,Chocolate \"Milka\",2,2.80",
                    "10,Victory White,3,4.8",
                    "3,Vodka FLIRT,1,7.00",
                    "10,Victory Blue,3,4.80",
                    "3,Vodka Absolute,1,10.00",
                    "9,Lotto Pizza,3,0.90",
                    "10,Victory Deluxe,3,2.80",
                    "9,Bruscetti MArreti,3,1.20",
                    "10,Victory Exclusive,3,4.80",
                    "3,Black Ram,1,9.00",
                    "9,7 Days,3,1.10",
                    "9,Mura ChocoDreams,3,0.70",
                    "4,Fanta Orange,2,1.00",
                    "5,Heineken,2,2.00",
                    "6,Carlsberg,2,2.50",
                    "7,Prestige,1,0.50",
                    "8,Kori,1,0.70"
                };

                for (int i = 0; i < products.Length; i++)
                {
                    string[] currProduct = products[i].Split(',');
                    int vendorID = int.Parse(currProduct[0]);
                    string productName = currProduct[1];
                    int measureId = int.Parse(currProduct[2]);
                    double price = double.Parse(currProduct[3]);
                    context.PRODUCTS.Add(new ProductDTO()
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