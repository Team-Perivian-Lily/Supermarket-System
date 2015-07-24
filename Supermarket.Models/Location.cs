using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Supermarket.Models
{
    using System.Collections.Generic;

    [NotMapped]
    public class Location
    {
        
        public Location()
        {
            Sales = new HashSet<Sale>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }
    }
}