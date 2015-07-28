namespace Oracle.Data
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using Models;

    public static class OracleRepository
    {
        public static List<ProductDTO> ReplicateOracleToMSSQL()
        {
            using (var orcContex = new OracleDbContext())
            {
                return orcContex.PRODUCTS
                    .Include(p => p.Measure)
                    .Include(p => p.Vendor)
                    .ToList();
            }
        }
    }
}