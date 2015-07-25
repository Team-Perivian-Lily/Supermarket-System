namespace SupermarketSoft
{
    partial class ExportSalesReportToXml
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
            this.GenerateSalesXml = new System.Windows.Forms.Button();
            this.EndDate = new System.Windows.Forms.Label();
            this.StartDate = new System.Windows.Forms.Label();
            this.endDatePicker = new System.Windows.Forms.DateTimePicker();
            this.startDatePicker = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // GenerateSalesXml
            // 
            this.GenerateSalesXml.Location = new System.Drawing.Point(84, 172);
            this.GenerateSalesXml.Name = "GenerateSalesXml";
            this.GenerateSalesXml.Size = new System.Drawing.Size(131, 23);
            this.GenerateSalesXml.TabIndex = 14;
            this.GenerateSalesXml.Text = "Generate Sales Xml";
            this.GenerateSalesXml.UseVisualStyleBackColor = true;
            this.GenerateSalesXml.Click += new System.EventHandler(this.GenerateSalesXml_Click);
            // 
            // EndDate
            // 
            this.EndDate.AutoSize = true;
            this.EndDate.Location = new System.Drawing.Point(22, 114);
            this.EndDate.Name = "EndDate";
            this.EndDate.Size = new System.Drawing.Size(55, 13);
            this.EndDate.TabIndex = 13;
            this.EndDate.Text = "End Date:";
            // 
            // StartDate
            // 
            this.StartDate.AutoSize = true;
            this.StartDate.Location = new System.Drawing.Point(20, 70);
            this.StartDate.Name = "StartDate";
            this.StartDate.Size = new System.Drawing.Size(58, 13);
            this.StartDate.TabIndex = 12;
            this.StartDate.Text = "Start Date:";
            // 
            // endDatePicker
            // 
            this.endDatePicker.Location = new System.Drawing.Point(84, 110);
            this.endDatePicker.Name = "endDatePicker";
            this.endDatePicker.Size = new System.Drawing.Size(181, 20);
            this.endDatePicker.TabIndex = 11;
            // 
            // startDatePicker
            // 
            this.startDatePicker.Location = new System.Drawing.Point(84, 65);
            this.startDatePicker.Name = "startDatePicker";
            this.startDatePicker.Size = new System.Drawing.Size(181, 20);
            this.startDatePicker.TabIndex = 10;
            // 
            // ExportSalesReportToXml
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 260);
            this.Controls.Add(this.GenerateSalesXml);
            this.Controls.Add(this.EndDate);
            this.Controls.Add(this.StartDate);
            this.Controls.Add(this.endDatePicker);
            this.Controls.Add(this.startDatePicker);
            this.Name = "ExportSalesReportToXml";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ExportSalesReportToXml";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button GenerateSalesXml;
        private System.Windows.Forms.Label EndDate;
        private System.Windows.Forms.Label StartDate;
        private System.Windows.Forms.DateTimePicker endDatePicker;
        private System.Windows.Forms.DateTimePicker startDatePicker;
    }
}