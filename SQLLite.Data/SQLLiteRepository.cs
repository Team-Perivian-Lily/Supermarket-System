namespace SQLLite.Data
{
    using System.Collections.Generic;
    using System.Linq;

    public class SQLLiteRepository
    {
        public SQLLiteRepository()
        {
            this.SqliteContext = new SQLiteEntities();
        }

        public SQLiteEntities SqliteContext { get; set; }

        public Dictionary<string, double?> GetProductTaxData()
        {
            return this.SqliteContext.Taxes.ToList().ToDictionary(tax => tax.ProductName, tax => tax.Tax1);
        }
    }
}
