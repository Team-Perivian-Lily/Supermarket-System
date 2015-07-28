using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    using System.Collections.Generic;

    public class Product
    {
        public Product()
        {
            this.Sales = new HashSet<Sale>();
        }

        [Key]
        public int Id { get; set; }

        public int VendorID { get; set; }

        public string ProductName { get; set; }

        public int MeasureID { get; set; }

        public double Price { get; set; }

        public virtual Measure Measure { get; set; }

        public virtual Vendor Vendor { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }
    }
}