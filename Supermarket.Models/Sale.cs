namespace Supermarket.Models
{
    using System;

    public class Sale
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int LocationID { get; set; }

        public int ProductID { get; set; }

        public int Quantity { get; set; }

        public virtual Location Location { get; set; }

        public virtual Product Product { get; set; }
    }
}