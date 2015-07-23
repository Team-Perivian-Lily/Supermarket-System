using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Supermarket.Models;
using Supermarket.Models.Reports;

namespace MySQL.DataSupermarket
{
    public static class MySQLRepository
    {
        public static void AddProducts(List<SalesReport> list)
        {
            var ctx = new MySQLEntities();

            //var p = list.Where(p => p.Product.ProductName != ctx.Locations);

            foreach (var salesReport in list)
            {
                ctx.Locations.Add(new Location()
                {
                    Name = salesReport.Product.ProductName
                });
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
