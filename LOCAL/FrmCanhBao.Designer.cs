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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.bntAlarm = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(77, 265);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1324, 538);
            this.dataGridView1.TabIndex = 0;
            // 
            // bntAlarm
            // 
            this.bntAlarm.Location = new System.Drawing.Point(1024, 65);
            this.bntAlarm.Name = "bntAlarm";
            this.bntAlarm.Size = new System.Drawing.Size(110, 23);
            this.bntAlarm.TabIndex = 27;
            this.bntAlarm.Text = "GhiData  Cảnh Báo";
            this.bntAlarm.UseVisualStyleBackColor = true;
            // 
            // FrmCanhBao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1717, 848);
            this.Controls.Add(this.bntAlarm);
            this.Controls.Add(this.dataGridView1);
            this.Name = "FrmCanhBao";
            this.Text = "FrmCanhBao";
            this.Load += new System.EventHandler(this.FrmCanhBao_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button bntAlarm;
    }
}