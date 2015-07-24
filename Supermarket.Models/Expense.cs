using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Models
{
    public class Expense
    {
        [Key]
        public int Id { get; set; }

        public int VendorId { get; set; }

        public virtual Vendor Vendor{get;set;}

        public DateTime DateOfExpense { get; set; }

        public decimal Value { get; set; }
    }
}
