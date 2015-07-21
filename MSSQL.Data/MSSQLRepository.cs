namespace MSSQL.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class MSSQLRepository
    {
        public static List<SalesReport> SalesByProductReports(DateTime startDate, DateTime endDate)
        {
            using (var context = new MSSQLSupermarketEntities())
            {
                return
                    context.Products
                        .Where(p => p.Sales.Any())
                        .Select(
                            p =>
                                new SalesReport
                                {
                                    ProductId = p.Id,
                                    Product = p,
                                    Vendor = p.Vendor,
                                    TotalQuantitySold =
                                        p.Sales.Where(s => s.Date >= startDate && s.Date <= endDate)
                                            .Select(s => s.Quantity).DefaultIfEmpty(0)
                                            .Sum(),
                                    TotalIncomes =
                                        p.Sales.Where(s => s.Date >= startDate && s.Date <= endDate)
                                            .Select(s => s.Quantity).DefaultIfEmpty(0)
                                            .Sum()*p.Price
                                })
                        .ToList();
            }
        }
    }
}