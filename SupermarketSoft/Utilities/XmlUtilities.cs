namespace SupermarketSoft.Utilities
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using System.Xml;
    using Supermarket.Models.Reports;

    public static class XmlUtilities
    {
        public static void CreateXmlFile(List<VendorsSalesReports> dataGroups)
        {
            if (!Directory.Exists(Path.GetTempPath() + "/Xml-Report"))
            {
                Directory.CreateDirectory(Path.GetTempPath() + "/Xml-Report");
            }

            string reportFileName = Path.GetTempPath() + "/Xml-Report" + "/Xml-Vendor-Report.xml";
            Encoding reportEncoding = Encoding.GetEncoding("utf-8");

            using (var reportWriter = new XmlTextWriter(reportFileName, reportEncoding))
            {
                reportWriter.Formatting = Formatting.Indented;
                reportWriter.IndentChar = '\t';
                reportWriter.Indentation = 1;

                reportWriter.WriteStartDocument();
                reportWriter.WriteStartElement("sales");
                foreach (var group in dataGroups)
                {
                    WriteSale(reportWriter, group.Vendor.VendorName, group.VendorReports);
                }

                reportWriter.WriteEndElement();
                reportWriter.WriteEndDocument();
            }

            Process.Start(Path.GetTempPath() + "/Xml-Report/");
        }

        private static void WriteSale(XmlWriter reportWriter, string vendorName, IEnumerable<VendorSalesReport> summaries)
        {
            reportWriter.WriteStartElement("sale");
            reportWriter.WriteAttributeString("vendor", vendorName);

            foreach (var summary in summaries)
            {
                reportWriter.WriteStartElement("summary");
                reportWriter.WriteAttributeString("date", summary.DateOfSale.ToShortDateString());
                reportWriter.WriteAttributeString("total-sum", summary.SumOfSales.ToString("F"));
                reportWriter.WriteEndElement();
            }

            reportWriter.WriteEndElement();
        }
    }
}
