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
            try
            {
                PdfUtilities.CreatePdfFile(MSSQLRepository.GetSalesByDate(
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