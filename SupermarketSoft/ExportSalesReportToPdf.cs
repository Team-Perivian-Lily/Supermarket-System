namespace SupermarketSoft
{
    using System;
    using System.Windows.Forms;
    using MSSQL.Data;

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
                PdfUtilities pdfUtils = new PdfUtilities();
                pdfUtils.CreateReportFile();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
