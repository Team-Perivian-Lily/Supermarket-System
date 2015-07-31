namespace Oracle.Data
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using Models;

    public class OracleRepository
    {
        public OracleRepository()
        {
            this.OracleContext = new OracleDbContext();
        }

        private OracleDbContext OracleContext { get; set; }

        public List<ProductDTO> GetOracleProductsData()
        {
            return this.OracleContext.PRODUCTS
                .Include(p => p.Vendor)
                .Include(p => p.Measure)
                .ToList();
        }
    }
}