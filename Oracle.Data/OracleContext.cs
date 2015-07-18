using Oracle.Data.Migrations;
using Supermarket.Models;

namespace Oracle.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class OracleDbContext : DbContext
    {
        // Your context has been configured to use a 'OracleContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Oracle.Data.OracleContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'OracleContext' 
        // connection string in the application configuration file.
        public OracleDbContext()
            : base("name=OracleDbContext")
        {
            var migrationStrategy = new MigrateDatabaseToLatestVersion<OracleDbContext,Configuration>();
            Database.SetInitializer(migrationStrategy);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("DJUMI1");
            base.OnModelCreating(modelBuilder);
        }

        public virtual IDbSet<Product> Products { get; set; }

       
        

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