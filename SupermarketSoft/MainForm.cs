namespace SupermarketSoft
{
    using System;
    using System.IO.Compression;
    using System.Windows.Forms;
    using MSSQL.Data;
    using MySQL.Data;
    using Oracle.Data;
    using Utilities;

    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void ReplicateOracle_Click(object sender, EventArgs e)
        {
            try
            {
                var productData = OracleRepository.GetOracleProductsData();
                MSSQLRepository.FillOracleDataToMsSql(productData);

                MessageBox.Show("Oracle data replicated complete!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Oracle data replicated failed: " + ex.Message);
            }
        }

        private void ImportSalesFromXls_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var filePath = openFileDialog.FileName;
                using (var zip = ZipFile.Open(filePath, ZipArchiveMode.Read))
                {
                    try
                    {
                        var sales = ExcelUtility.ReadSalesReportData(zip);
                        MSSQLRepository.FillSalesDataToSql(sales);

                        MessageBox.Show("Successfully added sales reports.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Precedure failed: " + ex.Message);
                    }
                }
            }
        }

        private void ImportXmlToSql_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var filePath = openFileDialog.FileName;
                    var vendorExpensesData = XmlUtility.ReadXmlReport(filePath);
                    MSSQLRepository.FillExpensesDataToSql(vendorExpensesData);

                    MessageBox.Show("Xml report imported successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed while importing xml report: " + ex.Message);
                }
            }
        }

        private void ExportSalesReportToPdf_Click(object sender, EventArgs e)
        {
            var exportToPdfForm = new ExportSalesReportToPdf();
            exportToPdfForm.ShowDialog();
        }

        private void ExportSalesReportToXml_Click(object sender, EventArgs e)
        {
            var xmlExportForm = new ExportSalesReportToXml();
            xmlExportForm.ShowDialog();
        }

        private void ExportToJsonMongoDb_Click(object sender, EventArgs e)
        {
            var jsonMongoForm = new ExportToJsonMongoForm();
            jsonMongoForm.ShowDialog();
        }

        private void GenerateMySqlDb_Click(object sender, EventArgs e)
        {
            try
            {
                MySQLRepository.GenerateMySqlDb();
                MessageBox.Show("MySQL db generated successffully");
            }
            catch (Exception dbe)
            {
                MessageBox.Show("MySQL db generation failed: " + dbe);
            }
        }

        private void ExportMsSqlToMySql_Click(object sender, EventArgs e)
        {
            try
            {
                var productsData = MSSQLRepository.GetFullProductsData();
                MySQLRepository.AddSqlProductsToMySql(productsData);

                MessageBox.Show("MySQL data seeded from MSSQL Server!");
            }
            catch (Exception dbe)
            {
                MessageBox.Show("MySQL db seed failed: " + dbe);
            }
        }

        private void ExportExcelReport_Click(object sender, EventArgs e)
        {
            try
            {
                ExcelUtility.GenerateExcelReportFile();

                MessageBox.Show("Excel report exported!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error exporting report: " + ex.Message);
            }
        }
    }
}