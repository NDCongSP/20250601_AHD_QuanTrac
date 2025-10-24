namespace RegistrationForm1
{
    partial class PopupSondai
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopupSondai));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.chartMucNuoc = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.cbFrequency = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartMucNuoc)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 25);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(255, 449);
            this.dataGridView1.TabIndex = 446;
            // 
            // chartMucNuoc
            // 
            chartArea1.Name = "ChartArea1";
            this.chartMucNuoc.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartMucNuoc.Legends.Add(legend1);
            this.chartMucNuoc.Location = new System.Drawing.Point(264, 115);
            this.chartMucNuoc.Name = "chartMucNuoc";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartMucNuoc.Series.Add(series1);
            this.chartMucNuoc.Size = new System.Drawing.Size(1077, 359);
            this.chartMucNuoc.TabIndex = 445;
            this.chartMucNuoc.Text = "chart1";
            // 
            // cbFrequency
            // 
            this.cbFrequency.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFrequency.FormattingEnabled = true;
            this.cbFrequency.Location = new System.Drawing.Point(698, 47);
            this.cbFrequency.Name = "cbFrequency";
            this.cbFrequency.Size = new System.Drawing.Size(194, 31);
            this.cbFrequency.TabIndex = 444;
            // 
            // PopupSondai
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1406, 520);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.chartMucNuoc);
            this.Controls.Add(this.cbFrequency);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PopupSondai";
            this.Text = "Mực nước sơn đài";
            this.Load += new System.EventHandler(this.PopupSondai_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartMucNuoc)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartMucNuoc;
        private System.Windows.Forms.ComboBox cbFrequency;
    }
}