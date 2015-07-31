namespace Oracle.Data
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using Models;

    public static class OracleRepository
    {
        public static List<ProductDTO> GetOracleProductsData()
        {
            using (var orcContex = new OracleDbContext())
            {
                return orcContex.PRODUCTS
                    .Include(p => p.Vendor)
                    .Include(p => p.Measure)
                    .ToList();
            }
        }
    }
}