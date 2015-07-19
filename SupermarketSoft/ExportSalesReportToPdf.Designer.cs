namespace SupermarketSoft
{
    partial class ExportSalesReportToPdf
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
            this.GenerateSalesPdf = new System.Windows.Forms.Button();
            this.EndDate = new System.Windows.Forms.Label();
            this.StartDate = new System.Windows.Forms.Label();
            this.endDatePicker = new System.Windows.Forms.DateTimePicker();
            this.startDatePicker = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // GenerateSalesPdf
            // 
            this.GenerateSalesPdf.Location = new System.Drawing.Point(89, 173);
            this.GenerateSalesPdf.Name = "GenerateSalesPdf";
            this.GenerateSalesPdf.Size = new System.Drawing.Size(131, 23);
            this.GenerateSalesPdf.TabIndex = 9;
            this.GenerateSalesPdf.Text = "Generate Sales Pdf";
            this.GenerateSalesPdf.UseVisualStyleBackColor = true;
            this.GenerateSalesPdf.Click += new System.EventHandler(this.GenerateSalesPdf_Click);
            // 
            // EndDate
            // 
            this.EndDate.AutoSize = true;
            this.EndDate.Location = new System.Drawing.Point(27, 115);
            this.EndDate.Name = "EndDate";
            this.EndDate.Size = new System.Drawing.Size(55, 13);
            this.EndDate.TabIndex = 8;
            this.EndDate.Text = "End Date:";
            // 
            // StartDate
            // 
            this.StartDate.AutoSize = true;
            this.StartDate.Location = new System.Drawing.Point(25, 71);
            this.StartDate.Name = "StartDate";
            this.StartDate.Size = new System.Drawing.Size(58, 13);
            this.StartDate.TabIndex = 7;
            this.StartDate.Text = "Start Date:";
            // 
            // endDatePicker
            // 
            this.endDatePicker.Location = new System.Drawing.Point(89, 111);
            this.endDatePicker.Name = "endDatePicker";
            this.endDatePicker.Size = new System.Drawing.Size(181, 20);
            this.endDatePicker.TabIndex = 6;
            // 
            // startDatePicker
            // 
            this.startDatePicker.Location = new System.Drawing.Point(89, 66);
            this.startDatePicker.Name = "startDatePicker";
            this.startDatePicker.Size = new System.Drawing.Size(181, 20);
            this.startDatePicker.TabIndex = 5;
            // 
            // ExportSalesReportToPdf
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(295, 260);
            this.Controls.Add(this.GenerateSalesPdf);
            this.Controls.Add(this.EndDate);
            this.Controls.Add(this.StartDate);
            this.Controls.Add(this.endDatePicker);
            this.Controls.Add(this.startDatePicker);
            this.Name = "ExportSalesReportToPdf";
            this.Text = "ExportSalesReportToPdf";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button GenerateSalesPdf;
        private System.Windows.Forms.Label EndDate;
        private System.Windows.Forms.Label StartDate;
        private System.Windows.Forms.DateTimePicker endDatePicker;
        private System.Windows.Forms.DateTimePicker startDatePicker;
    }
}