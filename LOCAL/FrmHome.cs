using Ahd.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dapper;
namespace RegistrationForm1
{
    public partial class FrmHome : Form
    {
        private string connectionString = "Data Source=ADMIN-PC\\SQLEXPRESS;Initial Catalog=RegistrationForm4;Integrated Security=True;TrustServerCertificate=True";
        int _id = 1;
        public FrmHome()
        {
            InitializeComponent();
            Load += FrmHome_Load;
        }
        IAhdDriverConnector driver;
        private void FrmHome_Load(object sender, EventArgs e)
        {
          //  lbl_User.Text = "Xin Chào    " + FrmLogin1.User_V;
            driver = AhdDriverConnectorProvider.GetAhdDriverConnector();
            if (!driver.IsStarted)
                driver.Started += Driver_Started;
            else
                Driver_Started(driver, null);
            timer1.Enabled = true;
            tm_login.Interval = 3000;
            tm_login.Enabled = true;
            tm_login.Tick += (s, o) =>
            {
                Timer t = (Timer)s;
                t.Enabled = false;
                this.Invoke((MethodInvoker)delegate { tm_login.Start(); });
                t.Enabled = true;
            };
        }
        private void Driver_Started(object sender, EventArgs e)
        {// khai báo sự kiện
         // 
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_1Open").ValueChanged += Doorlock2_1Open_ValueChanged;//Su kien Chốt bên trái cửa 2 mở hết
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_1Close").ValueChanged += Doorlock2_1Close_ValueChanged;//Chốt bên trái cửa 2 đóng hết
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_2Open").ValueChanged += Doorlock2_2Open_ValueChanged;////Su kien Chốt bên phải cửa 2 mở hết
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_2Close").ValueChanged += Doorlock2_2Close_ValueChanged;//Su kien Chốt bên phải cửa 2 đóng hết
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_Closing").ValueChanged += Doorlock2_Closing_ValueChanged;//Su kien chốt cửa 2 đang đóng
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_Opening").ValueChanged += Doorlock2_Opening_ValueChanged;//Su kien chốt cửa 2 đang mở
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Open").ValueChanged += Door1_Open_ValueChanged;//Su kien mở hết cửa
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Close").ValueChanged += Door1_Close_Close_ValueChanged;//Su kien đóng hết cửa
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_Open").ValueChanged += Door2_Open_ValueChanged;//Su kien mở hết cửa
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_Close").ValueChanged += Door2_Close_ValueChanged;//Su kien đóng hết cửa
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Opening").ValueChanged += Door1_Opening_ValueChanged;//Su kien đang đóng chốt 1
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Closing").ValueChanged += Door1_Closing_ValueChanged;//Su kien đang mở chốt 1           
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_Opening").ValueChanged += Door2_Opening_ValueChanged;//Su kien đang đóng cửa
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_Closing").ValueChanged += Door2_Closing_ValueChanged;//Su kien đang mở cửa         

            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/DC1_Running").ValueChanged += DC1_Running_ValueChanged;//Su kien đang mở cửa        
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/DC2_Running").ValueChanged += DC2_Running_ValueChanged;//Su kien đang mở cửa        
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/DC3_Running").ValueChanged += DC3_Running_ValueChanged;//Su kien đang mở cửa
                                                                                                                              //
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/DC1_Over").ValueChanged += DC1_Over_ValueChanged;//Su kien đang mở cửa        
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/DC2_Over").ValueChanged += DC2_Over_ValueChanged;//Su kien đang mở cửa        
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/DC3_Over").ValueChanged += DC3_Over_ValueChanged;//Su kien đang mở cửa        

            // Lấy giá trị ban đầu của Tag


            DC1_Over_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/DC1_Over"),
                             new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/DC1_Over")
                             , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/DC1_Over").Value));
            DC2_Over_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/DC2_Over"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/DC2_Over")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/DC2_Over").Value));
            DC3_Over_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/DC3_Over"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/DC3_Over")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/DC3_Over").Value));


