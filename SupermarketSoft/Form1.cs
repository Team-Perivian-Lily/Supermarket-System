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
    public partial class Form1 : Form
    {
        public Form1()
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
    }
}
