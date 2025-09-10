namespace RegistrationForm1
{
    partial class FrmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
            this.label5 = new System.Windows.Forms.Label();
            this.chkShowPassword = new System.Windows.Forms.CheckBox();
            this.bntRegister = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ahdPictureBox1 = new Ahd.Winforms.Controls.AhdPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ahdPictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Navy;
            this.label5.Location = new System.Drawing.Point(11, 9);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(460, 48);
            this.label5.TabIndex = 183;
            this.label5.Text = "CÔNG TY TNHH MTV KHAI THÁC THỦY LỢI MIỀN NAM";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkShowPassword
            // 
            this.chkShowPassword.AutoSize = true;
            this.chkShowPassword.ForeColor = System.Drawing.Color.Navy;
            this.chkShowPassword.Location = new System.Drawing.Point(159, 181);
            this.chkShowPassword.Margin = new System.Windows.Forms.Padding(4);
            this.chkShowPassword.Name = "chkShowPassword";
            this.chkShowPassword.Size = new System.Drawing.Size(175, 23);
            this.chkShowPassword.TabIndex = 181;
            this.chkShowPassword.Text = "Hiển Thị / Ẩn Mật Khẩu";
            this.chkShowPassword.UseVisualStyleBackColor = true;
            this.chkShowPassword.CheckedChanged += new System.EventHandler(this.chkShowPassword_CheckedChanged);
            // 
            // bntRegister
            // 
            this.bntRegister.Location = new System.Drawing.Point(282, 212);
            this.bntRegister.Margin = new System.Windows.Forms.Padding(4);
            this.bntRegister.Name = "bntRegister";
            this.bntRegister.Size = new System.Drawing.Size(106, 32);
            this.bntRegister.TabIndex = 180;
            this.bntRegister.Text = "Thoát";
            this.bntRegister.UseVisualStyleBackColor = true;
            this.bntRegister.Click += new System.EventHandler(this.bntRegister_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(159, 212);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(4);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(105, 31);
            this.btnLogin.TabIndex = 179;
            this.btnLogin.Text = "Đăng Nhập";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPassword.Location = new System.Drawing.Point(159, 147);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(229, 26);
            this.txtPassword.TabIndex = 178;
            this.txtPassword.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
            // 
            // txtUsername
            // 
            this.txtUsername.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUsername.Location = new System.Drawing.Point(159, 80);
            this.txtUsername.Margin = new System.Windows.Forms.Padding(4);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(229, 26);
            this.txtUsername.TabIndex = 177;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Navy;
            this.label2.Location = new System.Drawing.Point(159, 124);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 19);
            this.label2.TabIndex = 176;
            this.label2.Text = "Mật Khẩu";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(159, 57);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 19);
            this.label1.TabIndex = 175;
            this.label1.Text = "Tên Đăng Nhập";
            // 
            // ahdPictureBox1
            // 
            this.ahdPictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.ahdPictureBox1.FillMode = Ahd.Winforms.Controls.ImageFillMode.Original;
            this.ahdPictureBox1.FlipMode = Ahd.Winforms.Controls.ImageFlipMode.None;
            this.ahdPictureBox1.Image = global::RegistrationForm1.Properties.Resources.logo_removebg_preview;
            this.ahdPictureBox1.Location = new System.Drawing.Point(24, 70);
            this.ahdPictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.ahdPictureBox1.Name = "ahdPictureBox1";
            this.ahdPictureBox1.RotateAngle = 0;
            this.ahdPictureBox1.ShadedColor = System.Drawing.Color.Gray;
            this.ahdPictureBox1.Size = new System.Drawing.Size(103, 106);
            this.ahdPictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Normal;
            this.ahdPictureBox1.TabIndex = 182;
            this.ahdPictureBox1.TagPath = null;
            this.ahdPictureBox1.Text = "ahdPictureBox1";
            // 
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(473, 258);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ahdPictureBox1);
            this.Controls.Add(this.chkShowPassword);
            this.Controls.Add(this.bntRegister);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng Nhập";
            ((System.ComponentModel.ISupportInitialize)(this.ahdPictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private Ahd.Winforms.Controls.AhdPictureBox ahdPictureBox1;
        private System.Windows.Forms.CheckBox chkShowPassword;
        private System.Windows.Forms.Button bntRegister;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}