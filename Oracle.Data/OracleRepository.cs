namespace Oracle.Data
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using ClassLibrary1;

    public static class OracleRepository
    {
        public static List<ProductDTO> ReplicateOracleToMSSQL()
        {
            using (var orcContex = new OracleDbContext())
            {
                // Take data

                return orcContex.PRODUCTS
                    .Include(p => p.Measure)
                    .Include(p => p.Vendor)
                    .ToList();
            }
        }
    }
}