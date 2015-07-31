namespace Oracle.Data
{
    using System.Data.Entity;
    using Migrations;
    using Models;

    public class OracleDbContext : DbContext
    {
        public OracleDbContext()
            : base("name=OracleDbContext")
        {
            var migrationStrategy = new MigrateDatabaseToLatestVersion<OracleDbContext, Configuration>();
            //var migrationStrategy = new DropCreateDatabaseAlways<OracleDbContext>();

            Database.SetInitializer(migrationStrategy);
            Database.Initialize(false);
        }

        public virtual IDbSet<VendorDTO> VENDORS { get; set; }

        public virtual IDbSet<ProductDTO> PRODUCTS { get; set; }

        public virtual IDbSet<MeasureDTO> MEASURES { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("TEST");
            base.OnModelCreating(modelBuilder);
        }
    }
}