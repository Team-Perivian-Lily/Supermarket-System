namespace SQLLite.Data
{
    using System.Collections.Generic;
    using System.Linq;

    public static class SQLLiteRepository
    {
        public static Dictionary<string, double?> GetProductTaxData()
        {
            using (var context = new SQLiteEntities())
            {
                return context.Taxes.ToList().ToDictionary(tax => tax.ProductName, tax => tax.Tax1);
            }
        }
    }
}
