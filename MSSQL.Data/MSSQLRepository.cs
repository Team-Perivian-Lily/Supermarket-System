using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace MSSQL.Data
{
    using Supermarket.Models;

    public static class MSSQLRepository
    {
        public static string GetProductNames()
        {
            //for testing purposes only
            using (var entites = new MSSQLSupermarketEntities())
            {
                var productNames = "";

                foreach (var product in entites.Products)
                {
                    productNames += product.ProductName + "; ";
                }

                return productNames;
            }
        }

        public static List<SalesReport> SalesByProductReports(DateTime startDate, DateTime endDate)
        {
            using (var context = new MSSQLSupermarketEntities())
            {
                return
                    context.Products.Where(p => p.Sales.Any())
                        .Select(
                            p =>
                            new SalesReport()
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
                                            .Sum() * p.Price
                                })
                        .ToList();
            }
        }
    }
}
