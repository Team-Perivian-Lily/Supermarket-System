using MySql.Data.Entity;
using MySQL.DataSupermarket.Migrations;
using Supermarket.Models;

//using Supermarket.Models;

namespace MySQL.DataSupermarket
{
    using MySQL;

    using System;
    using System.Data.Entity;
    using System.Linq;

    //Uncomment the next row before migrate from SQL Server and then comment it again
    //[DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class MySQLEntities : DbContext
    {
        // Your context has been configured to use a 'MySQLEntities' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'MySQL.DataSupermarket.MySQLEntities' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'MySQLEntities' 
        // connection string in the application configuration file.
        public MySQLEntities()
            : base("name=MySQLEntities")
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<MySQLEntities, Configuration>());
        }

        public virtual IDbSet<Product> Products { get; set; }

        public virtual IDbSet<Sale> Sales { get; set; }

        public virtual IDbSet<Measure> Measures { get; set; }

        public virtual IDbSet<Vendor> Vendors { get; set; }

        public virtual IDbSet<Location> Locations { get; set; }

        public virtual IDbSet<Expense> Expenses  { get; set; }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}