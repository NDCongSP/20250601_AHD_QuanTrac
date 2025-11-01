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
            this.dataGridViewAlarm = new System.Windows.Forms.DataGridView();
            this.DgvHistory = new System.Windows.Forms.DataGridView();
            this.bntLoad = new System.Windows.Forms.Button();
            this.dateTimePickerStart = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerEnd = new System.Windows.Forms.DateTimePicker();
            this.lblToDate = new System.Windows.Forms.Label();
            this.lblFromDate = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.DgvDeviceStatus = new System.Windows.Forms.DataGridView();
            this.bntVh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAlarm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvDeviceStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewAlarm
            // 
            this.dataGridViewAlarm.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridViewAlarm.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAlarm.GridColor = System.Drawing.SystemColors.ControlLight;
            this.dataGridViewAlarm.Location = new System.Drawing.Point(4, 68);
            this.dataGridViewAlarm.Margin = new System.Windows.Forms.Padding(5);
            this.dataGridViewAlarm.Name = "dataGridViewAlarm";
            this.dataGridViewAlarm.RowHeadersWidth = 82;
            this.dataGridViewAlarm.Size = new System.Drawing.Size(757, 289);
            this.dataGridViewAlarm.TabIndex = 0;
            // 
            // DgvHistory
            // 
            this.DgvHistory.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.DgvHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvHistory.Location = new System.Drawing.Point(4, 418);
            this.DgvHistory.Margin = new System.Windows.Forms.Padding(5);
            this.DgvHistory.Name = "DgvHistory";
            this.DgvHistory.Size = new System.Drawing.Size(757, 444);
            this.DgvHistory.TabIndex = 2;
            // 
            // bntLoad
            // 
            this.bntLoad.Location = new System.Drawing.Point(862, 156);
            this.bntLoad.Margin = new System.Windows.Forms.Padding(5);
            this.bntLoad.Name = "bntLoad";
            this.bntLoad.Size = new System.Drawing.Size(125, 37);
            this.bntLoad.TabIndex = 1;
            this.bntLoad.Text = "Tìm Kiếm Lỗi";
            this.bntLoad.UseVisualStyleBackColor = true;
            this.bntLoad.Click += new System.EventHandler(this.bntLoad_Click);
            // 
            // dateTimePickerStart
            // 
            this.dateTimePickerStart.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dateTimePickerStart.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePickerStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerStart.Location = new System.Drawing.Point(781, 78);
            this.dateTimePickerStart.Name = "dateTimePickerStart";
            this.dateTimePickerStart.ShowUpDown = true;
            this.dateTimePickerStart.Size = new System.Drawing.Size(280, 44);
            this.dateTimePickerStart.TabIndex = 17;
            // 
            // dateTimePickerEnd
            // 
            this.dateTimePickerEnd.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dateTimePickerEnd.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePickerEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerEnd.Location = new System.Drawing.Point(1111, 78);
            this.dateTimePickerEnd.Name = "dateTimePickerEnd";
            this.dateTimePickerEnd.ShowUpDown = true;
            this.dateTimePickerEnd.Size = new System.Drawing.Size(280, 44);
            this.dateTimePickerEnd.TabIndex = 18;
            // 
            // lblToDate
            // 
            this.lblToDate.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToDate.Location = new System.Drawing.Point(1136, 14);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(228, 42);
            this.lblToDate.TabIndex = 20;
            this.lblToDate.Text = "Ngày Kết Thúc:";
            // 
            // lblFromDate
            // 
            this.lblFromDate.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFromDate.Location = new System.Drawing.Point(809, 14);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(204, 42);
            this.lblFromDate.TabIndex = 19;
            this.lblFromDate.Text = "Ngày Bắt Đầu:";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(240, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(204, 42);
            this.label1.TabIndex = 21;
            this.label1.Text = "Cảnh báo lỗi ";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(240, 369);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(204, 42);
            this.label2.TabIndex = 22;
            this.label2.Text = "Nhật kí lỗi";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1123, 348);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(204, 42);
            this.label3.TabIndex = 23;
            this.label3.Text = "Nhật kí vận hành";
            // 
            // DgvDeviceStatus
            // 
            this.DgvDeviceStatus.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.DgvDeviceStatus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvDeviceStatus.Location = new System.Drawing.Point(833, 418);
            this.DgvDeviceStatus.Margin = new System.Windows.Forms.Padding(5);
            this.DgvDeviceStatus.Name = "DgvDeviceStatus";
            this.DgvDeviceStatus.Size = new System.Drawing.Size(875, 444);
            this.DgvDeviceStatus.TabIndex = 24;
            // 
            // bntVh
            // 
            this.bntVh.Location = new System.Drawing.Point(1129, 156);
            this.bntVh.Margin = new System.Windows.Forms.Padding(5);
            this.bntVh.Name = "bntVh";
            this.bntVh.Size = new System.Drawing.Size(125, 37);
            this.bntVh.TabIndex = 25;
            this.bntVh.Text = "Tìm Kiếm VH";
            this.bntVh.UseVisualStyleBackColor = true;
            this.bntVh.Click += new System.EventHandler(this.bntVh_Click);
            // 
            // FrmCanhBao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1710, 848);
            this.Controls.Add(this.bntVh);
            this.Controls.Add(this.DgvDeviceStatus);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblToDate);
            this.Controls.Add(this.lblFromDate);
            this.Controls.Add(this.dateTimePickerEnd);
            this.Controls.Add(this.dateTimePickerStart);
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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAlarm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvDeviceStatus)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewAlarm;
        private System.Windows.Forms.DataGridView DgvHistory;
        private System.Windows.Forms.Button bntLoad;
        private System.Windows.Forms.DateTimePicker dateTimePickerStart;
        private System.Windows.Forms.DateTimePicker dateTimePickerEnd;
        private System.Windows.Forms.Label lblToDate;
        private System.Windows.Forms.Label lblFromDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView DgvDeviceStatus;
        private System.Windows.Forms.Button bntVh;
    }
}