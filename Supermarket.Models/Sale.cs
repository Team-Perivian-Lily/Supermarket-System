using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public System.DateTime Date { get; set; }
        public int LocationID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
    }
}
