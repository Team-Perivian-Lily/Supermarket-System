namespace Supermarket.Models.Reports
{
    using Models;

    public class DateSalesReport
    {
        public Product Product { get; set; }

        public string Quantity { get; set; }

        public Location Location { get; set; }

        public double SumOfSale { get; set; }
    }
}