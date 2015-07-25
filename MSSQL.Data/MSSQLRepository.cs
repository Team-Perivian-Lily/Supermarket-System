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
                            .Where(s => s.Date >= startDate && s.Date <= endDate)
                            .Select(s => s.Quantity)
                            .DefaultIfEmpty(0)
                            .Sum(),
                        TotalIncomes = p.Sales
                            .Where(s => s.Date >= startDate && s.Date <= endDate)
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
                    .Where(s => s.Date >= startDate && s.Date <= endDate)
                    .GroupBy(s => s.Date)
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
                        Key = @group.Key,
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
                    .Where(s => s.Date >= startDate && s.Date <= endDate)
                    .OrderBy(s => s.Product.Vendor.VendorName)
                    .GroupBy(s => s.Product.Vendor)
                    .Select(vg => new
                    {
                        Vendor = vg.Key,
                        Group = vg
                            .GroupBy(sale => sale.Date)
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
                        var expenseContained = !context.Expenses
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
    }
}