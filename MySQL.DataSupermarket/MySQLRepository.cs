using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySQL.DataSupermarket;
using Supermarket.Models;
using Supermarket.Models.Reports;

namespace MySQL.DataSupermarket
{
    public static class MySQLRepository
    {
        public static void AddSqlProductsToMsSql(List<Product> products, List<Vendor> emptyVendorsData)
        {
            HashSet<Location> addedLocations = new HashSet<Location>();

            using (var context = new MySQLEntities())
            {
                foreach (var product in products)
                {
                    // PRODUCT
                    var productToAdd = new Product()
                    {
                        ProductName = product.ProductName,
                        Price = product.Price,

                    };

                    var existingVendor = context.Vendors
                        .FirstOrDefault(v => v.VendorName.Equals(product.Vendor.VendorName));
                    var existingMeasure = context.Measures
                        .FirstOrDefault(v => v.MeasureName.Equals(product.Measure.MeasureName));

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

                    // VENDOR

                    if (existingVendor == null)
                    {
                        productToAdd.Vendor = new Vendor()
                        {
                            VendorName = product.Vendor.VendorName
                        };

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
                    // SALES
                    productToAdd.Sales = new List<Sale>();

                    foreach (var sale in product.Sales)
                    {
                        var locationToAdd = new Location()
                        {
                            Name = sale.Location.Name
                        };

                        var isLocationExist = addedLocations.Any(l => l.Name == locationToAdd.Name);


                        var saleToAdd = new Sale()
                        {
                            SoldOn = sale.SoldOn,
                            Quantity = sale.Quantity,

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

                    if (!context.Products.Any(p => p.ProductName == productToAdd.ProductName))
                    {
                        context.Products.Add(productToAdd);
                    }

                    context.SaveChanges();
                }

                foreach (var emptyVendor in emptyVendorsData)
                {
                    var vendorToAdd = new Vendor()
                    {
                        VendorName = emptyVendor.VendorName
                    };
                    context.Vendors.Add(vendorToAdd);
                    context.SaveChanges();
                }
            }
        }

        public static void GenerateMySqlDb()
        {
            var ctx = new MySQLEntities();
            ctx.Products.Count();
            ctx.SaveChanges();
        }

        public static List<Vendor> GetAllData()
        {
            var ctx = new MySQLEntities();
            return ctx.Vendors
                .Include(v => v.Products)
                .Include(v => v.Products.Select(p => p.Sales))
                .Include(v => v.Expenses).ToList();
        }
    }
}
