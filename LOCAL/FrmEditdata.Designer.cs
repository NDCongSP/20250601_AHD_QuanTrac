namespace RegistrationForm1
{
    partial class FrmEditdata
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
            this.txtHo = new System.Windows.Forms.TextBox();
            this.txtDautieng = new System.Windows.Forms.TextBox();
            this.txtBensuc = new System.Windows.Forms.TextBox();
            this.txtSondai = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBinhnham = new System.Windows.Forms.TextBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(76, 111);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(665, 560);
            this.dataGridView1.TabIndex = 0;
            // 
            // txtHo
            // 
            this.txtHo.Location = new System.Drawing.Point(959, 171);
            this.txtHo.Name = "txtHo";
            this.txtHo.Size = new System.Drawing.Size(243, 20);
            this.txtHo.TabIndex = 1;
            // 
            // txtDautieng
            // 
            this.txtDautieng.Location = new System.Drawing.Point(959, 237);
            this.txtDautieng.Name = "txtDautieng";
            this.txtDautieng.Size = new System.Drawing.Size(243, 20);
            this.txtDautieng.TabIndex = 2;
            // 
            // txtBensuc
            // 
            this.txtBensuc.Location = new System.Drawing.Point(959, 310);
            this.txtBensuc.Name = "txtBensuc";
            this.txtBensuc.Size = new System.Drawing.Size(243, 20);
            this.txtBensuc.TabIndex = 3;
            // 
            // txtSondai
            // 
            this.txtSondai.Location = new System.Drawing.Point(959, 385);
            this.txtSondai.Name = "txtSondai";
            this.txtSondai.Size = new System.Drawing.Size(243, 20);
            this.txtSondai.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1262, 174);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Hồ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1262, 244);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Dầu tiếng";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1271, 317);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Bến súc";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1271, 392);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Sơn đài";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1280, 461);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Bình nhâm";
            // 
            // txtBinhnham
            // 
            this.txtBinhnham.Location = new System.Drawing.Point(959, 454);
            this.txtBinhnham.Name = "txtBinhnham";
            this.txtBinhnham.Size = new System.Drawing.Size(243, 20);
            this.txtBinhnham.TabIndex = 10;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(954, 594);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 11;
            this.btnUpdate.Text = "Cập Nhật";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // FrmEditdata
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1441, 739);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.txtBinhnham);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSondai);
            this.Controls.Add(this.txtBensuc);
            this.Controls.Add(this.txtDautieng);
            this.Controls.Add(this.txtHo);
            this.Controls.Add(this.dataGridView1);
            this.Name = "FrmEditdata";
            this.Text = "FrmEditdata";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtHo;
        private System.Windows.Forms.TextBox txtDautieng;
        private System.Windows.Forms.TextBox txtBensuc;
        private System.Windows.Forms.TextBox txtSondai;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBinhnham;
        private System.Windows.Forms.Button btnUpdate;
    }
}