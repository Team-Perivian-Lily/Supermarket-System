namespace Oracle.Data
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using Models;

    public static class OracleRepository
    {
        public static List<ProductDTO> GetProductData()
        {
            using (var orcContex = new OracleDbContext())
            {
                return orcContex.PRODUCTS
                    .Include(p => p.Vendor)
                    .Include(p => p.Measure)
                    .ToList();
            }
        }

        public static List<VendorDTO> GetEmptyVendorData()
        {
            using (var orcContex = new OracleDbContext())
            {
                return orcContex.VENDORS
                    .Where(v => v.Products.Count == 0)
                    .ToList();
            }
        }
    }
}