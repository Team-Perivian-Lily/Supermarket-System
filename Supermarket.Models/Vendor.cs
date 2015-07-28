namespace Supermarket.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Vendor
    {
        public Vendor()
        {
            this.Products = new HashSet<Product>();
            this.Expenses = new HashSet<Expense>();
        }

        [Key]
        public int Id { get; set; }

        public string VendorName { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<Expense> Expenses { get; set; }
    }
}