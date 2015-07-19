using System.IO;
using Supermarket.Models;

namespace Oracle.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Oracle.Data.OracleDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Oracle.Data.OracleDbContext";
        }

        protected override void Seed(Oracle.Data.OracleDbContext context)
        {
            using (StreamReader reader = new StreamReader("../../../ProductData.txt"))
            {
                var line = reader.ReadLine();
                line = reader.ReadLine();
                while (line!=null)
                {
                    context.Products.Add(new Product()
                    {
                        ProductName = line
                    });


                    line = reader.ReadLine();
                }
            }
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
