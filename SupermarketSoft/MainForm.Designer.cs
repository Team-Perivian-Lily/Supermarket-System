namespace SupermarketSoft
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ExportFromMSSQL = new System.Windows.Forms.Button();
            this.programName = new System.Windows.Forms.Label();
            this.ExportToXml = new System.Windows.Forms.Button();
            this.ImportSalesFromXls = new System.Windows.Forms.Button();
            this.ReplicateOracle = new System.Windows.Forms.Button();
            this.ExportSalesReportToPdf = new System.Windows.Forms.Button();
            this.ExportToJsonMongoDb = new System.Windows.Forms.Button();
            this.ImportXmlToSql = new System.Windows.Forms.Button();
            this.GenerateMySqlDb = new System.Windows.Forms.Button();
            this.ExportMsSqlToMySql = new System.Windows.Forms.Button();
            this.ExportExcelReport = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ExportFromMSSQL
            // 
            this.ExportFromMSSQL.Location = new System.Drawing.Point(231, 413);
            this.ExportFromMSSQL.Name = "ExportFromMSSQL";
            this.ExportFromMSSQL.Size = new System.Drawing.Size(87, 28);
            this.ExportFromMSSQL.TabIndex = 0;
            this.ExportFromMSSQL.Text = "Test";
            this.ExportFromMSSQL.UseVisualStyleBackColor = true;
            this.ExportFromMSSQL.Click += new System.EventHandler(this.ExportFromMSSQL_Click);
            // 
            // programName
            // 
            this.programName.AutoSize = true;
            this.programName.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.programName.Location = new System.Drawing.Point(118, 31);
            this.programName.Name = "programName";
            this.programName.Size = new System.Drawing.Size(304, 39);
            this.programName.TabIndex = 1;
            this.programName.Text = "Supermarket Soft";
            // 
            // ExportToXml
            // 
            this.ExportToXml.Location = new System.Drawing.Point(156, 229);
            this.ExportToXml.Name = "ExportToXml";
            this.ExportToXml.Size = new System.Drawing.Size(243, 28);
            this.ExportToXml.TabIndex = 2;
            this.ExportToXml.Text = "5. Export MsSql Expenses-by-Vendor to Xml";
            this.ExportToXml.UseVisualStyleBackColor = true;
            this.ExportToXml.Click += new System.EventHandler(this.ExportSalesReportToXml_Click);
            // 
            // ImportSalesFromXls
            // 
            this.ImportSalesFromXls.Location = new System.Drawing.Point(156, 127);
            this.ImportSalesFromXls.Name = "ImportSalesFromXls";
            this.ImportSalesFromXls.Size = new System.Drawing.Size(243, 28);
            this.ImportSalesFromXls.TabIndex = 3;
            this.ImportSalesFromXls.Text = "2. Import Excel ZIP to MsSql";
            this.ImportSalesFromXls.UseVisualStyleBackColor = true;
            this.ImportSalesFromXls.Click += new System.EventHandler(this.ImportSalesFromXls_Click);
            // 
            // ReplicateOracle
            // 
            this.ReplicateOracle.Location = new System.Drawing.Point(156, 93);
            this.ReplicateOracle.Name = "ReplicateOracle";
            this.ReplicateOracle.Size = new System.Drawing.Size(243, 28);
            this.ReplicateOracle.TabIndex = 4;
            this.ReplicateOracle.Text = "1. Replicate Oracle Data to MsSql";
            this.ReplicateOracle.UseVisualStyleBackColor = true;
            this.ReplicateOracle.Click += new System.EventHandler(this.ReplicateOracle_Click);
            // 
            // ExportSalesReportToPdf
            // 
            this.ExportSalesReportToPdf.Location = new System.Drawing.Point(156, 195);
            this.ExportSalesReportToPdf.Name = "ExportSalesReportToPdf";
            this.ExportSalesReportToPdf.Size = new System.Drawing.Size(243, 28);
            this.ExportSalesReportToPdf.TabIndex = 5;
            this.ExportSalesReportToPdf.Text = "4. Export MsSql Sales Report to Pdf";
            this.ExportSalesReportToPdf.UseVisualStyleBackColor = true;
            this.ExportSalesReportToPdf.Click += new System.EventHandler(this.ExportSalesReportToPdf_Click);
            // 
            // ExportToJsonMongoDb
            // 
            this.ExportToJsonMongoDb.Location = new System.Drawing.Point(156, 263);
            this.ExportToJsonMongoDb.Name = "ExportToJsonMongoDb";
            this.ExportToJsonMongoDb.Size = new System.Drawing.Size(243, 28);
            this.ExportToJsonMongoDb.TabIndex = 6;
            this.ExportToJsonMongoDb.Text = "6. Export Sales Reports to JSON/MongoDB";
            this.ExportToJsonMongoDb.UseVisualStyleBackColor = true;
            this.ExportToJsonMongoDb.Click += new System.EventHandler(this.ExportToJsonMongoDb_Click);
            // 
            // ImportXmlToSql
            // 
            this.ImportXmlToSql.Location = new System.Drawing.Point(156, 161);
            this.ImportXmlToSql.Name = "ImportXmlToSql";
            this.ImportXmlToSql.Size = new System.Drawing.Size(243, 28);
            this.ImportXmlToSql.TabIndex = 7;
            this.ImportXmlToSql.Text = "3. Import Xml to MsSql";
            this.ImportXmlToSql.UseVisualStyleBackColor = true;
            this.ImportXmlToSql.Click += new System.EventHandler(this.ImportXmlToSql_Click);
            // 
            // GenerateMySqlDb
            // 
            this.GenerateMySqlDb.Location = new System.Drawing.Point(156, 297);
            this.GenerateMySqlDb.Name = "GenerateMySqlDb";
            this.GenerateMySqlDb.Size = new System.Drawing.Size(243, 28);
            this.GenerateMySqlDb.TabIndex = 8;
            this.GenerateMySqlDb.Text = "7. Generate MySQL Database";
            this.GenerateMySqlDb.UseVisualStyleBackColor = true;
            this.GenerateMySqlDb.Click += new System.EventHandler(this.GenerateMySqlDb_Click);
            // 
            // ExportMsSqlToMySql
            // 
            this.ExportMsSqlToMySql.Location = new System.Drawing.Point(156, 331);
            this.ExportMsSqlToMySql.Name = "ExportMsSqlToMySql";
            this.ExportMsSqlToMySql.Size = new System.Drawing.Size(243, 28);
            this.ExportMsSqlToMySql.TabIndex = 9;
            this.ExportMsSqlToMySql.Text = "8. Replicate MsSql Data to MySQL";
            this.ExportMsSqlToMySql.UseVisualStyleBackColor = true;
            this.ExportMsSqlToMySql.Click += new System.EventHandler(this.ExportMsSqlToMySql_Click);
            // 
            // ExportExcelReport
            // 
            this.ExportExcelReport.Location = new System.Drawing.Point(156, 365);
            this.ExportExcelReport.Name = "ExportExcelReport";
            this.ExportExcelReport.Size = new System.Drawing.Size(243, 28);
            this.ExportExcelReport.TabIndex = 10;
            this.ExportExcelReport.Text = "9. Export Excel Finance Report";
            this.ExportExcelReport.UseVisualStyleBackColor = true;
            this.ExportExcelReport.Click += new System.EventHandler(this.ExportExcelReport_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(547, 463);
            this.Controls.Add(this.ExportExcelReport);
            this.Controls.Add(this.ExportMsSqlToMySql);
            this.Controls.Add(this.GenerateMySqlDb);
            this.Controls.Add(this.ImportXmlToSql);
            this.Controls.Add(this.ExportToJsonMongoDb);
            this.Controls.Add(this.ExportSalesReportToPdf);
            this.Controls.Add(this.ReplicateOracle);
            this.Controls.Add(this.ExportToXml);
            this.Controls.Add(this.ImportSalesFromXls);
            this.Controls.Add(this.programName);
            this.Controls.Add(this.ExportFromMSSQL);
            this.Name = "MainForm";
            this.Text = "Team - Perivian-Lilly - Supermarket System";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ExportFromMSSQL;
        private System.Windows.Forms.Label programName;
        private System.Windows.Forms.Button ExportToXml;
        private System.Windows.Forms.Button ImportSalesFromXls;
        private System.Windows.Forms.Button ReplicateOracle;
        private System.Windows.Forms.Button ExportSalesReportToPdf;
        private System.Windows.Forms.Button ExportToJsonMongoDb;
        private System.Windows.Forms.Button ImportXmlToSql;
        private System.Windows.Forms.Button GenerateMySqlDb;
        private System.Windows.Forms.Button ExportMsSqlToMySql;
        private System.Windows.Forms.Button ExportExcelReport;
    }
}

