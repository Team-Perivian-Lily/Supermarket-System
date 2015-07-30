namespace MySQL.Data
{
    using System.Data.Entity;
    using Supermarket.Models;

    [DbConfigurationType(typeof(MySqlConfiguration))]
    public class MySQLEntities : DbContext
    {
        public MySQLEntities()
            : base("name=MySQLEntities")
        {
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