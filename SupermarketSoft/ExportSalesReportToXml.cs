namespace SupermarketSoft
{
    using System;
    using System.Windows.Forms;
    using MSSQL.Data;
    using Utilities;

    public partial class ExportSalesReportToXml : Form
    {
        public ExportSalesReportToXml()
        {
            InitializeComponent();
        }

        private void GenerateSalesXml_Click(object sender, EventArgs e)
        {
            var msSqlRepo = new MSSQLRepository();
            try
            {
                XmlUtility.CreateXmlFile(msSqlRepo.GetSalesByVendor(
                        DateTime.Parse(this.startDatePicker.Text),
                        DateTime.Parse(this.endDatePicker.Text)));

                MessageBox.Show("Data exported successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error exporting data: " + ex.Message);
            }
        }
    }
}
