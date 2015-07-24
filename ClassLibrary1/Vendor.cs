using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
     [Table("VENDORS")]
    public class Vendor
    {
        public Vendor()
        {
            this.Products = new HashSet<Product>();
        }

        public int Id { get; set; }

        public string VendorName { get; set; }

        public virtual ICollection<Product> Products { get; set; }

    }
}
