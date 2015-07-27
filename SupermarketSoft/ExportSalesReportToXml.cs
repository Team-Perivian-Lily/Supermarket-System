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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
