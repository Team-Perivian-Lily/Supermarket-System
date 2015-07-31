namespace MySQL.Data
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using Supermarket.Models;

    public static class MySQLRepository
    {
        public static void AddSqlProductsToMySql(List<Product> products)
        {
            HashSet<Location> addedLocations = new HashSet<Location>();

            using (var context = new MySQLEntities())
            {
                foreach (var product in products)
                {
                    var productToAdd = new Product()
                    {
                        ProductName = product.ProductName,
                        Price = product.Price,
                    };

                    var existingVendor = context.Vendors
                        .FirstOrDefault(v => v.VendorName.Equals(product.Vendor.VendorName));
                    var existingMeasure = context.Measures
                        .FirstOrDefault(v => v.MeasureName.Equals(product.Measure.MeasureName));

                    AddProductMeasure(existingMeasure, productToAdd, product);
                    AddProductVendor(existingVendor, productToAdd, product);

                    productToAdd.Sales = new List<Sale>();
                    AddProductSales(product, addedLocations, productToAdd);

                    if (!context.Products.Any(p => p.ProductName == productToAdd.ProductName))
                    {
                        context.Products.Add(productToAdd);
                    }

                    context.SaveChanges();
                }
            }
        }

        public static void GenerateMySqlDb()
        {
            using (var context = new MySQLEntities())
            {
                context.Measures.Count();
            }
        }

        public static List<Vendor> GetVendorSalesData()
        {
            using (var context = new MySQLEntities())
            {
                return context.Vendors
                .Include(v => v.Products)
                .Include(v => v.Products.Select(p => p.Sales))
                .Include(v => v.Expenses)
                .ToList();
            }
        }

        private static void AddProductSales(Product product, HashSet<Location> addedLocations, Product productToAdd)
        {
            foreach (var sale in product.Sales)
            {
                var locationToAdd = new Location { Name = sale.Location.Name };
                var isLocationExist = addedLocations.Any(l => l.Name == locationToAdd.Name);
                var saleToAdd = new Sale
                {
                    SoldOn = sale.SoldOn,
                    Quantity = sale.Quantity
                };

                if (isLocationExist == false)
                {
                    saleToAdd.Location = locationToAdd;
                    addedLocations.Add(locationToAdd);
                }
                else
                {
                    saleToAdd.Location = addedLocations.FirstOrDefault(l => l.Name == sale.Location.Name);
                }

                productToAdd.Sales.Add(saleToAdd);
            }
        }

        private static void AddProductMeasure(Measure existingMeasure, Product productToAdd, Product product)
        {
            if (existingMeasure == null)
            {
                productToAdd.Measure = new Measure()
                {
                    MeasureName = product.Measure.MeasureName
                };
            }
            else
            {
                productToAdd.Measure = existingMeasure;
            }
        }

        private static void AddProductVendor(Vendor existingVendor, Product productToAdd, Product product)
        {
            if (existingVendor == null)
            {
                productToAdd.Vendor = new Vendor() { VendorName = product.Vendor.VendorName };
                productToAdd.Vendor.Expenses = new List<Expense>();

                foreach (var expense in product.Vendor.Expenses)
                {
                    var expenseToAdd = new Expense()
                    {
                        DateOfExpense = expense.DateOfExpense,
                        Value = expense.Value,
                    };

                    productToAdd.Vendor.Expenses.Add(expenseToAdd);
                }
            }
            else
            {
                productToAdd.Vendor = existingVendor;
            }
        }
    }
}
