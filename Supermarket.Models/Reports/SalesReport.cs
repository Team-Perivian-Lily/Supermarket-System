namespace Supermarket.Models.Reports
{
    using Models;

    public class SalesReport
    {
        public int ProductId { get; set; }

        public Product Product { get; set; }

        public Vendor Vendor { get; set; }

        public int TotalQuantitySold { get; set; }

        public double TotalIncomes { get; set; }
    }
}