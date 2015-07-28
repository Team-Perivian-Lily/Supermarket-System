using System;
using System.Collections.Generic;
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
                    var productToAdd = new Product()
                    {
                        ProductName = product.ProductName,
                        Measure = new Measure()
                        {
                            MeasureName = product.Measure.MeasureName
                        },
                        Price = product.Price,
                        Vendor = new Vendor()
                        {
                            VendorName = product.Vendor.VendorName,
                            
                        }
                    };

                    productToAdd.Vendor.Expenses = new List<Expense>();

                    productToAdd.Sales = new List<Sale>();

                    foreach (var sale in product.Sales)
                    {
                        var saleToAdd = new Sale()
                        {
                            SoldOn = sale.SoldOn,
                            Quantity = sale.Quantity,
                            Location = new Location()
                            {
                                Name = sale.Location.Name
                            }
                        };
                        productToAdd.Sales.Add(saleToAdd);

                    }

                    foreach (var expense in product.Vendor.Expenses)
                    {
                        var expenseToAdd = new Expense()
                        {
                            DateOfExpense = expense.DateOfExpense,
                            Value = expense.Value,
                        };

                        productToAdd.Vendor.Expenses.Add(expenseToAdd);
                    }

                    if (!ctx.Products.Any(p => p.ProductName == productToAdd.ProductName))
                    {
                        ctx.Products.Add(productToAdd);
                    }
                }
                ctx.SaveChanges();
            }
        }

        public static void Test()
        {
            var ctx = new MySQLEntities();

            ctx.Locations.Add(new Location()
            {
                Name = "NEW"
            });

            ctx.SaveChanges();
        }
    }
}
