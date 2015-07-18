using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SupermarketSoft
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void ExportFromMSSQL_Click(object sender, EventArgs e)
        {
            var str = MSSQL.Data.MSSQLRepository.GetProductNames();
            MessageBox.Show(str);
        }

        private void ReplicateOracle_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Oracle.Data.OracleRepository.ReplicateOracleToMSSQL());
        }

        private void ExportToJsonMongoDb_Click(object sender, EventArgs e)
        {
            var jsonMongoForm = new ExportToJsonMongoForm();
            jsonMongoForm.ShowDialog();
        }
    }
}
