namespace SupermarketSoft
{
    using System;
    using System.Windows.Forms;
    using MSSQL.Data;
    using Utilities;

    public partial class ImportExpensesToMSSQL : Form
    {
        public ImportExpensesToMSSQL()
        {
            InitializeComponent();
        }

        private void ImportXmlData_Click(object sender, EventArgs e)
        {
            var vendorExpensesData = XmlUtilities.ReadXmlReport("Vendor-Expenses.xml");
            MSSQLRepository.FillXmlDataToSql(vendorExpensesData);
        }
    }
}
