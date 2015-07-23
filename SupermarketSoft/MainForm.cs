using System.IO;
using MSSQL.Data;
using Supermarket.Models;
using SupermarketSoft.Utilities;

namespace SupermarketSoft
{
    using System;
    using System.Windows.Forms;
    using Oracle.Data;
    using MySQL.DataSupermarket;
    using SQLLite.Data;


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
            var products = MSSQLRepository.GetProducts();

            MySQLRepository.AddProducts(products);
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