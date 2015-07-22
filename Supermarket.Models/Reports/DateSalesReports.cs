namespace Supermarket.Models.Reports
{
    using System;
    using System.Collections.Generic;

    public class DateSalesReports
    {
        public DateSalesReports()
        {
            this.ProductReports = new List<DateSalesReport>();
        }

        public DateTime Key { get; set; }

        public double TotalSumOfSales { get; set; }

        public IEnumerable<DateSalesReport> ProductReports { get; set; }
    }
}