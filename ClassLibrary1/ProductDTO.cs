using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
     [Table("PRODUCTS")]
    public class ProductDTO
    {
        

        public int Id { get; set; }

        public int VendorID { get; set; }

        public string ProductName { get; set; }

        public int MeasureID { get; set; }

        public double Price { get; set; }

        public virtual MeasureDTO Measure { get; set; }

        public virtual VendorDTO Vendor { get; set; }

        
    }
}
