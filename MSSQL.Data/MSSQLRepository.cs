﻿namespace MSSQL.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
    }
}