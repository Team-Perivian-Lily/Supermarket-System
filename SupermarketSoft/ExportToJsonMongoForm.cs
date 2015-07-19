using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web.Script.Serialization;

namespace SupermarketSoft
{

    using MSSQL.Data;

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
                var salesReports = MSSQLRepository.SalesByProductReports(
                    DateTime.Parse(this.startDatePicker.Text),
                    DateTime.Parse(this.endDatePicker.Text));

                JsonUtilities.CreateJsonFiles(salesReports);

                foreach (var salesReport in salesReports)
                {
                    MongoDB.Data.MongoDBRepository.ImportSalesByProductReport(JsonUtilities.CreateJsonReport(salesReport));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
