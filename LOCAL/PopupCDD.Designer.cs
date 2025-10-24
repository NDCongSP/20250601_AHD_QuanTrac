namespace RegistrationForm1
{
    partial class PopupCDD
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopupCDD));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.chartMucNuocCDD = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.cbFrequency = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartMucNuocCDD)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(5, 9);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(255, 449);
            this.dataGridView1.TabIndex = 446;
            // 
            // chartMucNuocCDD
            // 
            chartArea1.Name = "ChartArea1";
            this.chartMucNuocCDD.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartMucNuocCDD.Legends.Add(legend1);
            this.chartMucNuocCDD.Location = new System.Drawing.Point(263, 99);
            this.chartMucNuocCDD.Name = "chartMucNuocCDD";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartMucNuocCDD.Series.Add(series1);
            this.chartMucNuocCDD.Size = new System.Drawing.Size(1077, 359);
            this.chartMucNuocCDD.TabIndex = 445;
            this.chartMucNuocCDD.Text = "chart1";
            // 
            // cbFrequency
            // 
            this.cbFrequency.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFrequency.FormattingEnabled = true;
            this.cbFrequency.Location = new System.Drawing.Point(697, 31);
            this.cbFrequency.Name = "cbFrequency";
            this.cbFrequency.Size = new System.Drawing.Size(194, 31);
            this.cbFrequency.TabIndex = 444;
            this.cbFrequency.SelectedIndexChanged += new System.EventHandler(this.cbFrequency_SelectedIndexChanged);
            // 
            // PopupCDD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1344, 466);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.chartMucNuocCDD);
            this.Controls.Add(this.cbFrequency);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PopupCDD";
            this.Text = "Mực nước CDD";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartMucNuocCDD)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartMucNuocCDD;
        private System.Windows.Forms.ComboBox cbFrequency;
    }
}