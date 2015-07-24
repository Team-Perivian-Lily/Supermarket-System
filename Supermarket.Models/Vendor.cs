﻿namespace Supermarket.Models
{
    using System.Collections.Generic;

    public class Vendor
    {
        public Vendor()
        {
            this.Products = new HashSet<Product>();
        }

        public int Id { get; set; }

        public string VendorName { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public virtual string DatigonabiqNaKrivoNaMaikaTi { get; set; }
    }
}