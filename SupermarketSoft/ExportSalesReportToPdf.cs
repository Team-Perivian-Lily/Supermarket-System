namespace SupermarketSoft
{
    using System;
    using System.Windows.Forms;
    using MSSQL.Data;
    using Utilities;

    public partial class ExportSalesReportToPdf : Form
    {
        public ExportSalesReportToPdf()
        {
            InitializeComponent();
        }

        private void GenerateSalesPdf_Click(object sender, EventArgs e)
        {
            var msSqlRepo = new MSSQLRepository();
            try
            {
                PdfUtility.CreatePdfFile(msSqlRepo.GetSalesByDate(
                    DateTime.Parse(this.startDatePicker.Text),
                    DateTime.Parse(this.endDatePicker.Text)));

                MessageBox.Show("Successfully exported data!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error exporting data: " + ex.Message);
            }
        }
    }
}