            DC1_Running_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/DC1_Running"),
                             new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/DC1_Running")
                             , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/DC1_Running").Value));
            DC2_Running_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/DC2_Running"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/DC2_Running")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/DC2_Running").Value));
            DC3_Running_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/DC3_Running"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/DC3_Running")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/DC3_Running").Value));


            Door1_Opening_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Opening"),
                             new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Opening")
                             , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Opening").Value));
            Door1_Closing_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Closing"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Closing")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Closing").Value));
            Door2_Opening_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_Opening"),
                             new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_Opening")
                             , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_Opening").Value));
            Door2_Closing_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_Closing"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_Closing")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_Closing").Value));



            Doorlock2_1Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_1Open"),
                             new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_1Open")
                             , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_1Open").Value));
            Doorlock2_1Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_1Close"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_1Close")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_1Close").Value));
            Doorlock2_2Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_2Open"),
                              new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_2Open")
                              , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_2Open").Value));

            Doorlock2_2Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock1_1Close"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock1_1Close")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock1_1Close").Value));
            Doorlock2_Closing_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_Closing"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_Closing")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_Closing").Value));
            Doorlock2_Opening_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_Opening"),
                             new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_Opening")
                             , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_Opening").Value));

            Door1_Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Open"),
                           new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Open")
                           , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Open").Value));
            Door1_Close_Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Close"),
                             new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Close")
                             , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Close").Value));
            Door2_Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Open"),
                          new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_Open")
                          , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_Open").Value));
            Door2_Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_Close"),
                             new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_Close")
                             , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_Close").Value));
        }
        private void DC3_Over_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_DC3_Over.Visible = true; Pic_DC3Runing.Visible = false; Pic_DC3Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_DC3_Over.Visible = false; Pic_DC3Runing.Visible = false; Pic_DC3Stop.Visible = false; });
        }
        private void DC2_Over_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_DC2_Over.Visible = true; Pic_DC2Runing.Visible = false; Pic_DC2Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_DC2_Over.Visible = false; Pic_DC2Runing.Visible = false; Pic_DC2Stop.Visible = false; });
        }

        private void DC1_Over_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_DC1_Over.Visible = true; Pic_DC1Runing.Visible = false; Pic_DC1Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_DC1_Over.Visible = false; Pic_DC1Runing.Visible = false; Pic_DC1Stop.Visible = false; });
        }


        private void DC3_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_DC3Runing.Visible = true; Pic_DC3_Over.Visible = false; Pic_DC3Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_DC3_Over.Visible = false; Pic_DC3Stop.Visible = true; Pic_DC3Runing.Visible = false; });
        }
        private void DC2_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_DC2Runing.Visible = true; Pic_DC2_Over.Visible = false; Pic_DC2Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_DC2_Over.Visible = false; Pic_DC2Stop.Visible = true; Pic_DC2Runing.Visible = false; });
        }
        private void DC1_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_DC1Runing.Visible = true; Pic_DC1_Over.Visible = false; Pic_DC1Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_DC1_Over.Visible = false; Pic_DC1Stop.Visible = true; Pic_DC1Runing.Visible = false; });
        }


        private void Door2_Closing_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door2_Closing.Visible = true; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door2_Closing.Visible = false; });
        }
        private void Door2_Opening_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door2_Opening.Visible = true; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door2_Opening.Visible = false; });
        }
        private void Door1_Closing_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door1_Closing.Visible = true; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door1_Closing.Visible = false; });
        }
        private void Door1_Opening_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door1_Opening.Visible = true; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door1_Opening.Visible = false; });
        }
        private void Door2_Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { lblCua2.Text = "Đang Đóng"; lblCua2.ForeColor = Color.Green; lblCua2.BackColor = Color.Red; });
            }
            else
                this.Invoke((MethodInvoker)delegate { lblCua2.Text = "Cửa 2"; lblCua2.ForeColor = DefaultForeColor; lblCua2.BackColor = DefaultBackColor; });
        }

        private void Door2_Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { lblCua2.Text = "Đang Mở"; lblCua2.ForeColor = Color.Green; lblCua2.BackColor = Color.YellowGreen; });
            }
            else
                this.Invoke((MethodInvoker)delegate { lblCua2.Text = "Cửa 2"; lblCua2.ForeColor = DefaultForeColor; lblCua2.BackColor = DefaultBackColor; });
        }

        private void Door1_Close_Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { lblCua1.Text = "Đang Đóng"; lblCua1.ForeColor = Color.Green; lblCua1.BackColor = Color.Red; });
            }
            else
                this.Invoke((MethodInvoker)delegate { lblCua1.Text = "Cửa 1"; lblCua1.ForeColor = DefaultForeColor; lblCua1.BackColor = DefaultBackColor; });
        }

        private void Door1_Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { lblCua1.Text = "Đang Mở"; lblCua1.ForeColor = Color.Green; lblCua1.BackColor = Color.YellowGreen; });
            }
            else
                this.Invoke((MethodInvoker)delegate { lblCua1.Text = "Cửa 1"; lblCua1.ForeColor = DefaultForeColor; lblCua1.BackColor = DefaultBackColor; });
        }


        private void Doorlock2_1Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock2_1Close.Visible = true; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock2_1Close.Visible = false; });
        }
        private void Doorlock2_1Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock2_1Open.Visible = true; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock2_1Open.Visible = false; });
        }

        private void Doorlock2_2Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock2_2Open.Visible = true; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock2_2Open.Visible = false; });
        }
        private void Doorlock2_2Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock2_2Close.Visible = true; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock2_2Close.Visible = false; });
        }

        private void Doorlock2_Opening_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { lblChot2.Text = "Đang Mở"; lblChot2.ForeColor = Color.Green; lblChot2.BackColor = Color.YellowGreen; });

            }
            else

                this.Invoke((MethodInvoker)delegate { lblChot2.Text = "CHỐT 2"; lblChot2.ForeColor = DefaultForeColor; lblChot2.BackColor = DefaultBackColor; });

        }
        private void Doorlock2_Closing_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { lblChot2.Text = "Đang Đóng"; lblChot2.ForeColor = Color.Green; lblChot2.BackColor = Color.Red; });

            }
            else
                this.Invoke((MethodInvoker)delegate { lblChot2.Text = "CHỐT 2"; lblChot2.ForeColor = DefaultForeColor; lblChot2.BackColor = DefaultBackColor; });

        }

       
       
        private void _btnGhiStatus_Click(object sender, EventArgs e)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                db.Open();
                // doc data để lấy Id hiện tại
                string sqlQuery = "SELECT * FROM Datavanhanh";
                List<DataVanHanhModel> data = db.Query<DataVanHanhModel>(sqlQuery).AsList();
                _id = (data != null && data.Any()) ? data.Max(x => x.Id) : 0;
                // Thêm một dòng mới
                //tạo dữ liệu để thêm vào
                var newRow = new DataVanHanhModel
                {
                    Id = _id + 1,
                    CreateAt = DateTime.Now,
                    HT_Cylinder1_1 = HT_Cylinder1_1.Text,
                    HT_Cylinder1_2 = HT_Cylinder1_2.Text,
                    HT_Cylinder2_1 = HT_Cylinder2_1.Text,
                    HT_Cylinder2_2 = HT_Cylinder2_2.Text,
                    HT_Cylinder3_1 = HT_Cylinder3_1.Text,
                    HT_Cylinder3_2 = HT_Cylinder3_2.Text,
                    HT_Cylinder4_1 = HT_Cylinder4_1.Text,
                    HT_Cylinder4_2 = HT_Cylinder4_2.Text,
                    HT_Cylinder5_1 = HT_Cylinder5_1.Text,
                    HT_Cylinder5_2 = HT_Cylinder5_2.Text,
                    HT_Cylinder6_1 = HT_Cylinder6_1.Text,
                    HT_Cylinder6_2 = HT_Cylinder6_2.Text,
                    Door1_Aperture = Door1_Aperture.Text,
                    Door2_Aperture = Door2_Aperture.Text,
                    Door3_Aperture = Door3_Aperture.Text,
                    Door4_Aperture = Door4_Aperture.Text,
                    Door5_Aperture = Door5_Aperture.Text,
                    Door6_Aperture = Door6_Aperture.Text,
                    Temp_Oil1 = Temp_Oil1.Text,
                    Temp_Oil2 = Temp_Oil2.Text,
                    Temp_Oil3 = Temp_Oil3.Text,
                    Fllow_Door1 = Fllow_Door1.Text,
                    Fllow_Door2 = Fllow_Door2.Text,
                    Fllow_Door3 = Fllow_Door3.Text,
                    Fllow_Door4 = Fllow_Door4.Text,
                    Fllow_Door5 = Fllow_Door5.Text,
                    Fllow_Door6 = Fllow_Door6.Text,
                    Total_Fllow = Total_Fllow.Text,
                    Fllow_Ho = Fllow_Ho.Text,
                    Fllow_DauTieng = Fllow_DauTieng.Text,
                    Fllow_BenSuc = Fllow_BenSuc.Text,
                    Fllow_SonDai = Fllow_SonDai.Text,
                };
                // tao câu quẻy
                string insertQuery = "INSERT INTO Datavanhanh (Id,CreateAt,HT_Cylinder1_1, HT_Cylinder1_2, HT_Cylinder2_1,HT_Cylinder2_2,HT_Cylinder3_1,HT_Cylinder3_2,HT_Cylinder4_1,HT_Cylinder4_2,HT_Cylinder5_1,HT_Cylinder5_2,HT_Cylinder6_1,HT_Cylinder6_2," +
                    "Door1_Aperture,Door2_Aperture,Door3_Aperture,Door4_Aperture,Door5_Aperture,Door6_Aperture,Temp_Oil1,Temp_Oil2,Temp_Oil3," +
                    "Fllow_Door1,Fllow_Door2,Fllow_Door3,Fllow_Door4,Fllow_Door5,Fllow_Door6,Total_Fllow, Fllow_Ho, Fllow_DauTieng,Fllow_BenSuc,Fllow_SonDai) " +

                    "VALUES (@Id, @CreateAt, @HT_Cylinder1_1,@HT_Cylinder1_2,@HT_Cylinder2_1,@HT_Cylinder2_2,@HT_Cylinder3_1,@HT_Cylinder3_2,@HT_Cylinder4_1,@HT_Cylinder4_2,@HT_Cylinder5_1,@HT_Cylinder5_2,@HT_Cylinder6_1,@HT_Cylinder6_2," +
                                        "@Door1_Aperture,@Door2_Aperture,@Door3_Aperture,@Door4_Aperture,@Door5_Aperture,@Door6_Aperture,@Temp_Oil1,@Temp_Oil2,@Temp_Oil3," +
                                        "@Fllow_Door1,@Fllow_Door2,@Fllow_Door3,@Fllow_Door4,@Fllow_Door5,@Fllow_Door6,@Total_Fllow,@Fllow_Ho,@Fllow_DauTieng, @Fllow_BenSuc, @Fllow_SonDai)";

                db.Execute(insertQuery, newRow);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToLongTimeString();
            //  lblDate.Text = DateTime.Now.ToLongDateString();
            lblDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //  lblTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");


            if (ahdDriverConnector1.ConnectionStatus == ConnectionStatus.Connected)
            {
                labDriverStatus.BackColor = Color.Green;
                labDriverStatus.Text = "PLC Đang Kết Nối";
            }
            else
            {
                labDriverStatus.BackColor = Color.Red;
                labDriverStatus.Text = "PLC Mất Kết Nối";
            }
        }

        private void tm_login_Tick(object sender, EventArgs e)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                db.Open();
                // doc data để lấy Id hiện tại
                string sqlQuery = "SELECT * FROM Datavanhanh";
                List<DataVanHanhModel> data = db.Query<DataVanHanhModel>(sqlQuery).AsList();
                _id = (data != null && data.Any()) ? data.Max(x => x.Id) : 0;
                // Thêm một dòng mới
                //tạo dữ liệu để thêm vào
                var newRow = new DataVanHanhModel
                {
                    Id = _id + 1,
                    CreateAt = DateTime.Now,
                    HT_Cylinder1_1 = HT_Cylinder1_1.Text,
                    HT_Cylinder1_2 = HT_Cylinder1_2.Text,
                    HT_Cylinder2_1 = HT_Cylinder2_1.Text,
                    HT_Cylinder2_2 = HT_Cylinder2_2.Text,
                    HT_Cylinder3_1 = HT_Cylinder3_1.Text,
                    HT_Cylinder3_2 = HT_Cylinder3_2.Text,
                    HT_Cylinder4_1 = HT_Cylinder4_1.Text,
                    HT_Cylinder4_2 = HT_Cylinder4_2.Text,
                    HT_Cylinder5_1 = HT_Cylinder5_1.Text,
                    HT_Cylinder5_2 = HT_Cylinder5_2.Text,
                    HT_Cylinder6_1 = HT_Cylinder6_1.Text,
                    HT_Cylinder6_2 = HT_Cylinder6_2.Text,
                    Door1_Aperture = Door1_Aperture.Text,
                    Door2_Aperture = Door2_Aperture.Text,
                    Door3_Aperture = Door3_Aperture.Text,
                    Door4_Aperture = Door4_Aperture.Text,
                    Door5_Aperture = Door5_Aperture.Text,
                    Door6_Aperture = Door6_Aperture.Text,
                    Temp_Oil1 = Temp_Oil1.Text,
                    Temp_Oil2 = Temp_Oil2.Text,
                    Temp_Oil3 = Temp_Oil3.Text,
                    Fllow_Door1 = Fllow_Door1.Text,
                    Fllow_Door2 = Fllow_Door2.Text,
                    Fllow_Door3 = Fllow_Door3.Text,
                    Fllow_Door4 = Fllow_Door4.Text,
                    Fllow_Door5 = Fllow_Door5.Text,
                    Fllow_Door6 = Fllow_Door6.Text,
                    Total_Fllow = Total_Fllow.Text,
                    Fllow_Ho = Fllow_Ho.Text,
                    Fllow_DauTieng = Fllow_DauTieng.Text,
                    Fllow_BenSuc = Fllow_BenSuc.Text,
                    Fllow_SonDai = Fllow_SonDai.Text,
                };
                // tao câu quẻy
                string insertQuery = "INSERT INTO Datavanhanh (Id,CreateAt,HT_Cylinder1_1, HT_Cylinder1_2, HT_Cylinder2_1,HT_Cylinder2_2,HT_Cylinder3_1,HT_Cylinder3_2,HT_Cylinder4_1,HT_Cylinder4_2,HT_Cylinder5_1,HT_Cylinder5_2,HT_Cylinder6_1,HT_Cylinder6_2," +
                    "Door1_Aperture,Door2_Aperture,Door3_Aperture,Door4_Aperture,Door5_Aperture,Door6_Aperture,Temp_Oil1,Temp_Oil2,Temp_Oil3," +
                    "Fllow_Door1,Fllow_Door2,Fllow_Door3,Fllow_Door4,Fllow_Door5,Fllow_Door6,Total_Fllow, Fllow_Ho, Fllow_DauTieng,Fllow_BenSuc,Fllow_SonDai) " +

                    "VALUES (@Id, @CreateAt, @HT_Cylinder1_1,@HT_Cylinder1_2,@HT_Cylinder2_1,@HT_Cylinder2_2,@HT_Cylinder3_1,@HT_Cylinder3_2,@HT_Cylinder4_1,@HT_Cylinder4_2,@HT_Cylinder5_1,@HT_Cylinder5_2,@HT_Cylinder6_1,@HT_Cylinder6_2," +
                                        "@Door1_Aperture,@Door2_Aperture,@Door3_Aperture,@Door4_Aperture,@Door5_Aperture,@Door6_Aperture,@Temp_Oil1,@Temp_Oil2,@Temp_Oil3," +
                                        "@Fllow_Door1,@Fllow_Door2,@Fllow_Door3,@Fllow_Door4,@Fllow_Door5,@Fllow_Door6,@Total_Fllow,@Fllow_Ho,@Fllow_DauTieng, @Fllow_BenSuc, @Fllow_SonDai)";

                db.Execute(insertQuery, newRow);
            }
        }
    }
}
