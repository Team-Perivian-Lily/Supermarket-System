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
            try
            {
                XmlUtility.CreateXmlFile(MSSQLRepository.GetSalesByVendor(
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
