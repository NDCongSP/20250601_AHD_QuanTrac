namespace RegistrationForm1
{
    partial class FrmCanhBao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCanhBao));
            this.btnExportDeviceStatus = new System.Windows.Forms.Button();
            this.cboStation1 = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpEnd1 = new System.Windows.Forms.DateTimePicker();
            this.dtpStart1 = new System.Windows.Forms.DateTimePicker();
            this.BtnExport = new System.Windows.Forms.Button();
            this.cboStation = new System.Windows.Forms.ComboBox();
            this.bntVh = new System.Windows.Forms.Button();
            this.DgvDeviceStatus = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblToDate = new System.Windows.Forms.Label();
            this.lblFromDate = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.DgvHistory = new System.Windows.Forms.DataGridView();
            this.bntLoad = new System.Windows.Forms.Button();
            this.dataGridViewAlarm = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.DgvDeviceStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAlarm)).BeginInit();
            this.SuspendLayout();
            // 
            // btnExportDeviceStatus
            // 
            this.btnExportDeviceStatus.Location = new System.Drawing.Point(1538, 43);
            this.btnExportDeviceStatus.Margin = new System.Windows.Forms.Padding(5);
            this.btnExportDeviceStatus.Name = "btnExportDeviceStatus";
            this.btnExportDeviceStatus.Size = new System.Drawing.Size(152, 37);
            this.btnExportDeviceStatus.TabIndex = 53;
            this.btnExportDeviceStatus.Text = "Excel";
            this.btnExportDeviceStatus.UseVisualStyleBackColor = true;
            this.btnExportDeviceStatus.Click += new System.EventHandler(this.btnExportDeviceStatus_Click);
            // 
            // cboStation1
            // 
            this.cboStation1.FormattingEnabled = true;
            this.cboStation1.Location = new System.Drawing.Point(1536, 211);
            this.cboStation1.Name = "cboStation1";
            this.cboStation1.Size = new System.Drawing.Size(154, 29);
            this.cboStation1.TabIndex = 52;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(1180, 47);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(186, 29);
            this.label7.TabIndex = 51;
            this.label7.Text = "Ngày Kết Thúc:";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(838, 43);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(175, 36);
            this.label6.TabIndex = 50;
            this.label6.Text = "Ngày Bắt Đầu:";
            // 
            // dtpEnd1
            // 
            this.dtpEnd1.CalendarFont = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEnd1.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpEnd1.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEnd1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEnd1.Location = new System.Drawing.Point(1127, 109);
            this.dtpEnd1.Name = "dtpEnd1";
            this.dtpEnd1.ShowUpDown = true;
            this.dtpEnd1.Size = new System.Drawing.Size(257, 44);
            this.dtpEnd1.TabIndex = 49;
            // 
            // dtpStart1
            // 
            this.dtpStart1.CalendarFont = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpStart1.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpStart1.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpStart1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStart1.Location = new System.Drawing.Point(838, 109);
            this.dtpStart1.Name = "dtpStart1";
            this.dtpStart1.ShowUpDown = true;
            this.dtpStart1.Size = new System.Drawing.Size(253, 44);
            this.dtpStart1.TabIndex = 48;
            // 
            // BtnExport
            // 
            this.BtnExport.Location = new System.Drawing.Point(635, 293);
            this.BtnExport.Margin = new System.Windows.Forms.Padding(5);
            this.BtnExport.Name = "BtnExport";
            this.BtnExport.Size = new System.Drawing.Size(134, 37);
            this.BtnExport.TabIndex = 47;
            this.BtnExport.Text = "Excel";
            this.BtnExport.UseVisualStyleBackColor = true;
            this.BtnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // cboStation
            // 
            this.cboStation.FormattingEnabled = true;
            this.cboStation.Location = new System.Drawing.Point(635, 413);
            this.cboStation.Name = "cboStation";
            this.cboStation.Size = new System.Drawing.Size(134, 29);
            this.cboStation.TabIndex = 46;
            // 
            // bntVh
            // 
            this.bntVh.Location = new System.Drawing.Point(1538, 127);
            this.bntVh.Margin = new System.Windows.Forms.Padding(5);
            this.bntVh.Name = "bntVh";
            this.bntVh.Size = new System.Drawing.Size(152, 37);
            this.bntVh.TabIndex = 45;
            this.bntVh.Text = "Tìm Kiếm VH";
            this.bntVh.UseVisualStyleBackColor = true;
            this.bntVh.Click += new System.EventHandler(this.bntVh_Click);
            // 
            // DgvDeviceStatus
            // 
            this.DgvDeviceStatus.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.DgvDeviceStatus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvDeviceStatus.Location = new System.Drawing.Point(795, 279);
            this.DgvDeviceStatus.Margin = new System.Windows.Forms.Padding(5);
            this.DgvDeviceStatus.Name = "DgvDeviceStatus";
            this.DgvDeviceStatus.Size = new System.Drawing.Size(904, 579);
            this.DgvDeviceStatus.TabIndex = 44;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1091, 202);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(241, 42);
            this.label3.TabIndex = 43;
            this.label3.Text = "Nhật kí vận hành";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(266, 408);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(204, 42);
            this.label2.TabIndex = 42;
            this.label2.Text = "Nhật kí lỗi";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(248, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(204, 42);
            this.label1.TabIndex = 41;
            this.label1.Text = "Cảnh báo lỗi ";
            // 
            // lblToDate
            // 
            this.lblToDate.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToDate.Location = new System.Drawing.Point(329, 283);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(186, 29);
            this.lblToDate.TabIndex = 40;
            this.lblToDate.Text = "Ngày Kết Thúc:";
            // 
            // lblFromDate
            // 
            this.lblFromDate.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFromDate.Location = new System.Drawing.Point(30, 279);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(175, 36);
            this.lblFromDate.TabIndex = 39;
            this.lblFromDate.Text = "Ngày Bắt Đầu:";
            // 
            // dtpEnd
            // 
            this.dtpEnd.CalendarFont = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEnd.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpEnd.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEnd.Location = new System.Drawing.Point(325, 333);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.ShowUpDown = true;
            this.dtpEnd.Size = new System.Drawing.Size(257, 44);
            this.dtpEnd.TabIndex = 38;
            // 
            // dtpStart
            // 
            this.dtpStart.CalendarFont = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpStart.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpStart.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStart.Location = new System.Drawing.Point(36, 333);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.ShowUpDown = true;
            this.dtpStart.Size = new System.Drawing.Size(253, 44);
            this.dtpStart.TabIndex = 37;
            // 
            // DgvHistory
            // 
            this.DgvHistory.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.DgvHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvHistory.Location = new System.Drawing.Point(12, 457);
            this.DgvHistory.Margin = new System.Windows.Forms.Padding(5);
            this.DgvHistory.Name = "DgvHistory";
            this.DgvHistory.Size = new System.Drawing.Size(757, 401);
            this.DgvHistory.TabIndex = 36;
            // 
            // bntLoad
            // 
            this.bntLoad.Location = new System.Drawing.Point(635, 353);
            this.bntLoad.Margin = new System.Windows.Forms.Padding(5);
            this.bntLoad.Name = "bntLoad";
            this.bntLoad.Size = new System.Drawing.Size(134, 37);
            this.bntLoad.TabIndex = 35;
            this.bntLoad.Text = "Tìm Kiếm Lỗi";
            this.bntLoad.UseVisualStyleBackColor = true;
            this.bntLoad.Click += new System.EventHandler(this.bntLoad_Click);
            // 
            // dataGridViewAlarm
            // 
            this.dataGridViewAlarm.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridViewAlarm.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAlarm.GridColor = System.Drawing.SystemColors.ControlLight;
            this.dataGridViewAlarm.Location = new System.Drawing.Point(12, 48);
            this.dataGridViewAlarm.Margin = new System.Windows.Forms.Padding(5);
            this.dataGridViewAlarm.Name = "dataGridViewAlarm";
            this.dataGridViewAlarm.RowHeadersWidth = 82;
            this.dataGridViewAlarm.Size = new System.Drawing.Size(757, 226);
            this.dataGridViewAlarm.TabIndex = 34;
            // 
            // FrmCanhBao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1710, 848);
            this.Controls.Add(this.btnExportDeviceStatus);
            this.Controls.Add(this.cboStation1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dtpEnd1);
            this.Controls.Add(this.dtpStart1);
            this.Controls.Add(this.BtnExport);
            this.Controls.Add(this.cboStation);
            this.Controls.Add(this.bntVh);
            this.Controls.Add(this.DgvDeviceStatus);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblToDate);
            this.Controls.Add(this.lblFromDate);
            this.Controls.Add(this.dtpEnd);
            this.Controls.Add(this.dtpStart);
            this.Controls.Add(this.DgvHistory);
            this.Controls.Add(this.bntLoad);
            this.Controls.Add(this.dataGridViewAlarm);
            this.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FrmCanhBao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Cảnh Báo";
            this.Load += new System.EventHandler(this.FrmCanhBao_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgvDeviceStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAlarm)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExportDeviceStatus;
        private System.Windows.Forms.ComboBox cboStation1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpEnd1;
        private System.Windows.Forms.DateTimePicker dtpStart1;
        private System.Windows.Forms.Button BtnExport;
        private System.Windows.Forms.ComboBox cboStation;
        private System.Windows.Forms.Button bntVh;
        private System.Windows.Forms.DataGridView DgvDeviceStatus;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblToDate;
        private System.Windows.Forms.Label lblFromDate;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.DataGridView DgvHistory;
        private System.Windows.Forms.Button bntLoad;
        private System.Windows.Forms.DataGridView dataGridViewAlarm;
    }
}