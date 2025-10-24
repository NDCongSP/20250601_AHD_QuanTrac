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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBaoCao));
            this.label1 = new System.Windows.Forms.Label();
            this.dtTo = new System.Windows.Forms.DateTimePicker();
            this.dtFrom = new System.Windows.Forms.DateTimePicker();
            this.lblToDate = new System.Windows.Forms.Label();
            this.lblFromDate = new System.Windows.Forms.Label();
            this.bntDataVanHanh = new System.Windows.Forms.Button();
            this.bntDataMucNuoc = new System.Windows.Forms.Button();
            this.cbFrequency = new System.Windows.Forms.ComboBox();
            this.cbxExportType = new System.Windows.Forms.ComboBox();
            this.bntExportExcelOrPdf = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.bntExportBieuDo = new System.Windows.Forms.Button();
            this.bntData = new System.Windows.Forms.Button();
            this.bntQTM = new System.Windows.Forms.Button();
            this.tabMucNuoc = new System.Windows.Forms.TabPage();
            this.Dgv = new System.Windows.Forms.DataGridView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ChartMucnuoc = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabMucNuoc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChartMucnuoc)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(574, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 43);
            this.label1.TabIndex = 22;
            this.label1.Text = "Tần suất:";
            // 
            // dtTo
            // 
            this.dtTo.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtTo.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtTo.Location = new System.Drawing.Point(295, 73);
            this.dtTo.Name = "dtTo";
            this.dtTo.ShowUpDown = true;
            this.dtTo.Size = new System.Drawing.Size(249, 44);
            this.dtTo.TabIndex = 16;
            this.dtTo.Value = new System.DateTime(2025, 7, 25, 23, 59, 0, 0);
            // 
            // dtFrom
            // 
            this.dtFrom.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtFrom.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFrom.Location = new System.Drawing.Point(12, 73);
            this.dtFrom.Name = "dtFrom";
            this.dtFrom.ShowUpDown = true;
            this.dtFrom.Size = new System.Drawing.Size(249, 44);
            this.dtFrom.TabIndex = 15;
            this.dtFrom.Value = new System.DateTime(2025, 7, 25, 0, 0, 0, 0);
            // 
            // lblToDate
            // 
            this.lblToDate.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToDate.Location = new System.Drawing.Point(295, 12);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(228, 42);
            this.lblToDate.TabIndex = 14;
            this.lblToDate.Text = "Ngày kết thúc:";
            // 
            // lblFromDate
            // 
            this.lblFromDate.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFromDate.Location = new System.Drawing.Point(12, 12);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(204, 42);
            this.lblFromDate.TabIndex = 13;
            this.lblFromDate.Text = "Ngày bắt đầu:";
            // 
            // bntDataVanHanh
            // 
            this.bntDataVanHanh.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bntDataVanHanh.Location = new System.Drawing.Point(1146, 57);
            this.bntDataVanHanh.Name = "bntDataVanHanh";
            this.bntDataVanHanh.Size = new System.Drawing.Size(180, 45);
            this.bntDataVanHanh.TabIndex = 25;
            this.bntDataVanHanh.Text = "Dữ liệu vận hành";
            this.bntDataVanHanh.UseVisualStyleBackColor = true;
            this.bntDataVanHanh.Click += new System.EventHandler(this.bntDataVanHanh_Click);
            // 
            // bntDataMucNuoc
            // 
            this.bntDataMucNuoc.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bntDataMucNuoc.Location = new System.Drawing.Point(1146, 7);
            this.bntDataMucNuoc.Name = "bntDataMucNuoc";
            this.bntDataMucNuoc.Size = new System.Drawing.Size(180, 45);
            this.bntDataMucNuoc.TabIndex = 24;
            this.bntDataMucNuoc.Text = "Dữ liệu mức nước";
            this.bntDataMucNuoc.UseVisualStyleBackColor = true;
            this.bntDataMucNuoc.Click += new System.EventHandler(this.bntDataMucNuoc_Click);
            // 
            // cbFrequency
            // 
            this.cbFrequency.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFrequency.FormattingEnabled = true;
            this.cbFrequency.Location = new System.Drawing.Point(574, 73);
            this.cbFrequency.Name = "cbFrequency";
            this.cbFrequency.Size = new System.Drawing.Size(249, 44);
            this.cbFrequency.TabIndex = 29;
            // 
            // cbxExportType
            // 
            this.cbxExportType.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxExportType.FormattingEnabled = true;
            this.cbxExportType.Location = new System.Drawing.Point(1535, 76);
            this.cbxExportType.Name = "cbxExportType";
            this.cbxExportType.Size = new System.Drawing.Size(159, 44);
            this.cbxExportType.TabIndex = 32;
            // 
            // bntExportExcelOrPdf
            // 
            this.bntExportExcelOrPdf.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bntExportExcelOrPdf.Location = new System.Drawing.Point(1368, 44);
            this.bntExportExcelOrPdf.Name = "bntExportExcelOrPdf";
            this.bntExportExcelOrPdf.Size = new System.Drawing.Size(140, 45);
            this.bntExportExcelOrPdf.TabIndex = 33;
            this.bntExportExcelOrPdf.Text = "Xuất báo cáo";
            this.bntExportExcelOrPdf.UseVisualStyleBackColor = true;
            this.bntExportExcelOrPdf.Click += new System.EventHandler(this.bntExportExcelOrPdf_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1535, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(175, 42);
            this.label2.TabIndex = 35;
            this.label2.Text = "Kiểu dữ liệu";
            // 
            // bntExportBieuDo
            // 
            this.bntExportBieuDo.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bntExportBieuDo.Location = new System.Drawing.Point(1368, 106);
            this.bntExportBieuDo.Name = "bntExportBieuDo";
            this.bntExportBieuDo.Size = new System.Drawing.Size(140, 45);
            this.bntExportBieuDo.TabIndex = 36;
            this.bntExportBieuDo.Text = "Xuất biểu đồ";
            this.bntExportBieuDo.UseVisualStyleBackColor = true;
            this.bntExportBieuDo.Click += new System.EventHandler(this.bntExportBieuDo_Click);
            // 
            // bntData
            // 
            this.bntData.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bntData.Location = new System.Drawing.Point(1146, 157);
            this.bntData.Name = "bntData";
            this.bntData.Size = new System.Drawing.Size(180, 45);
            this.bntData.TabIndex = 39;
            this.bntData.Text = "Hồ chứa";
            this.bntData.UseVisualStyleBackColor = true;
            this.bntData.Click += new System.EventHandler(this.bntData_Click);
            // 
            // bntQTM
            // 
            this.bntQTM.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bntQTM.Location = new System.Drawing.Point(1146, 107);
            this.bntQTM.Name = "bntQTM";
            this.bntQTM.Size = new System.Drawing.Size(180, 45);
            this.bntQTM.TabIndex = 40;
            this.bntQTM.Text = "Quan trắc mưa";
            this.bntQTM.UseVisualStyleBackColor = true;
            this.bntQTM.Click += new System.EventHandler(this.bntQTM_Click);
            // 
            // tabMucNuoc
            // 
            this.tabMucNuoc.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tabMucNuoc.Controls.Add(this.Dgv);
            this.tabMucNuoc.Location = new System.Drawing.Point(4, 22);
            this.tabMucNuoc.Name = "tabMucNuoc";
            this.tabMucNuoc.Padding = new System.Windows.Forms.Padding(3);
            this.tabMucNuoc.Size = new System.Drawing.Size(1642, 602);
            this.tabMucNuoc.TabIndex = 0;
            this.tabMucNuoc.Text = "Dữ liệu";
            // 
            // Dgv
            // 
            this.Dgv.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.Dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv.Location = new System.Drawing.Point(13, 76);
            this.Dgv.Name = "Dgv";
            this.Dgv.RowHeadersWidth = 82;
            this.Dgv.Size = new System.Drawing.Size(1181, 472);
            this.Dgv.TabIndex = 28;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabMucNuoc);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(1, 208);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1650, 628);
            this.tabControl1.TabIndex = 31;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ChartMucnuoc);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1642, 507);
            this.tabPage1.TabIndex = 1;
            this.tabPage1.Text = "Biểu đồ";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // ChartMucnuoc
            // 
            this.ChartMucnuoc.BackColor = System.Drawing.SystemColors.Control;
            this.ChartMucnuoc.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            chartArea1.Name = "ChartArea1";
            this.ChartMucnuoc.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.ChartMucnuoc.Legends.Add(legend1);
            this.ChartMucnuoc.Location = new System.Drawing.Point(11, 6);
            this.ChartMucnuoc.Name = "ChartMucnuoc";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.ChartMucnuoc.Series.Add(series1);
            this.ChartMucnuoc.Size = new System.Drawing.Size(1443, 495);
            this.ChartMucnuoc.TabIndex = 32;
            this.ChartMucnuoc.Text = "chart1";
            // 
            // FrmBaoCao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(1710, 848);
            this.Controls.Add(this.bntQTM);
            this.Controls.Add(this.bntData);
            this.Controls.Add(this.bntExportBieuDo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bntExportExcelOrPdf);
            this.Controls.Add(this.cbxExportType);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.cbFrequency);
            this.Controls.Add(this.bntDataVanHanh);
            this.Controls.Add(this.bntDataMucNuoc);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtTo);
            this.Controls.Add(this.dtFrom);
            this.Controls.Add(this.lblToDate);
            this.Controls.Add(this.lblFromDate);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmBaoCao";
            this.Text = "Báo cáo";
            this.tabMucNuoc.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Dgv)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ChartMucnuoc)).EndInit();
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
        private System.Windows.Forms.ComboBox cbFrequency;
        private System.Windows.Forms.ComboBox cbxExportType;
        private System.Windows.Forms.Button bntExportExcelOrPdf;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bntExportBieuDo;
        private System.Windows.Forms.Button bntData;
        private System.Windows.Forms.Button bntQTM;
        private System.Windows.Forms.TabPage tabMucNuoc;
        private System.Windows.Forms.DataGridView Dgv;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataVisualization.Charting.Chart ChartMucnuoc;
    }
}