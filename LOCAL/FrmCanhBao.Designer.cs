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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAlarm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewAlarm
            // 
            this.dataGridViewAlarm.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAlarm.Location = new System.Drawing.Point(4, 14);
            this.dataGridViewAlarm.Margin = new System.Windows.Forms.Padding(5);
            this.dataGridViewAlarm.Name = "dataGridViewAlarm";
            this.dataGridViewAlarm.RowHeadersWidth = 82;
            this.dataGridViewAlarm.Size = new System.Drawing.Size(574, 177);
            this.dataGridViewAlarm.TabIndex = 0;
            // 
            // DgvHistory
            // 
            this.DgvHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvHistory.Location = new System.Drawing.Point(4, 213);
            this.DgvHistory.Margin = new System.Windows.Forms.Padding(5);
            this.DgvHistory.Name = "DgvHistory";
            this.DgvHistory.Size = new System.Drawing.Size(1702, 621);
            this.DgvHistory.TabIndex = 2;
            // 
            // bntLoad
            // 
            this.bntLoad.Location = new System.Drawing.Point(904, 113);
            this.bntLoad.Margin = new System.Windows.Forms.Padding(5);
            this.bntLoad.Name = "bntLoad";
            this.bntLoad.Size = new System.Drawing.Size(125, 37);
            this.bntLoad.TabIndex = 1;
            this.bntLoad.Text = "Load Data";
            this.bntLoad.UseVisualStyleBackColor = true;
            this.bntLoad.Click += new System.EventHandler(this.bntLoad_Click);
            // 
            // FrmCanhBao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1710, 848);
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewAlarm;
        private System.Windows.Forms.DataGridView DgvHistory;
        private System.Windows.Forms.Button bntLoad;
    }
}