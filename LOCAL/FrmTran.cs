using Ahd.Core;
using Ahd.Winforms.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegistrationForm1
{
    public partial class FrmTran : Form
    {      
        public FrmTran()
        {        
            InitializeComponent();
            Load += FrmTran_Load;
        }
        IAhdDriverConnector driver;
        private void FrmTran_Load(object sender, EventArgs e)
        {
            driver = AhdDriverConnectorProvider.GetAhdDriverConnector();
            if (!driver.IsStarted)
                driver.Started += Driver_Started;
            else
                Driver_Started(driver, null);
        }
        private void Driver_Started(object sender, EventArgs e)
        {
            // bảng điều khiển trạm 1
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_Remote").ValueChanged += S1_Remote_ValueChanged;//Su kien áp cửa 1 cao
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_Local").ValueChanged += S1_Local_ValueChanged;//sự kiện áp cửa 1 thấp
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_Auto").ValueChanged += S1_Auto_ValueChanged;//Su kien cửa 1 đang mở
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_Man").ValueChanged += S1_Man_ValueChanged;//Su kien cửa 1 đang đóng 
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_Local_Stop").ValueChanged += S1_Local_Stop_ValueChanged;//Su kien mở hết cửa
            // bảng điều khiển trạm 2
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_Remote").ValueChanged += S2_Remote_ValueChanged;//Su kien áp cửa 1 cao
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_Local").ValueChanged += S2_Local_ValueChanged;//sự kiện áp cửa 1 thấp
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_Auto").ValueChanged += S2_Auto_ValueChanged;//Su kien cửa 1 đang mở
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_Man").ValueChanged += S2_Man_ValueChanged;//Su kien cửa 1 đang đóng 
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_Local_Stop").ValueChanged += S2_Local_Stop_ValueChanged;//Su kien mở hết cửa
           // bảng điều khiển trạm 2 3                                                                                                                  
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_Remote").ValueChanged += S3_Remote_ValueChanged;//Su kien áp cửa 1 cao
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_Local").ValueChanged += S3_Local_ValueChanged;//sự kiện áp cửa 1 thấp
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_Auto").ValueChanged += S3_Auto_ValueChanged;//Su kien cửa 1 đang mở
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_Man").ValueChanged += S3_Man_ValueChanged;//Su kien cửa 1 đang đóng 
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_Local_Stop").ValueChanged += S3_Local_Stop_ValueChanged;//Su kien mở hết cửa
            // Tràn 1
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_PressureHigh").ValueChanged += Door1_PressureHigh_ValueChanged;//Su kien áp cửa 1 cao
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_PressureLow").ValueChanged += Door1_PressureLow_ValueChanged;//sự kiện áp cửa 1 thấp
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Opening").ValueChanged += Door1_Opening_ValueChanged;//Su kien cửa 1 đang mở
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Closing").ValueChanged += Door1_Closing_ValueChanged;//Su kien cửa 1 đang đóng 
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Open").ValueChanged += Door1_Open_ValueChanged;//Su kien mở hết cửa 1
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Close").ValueChanged += Door1_Close_ValueChanged;//Su kien đóng hết cửa 1
            // Tràn 2
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_1Open").ValueChanged += Doorlock2_1Open_ValueChanged;//Su kien Chốt bên trái cửa 2 mở hết
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_1Close").ValueChanged += Doorlock2_1Close_ValueChanged;//Chốt bên trái cửa 2 đóng hết
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_2Open").ValueChanged += Doorlock2_2Open_ValueChanged;//Su kien Chốt bên trái cửa 2 mở hết
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_2Close").ValueChanged += Doorlock2_2Close_ValueChanged;//Chốt bên trái cửa 2 đóng hết          
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_Opening").ValueChanged += Door2_Opening_ValueChanged;//Su kien cửa 2 đang mở
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_Closing").ValueChanged += Door2_Closing_ValueChanged;//Sự kiện cửa 2 đang đóng
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_Open").ValueChanged += Door2_Open_ValueChanged;//Su kien Chốt bên trái cửa 2 mở hết
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_Close").ValueChanged += Door2_Close_ValueChanged;//Chốt bên trái cửa 2 đóng hết
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_PressureHigh").ValueChanged += Door2_PressureHigh_ValueChanged;//Su kien áp cửa 1 cao
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_PressureLow").ValueChanged += Door2_PressureLow_ValueChanged;//sự kiện áp cửa 1 thấp
            // Tràn 3
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock3_1Open").ValueChanged += Doorlock3_1Open_ValueChanged;//Su kien Chốt bên trái cửa 3 mở hết
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock3_1Close").ValueChanged += Doorlock3_1Close_ValueChanged;//Chốt bên trái cửa 3 đóng hết
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock3_2Open").ValueChanged += Doorlock3_2Open_ValueChanged;//Su kien Chốt bên trái cửa 3 mở hết
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock3_2Close").ValueChanged += Doorlock3_2Close_ValueChanged;//Chốt bên trái cửa 3 đóng hết
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_Opening").ValueChanged += Door3_Opening_ValueChanged;//Su kien cửa 3 đang mở
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_Closing").ValueChanged += Door3_Closing_ValueChanged;//Su kien cửa 3 đang đóng 
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_Open").ValueChanged += Door3_Open_ValueChanged;//Su kien mở hết cửa 3
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_Close").ValueChanged += Door3_Close_ValueChanged;//Su kien đóng hết cửa 3
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_PressureHigh").ValueChanged += Door3_PressureHigh_ValueChanged;//Su kien áp cửa 1 cao
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_PressureLow").ValueChanged += Door3_PressureLow_ValueChanged;//sự kiện áp cửa 1 thấp
            // End Tràn 3
            //Tràn 4
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock4_1Open").ValueChanged += Doorlock4_1Open_ValueChanged;//Su kien Chốt bên trái cửa 4 mở hết
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock4_1Close").ValueChanged += Doorlock4_1Close_ValueChanged;//Chốt bên trái cửa 4 đóng hết
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock4_2Open").ValueChanged += Doorlock4_2Open_ValueChanged;//Su kien Chốt bên trái cửa 4 mở hết
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock4_2Close").ValueChanged += Doorlock4_2Close_ValueChanged;//Chốt bên trái cửa 4 đóng hết
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_Opening").ValueChanged += Door4_Opening_ValueChanged;//Su kien cửa 4 đang mở
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_Closing").ValueChanged += Door4_Closing_ValueChanged;//Su kien cửa 4 đang đóng 
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_Open").ValueChanged += Door4_Open_ValueChanged;//Su kien mở hết cửa 4
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_Close").ValueChanged += Door4_Close_ValueChanged;//Su kien đóng hết cửa 4
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_PressureHigh").ValueChanged += Door4_PressureHigh_ValueChanged;//Su kien áp cửa 4 cao
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_PressureLow").ValueChanged += Door4_PressureLow_ValueChanged;//sự kiện áp cửa 4 thấp
            // End Tràn 4
            //Tràn 5        
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock5_1Open").ValueChanged += Doorlock5_1Open_ValueChanged;//Su kien Chốt bên trái cửa 5 mở hết
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock5_1Close").ValueChanged += Doorlock5_1Close_ValueChanged;//Chốt bên trái cửa 5 đóng hết
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock5_2Open").ValueChanged += Doorlock5_2Open_ValueChanged;//Su kien Chốt bên trái cửa 5 mở hết
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock5_2Close").ValueChanged += Doorlock5_2Close_ValueChanged;//Chốt bên trái cửa 5 đóng hết
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_Opening").ValueChanged += Door5_Opening_ValueChanged;//Su kien cửa 5 đang mở
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_Closing").ValueChanged += Door5_Closing_ValueChanged;//Su kien cửa 5 đang đóng 
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_Open").ValueChanged += Door5_Open_ValueChanged;//Su kien mở hết cửa 5
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_Close").ValueChanged += Door5_Close_ValueChanged;//Su kien đóng hết cửa 5
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_PressureHigh").ValueChanged += Door5_PressureHigh_ValueChanged;//Su kien áp cửa 5 cao
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_PressureLow").ValueChanged += Door5_PressureLow_ValueChanged;//sự kiện áp cửa 5 thấp
            //End Tràn 5
            //Tràn 6
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_PressureHigh").ValueChanged += Door6_PressureHigh_ValueChanged;//Su kien áp cửa 6 cao
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_PressureLow").ValueChanged += Door6_PressureLow_ValueChanged;//sự kiện áp cửa 6 thấp
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_Opening").ValueChanged += Door6_Opening_ValueChanged;//Su kien cửa 6 đang mở
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_Closing").ValueChanged += Door6_Closing_ValueChanged;//Su kien cửa 6 đang đóng 
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_Open").ValueChanged += Door6_Open_ValueChanged;//Su kien mở hết cửa 6
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_Close").ValueChanged += Door6_Close_ValueChanged;//Su kien đóng hết cửa 6
            //End Tràn 6
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_DC1_Running").ValueChanged += S1_DC1_Running_ValueChanged;//Su kien trạm 1 động cơ 1 đang chạy        
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_DC2_Running").ValueChanged += S1_DC2_Running_ValueChanged;//Su kien trạm 1 động cơ 2 đang chạy       
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_DC3_Running").ValueChanged += S1_DC3_Running_ValueChanged;//Su kien trạm 1 động cơ 3 đang chạy
            // Trạng thái lổi động cơ Trạm 1
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_DC1_Over").ValueChanged += S1_DC1_Over_ValueChanged;//Su kien lổi động cơ 1        
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_DC2_Over").ValueChanged += S1_DC2_Over_ValueChanged;//Su kien lổi động cơ 2         
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_DC3_Over").ValueChanged += S1_DC3_Over_ValueChanged;//Su kien lổi động cơ 3  
               // Trạng thái động cơ Trạm 2                                                                                                               
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC1_Running").ValueChanged += S2_DC1_Running_ValueChanged;//Su kien trạm 2 động cơ 1 đang chạy        
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC2_Running").ValueChanged += S2_DC2_Running_ValueChanged;//Su kien trạm 2 động cơ 2 đang chạy       
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC3_Running").ValueChanged += S2_DC3_Running_ValueChanged;//Su kien trạm 2 động cơ 3 đang chạy
            // Trạng thái lổi động cơ Trạm 2
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC1_Over").ValueChanged += S2_DC1_Over_ValueChanged;//Su kien lổi động cơ 1        
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC2_Over").ValueChanged += S2_DC2_Over_ValueChanged;//Su kien lổi động cơ 2         
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC3_Over").ValueChanged += S2_DC3_Over_ValueChanged;//Su kien lổi động cơ 3  

            // Trạng thái động cơ Trạm 3                                                                                                              
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_DC1_Running").ValueChanged += S3_DC1_Running_ValueChanged;//Su kien trạm 3 động cơ 1 đang chạy        
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_DC2_Running").ValueChanged += S3_DC2_Running_ValueChanged;//Su kien trạm 3 động cơ 2 đang chạy       
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_DC3_Running").ValueChanged += S3_DC3_Running_ValueChanged;//Su kien trạm 3 động cơ 3 đang chạy
            // Trạng thái lổi động cơ Trạm 3
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_DC1_Over").ValueChanged += S3_DC1_Over_ValueChanged;//Su kien lổi động cơ 1        
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_DC2_Over").ValueChanged += S3_DC2_Over_ValueChanged;//Su kien lổi động cơ 2         
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_DC3_Over").ValueChanged += S3_DC3_Over_ValueChanged;//Su kien lổi động cơ 3  


            //ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_DC3_Running").ValueChanged += S3_DC3_Running_ValueChanged;//Su kien động cơ 3 đang chạy
            // Trạng thái bảng điều khiển
            S1_Remote_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_Remote"),
                             new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_Remote")
                             , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_Remote").Value));
            S1_Local_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_Local"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_Local")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_Local").Value));
            S1_Auto_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_Auto"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_Auto")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_Auto").Value));
            S1_Man_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_Man"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_Man")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_Man").Value));
            S1_Local_Stop_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_Local_Stop"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_Local_Stop")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_Local_Stop").Value));
            S2_Remote_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_Remote"),
                              new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_Remote")
                              , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_Remote").Value));
            S2_Local_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_Local"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_Local")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_Local").Value));
            S2_Auto_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_Auto"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_Auto")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_Auto").Value));
            S2_Man_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_Man"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_Man")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_Man").Value));
            S2_Local_Stop_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_Local_Stop"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_Local_Stop")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_Local_Stop").Value));
            S3_Remote_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_Remote"),
                             new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_Remote")
                             , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_Remote").Value));
            S3_Local_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_Local"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_Local")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_Local").Value));
            S3_Auto_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_Auto"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_Auto")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_Auto").Value));
            S3_Man_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_Man"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_Man")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_Man").Value));
            S3_Local_Stop_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_Local_Stop"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_Local_Stop")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_Local_Stop").Value));

            // End Trạng thái bảng điều khiển

            //Tràn 6
            Door6_PressureHigh_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_PressureHigh"),
                             new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_PressureHigh")
                             , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_PressureHigh").Value));
            Door6_PressureLow_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_PressureLow"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_PressureLow")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_PressureLow").Value));
            Door6_Opening_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_Opening"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_Opening")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_Opening").Value));
            Door6_Closing_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_Closing"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_Closing")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_Closing").Value));
            Door6_Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_Open"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_Open")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_Open").Value));
            Door6_Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_Close"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_Close")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door6_Close").Value));
            //End Tràn 6
            // Tràn 5           
            Door5_PressureHigh_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_PressureHigh"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_PressureHigh")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_PressureHigh").Value));
            Door5_PressureLow_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_PressureLow"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_PressureLow")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_PressureLow").Value));
            Door5_Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_Open"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_Open")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_Open").Value));
            Door5_Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_Close"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_Close")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_Close").Value));
            Door5_Opening_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_Opening"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_Opening")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_Opening").Value));
            Door5_Closing_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_Closing"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_Closing")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door5_Closing").Value));
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
            //End Tràn 5
            // Tràn 4
            Door4_PressureHigh_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_PressureHigh"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_PressureHigh")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_PressureHigh").Value));
            Door4_PressureLow_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_PressureLow"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_PressureLow")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_PressureLow").Value));
            Door4_Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_Open"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_Open")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_Open").Value));
            Door4_Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_Close"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_Close")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_Close").Value));
            Door4_Opening_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_Opening"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_Opening")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_Opening").Value));
            Door4_Closing_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_Closing"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_Closing")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door4_Closing").Value));
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
            //End Tràn 4
            // Tràn 3,
            Door3_PressureHigh_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_PressureHigh"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_PressureHigh")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_PressureHigh").Value));
            Door3_PressureLow_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_PressureLow"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_PressureLow")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_PressureLow").Value));
            Door3_Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_Open"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_Open")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_Open").Value));
            Door3_Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_Close"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_Close")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_Close").Value));
            Door3_Opening_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_Opening"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_Opening")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_Opening").Value));
            Door3_Closing_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_Closing"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_Closing")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door3_Closing").Value));
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

            // Tràn 1 

            Door1_PressureHigh_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_PressureHigh"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_PressureHigh")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_PressureHigh").Value));
            Door1_PressureLow_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_PressureLow"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_PressureLow")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_PressureLow").Value));
            Door1_Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Open"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Open")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Open").Value));
            Door1_Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Close"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Close")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Close").Value));
            Door1_Opening_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Opening"),
                             new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Opening")
                             , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Opening").Value));
            Door1_Closing_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Closing"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Closing")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Closing").Value));
            // Tràn 2

            Door2_Opening_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_Opening"),
                           new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_Opening")
                           , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_Opening").Value));
            Door2_Closing_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_Closing"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_Closing")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_Closing").Value));
            Door2_Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_Open"),
                           new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_Open")
                           , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_Open").Value));
            Door2_Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_Close"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_Close")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_Close").Value));
            Door2_PressureHigh_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_PressureHigh"),
                          new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_PressureHigh")
                          , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_PressureHigh").Value));
            Door2_PressureLow_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_PressureLow"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_PressureLow")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door2_PressureLow").Value)); 
            Doorlock2_2Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_2Open"),
                           new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_2Open")
                           , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_2Open").Value));
            Doorlock2_2Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_2Close"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_2Close")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_2Close").Value));
            Doorlock2_1Open_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_1Open"),
                           new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_1Open")
                           , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_1Open").Value));
            Doorlock2_1Close_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_1Close"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_1Close")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_1Close").Value));
