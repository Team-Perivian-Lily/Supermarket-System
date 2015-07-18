using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.Data;
using Supermarket.Models;

namespace WorkingProjectForSUre
{
    class Program
    {
        static void Main(string[] args)
        {
           
            OracleDbContext ctx = new OracleDbContext();
            ctx.Products.Add(new Product()
            {
                Name = ""
            });
            ctx.SaveChanges();
            Console.WriteLine(ctx.Products.Count());


        }
    }
}
