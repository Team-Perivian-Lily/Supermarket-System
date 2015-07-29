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
        public static void AddProducts(List<Product> products)
        {

            using (var ctx = new MySQLEntities())
            {
                foreach (var product in products)
                {
                    var existingVendor= ctx.Vendors
                        .FirstOrDefault(v => v.VendorName.Equals(product.Vendor.VendorName));
                    var existingMeasure = ctx.Measures
                        .FirstOrDefault(v => v.MeasureName.Equals(product.Measure.MeasureName));
                    

                    var productToAdd = new Product()
                    {
                        ProductName = product.ProductName,
                        Price = product.Price,
                       
                    };

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

                        productToAdd.Sales = new List<Sale>();
                        foreach (var sale in product.Sales)
                        {
                            var existingLocation = ctx.Locations.FirstOrDefault(l => l.Name.Equals(sale.Location.Name));

                            var saleToAdd = new Sale()
                            {
                                SoldOn = sale.SoldOn,
                                Quantity = sale.Quantity,

                            };

                            if (existingLocation == null)
                            {
                                saleToAdd.Location = new Location()
                                {
                                    Name = sale.Location.Name
                                };
                            }
                            else
                            {
                                saleToAdd.Location = existingLocation;
                            }
                            productToAdd.Sales.Add(saleToAdd);
                            if (!ctx.Products.Any(p => p.ProductName == productToAdd.ProductName))
                            {
                                ctx.Products.Add(productToAdd);
                            }
                            ctx.SaveChanges();
                        }
                    }
                    else
                    {
                        productToAdd.Vendor = existingVendor;
                        if (!ctx.Products.Any(p => p.ProductName == productToAdd.ProductName))
                        {
                            ctx.Products.Add(productToAdd);
                        }
                        ctx.SaveChanges();
                    }
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
            return ctx.Vendors.Include(v=>v.Products).Include(v=>v.Expenses).ToList();
            
        }
    }
}
