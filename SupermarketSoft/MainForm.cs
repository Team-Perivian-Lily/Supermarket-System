namespace SupermarketSoft
{
    using System;
    using System.IO.Compression;
    using System.Windows.Forms;
    using MySQL.DataSupermarket;
    using MSSQL.Data;
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

            // //Test Excel
            //ExcelUtilities.GenerateFile();

            //var ctx = new MySQLEntities();

            //ctx.Locations.Add(new Location()
            //{
            //    Name = "Tuk"
            //});
            //ctx.SaveChanges();
            //var products = MSSQLRepository.GetProducts();

            //MySQLRepository.AddProducts(products);

            var productsForSQLServer = OracleRepository.ReplicateOracleToMSSQL();
            MSSQLRepository.FillData(productsForSQLServer);
        }

        private void ReplicateOracle_Click(object sender, EventArgs e)
        {
            OracleRepository.ReplicateOracleToMSSQL();
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

        private void ExportSalesReportToXml_Click(object sender, EventArgs e)
        {
            var exportToPdfForm = new ExportSalesReportToXml();
            exportToPdfForm.ShowDialog();
	}

        private void ImportSalesFromXls_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var filePath = openFileDialog.FileName;

                using (var zip = ZipFile.Open(filePath, ZipArchiveMode.Read))
                {
                    foreach (var entry in zip.Entries)
                    {
                        string[] salesData = entry.FullName.Split('/');

                        if (entry.FullName.EndsWith(".xls"))
                        {
                            ExcelUtilities.ReadExcelData(entry, salesData);
                        }
                    }
                }
            }
        }
    }
}