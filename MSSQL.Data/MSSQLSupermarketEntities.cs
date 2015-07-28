namespace MSSQL.Data
{
    using System.Data.Entity;
    using Migrations;
    using Supermarket.Models;

    public class MSSQLSupermarketEntities : DbContext
    {
        public MSSQLSupermarketEntities()
            : base("name=MSSQLSupermarketEntities")
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<MSSQLSupermarketEntities, Configuration>());
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vendor>().HasMany(v => v.Products);
            modelBuilder.Entity<Product>().HasRequired(p => p.Vendor);

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