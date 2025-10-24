using Ahd.Core;
using Dapper;
using DocumentFormat.OpenXml.Drawing.Diagrams;
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
namespace RegistrationForm1
{
    public partial class FrmHome : Form
    {
        private Timer _timer = new Timer();
        public FrmHome()
        {
            InitializeComponent();
      
            _timer.Interval = 1000;
            _timer.Tick += Timer_Tick;
            _timer.Start();
          
          
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                _timer.Enabled = false; // Tạm dừng timer trong quá trình xử lý

                if (Globalvariable.RealtimeDisplays.Count == 0)
                    return; // Nếu không có dữ liệu, thoát khỏi hàm

                Globalvariable.InvokeIfRequired(this, () =>
                {
                    //Satation 1
                    var dataDisplayStation1 = Globalvariable.RealtimeDisplays.FirstOrDefault()?.Stations.FirstOrDefault(x => x.Path == "Local Station/DauTieng/S71500/Station_1");

                    if (dataDisplayStation1.Path == "Local Station/DauTieng/S71500/Station_1")
                    {
                        // _labALDoor1_Station1.Text = item.Al_Door1.ToString();
                        //  _labALDoor2_Station1.Text = item.Al_Door2.ToString();
                        _labHT_Cylinder1_1.Text = dataDisplayStation1.HT_Cylinder1_1.ToString();
                        _labHT_Cylinder1_2.Text = dataDisplayStation1.HT_Cylinder1_2.ToString();
                        _labHT_Cylinder2_1.Text = dataDisplayStation1.HT_Cylinder2_1.ToString();
                        _labHT_Cylinder2_2.Text = dataDisplayStation1.HT_Cylinder2_2.ToString();
                        _labPressure_Oil_Door1.Text = dataDisplayStation1.Pressure_Oil_Door1.ToString();
                        _labPressure_Oil_Door2.Text = dataDisplayStation1.Pressure_Oil_Door2.ToString();
                     //   _lblTemp_Oil1.Text = dataDisplayStation1.S1_Temp_Oil.ToString();
                     
                        _labDoor1_Aperture.Text = dataDisplayStation1.Door1_Aperture.ToString();
                        _labDoor2_Aperture.Text = dataDisplayStation1.Door2_Aperture.ToString();
                        _labFllow_Door1.Text = dataDisplayStation1.Q_i_1.ToString();
                        _labFllow_Door2.Text = dataDisplayStation1.Q_i_2.ToString();

                    }
 
                    if (dataDisplayStation1?.Doorlock2_1Open == true)
                    {
                        Pic_Doorlock2_1Open.Visible = true; 
                    }
                    else { Pic_Doorlock2_1Open.Visible = false; }
                    if (dataDisplayStation1?.Doorlock2_1Close == true)
                    {
                        Pic_Doorlock2_1Close.Visible = true;
                    }
                    else { Pic_Doorlock2_1Close.Visible = false; }
                    if (dataDisplayStation1?.Doorlock2_2Open == true)
                    {
                        Pic_Doorlock2_2Open.Visible = true;
                    }
                    else { Pic_Doorlock2_2Open.Visible = false; }
                    if (dataDisplayStation1?.Doorlock2_2Close == true)
                    {
                        Pic_Doorlock2_2Close.Visible = true;
                    }
                    else { Pic_Doorlock2_2Close.Visible = false; }


                    var s = dataDisplayStation1; // Gọn hơn

                    if (s?.Door1_Opening == true)
                    {
                        Pic_Door1_Opening.Visible = true;
                        Pic_Door1_Closing.Visible = false;

                        lblCua1.Text = "Đang mở";
                        lblCua1.ForeColor = Color.Green;
                        lblCua1.BackColor = Color.YellowGreen;
                    }
                    else if (s?.Door1_Closing == true)
                    {
                        Pic_Door1_Opening.Visible = false;
                        Pic_Door1_Closing.Visible = true;

                        lblCua1.Text = "Đang đóng";
                        lblCua1.ForeColor = Color.Black;
                        lblCua1.BackColor = Color.Red;
                    }
                    else if (s?.Door1_Close == false)
                    {
                        Pic_Door1_Opening.Visible = false;
                        Pic_Door1_Closing.Visible = false;

                        lblCua1.Text = "Cửa mở";
                        lblCua1.ForeColor = Color.Green;
                        lblCua1.BackColor = Color.YellowGreen;
                    }
                    else // Ngược lại là cửa đóng
                    {
                        Pic_Door1_Opening.Visible = false;
                        Pic_Door1_Closing.Visible = false;

                        lblCua1.Text = "Cửa đóng";
                        lblCua1.ForeColor = Color.Black;
                        lblCua1.BackColor = Color.Red;
                    }

                    // cửa 2
                    if (s?.Door2_Opening == true)
                    {
                        Pic_Door2_Opening.Visible = true;
                        Pic_Door2_Closing.Visible = false;

                        lblCua2.Text = "Đang mở";
                        lblCua2.ForeColor = Color.Green;
                        lblCua2.BackColor = Color.YellowGreen;
                    }
                    else if (s?.Door2_Closing == true)
                    {
                        Pic_Door2_Opening.Visible = false;
                        Pic_Door2_Closing.Visible = true;

                        lblCua2.Text = "Đang đóng";
                        lblCua2.ForeColor = Color.Black;
                        lblCua2.BackColor = Color.Red;
                    }
                    else if (s?.Door2_Close == false)
                    {
                        Pic_Door2_Opening.Visible = false;
                        Pic_Door2_Closing.Visible = false;

                        lblCua2.Text = "Cửa mở";
                        lblCua2.ForeColor = Color.Green;
                        lblCua2.BackColor = Color.YellowGreen;
                    }
                    else // Ngược lại là cửa đóng
                    {
                        Pic_Door2_Opening.Visible = false;
                        Pic_Door2_Closing.Visible = false;

                        lblCua2.Text = "Cửa đóng";
                        lblCua2.ForeColor = Color.Black;
                        lblCua2.BackColor = Color.Red;
                    }

                    //Station 2
                    var dataDisplayStation2 = Globalvariable.RealtimeDisplays.FirstOrDefault()?.Stations.FirstOrDefault(x => x.Path == "Local Station/DauTieng/S71500/Station_2");
                    
                    if (dataDisplayStation2.Path == "Local Station/DauTieng/S71500/Station_2")
                    {
                        _labHT_Cylinder3_1.Text = dataDisplayStation2.HT_Cylinder1_1.ToString();
                        _labHT_Cylinder3_2.Text = dataDisplayStation2.HT_Cylinder1_2.ToString();
                        _labHT_Cylinder4_1.Text = dataDisplayStation2.HT_Cylinder2_1.ToString();
                        _labHT_Cylinder4_2.Text = dataDisplayStation2.HT_Cylinder2_2.ToString();
                        _labPressure_Oil_Door3.Text = dataDisplayStation2.Pressure_Oil_Door1.ToString();
                        _labPressure_Oil_Door4.Text = dataDisplayStation2.Pressure_Oil_Door2.ToString();
                   //     _lblTemp_Oil2.Text = dataDisplayStation2.S1_Temp_Oil.ToString();
                        
                        _labDoor3_Aperture.Text = dataDisplayStation2.Door1_Aperture.ToString();
                        _labDoor4_Aperture.Text = dataDisplayStation2.Door2_Aperture.ToString();
                        _labFllow_Door3.Text = dataDisplayStation2.Q_i_1.ToString();
                        _labFllow_Door4.Text = dataDisplayStation2.Q_i_2.ToString();
                    }

                    if (dataDisplayStation2?.Doorlock1_1Open == true)
                    {
                        Pic_Doorlock3_1Open.Visible = true;
                    }
                    else { Pic_Doorlock3_1Open.Visible = false; }
                    if (dataDisplayStation2?.Doorlock1_1Close == true)
                    {
                        Pic_Doorlock3_1Close.Visible = true;
                    }
                    else { Pic_Doorlock3_1Close.Visible = false; }
                    if (dataDisplayStation2?.Doorlock1_2Open == true)
                    {
                        Pic_Doorlock3_2Open.Visible = true;
                    }
                    else { Pic_Doorlock3_2Open.Visible = false; }
                    if (dataDisplayStation2?.Doorlock1_2Close == true)
                    {
                        Pic_Doorlock3_2Close.Visible = true;
                    }
                    else { Pic_Doorlock3_2Close.Visible = false; }
                    if (dataDisplayStation2?.Doorlock2_1Open == true)
                    {
                        Pic_Doorlock4_1Open.Visible = true;
                    }
                    else {Pic_Doorlock4_1Open.Visible = false;  }
                    if (dataDisplayStation2?.Doorlock2_1Close == true)
                    {
                        Pic_Doorlock4_1Close.Visible = true;
                    }
                    else { Pic_Doorlock4_1Close.Visible = false; }
                    if (dataDisplayStation2?.Doorlock2_2Open == true)
                    {
                        Pic_Doorlock4_2Open.Visible = true;
                    }
                    else { Pic_Doorlock4_2Open.Visible = false; }
                    if (dataDisplayStation2?.Doorlock2_2Close == true)
                    {
                        Pic_Doorlock4_2Close.Visible = true;
                    }
                    else { Pic_Doorlock4_2Close.Visible = false; }
                   
                
                
                    var s2 = dataDisplayStation2; // gọn hơn, đỡ phải viết nhiều lần

                    if (s2?.Door1_Opening == true)
                    {
                        Pic_Door3_Opening.Visible = true;
                        Pic_Door3_Closing.Visible = false;

                        lblCua3.Text = "Đang mở";
                        lblCua3.ForeColor = Color.Green;
                        lblCua3.BackColor = Color.YellowGreen;
                    }
                    else if (s2?.Door1_Closing == true)
                    {
                        Pic_Door3_Opening.Visible = false;
                        Pic_Door3_Closing.Visible = true;

                        lblCua3.Text = "Đang đóng";
                        lblCua3.ForeColor = Color.Black;
                        lblCua3.BackColor = Color.Red;
                    }
                    else if (s2?.Door1_Close == false)
                    {
                        Pic_Door3_Opening.Visible = false;
                        Pic_Door3_Closing.Visible = false;

                        lblCua3.Text = "Cửa mở";
                        lblCua3.ForeColor = Color.Green;
                        lblCua3.BackColor = Color.YellowGreen;
                    }
                    else // Ngược lại là cửa đóng
                    {
                        Pic_Door3_Opening.Visible = false;
                        Pic_Door3_Closing.Visible = false;

                        lblCua3.Text = "Cửa đóng";
                        lblCua3.ForeColor = Color.Black;
                        lblCua3.BackColor = Color.Red;
                    }


                    // cửa 2
                    if (s2?.Door2_Opening == true)
                    {
                        Pic_Door4_Opening.Visible = true;
                        Pic_Door4_Closing.Visible = false;

                        lblCua4.Text = "Đang mở";
                        lblCua4.ForeColor = Color.Green;
                        lblCua4.BackColor = Color.YellowGreen;
                    }
                    else if (s2?.Door2_Closing == true)
                    {
                        Pic_Door4_Opening.Visible = false;
                        Pic_Door4_Closing.Visible = true;

                        lblCua4.Text = "Đang đóng";
                        lblCua4.ForeColor = Color.Black;
                        lblCua4.BackColor = Color.Red;
                    }
                    else if (s2?.Door2_Close == false)
                    {
                        Pic_Door4_Opening.Visible = false;
                        Pic_Door4_Closing.Visible = false;

                        lblCua4.Text = "Cửa mở";
                        lblCua4.ForeColor = Color.Green;
                        lblCua4.BackColor = Color.YellowGreen;
                    }
                    else // Ngược lại là cửa đóng
                    {
                        Pic_Door4_Opening.Visible = false;
                        Pic_Door4_Closing.Visible = false;

                        lblCua4.Text = "Cửa đóng";
                        lblCua4.ForeColor = Color.Black;
                        lblCua4.BackColor = Color.Red;
                    }
                    //Station 3
                    var dataDisplayStation3 = Globalvariable.RealtimeDisplays.FirstOrDefault()?.Stations.FirstOrDefault(x => x.Path == "Local Station/DauTieng/S71500/Station_3");

                     if (dataDisplayStation3.Path == "Local Station/DauTieng/S71500/Station_3")
                    {
                        _labHT_Cylinder5_1.Text = dataDisplayStation3.HT_Cylinder1_1.ToString();
                        _labHT_Cylinder5_2.Text = dataDisplayStation3.HT_Cylinder1_2.ToString();
                        _labHT_Cylinder6_1.Text = dataDisplayStation3.HT_Cylinder2_1.ToString();
                        _labHT_Cylinder6_2.Text = dataDisplayStation3.HT_Cylinder2_2.ToString();
                        _labPressure_Oil_Door5.Text = dataDisplayStation3.Pressure_Oil_Door1.ToString();
                        _labPressure_Oil_Door6.Text = dataDisplayStation3.Pressure_Oil_Door2.ToString();
                      //  _lblTemp_Oil3.Text = dataDisplayStation3.S1_Temp_Oil.ToString();
                       
                        _labDoor5_Aperture.Text = dataDisplayStation3.Door1_Aperture.ToString();
                        _labDoor6_Aperture.Text = dataDisplayStation3.Door2_Aperture.ToString();
                        _labFllow_Door5.Text = dataDisplayStation3.Q_i_1.ToString();
                        _labFllow_Door6.Text = dataDisplayStation3.Q_i_2.ToString();
                    }


                    if (dataDisplayStation3?.Doorlock1_1Open == true)
                    {
                        Pic_Doorlock5_1Open.Visible = true;
                    }
                    else { Pic_Doorlock5_1Open.Visible = false; }
                    if (dataDisplayStation3?.Doorlock1_1Close == true)
                    {
                        Pic_Doorlock5_1Close.Visible = true;
                    }
                    else { Pic_Doorlock5_1Close.Visible = false; }
                    if (dataDisplayStation3?.Doorlock1_2Open == true)
                    {
                        Pic_Doorlock5_2Open.Visible = true;
                    }
                    else { Pic_Doorlock5_2Open.Visible = false; }
                    if (dataDisplayStation3?.Doorlock1_2Close == true)
                    {
                        Pic_Doorlock5_2Close.Visible = true;
                    }
                    else { Pic_Doorlock5_2Close.Visible = false; }
                 
                   
                   

                    var s3 = dataDisplayStation3; // gọn hơn, đỡ phải viết nhiều lần

                    if (s3?.Door1_Opening == true)
                    {
                        Pic_Door5_Opening.Visible = true;
                        Pic_Door5_Closing.Visible = false;

                        lblCua5.Text = "Đang mở";
                        lblCua5.ForeColor = Color.Green;
                        lblCua5.BackColor = Color.YellowGreen;
                    }
                    else if (s3?.Door1_Closing == true)
                    {
                        Pic_Door5_Opening.Visible = false;
                        Pic_Door5_Closing.Visible = true;

                        lblCua5.Text = "Đang đóng";
                        lblCua5.ForeColor = Color.Black;
                        lblCua5.BackColor = Color.Red;
                    }
                    else if (s3?.Door1_Close == false)
                    {
                        Pic_Door5_Opening.Visible = false;
                        Pic_Door5_Closing.Visible = false;

                        lblCua5.Text = "Cửa mở";
                        lblCua5.ForeColor = Color.Green;
                        lblCua5.BackColor = Color.YellowGreen;
                    }
                    else // Ngược lại là cửa đóng
                    {
                        Pic_Door5_Opening.Visible = false;
                        Pic_Door5_Closing.Visible = false;

                        lblCua5.Text = "Cửa đóng";
                        lblCua5.ForeColor = Color.Black;
                        lblCua5.BackColor = Color.Red;
                    }


                    // cửa 2
                    if (s3?.Door2_Opening == true)
                    {
                        Pic_Door6_Opening.Visible = true;
                        Pic_Door6_Closing.Visible = false;

                        lblCua6.Text = "Đang mở";
                        lblCua6.ForeColor = Color.Green;
                        lblCua6.BackColor = Color.YellowGreen;
                    }
                    else if (s3?.Door2_Closing == true)
                    {
                        Pic_Door6_Opening.Visible = false;
                        Pic_Door6_Closing.Visible = true;

                        lblCua6.Text = "Đang đóng";
                        lblCua6.ForeColor = Color.Black;
                        lblCua6.BackColor = Color.Red;
                    }
                    else if (s3?.Door2_Close == false)
                    {
                        Pic_Door6_Opening.Visible = false;
                        Pic_Door6_Closing.Visible = false;

                        lblCua6.Text = "Cửa mở";
                        lblCua6.ForeColor = Color.Green;
                        lblCua6.BackColor = Color.YellowGreen;
                    }
                    else // Ngược lại là cửa đóng
                    {
                        Pic_Door6_Opening.Visible = false;
                        Pic_Door6_Closing.Visible = false;

                        lblCua6.Text = "Cửa đóng";
                        lblCua6.ForeColor = Color.Black;
                        lblCua6.BackColor = Color.Red;
                    }


                    var location = Globalvariable.RealtimeDisplays?.FirstOrDefault(loc => loc.LocationId == 1);
                    if (Location != null)
                    {
                        _labFllow_ho.Text = location.Stations.FirstOrDefault(x => x.Path.Contains("Location_Info"))?.Fllow_Ho.ToString();
                        
                  //      _labTotal_Fllow.Text = location.CalculatorValue.Qtr.ToString();
                  //    _labFllow_Door6.Text = location.CalculatorValue.Q_i.ToString();
                        _labAPISondai.Text = location.CalculatorValue.API_Fllow_SonDai.ToString();
                        _labAPIBensuc.Text = location.CalculatorValue.API_Fllow_BenSuc.ToString();
                        _labAPIDautieng.Text = location.CalculatorValue.API_Fllow_DauTieng.ToString();
                        _labAPIBinhnham.Text = location.CalculatorValue.API_Fllow_BinhNham.ToString();
                        //  _labAPIBinhnham2.Text = location.CalculatorValue.Fllow_BinhNham2.ToString();
                        _labAPICDD.Text = location.CalculatorValue.API_Fllow_TL_CDD.ToString();
                        //    _labAPIHL_TXL.Text = location.CalculatorValue.Fllow_HL_TXL.ToString();
                        _labTotal_Fllow.Text = location.CalculatorValue.Q_i_total.ToString();

                    }

                });
                }
        
            catch (Exception ex)
            {

            }
            finally
            {
                _timer.Enabled = true;
            }
        }


        private void _labFllow_ho_Click(object sender, EventArgs e)
        {
            PopupMucNuoc popup = new PopupMucNuoc();
            popup.ShowDialog();

        }

        private void _labAPISondai_Click(object sender, EventArgs e)
        {
            PopupSondai popupSondai = new PopupSondai();
            popupSondai.ShowDialog();
        }

        private void _labAPIBinhnham_Click(object sender, EventArgs e)
        {
            PopupBinhnham binhnham = new PopupBinhnham();
            binhnham.ShowDialog();
        }

        private void _labAPIDautieng_Click(object sender, EventArgs e)
        {
            PopupTVDauTieng tvdautieng = new PopupTVDauTieng();
            tvdautieng.ShowDialog();
        }

        private void _labAPIBensuc_Click(object sender, EventArgs e)
        {
            PopupBensuc bensuc = new PopupBensuc();
            bensuc.ShowDialog();
        }

        private void _labAPICDD_Click(object sender, EventArgs e)
        {
            PopupCDD CDD = new PopupCDD();
            CDD.ShowDialog();
        }
    }
     



  }
