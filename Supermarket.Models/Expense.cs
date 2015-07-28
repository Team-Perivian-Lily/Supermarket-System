namespace Supermarket.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

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
