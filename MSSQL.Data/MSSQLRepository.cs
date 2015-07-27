namespace MSSQL.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Oracle.Models;
    using Supermarket.Models;
    using Supermarket.Models.Reports;

    public static class MSSQLRepository
    {
        public static List<SalesReport> SalesByProductReports(DateTime startDate, DateTime endDate)
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

        //public static List<SalesReport> GetProducts()
        //{
        //    using (var context = new MSSQLSupermarketEntities())
        //    {
        //        return context.Products
        //            .Where(p => p.Sales.Any())
        //            .Select(p => new SalesReport
        //            {
        //                ProductId = p.Id,
        //                Product = p,
        //                Vendor = p.Vendor
        //            })
        //            .ToList();
        //    }
        //}

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

        public static void FillOracleDataToSql(List<ProductDTO> products)
        {
            using (var context = new MSSQLSupermarketEntities())
            {
                foreach (var product in products)
                {
                    context.Products.Add(new Product()
                    {
                        ProductName = product.ProductName,
                        Price = product.Price,
                        Measure = new Measure()
                        {
                            MeasureName = product.Measure.MeasureName
                        },
                        Vendor = new Vendor()
                        {
                            VendorName = product.Vendor.VendorName
                        }
                    });
                }

                context.SaveChanges();
            }
        }

        public static void FillXmlDataToSql(Dictionary<string, List<string[]>> expensesByVendor)
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

        public static bool InsertSalesBySaleData(List<List<string>> salesData)
        {
            using (var context = new MSSQLSupermarketEntities())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var saleData in salesData)
                        {
                            var saleDate = DateTime.Parse(saleData[0]);
                            string locationName = saleData[1];
                            string productName = saleData[2];
                            int locationId = context.Locations
                                .Where(l => l.Name == locationName)
                                .Take(1)
                                .Select(l => l.Id)
                                .FirstOrDefault();
                            int productId = context.Products
                                .Where(p => p.ProductName == productName)
                                .Take(1)
                                .Select(p => p.Id)
                                .FirstOrDefault();
                            var quantity = int.Parse(saleData[3]);

                            var sale = new Sale()
                            {
                                SoldOn = saleDate,
                                LocationID = locationId,
                                ProductID = productId,
                                Quantity = quantity
                            };
                            context.Sales.Add(sale);
                        }

                        context.SaveChanges();
                        dbContextTransaction.Commit();
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                        return false;
                    }
                }
            }

            return true;
        }
    }
}