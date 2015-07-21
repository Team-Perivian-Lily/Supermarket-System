namespace Oracle.Data
{
    using System.Data.Entity;
    using Migrations;
    using Supermarket.Models;

    public class OracleDbContext : DbContext
    {
        public OracleDbContext()
            : base("name=OracleDbContext")
        {
            var migrationStrategy = new MigrateDatabaseToLatestVersion<OracleDbContext, Configuration>();
            Database.SetInitializer(migrationStrategy);
        }

        public virtual IDbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("DJUMI1");
            base.OnModelCreating(modelBuilder);
        }
    }
}