using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Supermarket.Models;

namespace MySQL.DataSupermarket
{
    public static class MySQLRepository
    {
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
