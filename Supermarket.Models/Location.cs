﻿namespace Supermarket.Models
{
    using System.Collections.Generic;

    public class Location
    {
        public Location()
        {
            Sales = new HashSet<Sale>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
    }
}