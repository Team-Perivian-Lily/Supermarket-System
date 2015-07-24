using System.Reflection;
using ClassLibrary1;

namespace Oracle.Data
{
    using System.Data.Entity;
    using Migrations;
    //using Supermarket.Models;

    public class OracleDbContext : DbContext
    {
        public OracleDbContext()
            : base("name=OracleDbContext")
        {
            var migrationStrategy = new MigrateDatabaseToLatestVersion<OracleDbContext, Configuration>();
            Database.SetInitializer(migrationStrategy);
            Database.Initialize(false);
        }

        public virtual IDbSet<Vendor> VENDORS { get; set; }
        public virtual IDbSet<Product> PRODUCTS { get; set; }
        public virtual IDbSet<Measure> MEASURES { get; set; }
     

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("TEST");
            base.OnModelCreating(modelBuilder);
        }
    }
}