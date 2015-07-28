using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    using System.Collections.Generic;

    public class Measure
    {
        public Measure()
        {
            this.Products = new HashSet<Product>();
        }

        [Key]
        public int Id { get; set; }

        public string MeasureName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}