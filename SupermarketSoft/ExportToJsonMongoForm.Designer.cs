namespace SupermarketSoft
{
    partial class ExportToJsonMongoForm
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
            this.startDatePicker = new System.Windows.Forms.DateTimePicker();
            this.endDatePicker = new System.Windows.Forms.DateTimePicker();
            this.StartDate = new System.Windows.Forms.Label();
            this.EndDate = new System.Windows.Forms.Label();
            this.GenerateJsonMongo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // startDatePicker
            // 
            this.startDatePicker.Location = new System.Drawing.Point(74, 90);
            this.startDatePicker.Name = "startDatePicker";
            this.startDatePicker.Size = new System.Drawing.Size(181, 20);
            this.startDatePicker.TabIndex = 0;
            // 
            // endDatePicker
            // 
            this.endDatePicker.Location = new System.Drawing.Point(74, 135);
            this.endDatePicker.Name = "endDatePicker";
            this.endDatePicker.Size = new System.Drawing.Size(181, 20);
            this.endDatePicker.TabIndex = 1;
            // 
            // StartDate
            // 
            this.StartDate.AutoSize = true;
            this.StartDate.Location = new System.Drawing.Point(10, 95);
            this.StartDate.Name = "StartDate";
            this.StartDate.Size = new System.Drawing.Size(58, 13);
            this.StartDate.TabIndex = 2;
            this.StartDate.Text = "Start Date:";
            // 
            // EndDate
            // 
            this.EndDate.AutoSize = true;
            this.EndDate.Location = new System.Drawing.Point(12, 139);
            this.EndDate.Name = "EndDate";
            this.EndDate.Size = new System.Drawing.Size(55, 13);
            this.EndDate.TabIndex = 3;
            this.EndDate.Text = "End Date:";
            // 
            // GenerateJsonMongo
            // 
            this.GenerateJsonMongo.Location = new System.Drawing.Point(84, 196);
            this.GenerateJsonMongo.Name = "GenerateJsonMongo";
            this.GenerateJsonMongo.Size = new System.Drawing.Size(131, 23);
            this.GenerateJsonMongo.TabIndex = 4;
            this.GenerateJsonMongo.Text = "Generate JSON/Mongo";
            this.GenerateJsonMongo.UseVisualStyleBackColor = true;
            this.GenerateJsonMongo.Click += new System.EventHandler(this.GenerateJsonMongo_Click);
            // 
            // ExportToJsonMongoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 261);
            this.Controls.Add(this.GenerateJsonMongo);
            this.Controls.Add(this.EndDate);
            this.Controls.Add(this.StartDate);
            this.Controls.Add(this.endDatePicker);
            this.Controls.Add(this.startDatePicker);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExportToJsonMongoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Export To Json & Mongo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker startDatePicker;
        private System.Windows.Forms.DateTimePicker endDatePicker;
        private System.Windows.Forms.Label StartDate;
        private System.Windows.Forms.Label EndDate;
        private System.Windows.Forms.Button GenerateJsonMongo;
    }
}