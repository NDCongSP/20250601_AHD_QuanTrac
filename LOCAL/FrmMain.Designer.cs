namespace RegistrationForm1
{
    partial class FrmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.ahdPictureBox1 = new Ahd.Winforms.Controls.AhdPictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.bnt_User = new System.Windows.Forms.Button();
            this.bnt_TrangChu = new System.Windows.Forms.Button();
            this.bnt_BaoCao = new System.Windows.Forms.Button();
            this.bnt_CaiDat = new System.Windows.Forms.Button();
            this.bnt_Exit = new System.Windows.Forms.Button();
            this.bnt_LogIn = new System.Windows.Forms.Button();
            this.bnt_TramMN = new System.Windows.Forms.Button();
            this.bnt_CanhBao = new System.Windows.Forms.Button();
            this.bnt_Tran = new System.Windows.Forms.Button();
            this.ahdDriverConnector1 = new Ahd.Winforms.Controls.AhdDriverConnector(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panelDesktop = new System.Windows.Forms.Panel();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.labDriverStatus = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.tm_login = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ahdPictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ahdDriverConnector1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(699, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(520, 43);
            this.label1.TabIndex = 1;
            this.label1.Text = "HỆ THỐNG GIẢM SÁT CỬA TRÀN HỒ DẦU TIẾNG";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.ForestGreen;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.ahdPictureBox1);
            this.panel1.Location = new System.Drawing.Point(-1, -11);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1908, 116);
            this.panel1.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.ForestGreen;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Yellow;
            this.label5.Location = new System.Drawing.Point(145, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1676, 71);
            this.label5.TabIndex = 172;
            this.label5.Text = "CÔNG TY TNHH MỘT THÀNH VIÊN KHAI THÁC THỦY LỢI MIỀN NAM";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ahdPictureBox1
            // 
            this.ahdPictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.ahdPictureBox1.FillMode = Ahd.Winforms.Controls.ImageFillMode.Original;
            this.ahdPictureBox1.FlipMode = Ahd.Winforms.Controls.ImageFlipMode.None;
            this.ahdPictureBox1.Image = global::RegistrationForm1.Properties.Resources.logo_removebg_preview;
            this.ahdPictureBox1.Location = new System.Drawing.Point(3, 12);
            this.ahdPictureBox1.Name = "ahdPictureBox1";
            this.ahdPictureBox1.RotateAngle = 0;
            this.ahdPictureBox1.ShadedColor = System.Drawing.Color.Gray;
            this.ahdPictureBox1.Size = new System.Drawing.Size(100, 100);
            this.ahdPictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Normal;
            this.ahdPictureBox1.TabIndex = 8;
            this.ahdPictureBox1.TagPath = null;
            this.ahdPictureBox1.Text = "ahdPictureBox1";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.MediumAquamarine;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.bnt_User);
            this.panel2.Controls.Add(this.bnt_TrangChu);
            this.panel2.Controls.Add(this.bnt_BaoCao);
            this.panel2.Controls.Add(this.bnt_CaiDat);
            this.panel2.Controls.Add(this.bnt_Exit);
            this.panel2.Controls.Add(this.bnt_LogIn);
            this.panel2.Controls.Add(this.bnt_TramMN);
            this.panel2.Controls.Add(this.bnt_CanhBao);
            this.panel2.Controls.Add(this.bnt_Tran);
            this.panel2.Location = new System.Drawing.Point(-1, 154);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 887);
            this.panel2.TabIndex = 6;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(-1, 710);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(196, 64);
            this.button2.TabIndex = 15;
            this.button2.Text = "Cống 2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(-1, 639);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(196, 64);
            this.button1.TabIndex = 14;
            this.button1.Text = "Cống 1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // bnt_User
            // 
            this.bnt_User.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnt_User.Location = new System.Drawing.Point(-1, 355);
            this.bnt_User.Name = "bnt_User";
            this.bnt_User.Size = new System.Drawing.Size(196, 64);
            this.bnt_User.TabIndex = 13;
            this.bnt_User.Text = "Người Dùng";
            this.bnt_User.UseVisualStyleBackColor = true;
            this.bnt_User.Click += new System.EventHandler(this.bnt_User_Click);
            // 
            // bnt_TrangChu
            // 
            this.bnt_TrangChu.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnt_TrangChu.Location = new System.Drawing.Point(-1, 0);
            this.bnt_TrangChu.Name = "bnt_TrangChu";
            this.bnt_TrangChu.Size = new System.Drawing.Size(196, 64);
            this.bnt_TrangChu.TabIndex = 12;
            this.bnt_TrangChu.Text = "Trang Chủ";
            this.bnt_TrangChu.UseVisualStyleBackColor = true;
            this.bnt_TrangChu.Click += new System.EventHandler(this.bnt_TrangChu_Click);
            // 
            // bnt_BaoCao
            // 
            this.bnt_BaoCao.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnt_BaoCao.Location = new System.Drawing.Point(-1, 284);
            this.bnt_BaoCao.Name = "bnt_BaoCao";
            this.bnt_BaoCao.Size = new System.Drawing.Size(196, 64);
            this.bnt_BaoCao.TabIndex = 11;
            this.bnt_BaoCao.Text = "Báo Cáo";
            this.bnt_BaoCao.UseVisualStyleBackColor = true;
            this.bnt_BaoCao.Click += new System.EventHandler(this.bnt_BaoCao_Click);
            // 
            // bnt_CaiDat
            // 
            this.bnt_CaiDat.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnt_CaiDat.Location = new System.Drawing.Point(-1, 426);
            this.bnt_CaiDat.Name = "bnt_CaiDat";
            this.bnt_CaiDat.Size = new System.Drawing.Size(196, 64);
            this.bnt_CaiDat.TabIndex = 10;
            this.bnt_CaiDat.Text = "Cài Đặt";
            this.bnt_CaiDat.UseVisualStyleBackColor = true;
            this.bnt_CaiDat.Click += new System.EventHandler(this.bnt_CaiDat_Click);
            // 
            // bnt_Exit
            // 
            this.bnt_Exit.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnt_Exit.Location = new System.Drawing.Point(-1, 568);
            this.bnt_Exit.Name = "bnt_Exit";
            this.bnt_Exit.Size = new System.Drawing.Size(196, 64);
            this.bnt_Exit.TabIndex = 9;
            this.bnt_Exit.Text = "Thoát";
            this.bnt_Exit.UseVisualStyleBackColor = true;
            this.bnt_Exit.Click += new System.EventHandler(this.bnt_Exit_Click);
            // 
            // bnt_LogIn
            // 
            this.bnt_LogIn.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnt_LogIn.Location = new System.Drawing.Point(-1, 497);
            this.bnt_LogIn.Name = "bnt_LogIn";
            this.bnt_LogIn.Size = new System.Drawing.Size(196, 64);
            this.bnt_LogIn.TabIndex = 8;
            this.bnt_LogIn.Text = "Đăng Nhập";
            this.bnt_LogIn.UseVisualStyleBackColor = true;
            this.bnt_LogIn.Click += new System.EventHandler(this.bnt_LogIn_Click);
            // 
            // bnt_TramMN
            // 
            this.bnt_TramMN.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnt_TramMN.Location = new System.Drawing.Point(-1, 142);
            this.bnt_TramMN.Name = "bnt_TramMN";
            this.bnt_TramMN.Size = new System.Drawing.Size(196, 64);
            this.bnt_TramMN.TabIndex = 7;
            this.bnt_TramMN.Text = "Mức Nước";
            this.bnt_TramMN.UseVisualStyleBackColor = true;
            this.bnt_TramMN.Click += new System.EventHandler(this.bnt_TramMN_Click);
            // 
            // bnt_CanhBao
            // 
            this.bnt_CanhBao.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnt_CanhBao.Location = new System.Drawing.Point(-1, 213);
            this.bnt_CanhBao.Name = "bnt_CanhBao";
            this.bnt_CanhBao.Size = new System.Drawing.Size(196, 64);
            this.bnt_CanhBao.TabIndex = 6;
            this.bnt_CanhBao.Text = "Cảnh Báo";
            this.bnt_CanhBao.UseVisualStyleBackColor = true;
            this.bnt_CanhBao.Click += new System.EventHandler(this.bnt_CanhBao_Click);
            // 
            // bnt_Tran
            // 
            this.bnt_Tran.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnt_Tran.Location = new System.Drawing.Point(-1, 71);
            this.bnt_Tran.Name = "bnt_Tran";
            this.bnt_Tran.Size = new System.Drawing.Size(196, 64);
            this.bnt_Tran.TabIndex = 0;
            this.bnt_Tran.Text = "Tràn";
            this.bnt_Tran.UseVisualStyleBackColor = true;
            this.bnt_Tran.Click += new System.EventHandler(this.bnt_Tran_Click);
            // 
            // ahdDriverConnector1
            // 
            this.ahdDriverConnector1.CollectionName = null;
            this.ahdDriverConnector1.CommunicationMode = Ahd.Core.CommunicationMode.ReceiveFromServer;
            this.ahdDriverConnector1.DatabaseName = null;
            this.ahdDriverConnector1.MongoDb_ConnectionString = null;
            this.ahdDriverConnector1.Port = ((ushort)(8800));
            this.ahdDriverConnector1.RefreshRate = 1000;
            this.ahdDriverConnector1.ServerAddress = "127.0.0.1";
            this.ahdDriverConnector1.StationName = null;
            this.ahdDriverConnector1.Timeout = 30;
            this.ahdDriverConnector1.UseMongoDb = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panelDesktop
            // 
            this.panelDesktop.BackColor = System.Drawing.Color.Honeydew;
            this.panelDesktop.BackgroundImage = global::RegistrationForm1.Properties.Resources.M2;
            this.panelDesktop.Location = new System.Drawing.Point(192, 154);
            this.panelDesktop.Name = "panelDesktop";
            this.panelDesktop.Size = new System.Drawing.Size(1726, 887);
            this.panelDesktop.TabIndex = 7;
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcome.Location = new System.Drawing.Point(1575, 108);
            this.lblWelcome.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(66, 15);
            this.lblWelcome.TabIndex = 267;
            this.lblWelcome.Text = "UserDislay";
            // 
            // labDriverStatus
            // 
            this.labDriverStatus.AutoSize = true;
            this.labDriverStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.labDriverStatus.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labDriverStatus.Location = new System.Drawing.Point(1759, 132);
            this.labDriverStatus.Name = "labDriverStatus";
            this.labDriverStatus.Size = new System.Drawing.Size(77, 15);
            this.labDriverStatus.TabIndex = 270;
            this.labDriverStatus.Text = "Driver Status";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.BackColor = System.Drawing.Color.Transparent;
            this.lblDate.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.Location = new System.Drawing.Point(1641, 128);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(42, 19);
            this.lblDate.TabIndex = 269;
            this.lblDate.Text = "Date";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.BackColor = System.Drawing.Color.Transparent;
            this.lblTime.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.Location = new System.Drawing.Point(1537, 127);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(43, 19);
            this.lblTime.TabIndex = 268;
            this.lblTime.Text = "Time";
            // 
            // tm_login
            // 
            this.tm_login.Tick += new System.EventHandler(this.tm_login_Tick);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PowderBlue;
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.labDriverStatus);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panelDesktop);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Home";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ahdPictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ahdDriverConnector1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panelDesktop;
        private System.Windows.Forms.Button bnt_Tran;
        private Ahd.Winforms.Controls.AhdPictureBox ahdPictureBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button bnt_CaiDat;
        private System.Windows.Forms.Button bnt_Exit;
        private System.Windows.Forms.Button bnt_LogIn;
        private System.Windows.Forms.Button bnt_TramMN;
        private System.Windows.Forms.Button bnt_CanhBao;
        private System.Windows.Forms.Button bnt_BaoCao;
        private System.Windows.Forms.Button bnt_TrangChu;
        private System.Windows.Forms.Button bnt_User;
        private Ahd.Winforms.Controls.AhdDriverConnector ahdDriverConnector1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label labDriverStatus;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Timer tm_login;
    }
}