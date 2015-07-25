namespace SupermarketSoft
{
    partial class ImportExpensesToMSSQL
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
            this.btn_ImportXmlData = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_ImportXmlData
            // 
            this.btn_ImportXmlData.Location = new System.Drawing.Point(60, 64);
            this.btn_ImportXmlData.Name = "bt_ImportXmlData";
            this.btn_ImportXmlData.Size = new System.Drawing.Size(146, 33);
            this.btn_ImportXmlData.TabIndex = 0;
            this.btn_ImportXmlData.Text = "Import XML To MSSQL";
            this.btn_ImportXmlData.UseVisualStyleBackColor = true;
            this.btn_ImportXmlData.Click += new System.EventHandler(this.ImportXmlData_Click);
            // 
            // ImportExpensesToMSSQL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(256, 164);
            this.Controls.Add(this.btn_ImportXmlData);
            this.Name = "ImportExpensesToMSSQL";
            this.Text = "ImportExpensesToMSSQL";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_ImportXmlData;
    }
}