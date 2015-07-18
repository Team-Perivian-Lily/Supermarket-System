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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.ReplicateOracle = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.ExportToJsonMongoDb = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ExportFromMSSQL
            // 
            this.ExportFromMSSQL.Location = new System.Drawing.Point(76, 147);
            this.ExportFromMSSQL.Name = "ExportFromMSSQL";
            this.ExportFromMSSQL.Size = new System.Drawing.Size(129, 28);
            this.ExportFromMSSQL.TabIndex = 0;
            this.ExportFromMSSQL.Text = "Export From MSSQL";
            this.ExportFromMSSQL.UseVisualStyleBackColor = true;
            this.ExportFromMSSQL.Click += new System.EventHandler(this.ExportFromMSSQL_Click);
            // 
            // programName
            // 
            this.programName.AutoSize = true;
            this.programName.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.programName.Location = new System.Drawing.Point(216, 24);
            this.programName.Name = "programName";
            this.programName.Size = new System.Drawing.Size(304, 39);
            this.programName.TabIndex = 1;
            this.programName.Text = "Supermarket Soft";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(298, 212);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(129, 28);
            this.button1.TabIndex = 2;
            this.button1.Text = "MSSQL => XLS";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(298, 147);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(129, 28);
            this.button2.TabIndex = 3;
            this.button2.Text = "XLS => MSSQL";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // ReplicateOracle
            // 
            this.ReplicateOracle.Location = new System.Drawing.Point(76, 212);
            this.ReplicateOracle.Name = "ReplicateOracle";
            this.ReplicateOracle.Size = new System.Drawing.Size(129, 28);
            this.ReplicateOracle.TabIndex = 4;
            this.ReplicateOracle.Text = "Oracle => MSSQL";
            this.ReplicateOracle.UseVisualStyleBackColor = true;
            this.ReplicateOracle.Click += new System.EventHandler(this.ReplicateOracle_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(496, 212);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(172, 28);
            this.button4.TabIndex = 5;
            this.button4.Text = ".....";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // ExportToJsonMongoDb
            // 
            this.ExportToJsonMongoDb.Location = new System.Drawing.Point(496, 147);
            this.ExportToJsonMongoDb.Name = "ExportToJsonMongoDb";
            this.ExportToJsonMongoDb.Size = new System.Drawing.Size(172, 28);
            this.ExportToJsonMongoDb.TabIndex = 6;
            this.ExportToJsonMongoDb.Text = "Export to JSON/MongoDB";
            this.ExportToJsonMongoDb.UseVisualStyleBackColor = true;
            this.ExportToJsonMongoDb.Click += new System.EventHandler(this.ExportToJsonMongoDb_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(786, 383);
            this.Controls.Add(this.ExportToJsonMongoDb);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.ReplicateOracle);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.programName);
            this.Controls.Add(this.ExportFromMSSQL);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ExportFromMSSQL;
        private System.Windows.Forms.Label programName;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button ReplicateOracle;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button ExportToJsonMongoDb;
    }
}

