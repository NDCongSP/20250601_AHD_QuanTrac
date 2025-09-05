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
    //    private FrmMain _mainForm;
        private Timer _timer = new Timer();
        public FrmHome(FrmMain frmMain)
        {
            InitializeComponent();
      
            Load += FrmHome_Load;

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

                // Cửa 
                if ((dataDisplayStation1?.Door1_Opening == true) && (dataDisplayStation1?.Door1_Closing != true))
                {
                    Pic_Door1_Opening.Visible = true; lblCua1.Text = "Đang mở"; lblCua1.ForeColor = Color.Green; lblCua1.BackColor = Color.YellowGreen;
                }             
                 else if ((dataDisplayStation1?.Door1_Closing == true) && (dataDisplayStation1?.Door1_Opening != true))
                    {
                        Pic_Door1_Closing.Visible = true; lblCua1.Text = "Đang đóng"; lblCua1.ForeColor = Color.Black; lblCua1.BackColor = Color.Red;
                    }
                else { Pic_Door1_Opening.Visible = false; Pic_Door1_Closing.Visible = false; lblCua1.Text = "Cửa 1"; lblCua1.ForeColor = Color.Black; lblCua1.BackColor = DefaultBackColor; }

                if ((dataDisplayStation1?.Door2_Opening == true) && (dataDisplayStation1?.Door2_Closing != true))
                    {
                        Pic_Door2_Opening.Visible = true; lblCua2.Text = "Đang mở"; lblCua2.ForeColor = Color.Green; lblCua2.BackColor = Color.YellowGreen;
                    }
                else if ((dataDisplayStation1?.Door2_Closing == true) && (dataDisplayStation1?.Door2_Opening != true))
                    {
                        Pic_Door2_Closing.Visible = true; lblCua2.Text = "Đang đóng"; lblCua2.ForeColor = Color.Black; lblCua2.BackColor = Color.Red;
                    }
                else { Pic_Door2_Opening.Visible = false; Pic_Door2_Closing.Visible = false; lblCua2.Text = "Cửa 2"; lblCua2.ForeColor = Color.Black; lblCua2.BackColor = DefaultBackColor; }

                    //Station 2
                    var dataDisplayStation2 = Globalvariable.RealtimeDisplays.FirstOrDefault()?.Stations.FirstOrDefault(x => x.Path == "Local Station/DauTieng/S71500/Station_2");
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
                    // Cửa
                    if (dataDisplayStation2?.Door1_Opening == true)
                    {
                        Pic_Door3_Opening.Visible = true;
                    }
                    else { Pic_Door3_Opening.Visible = false; }
                    if (dataDisplayStation2?.Door1_Closing == true)
                    {
                        Pic_Door3_Closing.Visible = true;
                    }
                    else { Pic_Door3_Closing.Visible = false; }
                    if (dataDisplayStation2?.Door2_Opening == true)
                    {
                        Pic_Door4_Opening.Visible = true;
                    }
                    else { Pic_Door4_Opening.Visible = false; }
                    if (dataDisplayStation2?.Door2_Closing == true)
                    {
                        Pic_Door4_Closing.Visible = true;
                    }
                    else { Pic_Door4_Closing.Visible = false; }

                    if ((dataDisplayStation2?.Door1_Opening == true) && (dataDisplayStation2?.Door1_Closing != true))
                    {
                       Pic_Door3_Opening.Visible = true ; lblCua3.Text = "Đang mở"; lblCua3.ForeColor = Color.Green; lblCua3.BackColor = Color.YellowGreen;
                    }
                    else if ((dataDisplayStation2?.Door1_Closing == true) && (dataDisplayStation2?.Door1_Opening != true))
                    {
                        Pic_Door3_Closing.Visible = true; lblCua3.Text = "Đang đóng"; lblCua3.ForeColor = Color.Black; lblCua3.BackColor = Color.Red;
                    }
                    else { Pic_Door3_Opening.Visible = false; Pic_Door3_Closing.Visible = false; lblCua3.Text = "Cửa 3"; lblCua3.ForeColor = Color.Black; lblCua3.BackColor = DefaultBackColor; }
                    if ((dataDisplayStation2?.Door2_Opening == true) && (dataDisplayStation2?.Door2_Closing != true))
                    {
                        Pic_Door4_Opening.Visible = true; lblCua4.Text = "Đang mở"; lblCua4.ForeColor = Color.Green; lblCua4.BackColor = Color.YellowGreen;
                    }
                    else if ((dataDisplayStation2?.Door2_Closing == true) && (dataDisplayStation2?.Door2_Opening != true))
                    {
                        Pic_Door4_Closing.Visible = true; lblCua4.Text = "Đang đóng"; lblCua4.ForeColor = Color.Black; lblCua4.BackColor = Color.Red;
                    }
                    else { Pic_Door4_Opening.Visible = false; Pic_Door4_Closing.Visible = false; lblCua4.Text = "Cửa 4"; lblCua4.ForeColor = Color.Black; lblCua4.BackColor = DefaultBackColor; }


                    //Station 3
                    var dataDisplayStation3 = Globalvariable.RealtimeDisplays.FirstOrDefault()?.Stations.FirstOrDefault(x => x.Path == "Local Station/DauTieng/S71500/Station_3");
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
                    // Cửa
                    if (dataDisplayStation3?.Door1_Opening == true)
                    {
                        Pic_Door5_Opening.Visible = true;
                    }
                    else { Pic_Door5_Opening.Visible = false; }
                    if (dataDisplayStation3?.Door1_Closing == true)
                    {
                        Pic_Door5_Closing.Visible = true;
                    }
                    else { Pic_Door5_Closing.Visible = false; }
                    if (dataDisplayStation3?.Door2_Opening == true)
                    {
                        Pic_Door6_Opening.Visible = true;
                    }
                    else { Pic_Door6_Opening.Visible = false; }
                    if (dataDisplayStation3?.Door2_Closing == true)
                    {
                        Pic_Door6_Closing.Visible = true;
                    }
                    else { Pic_Door6_Closing.Visible = false; }
                    if ((dataDisplayStation3?.Door1_Opening == true) && (dataDisplayStation3?.Door1_Closing != true))
                    {
                        Pic_Door5_Opening.Visible = true; lblCua5.Text = "Đang mở"; lblCua5.ForeColor = Color.Green; lblCua5.BackColor = Color.YellowGreen;
                    }
                    else if ((dataDisplayStation3?.Door1_Closing == true) && (dataDisplayStation3?.Door1_Opening != true))
                    {
                        Pic_Door5_Closing.Visible = true; lblCua5.Text = "Đang đóng"; lblCua5.ForeColor = Color.Black; lblCua5.BackColor = Color.Red;
                    }
                    else { Pic_Door5_Opening.Visible = false; Pic_Door5_Closing.Visible = false; lblCua5.Text = "Cửa 5"; lblCua5.ForeColor = Color.Black; lblCua5.BackColor = DefaultBackColor; }
                    if ((dataDisplayStation3?.Door2_Opening == true) && (dataDisplayStation3?.Door2_Closing != true))
                    {
                        Pic_Door6_Opening.Visible = true; lblCua6.Text = "Đang mở"; lblCua6.ForeColor = Color.Green; lblCua6.BackColor = Color.YellowGreen;
                    }
                    else if ((dataDisplayStation3?.Door2_Closing == true) && (dataDisplayStation3?.Door2_Opening != true))
                    {
                        Pic_Door6_Closing.Visible = true; lblCua6.Text = "Đang đóng"; lblCua6.ForeColor = Color.Black; lblCua6.BackColor = Color.Red;
                    }
                    else { Pic_Door6_Opening.Visible = false; Pic_Door6_Closing.Visible = false; lblCua6.Text = "Cửa 6"; lblCua6.ForeColor = Color.Black; lblCua6.BackColor = DefaultBackColor; }










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


        private void FrmHome_Load(object sender, EventArgs e)
        {
           
        }

    }
     



    }
//}
