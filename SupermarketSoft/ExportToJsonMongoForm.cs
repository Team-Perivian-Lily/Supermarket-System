namespace SupermarketSoft
{
    using System;
    using System.Windows.Forms;
    using MSSQL.Data;
    using Utilities;

    public partial class ExportToJsonMongoForm : Form
    {
        public ExportToJsonMongoForm()
        {
            InitializeComponent();
        }

        private void GenerateJsonMongo_Click(object sender, EventArgs e)
        {
            try
            {
                var salesReports = MSSQLRepository.GetSalesByProduct(
                    DateTime.Parse(this.startDatePicker.Text),
                    DateTime.Parse(this.endDatePicker.Text));

                JsonUtility.CreateJsonFiles(salesReports);

                foreach (var salesReport in salesReports)
                {
                    MongoDB.Data.MongoDBRepository.ImportSalesByProductReport(
                        JsonUtility.CreateJsonReport(salesReport));
                }

                MessageBox.Show("Data exported successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error exporting data: " + ex.Message);
            }
        }
    }
}