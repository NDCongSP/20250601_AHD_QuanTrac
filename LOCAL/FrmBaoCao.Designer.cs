namespace RegistrationForm1
{
    partial class FrmBaoCao
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
            this.label1 = new System.Windows.Forms.Label();
            this.dtTo = new System.Windows.Forms.DateTimePicker();
            this.dtFrom = new System.Windows.Forms.DateTimePicker();
            this.lblToDate = new System.Windows.Forms.Label();
            this.lblFromDate = new System.Windows.Forms.Label();
            this.bntDataVanHanh = new System.Windows.Forms.Button();
            this.bntDataMucNuoc = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cbTimeRange = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.cbxExportType = new System.Windows.Forms.ComboBox();
            this.bntExportExcelOrPdf = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.bntExportBieuDo = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(708, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 43);
            this.label1.TabIndex = 22;
            this.label1.Text = "Tần Suất:";
            // 
            // dtTo
            // 
            this.dtTo.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtTo.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtTo.Location = new System.Drawing.Point(367, 108);
            this.dtTo.Name = "dtTo";
            this.dtTo.ShowUpDown = true;
            this.dtTo.Size = new System.Drawing.Size(280, 44);
            this.dtTo.TabIndex = 16;
            // 
            // dtFrom
            // 
            this.dtFrom.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtFrom.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFrom.Location = new System.Drawing.Point(28, 108);
            this.dtFrom.Name = "dtFrom";
            this.dtFrom.ShowUpDown = true;
            this.dtFrom.Size = new System.Drawing.Size(280, 44);
            this.dtFrom.TabIndex = 15;
            // 
            // lblToDate
            // 
            this.lblToDate.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToDate.Location = new System.Drawing.Point(393, 47);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(228, 42);
            this.lblToDate.TabIndex = 14;
            this.lblToDate.Text = "Ngày Kết Thúc:";
            // 
            // lblFromDate
            // 
            this.lblFromDate.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFromDate.Location = new System.Drawing.Point(66, 47);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(204, 42);
            this.lblFromDate.TabIndex = 13;
            this.lblFromDate.Text = "Ngày Bắt Đầu:";
            // 
            // bntDataVanHanh
            // 
            this.bntDataVanHanh.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bntDataVanHanh.Location = new System.Drawing.Point(921, 108);
            this.bntDataVanHanh.Name = "bntDataVanHanh";
            this.bntDataVanHanh.Size = new System.Drawing.Size(180, 45);
            this.bntDataVanHanh.TabIndex = 25;
            this.bntDataVanHanh.Text = "Dữ liệu Vận Hành";
            this.bntDataVanHanh.UseVisualStyleBackColor = true;
            this.bntDataVanHanh.Click += new System.EventHandler(this.bntDataVanHanh_Click);
            // 
            // bntDataMucNuoc
            // 
            this.bntDataMucNuoc.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bntDataMucNuoc.Location = new System.Drawing.Point(921, 46);
            this.bntDataMucNuoc.Name = "bntDataMucNuoc";
            this.bntDataMucNuoc.Size = new System.Drawing.Size(180, 45);
            this.bntDataMucNuoc.TabIndex = 24;
            this.bntDataMucNuoc.Text = "Dữ liệu Mức Nước";
            this.bntDataMucNuoc.UseVisualStyleBackColor = true;
            this.bntDataMucNuoc.Click += new System.EventHandler(this.bntDataMucNuoc_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(844, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 23);
            this.button1.TabIndex = 26;
            this.button1.Text = "GhiDataMucNuoc";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(991, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(110, 23);
            this.button2.TabIndex = 27;
            this.button2.Text = "GhiDataVanhHanh";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(210, 45);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 82;
            this.dataGridView1.Size = new System.Drawing.Size(1300, 581);
            this.dataGridView1.TabIndex = 28;
            // 
            // cbTimeRange
            // 
            this.cbTimeRange.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTimeRange.FormattingEnabled = true;
            this.cbTimeRange.Location = new System.Drawing.Point(696, 108);
            this.cbTimeRange.Name = "cbTimeRange";
            this.cbTimeRange.Size = new System.Drawing.Size(164, 44);
            this.cbTimeRange.TabIndex = 29;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(2, 193);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1800, 655);
            this.tabControl1.TabIndex = 31;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.MediumAquamarine;
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1792, 629);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Dữ Liệu";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.MistyRose;
            this.tabPage2.Controls.Add(this.chart1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1792, 629);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Biểu Đồ ";
            // 
            // chart1
            // 
            this.chart1.BackColor = System.Drawing.Color.LightSalmon;
            this.chart1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            this.chart1.BackHatchStyle = System.Windows.Forms.DataVisualization.Charting.ChartHatchStyle.BackwardDiagonal;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(17, 18);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(1684, 608);
            this.chart1.TabIndex = 30;
            this.chart1.Text = "chart1";
            // 
            // cbxExportType
            // 
            this.cbxExportType.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxExportType.FormattingEnabled = true;
            this.cbxExportType.Location = new System.Drawing.Point(1372, 108);
            this.cbxExportType.Name = "cbxExportType";
            this.cbxExportType.Size = new System.Drawing.Size(121, 44);
            this.cbxExportType.TabIndex = 32;
            // 
            // bntExportExcelOrPdf
            // 
            this.bntExportExcelOrPdf.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bntExportExcelOrPdf.Location = new System.Drawing.Point(1150, 46);
            this.bntExportExcelOrPdf.Name = "bntExportExcelOrPdf";
            this.bntExportExcelOrPdf.Size = new System.Drawing.Size(140, 45);
            this.bntExportExcelOrPdf.TabIndex = 33;
            this.bntExportExcelOrPdf.Text = "Xuất Báo Cáo";
            this.bntExportExcelOrPdf.UseVisualStyleBackColor = true;
            this.bntExportExcelOrPdf.Click += new System.EventHandler(this.bntExportExcelOrPdf_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1332, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(201, 42);
            this.label2.TabIndex = 35;
            this.label2.Text = "Kiểu Dữ Liệu";
            // 
            // bntExportBieuDo
            // 
            this.bntExportBieuDo.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bntExportBieuDo.Location = new System.Drawing.Point(1150, 108);
            this.bntExportBieuDo.Name = "bntExportBieuDo";
            this.bntExportBieuDo.Size = new System.Drawing.Size(140, 45);
            this.bntExportBieuDo.TabIndex = 36;
            this.bntExportBieuDo.Text = "Xuất Biểu Đồ";
            this.bntExportBieuDo.UseVisualStyleBackColor = true;
            this.bntExportBieuDo.Click += new System.EventHandler(this.bntExportBieuDo_Click);
            // 
            // FrmBaoCao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(1710, 848);
            this.Controls.Add(this.bntExportBieuDo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bntExportExcelOrPdf);
            this.Controls.Add(this.cbxExportType);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.cbTimeRange);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.bntDataVanHanh);
            this.Controls.Add(this.bntDataMucNuoc);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtTo);
            this.Controls.Add(this.dtFrom);
            this.Controls.Add(this.lblToDate);
            this.Controls.Add(this.lblFromDate);
            this.Name = "FrmBaoCao";
            this.Text = "FrmBaoCao";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtTo;
        private System.Windows.Forms.DateTimePicker dtFrom;
        private System.Windows.Forms.Label lblToDate;
        private System.Windows.Forms.Label lblFromDate;
        private System.Windows.Forms.Button bntDataVanHanh;
        private System.Windows.Forms.Button bntDataMucNuoc;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox cbTimeRange;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ComboBox cbxExportType;
        private System.Windows.Forms.Button bntExportExcelOrPdf;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bntExportBieuDo;
    }
}