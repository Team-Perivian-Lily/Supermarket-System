namespace MSSQL.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data.Entity;
    using Oracle.Models;
    using Supermarket.Models;
    using Supermarket.Models.Reports;

    public static class MSSQLRepository
    {
        public static List<SalesReport> GetSalesByProduct(DateTime startDate, DateTime endDate)
        {
            using (var context = new MSSQLSupermarketEntities())
            {
                return context.Products
                    .Where(p => p.Sales.Any())
                    .Select(p => new SalesReport
                    {
                        ProductId = p.Id,
                        Product = p,
                        Vendor = p.Vendor,
                        TotalQuantitySold = p.Sales
                            .Where(s => s.SoldOn >= startDate && s.SoldOn <= endDate)
                            .Select(s => s.Quantity)
                            .DefaultIfEmpty(0)
                            .Sum(),
                        TotalIncomes = p.Sales
                            .Where(s => s.SoldOn >= startDate && s.SoldOn <= endDate)
                            .Select(s => s.Quantity)
                            .DefaultIfEmpty(0)
                            .Sum() * p.Price
                    })
                    .ToList();
            }
        }

        public static List<DateSalesReports> GetSalesByDate(DateTime startDate, DateTime endDate)
        {
            var result = new List<DateSalesReports>();

            using (var context = new MSSQLSupermarketEntities())
            {
                var dateGroups = context.Sales
                    .Where(s => s.SoldOn >= startDate && s.SoldOn <= endDate)
                    .GroupBy(s => new { s.SoldOn.Year, s.SoldOn.Month, s.SoldOn.Day })
                    .Select(g => new
                    {
                        Key = g.Key,
                        TotalSales = g.Sum(sale => sale.Quantity * sale.Product.Price),
                        Group = g
                            .Select(sale => new DateSalesReport
                            {
                                Product = sale.Product,
                                Location = sale.Location,
                                Quantity = sale.Quantity + " " + sale.Product.Measure.MeasureName,
                                SumOfSale = sale.Quantity * sale.Product.Price
                            })
                    }).ToList();

                result.AddRange(dateGroups
                    .Select(@group => new DateSalesReports
                    {
                        Key = new DateTime(@group.Key.Year, @group.Key.Month, @group.Key.Day),
                        TotalSumOfSales = @group.TotalSales,
                        ProductReports = @group.Group
                    })
                    );
            }

            return result;
        }

        public static List<VendorsSalesReports> GetSalesByVendor(DateTime startDate, DateTime endDate)
        {
            var result = new List<VendorsSalesReports>();

            using (var context = new MSSQLSupermarketEntities())
            {
                var vendorGroups = context.Sales
                    .Where(s => s.SoldOn >= startDate && s.SoldOn <= endDate)
                    .OrderBy(s => s.Product.Vendor.VendorName)
                    .GroupBy(s => s.Product.Vendor)
                    .Select(vg => new
                    {
                        Vendor = vg.Key,
                        Group = vg
                            .GroupBy(sale => sale.SoldOn)
                            .Select(sg => new VendorSalesReport
                            {
                                DateOfSale = sg.Key,
                                SumOfSales = sg.Sum(s => s.Quantity * s.Product.Price)
                            })
                    }).ToList();

                result.AddRange(vendorGroups
                    .Select(@group => new VendorsSalesReports
                    {
                        Vendor = @group.Vendor,
                        VendorReports = @group.Group
                    })
                    );
            }

            return result;
        }

        public static List<Product> GetFullProductsData()
        {
            using (var context = new MSSQLSupermarketEntities())
            {
                return context.Products
                    .Include(p => p.Measure)
                    .Include(p => p.Vendor)
                    .Include(p => p.Sales)
                    .Include(p => p.Sales.Select(s => s.Location))
                    .Include(p => p.Vendor.Expenses)
                    .ToList();
            }
        }

        public static void FillOracleDataToMsSql(List<ProductDTO> products)
        {
            using (var context = new MSSQLSupermarketEntities())
            {
                foreach (var product in products)
                {
                    var existingVendor = context.Vendors
                        .FirstOrDefault(v => v.VendorName.Equals(product.Vendor.VendorName));
                    var existingMeasure = context.Measures
                        .FirstOrDefault(v => v.MeasureName.Equals(product.Measure.MeasureName));

                    var productToAdd = new Product()
                    {
                        ProductName = product.ProductName,
                        Price = product.Price
                    };

                    AddProductVendor(existingVendor, productToAdd, product);
                    AddProductMeasure(existingMeasure, productToAdd, product);

                    if (!context.Products.Any(p => p.ProductName == productToAdd.ProductName))
                    {
                        context.Products.Add(productToAdd);
                    }

                    context.SaveChanges();
                }
            }
        }

        public static void FillExpensesDataToSql(Dictionary<string, List<string[]>> expensesByVendor)
        {
            using (var context = new MSSQLSupermarketEntities())
            {
                foreach (var expenses in expensesByVendor)
                {
                    var vendor = context.Vendors.FirstOrDefault(v => v.VendorName.Equals(expenses.Key));
                    if (vendor == null)
                    {
                        vendor = new Vendor { VendorName = expenses.Key };
                    }

                    foreach (var expense in expenses.Value)
                    {
                        var expenseData = DateTime.Parse(expense[0]);
                        var expenseValue = decimal.Parse(expense[1]);
                        var expenseContained = context.Expenses
                            .Any(x => x.DateOfExpense.Equals(expenseData) &&
                                      x.Vendor.VendorName.Equals(vendor.VendorName));

                        if (!expenseContained)
                        {
                            context.Expenses.Add(new Expense
                            {
                                Vendor = vendor,
                                DateOfExpense = expenseData,
                                Value = expenseValue
                            });
                        }
                    }
                }

                context.SaveChanges();
            }
        }

        public static void FillSalesDataToSql(List<List<string>> salesData)
        {
            using (var context = new MSSQLSupermarketEntities())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var saleData in salesData)
                        {
                            var locationName = saleData[1];
                            var productName = saleData[2];
                            var soldOn = DateTime.Parse(saleData[0]);
                            var existingLocation = context.Locations.FirstOrDefault(l => l.Name.Equals(locationName));
                            var product = context.Products.FirstOrDefault(p => p.ProductName.Equals(productName));
                            var quantity = int.Parse(saleData[3]);
                            var saleToAdd = new Sale()
                            {
                                SoldOn = soldOn,
                                Product = product,
                                Quantity = quantity
                            };

                            if (existingLocation == null)
                            {
                                saleToAdd.Location = new Location { Name = locationName };
                            }
                            else
                            {
                                saleToAdd.Location = existingLocation;
                            }

                            var existingSale = context.Sales
                                .FirstOrDefault(
                                    s => s.Product.ProductName == productName &&
                                         s.Location.Name == locationName &&
                                         s.SoldOn.Year.Equals(soldOn.Year) &&
                                         s.SoldOn.Month.Equals(soldOn.Month) &&
                                         s.SoldOn.Day.Equals(soldOn.Day));

                            if (existingSale == null)
                            {
                                context.Sales.Add(saleToAdd);
                            }

                            context.SaveChanges();
                        }

                        dbContextTransaction.Commit();
                    }
                    catch (Exception e)
                    {
                        dbContextTransaction.Rollback();
                        throw new Exception("Transaction rolled back.");
                    }
                }
            }
        }

        private static void AddProductMeasure(Measure existingMeasure, Product productToAdd, ProductDTO product)
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

        private static void AddProductVendor(Vendor existingVendor, Product productToAdd, ProductDTO product)
        {
            if (existingVendor == null)
            {
                productToAdd.Vendor = new Vendor()
                {
                    VendorName = product.Vendor.VendorName
                };
            }
            else
            {
                productToAdd.Vendor = existingVendor;
            }
        }
    }
}