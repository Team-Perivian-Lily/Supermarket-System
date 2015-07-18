using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace MSSQL.Data
{
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

        public static List<Product> GetProducts()
        {
            using (var context = new MSSQLSupermarketEntities())
            {
                return context.Products.Include(p => p.Vendor).ToList();
            }
        }
    }
}
