namespace RegistrationForm1
{
    partial class FrmK5
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
            this.ahdLabel1 = new Ahd.Winforms.Controls.AhdLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ahdBar1 = new Ahd.Winforms.Controls.AhdBar();
            this.ahdBar2 = new Ahd.Winforms.Controls.AhdBar();
            this.ahdBar3 = new Ahd.Winforms.Controls.AhdBar();
            this.ahdLabel2 = new Ahd.Winforms.Controls.AhdLabel();
            this.ahdLabel3 = new Ahd.Winforms.Controls.AhdLabel();
            ((System.ComponentModel.ISupportInitialize)(this.ahdLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ahdBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ahdBar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ahdBar3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ahdLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ahdLabel3)).BeginInit();
            this.SuspendLayout();
            // 
            // ahdLabel1
            // 
            this.ahdLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ahdLabel1.DisplayMode = Ahd.Winforms.Controls.DisplayMode.Value;
            this.ahdLabel1.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ahdLabel1.ForeColor = System.Drawing.Color.Blue;
            this.ahdLabel1.Location = new System.Drawing.Point(464, 327);
            this.ahdLabel1.Name = "ahdLabel1";
            this.ahdLabel1.Size = new System.Drawing.Size(100, 26);
            this.ahdLabel1.StringFormat = null;
            this.ahdLabel1.TabIndex = 9;
            this.ahdLabel1.TagPath = "Local Station/K5_700/PLC/Do_Mo_CS1";
            this.ahdLabel1.Text = "0.0";
            this.ahdLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(1076, 327);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(198, 26);
            this.label3.TabIndex = 8;
            this.label3.Text = "Độ mở cửa 3 (Cm)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(646, 327);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(198, 26);
            this.label2.TabIndex = 7;
            this.label2.Text = "Độ mở cửa 2 (Cm)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(226, 327);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(198, 26);
            this.label1.TabIndex = 6;
            this.label1.Text = "Độ mở cửa 1 (Cm)";
            // 
            // ahdBar1
            // 
            this.ahdBar1.Direction = Ahd.Winforms.Controls.BarDirection.BottomToTop;
            this.ahdBar1.DisplayMode = Ahd.Winforms.Controls.BarDisplayMode.Value;
            this.ahdBar1.FillColor = System.Drawing.Color.Green;
            this.ahdBar1.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ahdBar1.ForeColor = System.Drawing.Color.Blue;
            this.ahdBar1.Location = new System.Drawing.Point(296, 639);
            this.ahdBar1.MaxValue = "100";
            this.ahdBar1.MinValue = "0";
            this.ahdBar1.Name = "ahdBar1";
            this.ahdBar1.Size = new System.Drawing.Size(312, 202);
            this.ahdBar1.TabIndex = 12;
            this.ahdBar1.TagPath = "Local Station/K5_700/PLC/Do_Mo_CS1";
            this.ahdBar1.ValueStringFormat = null;
            // 
            // ahdBar2
            // 
            this.ahdBar2.Direction = Ahd.Winforms.Controls.BarDirection.BottomToTop;
            this.ahdBar2.DisplayMode = Ahd.Winforms.Controls.BarDisplayMode.Value;
            this.ahdBar2.FillColor = System.Drawing.Color.Green;
            this.ahdBar2.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ahdBar2.ForeColor = System.Drawing.Color.Blue;
            this.ahdBar2.Location = new System.Drawing.Point(718, 639);
            this.ahdBar2.MaxValue = "100";
            this.ahdBar2.MinValue = "0";
            this.ahdBar2.Name = "ahdBar2";
            this.ahdBar2.Size = new System.Drawing.Size(312, 202);
            this.ahdBar2.TabIndex = 13;
            this.ahdBar2.TagPath = "Local Station/K5_700/PLC/Do_Mo_CS2";
            this.ahdBar2.ValueStringFormat = null;
            // 
            // ahdBar3
            // 
            this.ahdBar3.Direction = Ahd.Winforms.Controls.BarDirection.BottomToTop;
            this.ahdBar3.DisplayMode = Ahd.Winforms.Controls.BarDisplayMode.Value;
            this.ahdBar3.FillColor = System.Drawing.Color.Green;
            this.ahdBar3.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ahdBar3.ForeColor = System.Drawing.Color.Blue;
            this.ahdBar3.Location = new System.Drawing.Point(1135, 639);
            this.ahdBar3.MaxValue = "100";
            this.ahdBar3.MinValue = "0";
            this.ahdBar3.Name = "ahdBar3";
            this.ahdBar3.Size = new System.Drawing.Size(312, 202);
            this.ahdBar3.TabIndex = 14;
            this.ahdBar3.TagPath = "Local Station/K5_700/PLC/Do_Mo_CS3";
            this.ahdBar3.ValueStringFormat = null;
            // 
            // ahdLabel2
            // 
            this.ahdLabel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ahdLabel2.DisplayMode = Ahd.Winforms.Controls.DisplayMode.Value;
            this.ahdLabel2.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ahdLabel2.ForeColor = System.Drawing.Color.Blue;
            this.ahdLabel2.Location = new System.Drawing.Point(882, 327);
            this.ahdLabel2.Name = "ahdLabel2";
            this.ahdLabel2.Size = new System.Drawing.Size(100, 26);
            this.ahdLabel2.StringFormat = null;
            this.ahdLabel2.TabIndex = 15;
            this.ahdLabel2.TagPath = "Local Station/K5_700/PLC/Do_Mo_CS2";
            this.ahdLabel2.Text = "0.0";
            this.ahdLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ahdLabel3
            // 
            this.ahdLabel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ahdLabel3.DisplayMode = Ahd.Winforms.Controls.DisplayMode.Value;
            this.ahdLabel3.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ahdLabel3.ForeColor = System.Drawing.Color.Blue;
            this.ahdLabel3.Location = new System.Drawing.Point(1309, 327);
            this.ahdLabel3.Name = "ahdLabel3";
            this.ahdLabel3.Size = new System.Drawing.Size(100, 26);
            this.ahdLabel3.StringFormat = null;
            this.ahdLabel3.TabIndex = 16;
            this.ahdLabel3.TagPath = "Local Station/K5_700/PLC/Do_Mo_CS3";
            this.ahdLabel3.Text = "0.0";
            this.ahdLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmK5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::RegistrationForm1.Properties.Resources.K500;
            this.ClientSize = new System.Drawing.Size(1694, 848);
            this.Controls.Add(this.ahdLabel3);
            this.Controls.Add(this.ahdLabel2);
            this.Controls.Add(this.ahdBar3);
            this.Controls.Add(this.ahdBar2);
            this.Controls.Add(this.ahdBar1);
            this.Controls.Add(this.ahdLabel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FrmK5";
            this.Text = "FrmK5";
            ((System.ComponentModel.ISupportInitialize)(this.ahdLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ahdBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ahdBar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ahdBar3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ahdLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ahdLabel3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Ahd.Winforms.Controls.AhdLabel ahdLabel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private Ahd.Winforms.Controls.AhdBar ahdBar1;
        private Ahd.Winforms.Controls.AhdBar ahdBar2;
        private Ahd.Winforms.Controls.AhdBar ahdBar3;
        private Ahd.Winforms.Controls.AhdLabel ahdLabel2;
        private Ahd.Winforms.Controls.AhdLabel ahdLabel3;
    }
}