using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
     [Table("MEASURES")]
    public class Measure
    {
        public Measure()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }

        public string MeasureName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
