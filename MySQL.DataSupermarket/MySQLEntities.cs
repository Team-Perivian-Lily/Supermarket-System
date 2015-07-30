namespace MySQL.DataSupermarket
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using Supermarket.Models;
    using MySql.Data.Entity;

    //Uncomment the next row before migrate from SQL Server and then comment it again
    //[DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class MySQLEntities : DbContext
    {
        public MySQLEntities()
            : base("name=MySQLEntities")
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<MySQLEntities, Configuration>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vendor>().HasMany(v => v.Products);
            modelBuilder.Entity<Product>().HasRequired(p => p.Vendor);
            modelBuilder.Entity<Sale>().HasRequired(s => s.Location);
            modelBuilder.Entity<Location>().HasMany(l => l.Sales);

            base.OnModelCreating(modelBuilder);
        }

        public virtual IDbSet<Product> Products { get; set; }

        public virtual IDbSet<Sale> Sales { get; set; }

        public virtual IDbSet<Measure> Measures { get; set; }

        public virtual IDbSet<Vendor> Vendors { get; set; }

        public virtual IDbSet<Location> Locations { get; set; }

        public virtual IDbSet<Expense> Expenses { get; set; }
    }
}