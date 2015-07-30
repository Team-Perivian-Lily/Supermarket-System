using System.Diagnostics;

namespace SupermarketSoft
{
    using System;
    using System.IO.Compression;
    using System.Windows.Forms;
    using MSSQL.Data;
    using MySQL.DataSupermarket;
    using Oracle.Data;
    using Utilities;

    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void ExportFromMSSQL_Click(object sender, EventArgs e)
        {
            //// Test MySQL
            //MySQLRepository.Test();

            // //Test SQLite
            //SQLiteRepository.Test();

            //Test Excel


            //var ctx = new MySQLEntities();

            //ctx.Locations.Add(new Location()
            //{
            //    Name = "Tuk"
            //});
            //ctx.SaveChanges();
            //var products = MSSQLRepository.GetProducts();

            //MySQLRepository.AddSqlProductsToMsSql(products);

            //var productsForSQLServer = OracleRepository.GetProductData();
            //MSSQLRepository.FillOracleDataToMsSql(productsForSQLServer);


            // Working MSSQL to MySQL


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

        private void ImportSalesFromXls_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var filePath = openFileDialog.FileName;

                using (var zip = ZipFile.Open(filePath, ZipArchiveMode.Read))
                {
                    bool operationNotCompleted = false;
                    var sales = ExcelUtility.ReadSaleData(zip);

                    if (MSSQLRepository.InsertSalesBySaleData(sales) == false)
                    {
                        operationNotCompleted = true;
                    }

                    MessageBox.Show(
                        operationNotCompleted ?
                        "Operation Error, no data was inserted" :
                        "Operation Completed");
                }
            }
        }

        private void ImportXmlToSql_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var filePath = openFileDialog.FileName;
                var vendorExpensesData = XmlUtility.ReadXmlReport(filePath);
                MSSQLRepository.FillXmlDataToSql(vendorExpensesData);
            }
        }

        private void ReplicateOracle_Click(object sender, EventArgs e)
        {
            var productData = OracleRepository.GetProductData();
            var emptyVendorsData = OracleRepository.GetEmptyVendorData();
            MSSQLRepository.FillOracleDataToMsSql(productData, emptyVendorsData);

        }

        private void GenerateMySqlDb_Click(object sender, EventArgs e)
        {
            MySQLRepository.GenerateMySqlDb();
            MessageBox.Show("MySQL db generated successffully", "Confirm", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void ExportMsSqlToMySql_Click(object sender, EventArgs e)
        {
            var productsData = MSSQLRepository.GetProductsData();
            var emptyVendorsData = MSSQLRepository.GetEmptyVendorData();
            MySQLRepository.AddSqlProductsToMsSql(productsData, emptyVendorsData);
            MessageBox.Show("MySQL data seeded from MS SQL Server", "Confirm", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void ExportExcelReport_Click(object sender, EventArgs e)
        {
            ExcelUtility.GenerateFile();
            
            
        }
    }
}