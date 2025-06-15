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
        {// Trạng thái động cơ Trạm 1
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_PressureHigh").ValueChanged += Door1_PressureHigh_ValueChanged;//Su kien áp cửa 1 cao
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_PressureLow").ValueChanged += Door1_PressureLow_ValueChanged;//sự kiện áp cửa 1 thấp

            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_1Open").ValueChanged += Doorlock2_1Open_ValueChanged;//Su kien Chốt bên trái cửa 2 mở hết
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Doorlock2_1Close").ValueChanged += Doorlock2_1Close_ValueChanged;//Chốt bên trái cửa 2 đóng hết
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Opening").ValueChanged += Door1_Opening_ValueChanged;//Su kien cửa 1 đang mở
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Closing").ValueChanged += Door1_Closing_ValueChanged;//Su kien cửa 1 đang đóng 
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Open").ValueChanged += Door1_Open_ValueChanged;//Su kien mở hết cửa 1
            ahdDriverConnector1.GetTag("Local Station/Channel1/Device1/Door1_Close").ValueChanged += Door1_Close_ValueChanged;//Su kien đóng hết cửa 1


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
