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
            this.ahdPictureBox1 = new Ahd.Winforms.Controls.AhdPictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.bntEditdata = new System.Windows.Forms.Button();
            this.bntNhaplieu = new System.Windows.Forms.Button();
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
            this.lblWelcome = new System.Windows.Forms.Label();
            this.labDriverStatus = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.tm_login = new System.Windows.Forms.Timer(this.components);
            this.tm_loginMN = new System.Windows.Forms.Timer(this.components);
            this.panelDesktop = new System.Windows.Forms.Panel();
            this._labFllowHo = new System.Windows.Forms.Label();
            this._labALDoor1_Station3 = new System.Windows.Forms.Label();
            this._labALDoor1_Station2 = new System.Windows.Forms.Label();
            this._labALDoor1_Station1 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.lblStationsTitle = new System.Windows.Forms.Label();
            this.dgvStations = new System.Windows.Forms.DataGridView();
            this.dgvStats = new System.Windows.Forms.DataGridView();
            this.lblStatusMessage = new System.Windows.Forms.Label();
            this._labFllow_Ho = new System.Windows.Forms.Label();
            this._labFlowHoFinal = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ahdPictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ahdDriverConnector1)).BeginInit();
            this.panelDesktop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStats)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Highlight;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(442, 208);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(2464, 83);
            this.label1.TabIndex = 1;
            this.label1.Text = "HỆ THỐNG GIÁM SÁT VẬN HÀNH TRÀN XẢ LŨ HỒ DẦU TIẾNG";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.ahdPictureBox1);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Location = new System.Drawing.Point(-2, -21);
            this.panel1.Margin = new System.Windows.Forms.Padding(6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(3812, 219);
            this.panel1.TabIndex = 5;
            // 
            // ahdPictureBox1
            // 
            this.ahdPictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.ahdPictureBox1.FillMode = Ahd.Winforms.Controls.ImageFillMode.Original;
            this.ahdPictureBox1.FlipMode = Ahd.Winforms.Controls.ImageFlipMode.None;
            this.ahdPictureBox1.Image = global::RegistrationForm1.Properties.Resources.logo_removebg_preview1;
            this.ahdPictureBox1.Location = new System.Drawing.Point(94, 25);
            this.ahdPictureBox1.Margin = new System.Windows.Forms.Padding(6);
            this.ahdPictureBox1.Name = "ahdPictureBox1";
            this.ahdPictureBox1.RotateAngle = 0;
            this.ahdPictureBox1.ShadedColor = System.Drawing.Color.Gray;
            this.ahdPictureBox1.Size = new System.Drawing.Size(200, 192);
            this.ahdPictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Normal;
            this.ahdPictureBox1.TabIndex = 8;
            this.ahdPictureBox1.TagPath = null;
            this.ahdPictureBox1.Text = "ahdPictureBox1";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Navy;
            this.label5.Location = new System.Drawing.Point(290, 54);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(3352, 137);
            this.label5.TabIndex = 172;
            this.label5.Text = "CÔNG TY TNHH MTV KHAI THÁC THỦY LỢI MIỀN NAM";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Highlight;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.button3);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.bntEditdata);
            this.panel2.Controls.Add(this.bntNhaplieu);
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
            this.panel2.Location = new System.Drawing.Point(-20, 290);
            this.panel2.Margin = new System.Windows.Forms.Padding(6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(396, 1702);
            this.panel2.TabIndex = 6;
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(-2, 1431);
            this.button3.Margin = new System.Windows.Forms.Padding(6);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(392, 96);
            this.button3.TabIndex = 18;
            this.button3.Text = "Cống số 2";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(-2, 1321);
            this.button2.Margin = new System.Windows.Forms.Padding(6);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(392, 96);
            this.button2.TabIndex = 17;
            this.button2.Text = "Cống số 1";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // bntEditdata
            // 
            this.bntEditdata.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bntEditdata.Location = new System.Drawing.Point(-2, 1212);
            this.bntEditdata.Margin = new System.Windows.Forms.Padding(6);
            this.bntEditdata.Name = "bntEditdata";
            this.bntEditdata.Size = new System.Drawing.Size(392, 96);
            this.bntEditdata.TabIndex = 16;
            this.bntEditdata.Text = "Edit data";
            this.bntEditdata.UseVisualStyleBackColor = true;
            // 
            // bntNhaplieu
            // 
            this.bntNhaplieu.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bntNhaplieu.Location = new System.Drawing.Point(-2, 1102);
            this.bntNhaplieu.Margin = new System.Windows.Forms.Padding(6);
            this.bntNhaplieu.Name = "bntNhaplieu";
            this.bntNhaplieu.Size = new System.Drawing.Size(392, 96);
            this.bntNhaplieu.TabIndex = 15;
            this.bntNhaplieu.Text = "NL";
            this.bntNhaplieu.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(-2, 115);
            this.button1.Margin = new System.Windows.Forms.Padding(6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(392, 96);
            this.button1.TabIndex = 14;
            this.button1.Text = "Hồ chứa";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // bnt_User
            // 
            this.bnt_User.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnt_User.Location = new System.Drawing.Point(-2, 663);
            this.bnt_User.Margin = new System.Windows.Forms.Padding(6);
            this.bnt_User.Name = "bnt_User";
            this.bnt_User.Size = new System.Drawing.Size(392, 96);
            this.bnt_User.TabIndex = 13;
            this.bnt_User.Text = "Người dùng";
            this.bnt_User.UseVisualStyleBackColor = true;
            // 
            // bnt_TrangChu
            // 
            this.bnt_TrangChu.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnt_TrangChu.Location = new System.Drawing.Point(-2, 6);
            this.bnt_TrangChu.Margin = new System.Windows.Forms.Padding(6);
            this.bnt_TrangChu.Name = "bnt_TrangChu";
            this.bnt_TrangChu.Size = new System.Drawing.Size(392, 96);
            this.bnt_TrangChu.TabIndex = 12;
            this.bnt_TrangChu.Text = "Trang chủ";
            this.bnt_TrangChu.UseVisualStyleBackColor = true;
            // 
            // bnt_BaoCao
            // 
            this.bnt_BaoCao.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnt_BaoCao.Location = new System.Drawing.Point(-2, 554);
            this.bnt_BaoCao.Margin = new System.Windows.Forms.Padding(6);
            this.bnt_BaoCao.Name = "bnt_BaoCao";
            this.bnt_BaoCao.Size = new System.Drawing.Size(392, 96);
            this.bnt_BaoCao.TabIndex = 11;
            this.bnt_BaoCao.Text = "Báo cáo";
            this.bnt_BaoCao.UseVisualStyleBackColor = true;
            // 
            // bnt_CaiDat
            // 
            this.bnt_CaiDat.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnt_CaiDat.Location = new System.Drawing.Point(-2, 773);
            this.bnt_CaiDat.Margin = new System.Windows.Forms.Padding(6);
            this.bnt_CaiDat.Name = "bnt_CaiDat";
            this.bnt_CaiDat.Size = new System.Drawing.Size(392, 96);
            this.bnt_CaiDat.TabIndex = 10;
            this.bnt_CaiDat.Text = "Cài đặt";
            this.bnt_CaiDat.UseVisualStyleBackColor = true;
            // 
            // bnt_Exit
            // 
            this.bnt_Exit.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnt_Exit.Location = new System.Drawing.Point(-2, 992);
            this.bnt_Exit.Margin = new System.Windows.Forms.Padding(6);
            this.bnt_Exit.Name = "bnt_Exit";
            this.bnt_Exit.Size = new System.Drawing.Size(392, 96);
            this.bnt_Exit.TabIndex = 9;
            this.bnt_Exit.Text = "Thoát";
            this.bnt_Exit.UseVisualStyleBackColor = true;
            // 
            // bnt_LogIn
            // 
            this.bnt_LogIn.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnt_LogIn.Location = new System.Drawing.Point(-2, 883);
            this.bnt_LogIn.Margin = new System.Windows.Forms.Padding(6);
            this.bnt_LogIn.Name = "bnt_LogIn";
            this.bnt_LogIn.Size = new System.Drawing.Size(392, 96);
            this.bnt_LogIn.TabIndex = 8;
            this.bnt_LogIn.Text = "Đăng nhập";
            this.bnt_LogIn.UseVisualStyleBackColor = true;
            // 
            // bnt_TramMN
            // 
            this.bnt_TramMN.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnt_TramMN.Location = new System.Drawing.Point(-2, 335);
            this.bnt_TramMN.Margin = new System.Windows.Forms.Padding(6);
            this.bnt_TramMN.Name = "bnt_TramMN";
            this.bnt_TramMN.Size = new System.Drawing.Size(392, 96);
            this.bnt_TramMN.TabIndex = 7;
            this.bnt_TramMN.Text = "Mực nước HL SSG";
            this.bnt_TramMN.UseVisualStyleBackColor = true;
            // 
            // bnt_CanhBao
            // 
            this.bnt_CanhBao.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnt_CanhBao.Location = new System.Drawing.Point(-2, 444);
            this.bnt_CanhBao.Margin = new System.Windows.Forms.Padding(6);
            this.bnt_CanhBao.Name = "bnt_CanhBao";
            this.bnt_CanhBao.Size = new System.Drawing.Size(392, 96);
            this.bnt_CanhBao.TabIndex = 6;
            this.bnt_CanhBao.Text = "Cảnh báo";
            this.bnt_CanhBao.UseVisualStyleBackColor = true;
            // 
            // bnt_Tran
            // 
            this.bnt_Tran.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnt_Tran.Location = new System.Drawing.Point(-2, 223);
            this.bnt_Tran.Margin = new System.Windows.Forms.Padding(6);
            this.bnt_Tran.Name = "bnt_Tran";
            this.bnt_Tran.Size = new System.Drawing.Size(392, 96);
            this.bnt_Tran.TabIndex = 0;
            this.bnt_Tran.Text = "Tràn xả lũ";
            this.bnt_Tran.UseVisualStyleBackColor = true;
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
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcome.ForeColor = System.Drawing.Color.White;
            this.lblWelcome.Location = new System.Drawing.Point(3150, 208);
            this.lblWelcome.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(127, 29);
            this.lblWelcome.TabIndex = 267;
            this.lblWelcome.Text = "UserDislay";
            // 
            // labDriverStatus
            // 
            this.labDriverStatus.AutoSize = true;
            this.labDriverStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.labDriverStatus.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labDriverStatus.Location = new System.Drawing.Point(3518, 254);
            this.labDriverStatus.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labDriverStatus.Name = "labDriverStatus";
            this.labDriverStatus.Size = new System.Drawing.Size(149, 29);
            this.labDriverStatus.TabIndex = 270;
            this.labDriverStatus.Text = "Driver Status";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.BackColor = System.Drawing.Color.Transparent;
            this.lblDate.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.ForeColor = System.Drawing.Color.White;
            this.lblDate.Location = new System.Drawing.Point(3282, 246);
            this.lblDate.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(79, 36);
            this.lblDate.TabIndex = 269;
            this.lblDate.Text = "Date";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.BackColor = System.Drawing.Color.Transparent;
            this.lblTime.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.ForeColor = System.Drawing.Color.White;
            this.lblTime.Location = new System.Drawing.Point(3074, 244);
            this.lblTime.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(85, 36);
            this.lblTime.TabIndex = 268;
            this.lblTime.Text = "Time";
            // 
            // panelDesktop
            // 
            this.panelDesktop.BackColor = System.Drawing.Color.Honeydew;
            this.panelDesktop.BackgroundImage = global::RegistrationForm1.Properties.Resources.Main;
            this.panelDesktop.Controls.Add(this._labFlowHoFinal);
            this.panelDesktop.Controls.Add(this._labFllow_Ho);
            this.panelDesktop.Controls.Add(this._labFllowHo);
            this.panelDesktop.Controls.Add(this._labALDoor1_Station3);
            this.panelDesktop.Controls.Add(this._labALDoor1_Station2);
            this.panelDesktop.Controls.Add(this._labALDoor1_Station1);
            this.panelDesktop.Controls.Add(this.button4);
            this.panelDesktop.Controls.Add(this.lblStationsTitle);
            this.panelDesktop.Controls.Add(this.dgvStations);
            this.panelDesktop.Controls.Add(this.dgvStats);
            this.panelDesktop.Controls.Add(this.lblStatusMessage);
            this.panelDesktop.Location = new System.Drawing.Point(384, 290);
            this.panelDesktop.Margin = new System.Windows.Forms.Padding(6);
            this.panelDesktop.Name = "panelDesktop";
            this.panelDesktop.Size = new System.Drawing.Size(3452, 1706);
            this.panelDesktop.TabIndex = 7;
            // 
            // _labFllowHo
            // 
            this._labFllowHo.AutoSize = true;
            this._labFllowHo.Location = new System.Drawing.Point(702, 577);
            this._labFllowHo.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labFllowHo.Name = "_labFllowHo";
            this._labFllowHo.Size = new System.Drawing.Size(70, 25);
            this._labFllowHo.TabIndex = 183;
            this._labFllowHo.Text = "label2";
            // 
            // _labALDoor1_Station3
            // 
            this._labALDoor1_Station3.AutoSize = true;
            this._labALDoor1_Station3.Location = new System.Drawing.Point(400, 688);
            this._labALDoor1_Station3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labALDoor1_Station3.Name = "_labALDoor1_Station3";
            this._labALDoor1_Station3.Size = new System.Drawing.Size(70, 25);
            this._labALDoor1_Station3.TabIndex = 182;
            this._labALDoor1_Station3.Text = "label2";
            this._labALDoor1_Station3.Click += new System.EventHandler(this.label3_Click);
            // 
            // _labALDoor1_Station2
            // 
            this._labALDoor1_Station2.AutoSize = true;
            this._labALDoor1_Station2.Location = new System.Drawing.Point(400, 629);
            this._labALDoor1_Station2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labALDoor1_Station2.Name = "_labALDoor1_Station2";
            this._labALDoor1_Station2.Size = new System.Drawing.Size(70, 25);
            this._labALDoor1_Station2.TabIndex = 181;
            this._labALDoor1_Station2.Text = "label2";
            // 
            // _labALDoor1_Station1
            // 
            this._labALDoor1_Station1.AutoSize = true;
            this._labALDoor1_Station1.Location = new System.Drawing.Point(400, 577);
            this._labALDoor1_Station1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labALDoor1_Station1.Name = "_labALDoor1_Station1";
            this._labALDoor1_Station1.Size = new System.Drawing.Size(70, 25);
            this._labALDoor1_Station1.TabIndex = 180;
            this._labALDoor1_Station1.Text = "label2";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(1170, 469);
            this.button4.Margin = new System.Windows.Forms.Padding(6);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(150, 44);
            this.button4.TabIndex = 179;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // lblStationsTitle
            // 
            this.lblStationsTitle.AutoSize = true;
            this.lblStationsTitle.Location = new System.Drawing.Point(918, 448);
            this.lblStationsTitle.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblStationsTitle.Name = "lblStationsTitle";
            this.lblStationsTitle.Size = new System.Drawing.Size(70, 25);
            this.lblStationsTitle.TabIndex = 178;
            this.lblStationsTitle.Text = "label2";
            // 
            // dgvStations
            // 
            this.dgvStations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStations.Location = new System.Drawing.Point(286, 58);
            this.dgvStations.Margin = new System.Windows.Forms.Padding(6);
            this.dgvStations.Name = "dgvStations";
            this.dgvStations.RowHeadersWidth = 82;
            this.dgvStations.Size = new System.Drawing.Size(1072, 288);
            this.dgvStations.TabIndex = 177;
            // 
            // dgvStats
            // 
            this.dgvStats.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStats.Location = new System.Drawing.Point(1490, 10);
            this.dgvStats.Margin = new System.Windows.Forms.Padding(6);
            this.dgvStats.Name = "dgvStats";
            this.dgvStats.RowHeadersWidth = 82;
            this.dgvStats.Size = new System.Drawing.Size(1072, 592);
            this.dgvStats.TabIndex = 176;
            // 
            // lblStatusMessage
            // 
            this.lblStatusMessage.AutoSize = true;
            this.lblStatusMessage.Location = new System.Drawing.Point(148, 469);
            this.lblStatusMessage.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblStatusMessage.Name = "lblStatusMessage";
            this.lblStatusMessage.Size = new System.Drawing.Size(70, 25);
            this.lblStatusMessage.TabIndex = 173;
            this.lblStatusMessage.Text = "label2";
            // 
            // _labFllow_Ho
            // 
            this._labFllow_Ho.AutoSize = true;
            this._labFllow_Ho.Location = new System.Drawing.Point(2920, 857);
            this._labFllow_Ho.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labFllow_Ho.Name = "_labFllow_Ho";
            this._labFllow_Ho.Size = new System.Drawing.Size(70, 25);
            this._labFllow_Ho.TabIndex = 184;
            this._labFllow_Ho.Text = "label2";
            // 
            // _labFlowHoFinal
            // 
            this._labFlowHoFinal.AutoSize = true;
            this._labFlowHoFinal.Location = new System.Drawing.Point(711, 665);
            this._labFlowHoFinal.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labFlowHoFinal.Name = "_labFlowHoFinal";
            this._labFlowHoFinal.Size = new System.Drawing.Size(70, 25);
            this._labFlowHoFinal.TabIndex = 185;
            this._labFlowHoFinal.Text = "label2";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Highlight;
            this.ClientSize = new System.Drawing.Size(3808, 2002);
            this.Controls.Add(this.labDriverStatus);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panelDesktop);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6);
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
            this.panelDesktop.ResumeLayout(false);
            this.panelDesktop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStats)).EndInit();
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
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label labDriverStatus;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Timer tm_login;
        public Ahd.Winforms.Controls.AhdDriverConnector ahdDriverConnector1;
        private System.Windows.Forms.Timer tm_loginMN;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button bntNhaplieu;
        private System.Windows.Forms.Button bntEditdata;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label lblStatusMessage;
        private System.Windows.Forms.DataGridView dgvStats;
        private System.Windows.Forms.DataGridView dgvStations;
        private System.Windows.Forms.Label lblStationsTitle;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label _labALDoor1_Station1;
        private System.Windows.Forms.Label _labALDoor1_Station3;
        private System.Windows.Forms.Label _labALDoor1_Station2;
        private System.Windows.Forms.Label _labFllowHo;
        private System.Windows.Forms.Label _labFllow_Ho;
        private System.Windows.Forms.Label _labFlowHoFinal;
    }
}