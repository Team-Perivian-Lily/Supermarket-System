﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Models
{
    public class Product
    {
        public int Id { get; set; }
        public int VendorID { get; set; }
        public string ProductName { get; set; }
        public int MeasureID { get; set; }
        public double Price { get; set; }
    }
}
