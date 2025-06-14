namespace RegistrationForm1
{
    partial class FrmDangKyUser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDangKyUser));
            this.cmbPosition = new System.Windows.Forms.ComboBox();
            this.dtpBirth = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSignup = new System.Windows.Forms.Button();
            this.txtconPassword = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPhoneNumber = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.ahdPictureBox1 = new Ahd.Winforms.Controls.AhdPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ahdPictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbPosition
            // 
            this.cmbPosition.FormattingEnabled = true;
            this.cmbPosition.Items.AddRange(new object[] {
            "Quản trị viên ",
            "Nhân viên ",
            "Vận hành bảo trì"});
            this.cmbPosition.Location = new System.Drawing.Point(123, 213);
            this.cmbPosition.Name = "cmbPosition";
            this.cmbPosition.Size = new System.Drawing.Size(344, 23);
            this.cmbPosition.TabIndex = 25;
            // 
            // dtpBirth
            // 
            this.dtpBirth.Location = new System.Drawing.Point(123, 156);
            this.dtpBirth.Name = "dtpBirth";
            this.dtpBirth.Size = new System.Drawing.Size(344, 22);
            this.dtpBirth.TabIndex = 24;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(123, 188);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 22);
            this.label6.TabIndex = 23;
            this.label6.Text = "Chức vụ:";
            // 
            // btnSignup
            // 
            this.btnSignup.Location = new System.Drawing.Point(241, 446);
            this.btnSignup.Name = "btnSignup";
            this.btnSignup.Size = new System.Drawing.Size(121, 43);
            this.btnSignup.TabIndex = 22;
            this.btnSignup.Text = "Đăng ký";
            this.btnSignup.UseVisualStyleBackColor = true;
            this.btnSignup.Click += new System.EventHandler(this.btnSignup_Click_1);
            // 
            // txtconPassword
            // 
            this.txtconPassword.Location = new System.Drawing.Point(123, 405);
            this.txtconPassword.Name = "txtconPassword";
            this.txtconPassword.PasswordChar = '*';
            this.txtconPassword.Size = new System.Drawing.Size(344, 22);
            this.txtconPassword.TabIndex = 21;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(123, 333);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(344, 22);
            this.txtPassword.TabIndex = 20;
            // 
            // txtFullName
            // 
            this.txtFullName.Location = new System.Drawing.Point(123, 84);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(344, 22);
            this.txtFullName.TabIndex = 19;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(123, 371);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(139, 22);
            this.label5.TabIndex = 18;
            this.label5.Text = "Xác nhận mật khẩu:";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(123, 308);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 22);
            this.label4.TabIndex = 17;
            this.label4.Text = "Mật khẩu:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(123, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 22);
            this.label3.TabIndex = 16;
            this.label3.Text = "Ngày sinh:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(123, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 22);
            this.label2.TabIndex = 15;
            this.label2.Text = "Họ và tên:";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(173, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(265, 44);
            this.label1.TabIndex = 14;
            this.label1.Text = "Đăng Ký Tài Khoản";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtPhoneNumber
            // 
            this.txtPhoneNumber.Location = new System.Drawing.Point(123, 270);
            this.txtPhoneNumber.Name = "txtPhoneNumber";
            this.txtPhoneNumber.Size = new System.Drawing.Size(344, 22);
            this.txtPhoneNumber.TabIndex = 27;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(123, 245);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 22);
            this.label7.TabIndex = 26;
            this.label7.Text = "Số điện thoại:";
            // 
            // ahdPictureBox1
            // 
            this.ahdPictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.ahdPictureBox1.FillMode = Ahd.Winforms.Controls.ImageFillMode.Original;
            this.ahdPictureBox1.FlipMode = Ahd.Winforms.Controls.ImageFlipMode.None;
            this.ahdPictureBox1.Image = global::RegistrationForm1.Properties.Resources.logo_removebg_preview;
            this.ahdPictureBox1.Location = new System.Drawing.Point(12, 4);
            this.ahdPictureBox1.Name = "ahdPictureBox1";
            this.ahdPictureBox1.RotateAngle = 0;
            this.ahdPictureBox1.ShadedColor = System.Drawing.Color.Gray;
            this.ahdPictureBox1.Size = new System.Drawing.Size(100, 100);
            this.ahdPictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Normal;
            this.ahdPictureBox1.TabIndex = 28;
            this.ahdPictureBox1.TagPath = null;
            this.ahdPictureBox1.Text = "ahdPictureBox1";
            // 
            // FrmDangKyUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Honeydew;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(564, 511);
            this.Controls.Add(this.ahdPictureBox1);
            this.Controls.Add(this.txtPhoneNumber);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cmbPosition);
            this.Controls.Add(this.dtpBirth);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnSignup);
            this.Controls.Add(this.txtconPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtFullName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmDangKyUser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng Ký Tài Khoản";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ahdPictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbPosition;
        private System.Windows.Forms.DateTimePicker dtpBirth;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSignup;
        private System.Windows.Forms.TextBox txtconPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPhoneNumber;
        private System.Windows.Forms.Label label7;
        private Ahd.Winforms.Controls.AhdPictureBox ahdPictureBox1;
    }
}

