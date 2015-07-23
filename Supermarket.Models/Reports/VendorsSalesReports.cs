namespace Supermarket.Models.Reports
{
    using System.Collections.Generic;

    public class VendorsSalesReports
    {
        public Vendor Vendor { get; set; }

        public IEnumerable<VendorSalesReport> VendorReports { get; set; } 
    }
}
