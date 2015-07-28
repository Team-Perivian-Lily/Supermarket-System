using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Sale
    {
        [Key]
        public int Id { get; set; }

        [Column("SoldOn", TypeName="datetime")]
        public DateTime SoldOn { get; set; }

        public int LocationID { get; set; }

        public int ProductID { get; set; }

        public int Quantity { get; set; }

        public virtual Location Location { get; set; }

        public virtual Product Product { get; set; }
    }
}