namespace SupermarketSoft
{
    using System;
    using System.Windows.Forms;
    using Oracle.Data;

    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void ExportFromMSSQL_Click(object sender, EventArgs e)
        {
        }

        private void ReplicateOracle_Click(object sender, EventArgs e)
        {
            MessageBox.Show(OracleRepository.ReplicateOracleToMSSQL());
        }

        private void ExportToJsonMongoDb_Click(object sender, EventArgs e)
        {
            var jsonMongoForm = new ExportToJsonMongoForm();
            jsonMongoForm.ShowDialog();
        }

        private void ExportSalesReportToPdf_Click(object sender, EventArgs e)
        {
            var exportToPdfForm = new ExportSalesReportToPdf();
            exportToPdfForm.ShowDialog();
        }
    }
}