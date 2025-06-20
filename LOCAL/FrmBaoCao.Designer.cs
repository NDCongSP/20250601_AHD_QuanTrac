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
            this.label1 = new System.Windows.Forms.Label();
            this.btnReport = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.lblToDate = new System.Windows.Forms.Label();
            this.lblFromDate = new System.Windows.Forms.Label();
            this.bntDataVanHanh = new System.Windows.Forms.Button();
            this.bntDataMucNuoc = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cbTimeRange = new System.Windows.Forms.ComboBox();
            this.bntExportExcel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(1047, 189);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 23);
            this.label1.TabIndex = 22;
            this.label1.Text = "Tần Suất:";
            // 
            // btnReport
            // 
            this.btnReport.Location = new System.Drawing.Point(1136, 349);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(115, 42);
            this.btnReport.TabIndex = 20;
            this.btnReport.Text = "Xuất Báo cáo";
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(1163, 17);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(115, 42);
            this.btnSearch.TabIndex = 19;
            this.btnSearch.Text = "Tra Cứu";
            this.btnSearch.UseVisualStyleBackColor = true;
            // 
            // dtpTo
            // 
            this.dtpTo.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTo.Location = new System.Drawing.Point(105, 85);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.ShowUpDown = true;
            this.dtpTo.Size = new System.Drawing.Size(200, 20);
            this.dtpTo.TabIndex = 16;
            // 
            // dtpFrom
            // 
            this.dtpFrom.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrom.Location = new System.Drawing.Point(105, 16);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.ShowUpDown = true;
            this.dtpFrom.Size = new System.Drawing.Size(200, 20);
            this.dtpFrom.TabIndex = 15;
            // 
            // lblToDate
            // 
            this.lblToDate.Location = new System.Drawing.Point(11, 84);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(101, 23);
            this.lblToDate.TabIndex = 14;
            this.lblToDate.Text = "Ngày kết thúc:";
            // 
            // lblFromDate
            // 
            this.lblFromDate.Location = new System.Drawing.Point(11, 16);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(101, 23);
            this.lblFromDate.TabIndex = 13;
            this.lblFromDate.Text = "Ngày bắt đầu:";
            // 
            // bntDataVanHanh
            // 
            this.bntDataVanHanh.Location = new System.Drawing.Point(796, 89);
            this.bntDataVanHanh.Name = "bntDataVanHanh";
            this.bntDataVanHanh.Size = new System.Drawing.Size(115, 42);
            this.bntDataVanHanh.TabIndex = 25;
            this.bntDataVanHanh.Text = "Dữ liệu Vận Hành";
            this.bntDataVanHanh.UseVisualStyleBackColor = true;
            this.bntDataVanHanh.Click += new System.EventHandler(this.bntDataVanHanh_Click);
            // 
            // bntDataMucNuoc
            // 
            this.bntDataMucNuoc.Location = new System.Drawing.Point(796, 16);
            this.bntDataMucNuoc.Name = "bntDataMucNuoc";
            this.bntDataMucNuoc.Size = new System.Drawing.Size(115, 42);
            this.bntDataMucNuoc.TabIndex = 24;
            this.bntDataMucNuoc.Text = "Dữ liệu Mức Nước";
            this.bntDataMucNuoc.UseVisualStyleBackColor = true;
            this.bntDataMucNuoc.Click += new System.EventHandler(this.bntDataMucNuoc_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(983, 28);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 23);
            this.button1.TabIndex = 26;
            this.button1.Text = "GhiDataMucNuoc";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(983, 79);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(110, 23);
            this.button2.TabIndex = 27;
            this.button2.Text = "GhiDataVanhHanh";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(14, 169);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(984, 632);
            this.dataGridView1.TabIndex = 28;
            // 
            // cbTimeRange
            // 
            this.cbTimeRange.FormattingEnabled = true;
            this.cbTimeRange.Location = new System.Drawing.Point(1130, 189);
            this.cbTimeRange.Name = "cbTimeRange";
            this.cbTimeRange.Size = new System.Drawing.Size(121, 21);
            this.cbTimeRange.TabIndex = 29;
            // 
            // bntExportExcel
            // 
            this.bntExportExcel.Location = new System.Drawing.Point(1130, 254);
            this.bntExportExcel.Name = "bntExportExcel";
            this.bntExportExcel.Size = new System.Drawing.Size(115, 42);
            this.bntExportExcel.TabIndex = 30;
            this.bntExportExcel.Text = "Xuất Excel";
            this.bntExportExcel.UseVisualStyleBackColor = true;
            this.bntExportExcel.Click += new System.EventHandler(this.bntExportExcel_Click);
            // 
            // FrmBaoCao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1717, 848);
            this.Controls.Add(this.bntExportExcel);
            this.Controls.Add(this.cbTimeRange);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.bntDataVanHanh);
            this.Controls.Add(this.bntDataMucNuoc);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnReport);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.dtpTo);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this.lblToDate);
            this.Controls.Add(this.lblFromDate);
            this.Name = "FrmBaoCao";
            this.Text = "FrmBaoCao";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label lblToDate;
        private System.Windows.Forms.Label lblFromDate;
        private System.Windows.Forms.Button bntDataVanHanh;
        private System.Windows.Forms.Button bntDataMucNuoc;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox cbTimeRange;
        private System.Windows.Forms.Button bntExportExcel;
    }
}