//////////////////////////////////

            S3_DC1_Over_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_DC1_Over"),
                           new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_DC1_Over")
                           , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_DC1_Over").Value));
            S3_DC2_Over_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_DC2_Over"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_DC2_Over")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_DC2_Over").Value));
            S3_DC3_Over_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_DC3_Over"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_DC3_Over")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_DC3_Over").Value));
            S3_DC1_Running_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_DC1_Running"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_DC1_Running")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_DC1_Running").Value));
            S3_DC2_Running_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_DC2_Running"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_DC2_Running")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_DC2_Running").Value));
            S3_DC3_Running_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_DC3_Running"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_DC3_Running")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S3_DC3_Running").Value));


            S2_DC1_Over_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC1_Over"),
                             new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC1_Over")
                             , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC1_Over").Value));
            S2_DC2_Over_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC2_Over"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC2_Over")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC2_Over").Value));
            S2_DC3_Over_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC3_Over"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC3_Over")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC3_Over").Value));
            S2_DC1_Running_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC1_Running"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC1_Running")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC1_Running").Value));
            S2_DC2_Running_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC2_Running"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC2_Running")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC2_Running").Value));
            S2_DC3_Running_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC3_Running"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC3_Running")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S2_DC3_Running").Value));

            S1_DC1_Over_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_DC1_Over"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_DC1_Over")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_DC1_Over").Value));
            S1_DC2_Over_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_DC2_Over"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_DC2_Over")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_DC2_Over").Value));
            S1_DC3_Over_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_DC3_Over"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_DC3_Over")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_DC3_Over").Value));
            S1_DC1_Running_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_DC1_Running"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_DC1_Running")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_DC1_Running").Value));
            S1_DC2_Running_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_DC2_Running"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_DC2_Running")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_DC2_Running").Value));
            S1_DC3_Running_ValueChanged(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_DC3_Running"),
                            new TagValueChangedEventArgs(ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_DC3_Running")
                            , "", ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/S1_DC3_Running").Value));

        }
        // Bảng điều khiển 3
        private void S3_Remote_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { bnt_Remote_T5.BackColor = Color.GreenYellow; bnt_Remote_T6.BackColor = Color.GreenYellow; });
            }
            else
                this.Invoke((MethodInvoker)delegate { bnt_Remote_T5.BackColor = DefaultBackColor; bnt_Remote_T6.BackColor = DefaultBackColor; });
        }
        private void S3_Local_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { bnt_Local_T5.BackColor = Color.GreenYellow; bnt_Local_T6.BackColor = Color.GreenYellow; });
            }
            else
                this.Invoke((MethodInvoker)delegate { bnt_Local_T5.BackColor = DefaultBackColor; bnt_Local_T6.BackColor = DefaultBackColor; });
        }
        private void S3_Auto_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { bnt_Auto_T5.BackColor = Color.GreenYellow; bnt_Auto_T6.BackColor = Color.GreenYellow; });
            }
            else
                this.Invoke((MethodInvoker)delegate { bnt_Auto_T5.BackColor = DefaultBackColor; bnt_Auto_T6.BackColor = DefaultBackColor; });
        }
        private void S3_Man_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { bnt_Hand_T5.BackColor = Color.GreenYellow; bnt_Hand_T6.BackColor = Color.GreenYellow; });
            }
            else
                this.Invoke((MethodInvoker)delegate { bnt_Hand_T5.BackColor = DefaultBackColor; bnt_Hand_T6.BackColor = DefaultBackColor; });
        }
        private void S3_Local_Stop_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { bnt_Estop_T5.BackColor = Color.GreenYellow; bnt_Estop_T6.BackColor = Color.GreenYellow; });
            }
            else
                this.Invoke((MethodInvoker)delegate { bnt_Estop_T5.BackColor = DefaultBackColor; bnt_Estop_T6.BackColor = DefaultBackColor; });
        }
        // End bảng điều khiển 3
        // Bảng điều khiển 2
        private void S2_Remote_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { bnt_Remote_T3.BackColor = Color.GreenYellow; bnt_Remote_T4.BackColor = Color.GreenYellow; });
            }
            else
                this.Invoke((MethodInvoker)delegate { bnt_Remote_T3.BackColor = DefaultBackColor; bnt_Remote_T4.BackColor = DefaultBackColor; });
        }
        private void S2_Local_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { bnt_Local_T3.BackColor = Color.GreenYellow; bnt_Local_T4.BackColor = Color.GreenYellow; });
            }
            else
                this.Invoke((MethodInvoker)delegate { bnt_Local_T3.BackColor = DefaultBackColor; bnt_Local_T4.BackColor = DefaultBackColor; });
        }
        private void S2_Auto_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { bnt_Auto_T3.BackColor = Color.GreenYellow; bnt_Auto_T4.BackColor = Color.GreenYellow; });
            }
            else
                this.Invoke((MethodInvoker)delegate { bnt_Auto_T3.BackColor = DefaultBackColor; bnt_Auto_T4.BackColor = DefaultBackColor; });
        }
        private void S2_Man_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { bnt_Hand_T3.BackColor = Color.GreenYellow; bnt_Hand_T4.BackColor = Color.GreenYellow; });
            }
            else
                this.Invoke((MethodInvoker)delegate { bnt_Hand_T3.BackColor = DefaultBackColor; bnt_Hand_T4.BackColor = DefaultBackColor; });
        }
        private void S2_Local_Stop_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { bnt_Estop_T3.BackColor = Color.GreenYellow; bnt_Estop_T4.BackColor = Color.GreenYellow; });
            }
            else
                this.Invoke((MethodInvoker)delegate { bnt_Estop_T3.BackColor = DefaultBackColor; bnt_Estop_T4.BackColor = DefaultBackColor; });
        }
        // End Bảng điều khiển 2
        // Bảng điều khiển 1,
        private void S1_Remote_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { bnt_Remote_T1.BackColor = Color.GreenYellow;bnt_Remote_T2.BackColor = Color.GreenYellow;  });
            }
            else
                this.Invoke((MethodInvoker)delegate { bnt_Remote_T1.BackColor = DefaultBackColor;bnt_Remote_T2.BackColor = DefaultBackColor; });
        }
        private void S1_Local_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { bnt_Local_T1.BackColor = Color.GreenYellow; bnt_Local_T2.BackColor = Color.GreenYellow; });
            }
            else
                this.Invoke((MethodInvoker)delegate { bnt_Local_T1.BackColor = DefaultBackColor; bnt_Local_T2.BackColor = DefaultBackColor; });
        }
        private void S1_Auto_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { bnt_Auto_T1.BackColor = Color.GreenYellow; bnt_Auto_T2.BackColor = Color.GreenYellow; });
            }
            else
                this.Invoke((MethodInvoker)delegate { bnt_Auto_T1.BackColor = DefaultBackColor; bnt_Auto_T2.BackColor = DefaultBackColor; });
        }
        private void S1_Man_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { bnt_Hand_T1.BackColor = Color.GreenYellow; bnt_Hand_T2.BackColor = Color.GreenYellow; });
            }
            else
                this.Invoke((MethodInvoker)delegate { bnt_Hand_T1.BackColor = DefaultBackColor; bnt_Hand_T2.BackColor = DefaultBackColor; });
        }
        private void S1_Local_Stop_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { bnt_Estop_T1.BackColor = Color.GreenYellow; bnt_Estop_T2.BackColor = Color.GreenYellow; });
            }
            else
                this.Invoke((MethodInvoker)delegate { bnt_Estop_T1.BackColor = DefaultBackColor; bnt_Estop_T2.BackColor = DefaultBackColor; });
        }

        // End Bảng điều khiển 1

        //Tràn 6
        private void Door6_PressureLow_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door6_PressureLow.Visible = true; Pic_Door6_PressureLow_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door6_PressureLow_Stop.Visible = true; Pic_Door6_PressureLow.Visible = false; });
        }
        private void Door6_PressureHigh_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door6_PressureHigh.Visible = true; Pic_Door6_PressureHigh_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door6_PressureHigh_Stop.Visible = true; Pic_Door6_PressureHigh.Visible = false; });
        }


        private void Door6_Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door6_Close.Visible = true; Pic_Door6_Close_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door6_Close_Stop.Visible = true; Pic_Door6_Close.Visible = false; });
        }

        private void Door6_Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door6_Open.Visible = true; Pic_Door6_Open_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door6_Open_Stop.Visible = true; Pic_Door6_Open.Visible = false; });
        }


        private void Door6_Closing_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door6_Closing.Visible = true; Pic_Door6_Closing_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door6_Closing_Stop.Visible = true; Pic_Door6_Closing.Visible = false; });
        }
        private void Door6_Opening_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door6_Opening.Visible = true; Pic_Door6_Opening_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door6_Opening_Stop.Visible = true; Pic_Door6_Opening.Visible = false; });
        }
        //End Tràn 6
        // Tràn 5
        private void Door5_PressureHigh_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door5_PressureHigh.Visible = true; Pic_Door5_PressureHigh_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door5_PressureHigh_Stop.Visible = true; Pic_Door5_PressureHigh.Visible = false; });
        }
        private void Door5_PressureLow_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door5_PressureLow.Visible = true; Pic_Door5_PressureLow_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door5_PressureLow_Stop.Visible = true; Pic_Door5_PressureLow.Visible = false; });
        }
        private void Door5_Opening_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door5_Opening.Visible = true; Pic_Door5_Opening_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door5_Opening_Stop.Visible = true; Pic_Door5_Opening.Visible = false; });
        }
        private void Door5_Closing_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door5_Closing.Visible = true; Pic_Door5_Closing_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door5_Closing_Stop.Visible = true; Pic_Door5_Closing.Visible = false; });
        }

        private void Door5_Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door5_Open.Visible = true; Pic_Door5_Open_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door5_Open_Stop.Visible = true; Pic_Door5_Open.Visible = false; });
        }
        private void Door5_Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door5_Close.Visible = true; Pic_Door5_Close_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door5_Close_Stop.Visible = true; Pic_Door5_Close.Visible = false; });
        }

        private void Doorlock5_2Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock5_2Open.Visible = true; Pic_Doorlock5_2Open_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock5_2Open_Stop.Visible = true; Pic_Doorlock5_2Open.Visible = false; });
        }
        private void Doorlock5_2Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock5_2Close.Visible = true; Pic_Doorlock5_2Close_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock5_2Close_Stop.Visible = true; Pic_Doorlock5_2Close.Visible = false; });
        }
        private void Doorlock5_1Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock5_1Open.Visible = true; Pic_Doorlock5_1Open_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock5_1Open_Stop.Visible = true; Pic_Doorlock5_1Open.Visible = false; });
        }
        private void Doorlock5_1Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock5_1Close.Visible = true; Pic_Doorlock5_1Close_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock5_1Close_Stop.Visible = true; Pic_Doorlock5_1Close.Visible = false; });
        }
        //End Tràn 5
        //Tràn 4
        private void Door4_PressureHigh_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door4_PressureHigh.Visible = true; Pic_Door4_PressureHigh_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door4_PressureHigh_Stop.Visible = true; Pic_Door4_PressureHigh.Visible = false; });
        }
        private void Door4_PressureLow_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door4_PressureLow.Visible = true; Pic_Door4_PressureLow_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door4_PressureLow_Stop.Visible = true; Pic_Door4_PressureLow.Visible = false; });
        }
        private void Door4_Opening_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door4_Opening.Visible = true; Pic_Door4_Opening_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door4_Opening_Stop.Visible = true; Pic_Door4_Opening.Visible = false; });
        }
        private void Door4_Closing_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door4_Closing.Visible = true; Pic_Door4_Closing_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door4_Closing_Stop.Visible = true; Pic_Door4_Closing.Visible = false; });
        }

        private void Door4_Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door4_Open.Visible = true; Pic_Door4_Open_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door4_Open_Stop.Visible = true; Pic_Door4_Open.Visible = false; });
        }
        private void Door4_Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door4_Close.Visible = true; Pic_Door4_Close_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door4_Close_Stop.Visible = true; Pic_Door4_Close.Visible = false; });
        }

        private void Doorlock4_2Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock4_2Open.Visible = true; Pic_Doorlock4_2Open_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock4_2Open_Stop.Visible = true; Pic_Doorlock4_2Open.Visible = false; });
        }
        private void Doorlock4_2Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock4_2Close.Visible = true; Pic_Doorlock4_2Close_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock4_2Close_Stop.Visible = true; Pic_Doorlock4_2Close.Visible = false; });
        }
        private void Doorlock4_1Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock4_1Open.Visible = true; Pic_Doorlock4_1Open_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock4_1Open_Stop.Visible = true; Pic_Doorlock4_1Open.Visible = false; });
        }
        private void Doorlock4_1Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock4_1Close.Visible = true; Pic_Doorlock4_1Close_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock4_1Close_Stop.Visible = true; Pic_Doorlock4_1Close.Visible = false; });
        }
        // End Tràn 4

        // Tràn 2
        private void Door2_PressureHigh_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door2_PressureHigh.Visible = true; Pic_Door2_PressureHigh_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door2_PressureHigh_Stop.Visible = true; Pic_Door2_PressureHigh.Visible = false; });
        }
        private void Door2_PressureLow_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door2_PressureLow.Visible = true; Pic_Door2_PressureLow_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door2_PressureLow_Stop.Visible = true; Pic_Door2_PressureLow.Visible = false; });
        }
        private void Door2_Opening_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door2_Opening.Visible = true; Pic_Door2_Opening_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door2_Opening_Stop.Visible = true; Pic_Door2_Opening.Visible = false; });
        }
        private void Door2_Closing_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door2_Closing.Visible = true; Pic_Door2_Closing_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door2_Closing_Stop.Visible = true; Pic_Door2_Closing.Visible = false; });
        }

        private void Door2_Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door2_Open.Visible = true; Pic_Door2_Open_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door2_Open_Stop.Visible = true; Pic_Door2_Open.Visible = false; });
        }
        private void Door2_Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door2_Close.Visible = true; Pic_Door2_Close_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door2_Close_Stop.Visible = true; Pic_Door2_Close.Visible = false; });
        }

        // Tràn 3,

        private void Door3_PressureHigh_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door3_PressureHigh.Visible = true; Pic_Door3_PressureHigh_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door3_PressureHigh_Stop.Visible = true; Pic_Door3_PressureHigh.Visible = false; });
        }
        private void Door3_PressureLow_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door3_PressureLow.Visible = true; Pic_Door3_PressureLow_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door3_PressureLow_Stop.Visible = true; Pic_Door3_PressureLow.Visible = false; });
        }
        private void Door3_Opening_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door3_Opening.Visible = true; Pic_Door3_Opening_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door3_Opening_Stop.Visible = true; Pic_Door3_Opening.Visible = false; });
        }
        private void Door3_Closing_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door3_Closing.Visible = true; Pic_Door3_Closing_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door3_Closing_Stop.Visible = true; Pic_Door3_Closing.Visible = false; });
        }

        private void Door3_Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door3_Open.Visible = true; Pic_Door3_Open_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door3_Open_Stop.Visible = true; Pic_Door3_Open.Visible = false; });
        }
        private void Door3_Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door3_Close.Visible = true; Pic_Door3_Close_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door3_Close_Stop.Visible = true; Pic_Door3_Close.Visible = false; });
        }

        private void Doorlock3_2Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock3_2Open.Visible = true; Pic_Doorlock3_2Open_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock3_2Open_Stop.Visible = true; Pic_Doorlock3_2Open.Visible = false; });
        }
        private void Doorlock3_2Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock3_2Close.Visible = true; Pic_Doorlock3_2Close_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock3_2Close_Stop.Visible = true; Pic_Doorlock3_2Close.Visible = false; });
        }
        private void Doorlock3_1Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock3_1Open.Visible = true; Pic_Doorlock3_1Open_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock3_1Open_Stop.Visible = true; Pic_Doorlock3_1Open.Visible = false; });
        }
        private void Doorlock3_1Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock3_1Close.Visible = true; Pic_Doorlock3_1Close_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock3_1Close_Stop.Visible = true; Pic_Doorlock3_1Close.Visible = false; });
        }


        // Hết Tràn 3,4

        private void Door1_PressureLow_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door1_PressureLow.Visible = true; Pic_Door1_PressureLow_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door1_PressureLow_Stop.Visible = true; Pic_Door1_PressureLow.Visible = false; });
        }
        private void Door1_PressureHigh_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door1_PressureHigh.Visible = true; Pic_Door1_PressureHigh_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door1_PressureHigh_Stop.Visible = true; Pic_Door1_PressureHigh.Visible = false; });
        }


        private void Door1_Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door1_Close.Visible = true; Pic_Door1_Close_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door1_Close_Stop.Visible = true; Pic_Door1_Close.Visible = false; });
        }

        private void Door1_Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door1_Open.Visible = true; Pic_Door1_Open_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door1_Open_Stop.Visible = true; Pic_Door1_Open.Visible = false; });
        }


        private void Door1_Closing_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door1_Closing.Visible = true;Pic_Door1_Closing_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door1_Closing_Stop.Visible = true; Pic_Door1_Closing.Visible = false; });
        }
        private void Door1_Opening_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door1_Opening.Visible = true;Pic_Door1_Opening_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door1_Opening_Stop.Visible = true; Pic_Door1_Opening.Visible = false; });
        }

        private void Doorlock2_2Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock2_2Open.Visible = true; Pic_Doorlock2_2Open_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock2_2Open_Stop.Visible = true; Pic_Doorlock2_2Open.Visible = false; });
        }
        private void Doorlock2_2Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock2_2Close.Visible = true; Pic_Doorlock2_2Close_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock2_2Close_Stop.Visible = true; Pic_Doorlock2_2Close.Visible = false; });
        }

        private void Doorlock2_1Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock2_1Open.Visible = true;  Pic_Doorlock2_1Open_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock2_1Open_Stop.Visible = true; Pic_Doorlock2_1Open.Visible = false; });
        }
        private void Doorlock2_1Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock2_1Close.Visible = true; Pic_Doorlock2_1Close_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock2_1Close_Stop.Visible = true; Pic_Doorlock2_1Close.Visible = false; });
        }




        // Trạng thái lổi Trạm 3
        private void S3_DC3_Over_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_S3_DC3_Over.Visible = true; PicT6_S3_DC3_Over.Visible = true; Pic_S3_DC3_Over_Stop.Visible = false; PicT6_S3_DC3_Over_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_S3_DC3_Over_Stop.Visible = true; PicT6_S3_DC3_Over_Stop.Visible = true; Pic_S3_DC3_Over.Visible = false; PicT6_S3_DC3_Over.Visible = false; });
        }
        private void S3_DC2_Over_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_S3_DC2_Over.Visible = true; PicT6_S3_DC2_Over.Visible = true; Pic_S3_DC2_Over_Stop.Visible = false; PicT6_S3_DC2_Over_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_S3_DC2_Over_Stop.Visible = true; PicT6_S3_DC2_Over_Stop.Visible = true; Pic_S3_DC2_Over.Visible = false; PicT6_S3_DC2_Over.Visible = false; });
        }
        private void S3_DC1_Over_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_S3_DC1_Over.Visible = true; PicT6_S3_DC1_Over.Visible = true; Pic_S3_DC1_Over_Stop.Visible = false; PicT6_S3_DC1_Over_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_S3_DC1_Over_Stop.Visible = true; PicT6_S3_DC1_Over_Stop.Visible = true; Pic_S3_DC1_Over.Visible = false; PicT6_S3_DC1_Over.Visible = false; });
        }

        private void S3_DC3_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_S3_DC3_Running.Visible = true; PicT6_S3_DC3_Running.Visible = true; Pic_S3_DC3_Stop.Visible = false; PicT6_S3_DC3_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_S3_DC3_Stop.Visible = true; PicT6_S3_DC3_Stop.Visible = true; Pic_S3_DC3_Running.Visible = false; PicT6_S3_DC3_Running.Visible = false; });
        }
        private void S3_DC2_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_S3_DC2_Running.Visible = true; PicT6_S3_DC2_Running.Visible = true; Pic_S3_DC2_Stop.Visible = false; PicT6_S3_DC2_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_S3_DC2_Stop.Visible = true; PicT6_S3_DC2_Stop.Visible = true; Pic_S3_DC2_Running.Visible = false; PicT6_S3_DC2_Running.Visible = false; });
        }
        private void S3_DC1_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_S3_DC1_Running.Visible = true; PicT6_S3_DC1_Running.Visible = true; Pic_S3_DC1_Stop.Visible = false; PicT6_S3_DC1_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_S3_DC1_Stop.Visible = true; PicT6_S3_DC1_Stop.Visible = true; Pic_S3_DC1_Running.Visible = false; PicT6_S3_DC1_Running.Visible = false; });
        }




        // Trạng thái lổi Trạm 2
        private void S2_DC3_Over_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_S2_DC3_Over.Visible = true; PicT4_S2_DC3_Over.Visible = true; Pic_S2_DC3_Over_Stop.Visible = false; PicT4_S2_DC3_Over_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_S2_DC3_Over_Stop.Visible = true; PicT4_S2_DC3_Over_Stop.Visible = true; Pic_S2_DC3_Over.Visible = false; PicT4_S2_DC3_Over.Visible = false; });
        }
        private void S2_DC2_Over_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_S2_DC2_Over.Visible = true; PicT4_S2_DC2_Over.Visible = true; Pic_S2_DC2_Over_Stop.Visible = false; PicT4_S2_DC2_Over_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_S2_DC2_Over_Stop.Visible = true; PicT4_S2_DC2_Over_Stop.Visible = true; Pic_S2_DC2_Over.Visible = false; PicT4_S2_DC2_Over.Visible = false; });
        }
        private void S2_DC1_Over_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_S2_DC1_Over.Visible = true; PicT4_S2_DC1_Over.Visible = true; Pic_S2_DC1_Over_Stop.Visible = false; PicT4_S2_DC1_Over_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_S2_DC1_Over_Stop.Visible = true; PicT4_S2_DC1_Over_Stop.Visible = true; Pic_S2_DC1_Over.Visible = false; PicT4_S2_DC1_Over.Visible = false; });
        }
      
        // Running Trạm 2
        private void S2_DC1_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_S2_DC1_Running.Visible = true; PicT4_S2_DC1_Running.Visible = true; Pic_S2_DC1_Stop.Visible = false; PicT4_S2_DC1_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_S2_DC1_Stop.Visible = true; PicT4_S2_DC1_Stop.Visible = true; Pic_S2_DC1_Running.Visible = false; PicT4_S2_DC1_Running.Visible = false; });
        }
        private void S2_DC2_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_S2_DC2_Running.Visible = true; PicT4_S2_DC2_Running.Visible = true; Pic_S2_DC2_Stop.Visible = false; PicT4_S2_DC2_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_S2_DC2_Stop.Visible = true; PicT4_S2_DC2_Stop.Visible = true; Pic_S2_DC2_Running.Visible = false; PicT4_S2_DC2_Running.Visible = false; });
        }
        private void S2_DC3_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_S2_DC3_Running.Visible = true; PicT4_S2_DC3_Running.Visible = true; Pic_S2_DC3_Stop.Visible = false; PicT4_S2_DC3_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_S2_DC3_Stop.Visible = true; PicT4_S2_DC3_Stop.Visible = true; Pic_S2_DC3_Running.Visible = false; PicT4_S2_DC3_Running.Visible = false; });
        }

        private void S1_DC3_Over_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_S1_DC3_Over.Visible = true; PicT2_S1_DC3_Over.Visible = true; Pic_S1_DC3_Over_Stop.Visible = false; PicT2_S1_DC3_Over_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_S1_DC3_Over_Stop.Visible = true; PicT2_S1_DC3_Over_Stop.Visible = true; Pic_S1_DC3_Over.Visible = false; PicT2_S1_DC3_Over.Visible = false; });
        }
        private void S1_DC2_Over_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_S1_DC2_Over.Visible = true; PicT2_S1_DC2_Over.Visible = true; Pic_S1_DC2_Over_Stop.Visible = false; PicT2_S1_DC2_Over_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_S1_DC2_Over_Stop.Visible = true; PicT2_S1_DC2_Over_Stop.Visible = true; Pic_S1_DC2_Over.Visible = false; PicT2_S1_DC2_Over.Visible = false; });
        }
        private void S1_DC1_Over_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_S1_DC1_Over.Visible = true; PicT2_S1_DC1_Over.Visible = true; Pic_S1_DC1_Over_Stop.Visible = false; PicT2_S1_DC1_Over_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_S1_DC1_Over_Stop.Visible = true; PicT2_S1_DC1_Over_Stop.Visible = true; Pic_S1_DC1_Over.Visible = false; PicT2_S1_DC1_Over.Visible = false; });
        }

        private void S1_DC3_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_S1_DC3_Running.Visible = true; PicT2_S1_DC3_Running.Visible = true; Pic_S1_DC3_Stop.Visible = false; PicT2_S1_DC3_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_S1_DC3_Stop.Visible = true; PicT2_S1_DC3_Stop.Visible = true; Pic_S1_DC3_Running.Visible = false; PicT2_S1_DC3_Running.Visible = false; });
        }
        private void S1_DC2_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_S1_DC2_Running.Visible = true; PicT2_S1_DC2_Running.Visible = true; Pic_S1_DC2_Stop.Visible = false; PicT2_S1_DC2_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_S1_DC2_Stop.Visible = true; PicT2_S1_DC2_Stop.Visible = true; Pic_S1_DC2_Running.Visible = false; PicT2_S1_DC2_Running.Visible = false; });
        }
        private void S1_DC1_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_S1_DC1_Running.Visible = true;PicT2_S1_DC1_Running.Visible = true ; Pic_S1_DC1_Stop.Visible = false;PicT2_S1_DC1_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_S1_DC1_Stop.Visible = true; PicT2_S1_DC1_Stop.Visible = true; Pic_S1_DC1_Running.Visible = false; PicT2_S1_DC1_Running.Visible = false; });
        }
    }
}
