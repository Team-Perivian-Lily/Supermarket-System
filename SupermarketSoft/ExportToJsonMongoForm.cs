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
    using System.Diagnostics;
    using System.IO;

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
                MSSQL.Data.JsonUtilities.CreateJsonFiles(
                DateTime.Parse(this.startDatePicker.Text),
                DateTime.Parse(this.endDatePicker.Text),
                MSSQLRepository.GetProducts());

                foreach (var product in MSSQLRepository.GetProducts())
                {
                    MongoDB.Data.MongoDBRepository.ImportSalesByProductReports(JsonUtilities.CreateJsonReport(product));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
