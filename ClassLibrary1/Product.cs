using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
     [Table("PRODUCTS")]
    public class Product
    {
        

        public int Id { get; set; }

        public int VendorID { get; set; }

        public string ProductName { get; set; }

        public int MeasureID { get; set; }

        public double Price { get; set; }

        public virtual Measure Measure { get; set; }

        public virtual Vendor Vendor { get; set; }

        
    }
}
