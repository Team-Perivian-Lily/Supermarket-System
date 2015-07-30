namespace MySQL.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using MySql.Data.Entity;

    internal sealed class Configuration : DbMigrationsConfiguration<MySQLEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            SetSqlGenerator("MySql.Data.MySqlClient", new MySqlMigrationSqlGenerator());
        }

        protected override void Seed(MySQLEntities entities)
        {
        }
    }
}
