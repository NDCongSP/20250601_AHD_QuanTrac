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
            lbl_User.Text = "Xin Chào   " ;
          //  lbl_User.Text = "Xin Chào    " + { currentUserName};
            
            driver = AhdDriverConnectorProvider.GetAhdDriverConnector();
            if (!driver.IsStarted)
                driver.Started += Driver_Started;
            else
                Driver_Started(driver, null);
            timer1.Enabled = true;
            tm_login.Interval = 30000;
            tm_login.Enabled = true;
            tm_login.Tick += (s, o) =>
            {
                Timer t = (Timer)s;
                t.Enabled = false;
                this.Invoke((MethodInvoker)delegate { tm_login.Start(); });
                t.Enabled = true;
            };
        }
        //
              
        private void Driver_Started(object sender, EventArgs e)
        {// khai báo sự kiện
            // Trạng thái động cơ Trạm 1,2,3
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Station1_Run").ValueChanged += Station1_Run_ValueChanged;//Su kien trạm 1 đang chạy        
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Station1_Stop").ValueChanged += Station1_Stop_ValueChanged;//Su kien trạm 1 đang dừng       
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Station1_Alarm").ValueChanged += Station1_Alarm_ValueChanged;//Su kien trạm 1 đang lỗi
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Station2_Run").ValueChanged += Station2_Run_ValueChanged;//Su kien trạm 2 đang chạy        
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Station2_Stop").ValueChanged += Station2_Stop_ValueChanged;//Su kien trạm 2 đang dừng       
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Station2_Alarm").ValueChanged += Station2_Alarm_ValueChanged;//Su kien trạm 2 đang lỗi
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Station3_Run").ValueChanged += Station3_Run_ValueChanged;//Su kien trạm 3 đang chạy        
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Station3_Stop").ValueChanged += Station3_Stop_ValueChanged;//Su kien trạm 3 đang dừng       
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Station3_Alarm").ValueChanged += Station3_Alarm_ValueChanged;//Su kien trạm 3 đang lỗi                                                                                                                                 // Trạng thái cửa 1,2 đang mở, đóng
            // Trạng thái cửa 1 -> 6 đang mở, đóng
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Opening").ValueChanged += Door1_Opening_ValueChanged;//Su kien cửa 1 đang mở
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Closing").ValueChanged += Door1_Closing_ValueChanged;//Su kien cửa 1 đang đóng         
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_Opening").ValueChanged += Door2_Opening_ValueChanged;//Su kien cửa 2 đang mở
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_Closing").ValueChanged += Door2_Closing_ValueChanged;//Su kien cửa 2 đang đóng
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_Opening").ValueChanged += Door3_Opening_ValueChanged;//Su kien cửa 3 đang mở
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_Closing").ValueChanged += Door3_Closing_ValueChanged;//Su kien cửa 3 đang đóng
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_Opening").ValueChanged += Door4_Opening_ValueChanged;//Su kien cửa 4 đang mở
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_Closing").ValueChanged += Door4_Closing_ValueChanged;//Su kien cửa 4 đang đóng         
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_Opening").ValueChanged += Door5_Opening_ValueChanged;//Su kien cửa 5 đang mở
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_Closing").ValueChanged += Door5_Closing_ValueChanged;//Su kien cửa 5 đang đóng
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_Opening").ValueChanged += Door6_Opening_ValueChanged;//Su kien cửa 6 đang mở
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_Closing").ValueChanged += Door6_Closing_ValueChanged;//Su kien cửa 6 đang đóng

            // Trạng thái  cửa đang mở, đang đóng
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Open").ValueChanged += Door1_Open_ValueChanged;//Su kien mở hết cửa 1
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Close").ValueChanged += Door1_Close_ValueChanged;//Su kien đóng hết cửa 1
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_Open").ValueChanged += Door2_Open_ValueChanged;//Su kien mở hết cửa 2
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_Close").ValueChanged += Door2_Close_ValueChanged;//Su kien đóng hết cửa 2
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_Open").ValueChanged += Door3_Open_ValueChanged;//Su kien mở hết cửa 3
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_Close").ValueChanged += Door3_Close_ValueChanged;//Su kien đóng hết cửa 3
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_Open").ValueChanged += Door4_Open_ValueChanged;//Su kien mở hết cửa 4
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_Close").ValueChanged += Door4_Close_ValueChanged;//Su kien đóng hết cửa 4
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_Open").ValueChanged += Door5_Open_ValueChanged;//Su kien mở hết cửa 5
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_Close").ValueChanged += Door5_Close_ValueChanged;//Su kien đóng hết cửa 5
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_Open").ValueChanged += Door6_Open_ValueChanged;//Su kien mở hết cửa 6
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_Close").ValueChanged += Door6_Close_ValueChanged;//Su kien đóng hết cửa 6
            // Trạng thái chốt đang mở , đang đóng 2->5
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_Opening").ValueChanged += Doorlock2_Opening_ValueChanged;//Su kien chốt cửa 2 đang mở
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_Closing").ValueChanged += Doorlock2_Closing_ValueChanged;//Su kien chốt cửa 2 đang đóng
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock3_Opening").ValueChanged += Doorlock3_Opening_ValueChanged;//Su kien chốt cửa 3 đang mở
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock3_Closing").ValueChanged += Doorlock3_Closing_ValueChanged;//Su kien chốt cửa 3 đang đóng
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock4_Opening").ValueChanged += Doorlock4_Opening_ValueChanged;//Su kien chốt cửa 4 đang mở
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock4_Closing").ValueChanged += Doorlock4_Closing_ValueChanged;//Su kien chốt cửa 4 đang đóng
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock5_Opening").ValueChanged += Doorlock5_Opening_ValueChanged;//Su kien chốt cửa 5 đang mở
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock5_Closing").ValueChanged += Doorlock5_Closing_ValueChanged;//Su kien chốt cửa 5 đang đóng
             // Trạng thái chốt đóng hết , mở hết 

            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_1Open").ValueChanged += Doorlock2_1Open_ValueChanged;//Su kien Chốt bên trái cửa 2 mở hết
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_1Close").ValueChanged += Doorlock2_1Close_ValueChanged;//Chốt bên trái cửa 2 đóng hết
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_2Open").ValueChanged += Doorlock2_2Open_ValueChanged;////Su kien Chốt bên phải cửa 2 mở hết
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_2Close").ValueChanged += Doorlock2_2Close_ValueChanged;//Su kien Chốt bên phải cửa 2 đóng hết
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock3_1Open").ValueChanged += Doorlock3_1Open_ValueChanged;//Su kien Chốt bên trái cửa 3 mở hết
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock3_1Close").ValueChanged += Doorlock3_1Close_ValueChanged;//Chốt bên trái cửa 3 đóng hết
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock3_2Open").ValueChanged += Doorlock3_2Open_ValueChanged;////Su kien Chốt bên phải cửa 3 mở hết
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock3_2Close").ValueChanged += Doorlock3_2Close_ValueChanged;//Su kien Chốt bên phải cửa 3 đóng hết
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock4_1Open").ValueChanged += Doorlock4_1Open_ValueChanged;//Su kien Chốt bên trái cửa 4 mở hết
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock4_1Close").ValueChanged += Doorlock4_1Close_ValueChanged;//Chốt bên trái cửa 4 đóng hết
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock4_2Open").ValueChanged += Doorlock4_2Open_ValueChanged;////Su kien Chốt bên phải cửa 4 mở hết
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock4_2Close").ValueChanged += Doorlock4_2Close_ValueChanged;//Su kien Chốt bên phải cửa 4 đóng hết
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock5_1Open").ValueChanged += Doorlock5_1Open_ValueChanged;//Su kien Chốt bên trái cửa 5 mở hết
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock5_1Close").ValueChanged += Doorlock5_1Close_ValueChanged;//Chốt bên trái cửa 5 đóng hết
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock5_2Open").ValueChanged += Doorlock5_2Open_ValueChanged;////Su kien Chốt bên phải cửa 5 mở hết
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock5_2Close").ValueChanged += Doorlock5_2Close_ValueChanged;//Su kien Chốt bên phải cửa 5 đóng hết


           
            // Gán giá trị trạng thái chốt đóng hết, mở hết

            Doorlock2_1Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_1Open"),
                             new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_1Open")
                             , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_1Open").Value));
            Doorlock2_1Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_1Close"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_1Close")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_1Close").Value));
            Doorlock2_2Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_2Open"),
                              new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_2Open")
                              , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_2Open").Value));
            Doorlock2_2Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_2Close"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_2Close")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_2Close").Value));
            Doorlock3_1Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock3_1Open"),
                             new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock3_1Open")
                             , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock3_1Open").Value));

            Doorlock3_1Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock3_1Close"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock3_1Close")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock3_1Close").Value));
            Doorlock3_2Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock3_2Open"),
                              new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock3_2Open")
                              , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock3_2Open").Value));
            Doorlock3_2Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock3_2Close"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock3_2Close")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock3_2Close").Value));
            Doorlock4_1Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock4_1Open"),
                             new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock4_1Open")
                             , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock4_1Open").Value));
            Doorlock4_1Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock4_1Close"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock4_1Close")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock4_1Close").Value));
            Doorlock4_2Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock4_2Open"),
                              new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock4_2Open")
                              , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock4_2Open").Value));
            Doorlock4_2Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock4_2Close"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock4_2Close")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock4_2Close").Value));
            Doorlock5_1Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock5_1Open"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock5_1Open")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock5_1Open").Value));
            Doorlock5_1Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock5_1Close"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock5_1Close")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock5_1Close").Value));
            Doorlock5_2Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock5_2Open"),
                              new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock5_2Open")
                              , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock5_2Open").Value));
            Doorlock5_2Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock5_2Close"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock5_2Close")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock5_2Close").Value));
            // Gán Trạng thái chốt đang mở, đang đóng 2->5
            Doorlock2_Opening_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_Opening"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_Opening")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_Opening").Value));
            Doorlock2_Closing_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_Closing"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_Closing")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_Closing").Value));
            Doorlock3_Opening_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock3_Opening"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock3_Opening")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock3_Opening").Value));
            Doorlock3_Closing_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock3_Closing"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock3_Closing")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock3_Closing").Value));
            Doorlock4_Opening_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock4_Opening"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock4_Opening")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock4_Opening").Value));
            Doorlock4_Closing_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock4_Closing"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock4_Closing")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock4_Closing").Value));
            Doorlock5_Opening_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock5_Opening"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock5_Opening")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock5_Opening").Value));
            Doorlock5_Closing_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock5_Closing"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock5_Closing")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock5_Closing").Value));
            //Gán Trạng thái  cửa đang mở, đang đóng 1->6
            Door1_Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Open"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Open")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Open").Value));
            Door1_Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Close"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Close")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Close").Value));
            Door2_Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_Open"),
                           new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_Open")
                           , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_Open").Value));
            Door2_Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_Close"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_Close")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_Close").Value));
            Door3_Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_Open"),
                           new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_Open")
                           , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_Open").Value));
            Door3_Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_Close"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_Close")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_Close").Value));
            Door4_Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_Open"),
                           new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_Open")
                           , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_Open").Value));
            Door4_Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_Close"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_Close")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_Close").Value));
            Door5_Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_Open"),
                           new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_Open")
                           , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_Open").Value));
            Door5_Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_Close"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_Close")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_Close").Value));
            Door6_Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_Open"),
                           new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_Open")
                           , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_Open").Value));
            Door6_Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_Close"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_Close")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_Close").Value));
            // Gán giá trị Trạng thái cửa 1 -> 6 đang mở, đóng
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
            Door3_Opening_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_Opening"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_Opening")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_Opening").Value));
            Door3_Closing_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_Closing"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_Closing")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_Closing").Value));
            Door4_Opening_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_Opening"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_Opening")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_Opening").Value));
            Door4_Closing_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_Closing"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_Closing")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_Closing").Value));
            Door5_Opening_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_Opening"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_Opening")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_Opening").Value));
            Door5_Closing_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_Closing"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_Closing")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_Closing").Value));
            Door6_Opening_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_Opening"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_Opening")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_Opening").Value));
            Door6_Closing_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_Closing"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_Closing")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_Closing").Value));
            // Gán giá trị  trạm 1                                                                                                                                                                                                                     
            Station1_Run_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Station1_Run"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Station1_Run")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Station1_Run").Value));
            Station1_Stop_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Station1_Stop"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Station1_Stop")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Station1_Stop").Value));
            Station1_Alarm_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Station1_Alarm"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Station1_Alarm")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Station1_Alarm").Value));
            // Gán giá trị  trạm 2                                                                                                                                                                                                                     
            Station2_Run_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Station2_Run"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Station2_Run")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Station2_Run").Value));
            Station2_Stop_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Station2_Stop"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Station2_Stop")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Station2_Stop").Value));
            Station2_Alarm_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Station2_Alarm"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Station2_Alarm")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Station2_Alarm").Value));
            // Gán giá trị  trạm 3                                                                                                                                                                                                                    
            Station3_Run_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Station3_Run"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Station3_Run")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Station3_Run").Value));
            Station3_Stop_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Station3_Stop"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Station3_Stop")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Station3_Stop").Value));
            Station3_Alarm_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Station3_Alarm"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Station3_Alarm")
                           , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Station3_Alarm").Value));
            // Gán giá trị động cơ trạm 1                                                                                                                                                                                                                     
            S1_DC1_Running_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_DC1_Running"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_DC1_Running")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_DC1_Running").Value));
            S1_DC2_Running_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_DC2_Running"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_DC2_Running")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_DC2_Running").Value));
            S1_DC3_Running_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_DC3_Running"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_DC3_Running")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_DC3_Running").Value));
            // Gán giá trị động cơ trạm 2                                                                                                                                                                                                                    
            S2_DC1_Running_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC1_Running"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC1_Running")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC1_Running").Value));
            S2_DC2_Running_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC2_Running"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC2_Running")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC2_Running").Value));
            S2_DC3_Running_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC3_Running"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC3_Running")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC3_Running").Value));
            // Gán giá trị động cơ trạm 3                                                                                                                                                                                                                    
            S3_DC1_Running_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_DC1_Running"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_DC1_Running")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC1_Running").Value));
            S3_DC2_Running_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC2_Running"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_DC2_Running")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC2_Running").Value));
            S3_DC3_Running_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC3_Running"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_DC3_Running")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_DC3_Running").Value));


            
            
        }
        // Trạng thái chốt đóng hết , mở hết 2 -> 5
        private void Doorlock5_1Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock5_1Open.Visible = true; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock5_1Open.Visible = false; });
        }
        private void Doorlock5_1Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock5_1Close.Visible = true; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock5_1Close.Visible = false; });
        }
        private void Doorlock5_2Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock5_2Open.Visible = true; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock5_2Open.Visible = false; });
        }
        private void Doorlock5_2Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock5_2Close.Visible = true; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock5_2Close.Visible = false; });
        }
        private void Doorlock4_1Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock4_1Open.Visible = true; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock4_1Open.Visible = false; });
        }
        private void Doorlock4_1Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock4_1Close.Visible = true; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock4_1Close.Visible = false; });
        }
        private void Doorlock4_2Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock4_2Open.Visible = true; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock4_2Open.Visible = false; });
        }
        private void Doorlock4_2Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock4_2Close.Visible = true; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock4_2Close.Visible = false; });
        }
        private void Doorlock3_1Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock3_1Open.Visible = true; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock3_1Open.Visible = false; });
        }
        private void Doorlock3_1Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock3_1Close.Visible = true; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock3_1Close.Visible = false; });
        }
        private void Doorlock3_2Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock3_2Open.Visible = true; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock3_2Open.Visible = false; });
        }
        private void Doorlock3_2Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock3_2Close.Visible = true; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock3_2Close.Visible = false; });
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
        private void Doorlock2_1Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock2_1Close.Visible = true; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock2_1Close.Visible = false; });
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
        // Trạng thái chốt đang mở, đang đóng 2 -> 5
        private void Doorlock5_Opening_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { lblChot5.Text = "Đang Mở"; lblChot5.ForeColor = Color.Green; lblChot5.BackColor = Color.YellowGreen; });
            }
            else
                this.Invoke((MethodInvoker)delegate { lblChot5.Text = "CHỐT 5"; lblChot5.ForeColor = DefaultForeColor; lblChot5.BackColor = DefaultBackColor; });
        }
        private void Doorlock5_Closing_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { lblChot5.Text = "Đang Đóng"; lblChot5.ForeColor = Color.Green; lblChot5.BackColor = Color.Red; });
            }
            else
                this.Invoke((MethodInvoker)delegate { lblChot5.Text = "CHỐT 5"; lblChot5.ForeColor = DefaultForeColor; lblChot5.BackColor = DefaultBackColor; });
        }
        private void Doorlock4_Opening_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { lblChot4.Text = "Đang Mở"; lblChot4.ForeColor = Color.Green; lblChot4.BackColor = Color.YellowGreen; });
            }
            else
                this.Invoke((MethodInvoker)delegate { lblChot4.Text = "CHỐT 4"; lblChot4.ForeColor = DefaultForeColor; lblChot4.BackColor = DefaultBackColor; });
        }
        private void Doorlock4_Closing_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { lblChot4.Text = "Đang Đóng"; lblChot4.ForeColor = Color.Green; lblChot4.BackColor = Color.Red; });
            }
            else
                this.Invoke((MethodInvoker)delegate { lblChot4.Text = "CHỐT 4"; lblChot4.ForeColor = DefaultForeColor; lblChot4.BackColor = DefaultBackColor; });
        }
        private void Doorlock3_Opening_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { lblChot3.Text = "Đang Mở"; lblChot3.ForeColor = Color.Green; lblChot3.BackColor = Color.YellowGreen; });
            }
            else
                this.Invoke((MethodInvoker)delegate { lblChot3.Text = "CHỐT 3"; lblChot3.ForeColor = DefaultForeColor; lblChot3.BackColor = DefaultBackColor; });
        }
        private void Doorlock3_Closing_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { lblChot3.Text = "Đang Đóng"; lblChot3.ForeColor = Color.Green; lblChot3.BackColor = Color.Red; });
            }
            else
                this.Invoke((MethodInvoker)delegate { lblChot3.Text = "CHỐT 3"; lblChot3.ForeColor = DefaultForeColor; lblChot3.BackColor = DefaultBackColor; });
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

        // Trạng thái cửa đang mở, đang đóng 1 -> 6
        private void Door6_Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { lblCua6.Text = "Đang Đóng"; lblCua6.ForeColor = Color.Green; lblCua6.BackColor = Color.Red; });
            }
            else
                this.Invoke((MethodInvoker)delegate { lblCua6.Text = "Cửa 6"; lblCua6.ForeColor = DefaultForeColor; lblCua6.BackColor = DefaultBackColor; });
        }

        private void Door6_Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { lblCua6.Text = "Đang Mở"; lblCua6.ForeColor = Color.Green; lblCua6.BackColor = Color.YellowGreen; });
            }
            else
                this.Invoke((MethodInvoker)delegate { lblCua6.Text = "Cửa 1"; lblCua6.ForeColor = DefaultForeColor; lblCua6.BackColor = DefaultBackColor; });
        }
        private void Door5_Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { lblCua5.Text = "Đang Đóng"; lblCua5.ForeColor = Color.Green; lblCua5.BackColor = Color.Red; });
            }
            else
                this.Invoke((MethodInvoker)delegate { lblCua5.Text = "Cửa 5"; lblCua5.ForeColor = DefaultForeColor; lblCua5.BackColor = DefaultBackColor; });
        }
        private void Door5_Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { lblCua5.Text = "Đang Mở"; lblCua5.ForeColor = Color.Green; lblCua5.BackColor = Color.YellowGreen; });
            }
            else
                this.Invoke((MethodInvoker)delegate { lblCua5.Text = "Cửa 1"; lblCua5.ForeColor = DefaultForeColor; lblCua5.BackColor = DefaultBackColor; });
        }
        private void Door4_Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { lblCua4.Text = "Đang Đóng"; lblCua4.ForeColor = Color.Green; lblCua4.BackColor = Color.Red; });
            }
            else
                this.Invoke((MethodInvoker)delegate { lblCua4.Text = "Cửa 4"; lblCua4.ForeColor = DefaultForeColor; lblCua4.BackColor = DefaultBackColor; });
        }
        private void Door4_Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { lblCua4.Text = "Đang Mở"; lblCua4.ForeColor = Color.Green; lblCua4.BackColor = Color.YellowGreen; });
            }
            else
                this.Invoke((MethodInvoker)delegate { lblCua4.Text = "Cửa 4"; lblCua4.ForeColor = DefaultForeColor; lblCua4.BackColor = DefaultBackColor; });
        }
        private void Door3_Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { lblCua3.Text = "Đang Đóng"; lblCua3.ForeColor = Color.Green; lblCua3.BackColor = Color.Red; });
            }
            else
                this.Invoke((MethodInvoker)delegate { lblCua3.Text = "Cửa 3"; lblCua3.ForeColor = DefaultForeColor; lblCua3.BackColor = DefaultBackColor; });
        }
        private void Door3_Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { lblCua3.Text = "Đang Mở"; lblCua3.ForeColor = Color.Green; lblCua3.BackColor = Color.YellowGreen; });
            }
            else
                this.Invoke((MethodInvoker)delegate { lblCua3.Text = "Cửa 3"; lblCua3.ForeColor = DefaultForeColor; lblCua3.BackColor = DefaultBackColor; });
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
        private void Door1_Close_ValueChanged(object sender, TagValueChangedEventArgs e)
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
        // TRạng thái đang đóng , đang mở cửa 1 -> 6
        private void Door6_Closing_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door6_Closing.Visible = true; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door6_Closing.Visible = false; });
        }
        private void Door6_Opening_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door6_Opening.Visible = true; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door6_Opening.Visible = false; });
        }
        private void Door5_Closing_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door5_Closing.Visible = true; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door5_Closing.Visible = false; });
        }
        private void Door5_Opening_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door5_Opening.Visible = true; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door5_Opening.Visible = false; });
        }
        private void Door4_Closing_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door4_Closing.Visible = true; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door4_Closing.Visible = false; });
        }
        private void Door4_Opening_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door4_Opening.Visible = true; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door4_Opening.Visible = false; });
        }
        private void Door3_Closing_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door3_Closing.Visible = true; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door3_Closing.Visible = false; });
        }
        private void Door3_Opening_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door3_Opening.Visible = true; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door3_Opening.Visible = false; });
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
        // Kết thúc TRạng thái đang đóng , đang mở cửa 1 -> 6
        // Trang thái động cơ Trạm 3
        private void Station3_Run_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_DC3Runing.Visible = true; Pic_DC3_Over.Visible = false; Pic_DC3Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_DC3_Over.Visible = false; Pic_DC3Stop.Visible = false; Pic_DC3Runing.Visible = false; });
        }
        private void Station3_Stop_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_DC3Stop.Visible = true; Pic_DC3_Over.Visible = false; Pic_DC3Runing.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_DC3_Over.Visible = false; Pic_DC3Stop.Visible = false; Pic_DC3Runing.Visible = false; });
        }
        private void Station3_Alarm_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_DC3_Over.Visible = true; Pic_DC3Runing.Visible = false; Pic_DC3Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_DC3_Over.Visible = false; Pic_DC3Stop.Visible = false; Pic_DC3Runing.Visible = false; });
        }
        // Trang thái động cơ Trạm 2
        private void Station2_Run_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_DC2Runing.Visible = true; Pic_DC2_Over.Visible = false; Pic_DC2Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_DC2_Over.Visible = false; Pic_DC2Stop.Visible = false; Pic_DC2Runing.Visible = false; });
        }
        private void Station2_Stop_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_DC2Stop.Visible = true; Pic_DC2_Over.Visible = false; Pic_DC2Runing.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_DC2_Over.Visible = false; Pic_DC2Stop.Visible = false; Pic_DC2Runing.Visible = false; });
        }
        private void Station2_Alarm_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_DC2_Over.Visible = true; Pic_DC2Runing.Visible = false; Pic_DC2Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_DC2_Over.Visible = false; Pic_DC2Stop.Visible = false; Pic_DC2Runing.Visible = false; });
        }

        // Kết thúc Trạng thái động cơ Trạm 2


        // Trạng thái động cơ Trạm 1
        private void Station1_Run_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_DC1Runing.Visible = true; Pic_DC1_Over.Visible = false; Pic_DC1Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_DC1_Over.Visible = false; Pic_DC1Stop.Visible = false; Pic_DC1Runing.Visible = false; });
        }
        private void Station1_Stop_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_DC1Stop.Visible = true; Pic_DC1_Over.Visible = false; Pic_DC1Runing.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_DC1_Over.Visible = false; Pic_DC1Stop.Visible = false; Pic_DC1Runing.Visible = false; });
        }
        private void Station1_Alarm_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_DC1_Over.Visible = true; Pic_DC1Runing.Visible = false; Pic_DC1Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_DC1_Over.Visible = false; Pic_DC1Stop.Visible = false; Pic_DC1Runing.Visible = false; });
        }
        // Kết thúc Trạng thái động cơ Trạm 1
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

        /// <summary>
        /// /////////////////////////////////////
        /// 
        private void S3_DC3_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_DC3Runing.Visible = true; Pic_DC3_Over.Visible = false; Pic_DC3Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_DC3_Over.Visible = false; Pic_DC3Stop.Visible = true; Pic_DC3Runing.Visible = false; });
        }
        private void S3_DC2_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_DC2Runing.Visible = true; Pic_DC2_Over.Visible = false; Pic_DC2Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_DC2_Over.Visible = false; Pic_DC2Stop.Visible = true; Pic_DC2Runing.Visible = false; });
        }
        private void S3_DC1_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_DC1Runing.Visible = true; Pic_DC1_Over.Visible = false; Pic_DC1Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_DC1_Over.Visible = false; Pic_DC1Stop.Visible = true; Pic_DC1Runing.Visible = false; });
        }
        private void S2_DC3_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_DC3Runing.Visible = true; Pic_DC3_Over.Visible = false; Pic_DC3Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_DC3_Over.Visible = false; Pic_DC3Stop.Visible = true; Pic_DC3Runing.Visible = false; });
        }
        private void S2_DC2_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_DC2Runing.Visible = true; Pic_DC2_Over.Visible = false; Pic_DC2Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_DC2_Over.Visible = false; Pic_DC2Stop.Visible = true; Pic_DC2Runing.Visible = false; });
        }
        private void S2_DC1_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_DC1Runing.Visible = true; Pic_DC1_Over.Visible = false; Pic_DC1Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_DC1_Over.Visible = false; Pic_DC1Stop.Visible = true; Pic_DC1Runing.Visible = false; });
        }
        private void S1_DC3_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_DC1Runing.Visible = true; Pic_DC1_Over.Visible = false; Pic_DC1Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_DC1_Over.Visible = false; Pic_DC1Stop.Visible = true; Pic_DC1Runing.Visible = false; });
        }
        private void S1_DC2_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_DC1Runing.Visible = true; Pic_DC1_Over.Visible = false; Pic_DC1Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_DC1_Over.Visible = false; Pic_DC1Stop.Visible = true; Pic_DC1Runing.Visible = false; });
        }
        private void S1_DC1_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_DC1Runing.Visible = true; Pic_DC1_Over.Visible = false; Pic_DC1Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_DC1_Over.Visible = false; Pic_DC1Stop.Visible = true; Pic_DC1Runing.Visible = false; });
        }               
        //private void _btnGhiStatus_Click(object sender, EventArgs e)
        //{
        //    using (IDbConnection dbb = new SqlConnection(connectionString))
        //    {
        //        dbb.Open();
        //        // doc data để lấy Id hiện tại
        //        string sqlQuery = "SELECT * FROM TrangThaiTram";
        //        List<DataTrangThaiTramModel> data = dbb.Query<DataTrangThaiTramModel>(sqlQuery).AsList();
        //        _id = (data != null && data.Any()) ? data.Max(x => x.Id) : 0;
        //        // Thêm một dòng mới
        //        //tạo dữ liệu để thêm vào
        //        var newRow = new DataTrangThaiTramModel
        //        {
        //            Id = _id + 1,
        //            CreateAt = DateTime.Now,
        //            Remote = lbl_Remote.Text,
        //            Local = lbl_Local.Text,
        //            Auto = lbl_Auto.Text,
        //            Man = lbl_Man.Text,
        //            Local_Stop = lbl_Stop.Text,                  
        //        };
        //        // tao câu quẻy
        //        string insertQuery = "INSERT INTO TrangThaiTram (Id,CreateAt,Remote, Local, Auto,Man,Local_Stop)" +
        //                                "VALUES (@Id, @CreateAt, @Remote,@Local,@Auto,@Man,@Local_Stop)";                                                                         
        //        dbb.Execute(insertQuery, newRow);
        //    }
        //}
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
