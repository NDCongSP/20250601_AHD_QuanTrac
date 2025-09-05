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
       //     _mainForm = frmMain; // Gán trước khi sử dụng
            Load += FrmHome_Load;

            _timer.Interval = 1000;
            _timer.Tick += Timer_Tick;
            _timer.Start();
            // Load trạng thái ban đầu ngay khi khởi tạo
            LoadInitialValues();
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


private void UpdateDoorlockStatus(Label lbl, string openingValue, string closingValue, string defaultText)
        {
            if (openingValue == "1")
            {
                lbl.Text = "Đang Mở";
                lbl.BackColor = Color.GreenYellow;
            }
            else if (closingValue == "1")
            {
                lbl.Text = "Đang Đóng";
                lbl.BackColor = Color.Red;
            }
            else
            {
                lbl.Text = defaultText;
                lbl.BackColor = DefaultBackColor;
            }
        }
        private void LoadInitialValues()
            {
            //Pic_Station1_Run.Visible = _mainForm.GetS1_Station_RunValue() == "1";
            //Pic_Station1_Stop.Visible = _mainForm.GetS1_Station_StopValue() == "1";
            //Pic_Station1_Alarm.Visible = _mainForm.GetS1_Station_AlarmValue() == "1";
            //Pic_Station2_Run.Visible = _mainForm.GetS2_Station_RunValue() == "1";
            //Pic_Station2_Stop.Visible = _mainForm.GetS2_Station_StopValue() == "1";
            //Pic_Station2_Alarm.Visible = _mainForm.GetS2_Station_AlarmValue() == "1";
            //Pic_Station3_Run.Visible = _mainForm.GetS3_Station_RunValue() == "1";
            //Pic_Station3_Stop.Visible = _mainForm.GetS3_Station_StopValue() == "1";
            //Pic_Station3_Alarm.Visible = _mainForm.GetS3_Station_AlarmValue() == "1";

            //Pic_Doorlock2_1Open.Visible = _mainForm.GetDoorlock2_1OpenValue() == "1";
            //Pic_Doorlock2_1Close.Visible = _mainForm.GetDoorlock2_1CloseValue() == "1";
            //Pic_Doorlock2_2Open.Visible = _mainForm.GetDoorlock2_2OpenValue() == "1";
            //Pic_Doorlock2_2Close.Visible = _mainForm.GetDoorlock2_2CloseValue() == "1";
            //Pic_Doorlock3_1Open.Visible = _mainForm.GetDoorlock3_1OpenValue() == "1";
            //Pic_Doorlock3_1Close.Visible = _mainForm.GetDoorlock3_1CloseValue() == "1";
            //Pic_Doorlock3_2Open.Visible = _mainForm.GetDoorlock3_2OpenValue() == "1";
            //Pic_Doorlock3_2Close.Visible = _mainForm.GetDoorlock3_2CloseValue() == "1";
            //Pic_Doorlock4_1Open.Visible = _mainForm.GetDoorlock4_1OpenValue() == "1";
            //Pic_Doorlock4_1Close.Visible = _mainForm.GetDoorlock4_1CloseValue() == "1";
            //Pic_Doorlock4_2Open.Visible = _mainForm.GetDoorlock4_2OpenValue() == "1";
            //Pic_Doorlock4_2Close.Visible = _mainForm.GetDoorlock4_2CloseValue() == "1";
            //Pic_Doorlock5_1Open.Visible = _mainForm.GetDoorlock5_1OpenValue() == "1";
            //Pic_Doorlock5_1Close.Visible = _mainForm.GetDoorlock5_1CloseValue() == "1";
            //Pic_Doorlock5_2Open.Visible = _mainForm.GetDoorlock5_2OpenValue() == "1";
            //Pic_Doorlock5_2Close.Visible = _mainForm.GetDoorlock5_2CloseValue() == "1";
            //Pic_Door1_Opening.Visible = _mainForm.GetDoor1_OpeningValue() == "1";
            //Pic_Door1_Closing.Visible = _mainForm.GetDoor1_ClosingValue() == "1";
            //Pic_Door2_Opening.Visible = _mainForm.GetDoor2_OpeningValue() == "1";
            //Pic_Door2_Closing.Visible = _mainForm.GetDoor2_ClosingValue() == "1";
            //Pic_Door3_Opening.Visible = _mainForm.GetDoor3_OpeningValue() == "1";
            //Pic_Door3_Closing.Visible = _mainForm.GetDoor3_ClosingValue() == "1";
            //Pic_Door4_Opening.Visible = _mainForm.GetDoor4_OpeningValue() == "1";
            //Pic_Door4_Closing.Visible = _mainForm.GetDoor4_ClosingValue() == "1";
            //Pic_Door5_Opening.Visible = _mainForm.GetDoor5_OpeningValue() == "1";
            //Pic_Door5_Closing.Visible = _mainForm.GetDoor5_ClosingValue() == "1";
            //Pic_Door6_Opening.Visible = _mainForm.GetDoor6_OpeningValue() == "1";
            //Pic_Door6_Closing.Visible = _mainForm.GetDoor6_ClosingValue() == "1";
         
      //      UpdateDoorlockStatus(lblASCua1, _mainForm.GetDoorlock1_OpeningValue(), _mainForm.GetDoorlock1_ClosingValue(), "Chốt 1");
       //     UpdateDoorlockStatus(lblASCua2, _mainForm.GetDoorlock2_OpeningValue(), _mainForm.GetDoorlock2_ClosingValue(), "Chốt 2");
      //      UpdateDoorlockStatus(lblASCua3, _mainForm.GetDoorlock3_OpeningValue(), _mainForm.GetDoorlock3_ClosingValue(), "Chốt 3");
       //     UpdateDoorlockStatus(lblASCua4, _mainForm.GetDoorlock4_OpeningValue(), _mainForm.GetDoorlock4_ClosingValue(), "Chốt 4");
       //     UpdateDoorlockStatus(lblASCua5, _mainForm.GetDoorlock5_OpeningValue(), _mainForm.GetDoorlock5_ClosingValue(), "Chốt 5");
       //     UpdateDoorlockStatus(lblASCua6, _mainForm.GetDoorlock6_OpeningValue(), _mainForm.GetDoorlock6_ClosingValue(), "Chốt 6");

            //UpdateDoorlockStatus(lblCua1, _mainForm.GetDoor1_OpenValue(), _mainForm.GetDoor1_CloseValue(), "Cửa 1");
            //UpdateDoorlockStatus(lblCua2, _mainForm.GetDoor2_OpenValue(), _mainForm.GetDoor2_CloseValue(), "Cửa 2");
            //UpdateDoorlockStatus(lblCua3, _mainForm.GetDoor3_OpenValue(), _mainForm.GetDoor3_CloseValue(), "Cửa 3");
            //UpdateDoorlockStatus(lblCua4, _mainForm.GetDoor4_OpenValue(), _mainForm.GetDoor4_CloseValue(), "Cửa 4");
            //UpdateDoorlockStatus(lblCua5, _mainForm.GetDoor5_OpenValue(), _mainForm.GetDoor5_CloseValue(), "Cửa 5");
            //UpdateDoorlockStatus(lblCua6, _mainForm.GetDoor6_OpenValue(), _mainForm.GetDoor6_CloseValue(), "Cửa 6");

        }

        private void FrmHome_Load(object sender, EventArgs e)
        {                    

           
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
        //    if (e.NewValue == "1")
        //    {
        //        this.Invoke((MethodInvoker)delegate { lblChot5.Text = "Đang Mở"; lblChot5.ForeColor = Color.Green; lblChot5.BackColor = Color.YellowGreen; });
        //    }
        //    else
        //        this.Invoke((MethodInvoker)delegate { lblChot5.Text = "CHỐT 5"; lblChot5.ForeColor = DefaultForeColor; lblChot5.BackColor = DefaultBackColor; });
        }
        private void Doorlock5_Closing_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
        //    if (e.NewValue == "1")
        //    {
        //        this.Invoke((MethodInvoker)delegate { lblChot5.Text = "Đang Đóng"; lblChot5.ForeColor = Color.Green; lblChot5.BackColor = Color.Red; });
        //    }
        //    else
        //        this.Invoke((MethodInvoker)delegate { lblChot5.Text = "CHỐT 5"; lblChot5.ForeColor = DefaultForeColor; lblChot5.BackColor = DefaultBackColor; });
        }
        private void Doorlock4_Opening_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
        //    if (e.NewValue == "1")
        //    {
        //        this.Invoke((MethodInvoker)delegate { lblChot4.Text = "Đang Mở"; lblChot4.ForeColor = Color.Green; lblChot4.BackColor = Color.YellowGreen; });
        //    }
        //    else
        //        this.Invoke((MethodInvoker)delegate { lblChot4.Text = "CHỐT 4"; lblChot4.ForeColor = DefaultForeColor; lblChot4.BackColor = DefaultBackColor; });
        }
        private void Doorlock4_Closing_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
        //    if (e.NewValue == "1")
        //    {
        //        this.Invoke((MethodInvoker)delegate { lblChot4.Text = "Đang Đóng"; lblChot4.ForeColor = Color.Green; lblChot4.BackColor = Color.Red; });
        //    }
        //    else
        //        this.Invoke((MethodInvoker)delegate { lblChot4.Text = "CHỐT 4"; lblChot4.ForeColor = DefaultForeColor; lblChot4.BackColor = DefaultBackColor; });
        }
        private void Doorlock3_Opening_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
        //    if (e.NewValue == "1")
        //    {
        //        this.Invoke((MethodInvoker)delegate { lblChot3.Text = "Đang Mở"; lblChot3.ForeColor = Color.Green; lblChot3.BackColor = Color.YellowGreen; });
        //    }
        //    else
        //        this.Invoke((MethodInvoker)delegate { lblChot3.Text = "CHỐT 3"; lblChot3.ForeColor = DefaultForeColor; lblChot3.BackColor = DefaultBackColor; });
        }
        private void Doorlock3_Closing_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            //if (e.NewValue == "1")
            //{
            //    this.Invoke((MethodInvoker)delegate { lblChot3.Text = "Đang Đóng"; lblChot3.ForeColor = Color.Green; lblChot3.BackColor = Color.Red; });
            //}
            //else
            //    this.Invoke((MethodInvoker)delegate { lblChot3.Text = "CHỐT 3"; lblChot3.ForeColor = DefaultForeColor; lblChot3.BackColor = DefaultBackColor; });
        }
        private void Doorlock2_Opening_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            //if (e.NewValue == "1")
            //{
            //    this.Invoke((MethodInvoker)delegate { lblChot2.Text = "Đang Mở1"; lblChot2.ForeColor = Color.Green; lblChot2.BackColor = Color.YellowGreen; });
            //}
            //else
            //    this.Invoke((MethodInvoker)delegate { lblChot2.Text = "CHỐT 2"; lblChot2.ForeColor = DefaultForeColor; lblChot2.BackColor = DefaultBackColor; });
        }
        private void Doorlock2_Closing_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
        //    if (e.NewValue == "1")
        //    {
        //        this.Invoke((MethodInvoker)delegate { lblASCua2.Text = "Đang Đóng"; lblASCua2.ForeColor = Color.Green; lblASCua2.BackColor = Color.Red; });
        //    }
        //    else
        //        this.Invoke((MethodInvoker)delegate { lblASCua2.Text = "CHỐT 2"; lblASCua2.ForeColor = DefaultForeColor; lblASCua2.BackColor = DefaultBackColor; });
        }

        // Trạng thái cửa đang mở, đang đóng 1 -> 6
        private void Door6_Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { lblCua6.Text = "Đóng"; lblCua6.ForeColor = Color.Green; lblCua6.BackColor = Color.Red; });
            }
            else
                this.Invoke((MethodInvoker)delegate { lblCua6.Text = "Cửa 6"; lblCua6.ForeColor = DefaultForeColor; lblCua6.BackColor = DefaultBackColor; });
        }

        private void Door6_Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { lblCua6.Text = "Mở"; lblCua6.ForeColor = Color.Green; lblCua6.BackColor = Color.YellowGreen; });
            }
            else
                this.Invoke((MethodInvoker)delegate { lblCua6.Text = "Cửa 6"; lblCua6.ForeColor = DefaultForeColor; lblCua6.BackColor = DefaultBackColor; });
        }
        private void Door5_Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { lblCua5.Text = "Đóng"; lblCua5.ForeColor = Color.Green; lblCua5.BackColor = Color.Red; });
            }
            else
                this.Invoke((MethodInvoker)delegate { lblCua5.Text = "Cửa 5"; lblCua5.ForeColor = DefaultForeColor; lblCua5.BackColor = DefaultBackColor; });
        }
        private void Door5_Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { lblCua5.Text = "Mở"; lblCua5.ForeColor = Color.Green; lblCua5.BackColor = Color.YellowGreen; });
            }
            else
                this.Invoke((MethodInvoker)delegate { lblCua5.Text = "Cửa 5"; lblCua5.ForeColor = DefaultForeColor; lblCua5.BackColor = DefaultBackColor; });
        }
        private void Door4_Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { lblCua4.Text = "Đóng"; lblCua4.ForeColor = Color.Green; lblCua4.BackColor = Color.Red; });
            }
            else
                this.Invoke((MethodInvoker)delegate { lblCua4.Text = "Cửa 4"; lblCua4.ForeColor = DefaultForeColor; lblCua4.BackColor = DefaultBackColor; });
        }
        private void Door4_Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { lblCua4.Text = "Mở"; lblCua4.ForeColor = Color.Green; lblCua4.BackColor = Color.YellowGreen; });
            }
            else
                this.Invoke((MethodInvoker)delegate { lblCua4.Text = "Cửa 4"; lblCua4.ForeColor = DefaultForeColor; lblCua4.BackColor = DefaultBackColor; });
        }
        private void Door3_Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { lblCua3.Text = "Đóng"; lblCua3.ForeColor = Color.Green; lblCua3.BackColor = Color.Red; });
            }
            else
                this.Invoke((MethodInvoker)delegate { lblCua3.Text = "Cửa 3"; lblCua3.ForeColor = DefaultForeColor; lblCua3.BackColor = DefaultBackColor; });
        }
        private void Door3_Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { lblCua3.Text = "Mở"; lblCua3.ForeColor = Color.Green; lblCua3.BackColor = Color.YellowGreen; });
            }
            else
                this.Invoke((MethodInvoker)delegate { lblCua3.Text = "Cửa 3"; lblCua3.ForeColor = DefaultForeColor; lblCua3.BackColor = DefaultBackColor; });
        }
        private void Door2_Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { lblCua2.Text = "Đóng"; lblCua2.ForeColor = Color.Green; lblCua2.BackColor = Color.Red; });
            }
            else
                this.Invoke((MethodInvoker)delegate { lblCua2.Text = "Cửa 2"; lblCua2.ForeColor = DefaultForeColor; lblCua2.BackColor = DefaultBackColor; });
        }
        private void Door2_Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { lblCua2.Text = "Mở"; lblCua2.ForeColor = Color.Green; lblCua2.BackColor = Color.YellowGreen; });
            }
            else
                this.Invoke((MethodInvoker)delegate { lblCua2.Text = "Cửa 2"; lblCua2.ForeColor = DefaultForeColor; lblCua2.BackColor = DefaultBackColor; });
        }
        private void Door1_Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { lblCua1.Text = "Đóng"; lblCua1.ForeColor = Color.Green; lblCua1.BackColor = Color.Red; });
            }
            else
                this.Invoke((MethodInvoker)delegate { lblCua1.Text = "Cửa 1"; lblCua1.ForeColor = DefaultForeColor; lblCua1.BackColor = DefaultBackColor; });
        }
        private void Door1_Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { lblCua1.Text = "Mở"; lblCua1.ForeColor = Color.Green; lblCua1.BackColor = Color.YellowGreen; });
            }
            else
                this.Invoke((MethodInvoker)delegate { lblCua1.Text = "Cửa 1"; lblCua1.ForeColor = DefaultForeColor; lblCua1.BackColor = DefaultBackColor; });
        }
        // TRạng thái đang đóng , đang mở cửa 1 -> 6
        private void Door6_Closing_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door6_Closing.Visible = true; lblCua6.Text = "Đang đóng"; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door6_Closing.Visible = false; });
        }
        private void Door6_Opening_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door6_Opening.Visible = true; lblCua6.Text = "Đang mở"; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door6_Opening.Visible = false; });
        }
        private void Door5_Closing_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door5_Closing.Visible = true; lblCua5.Text = "Đang đóng"; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door5_Closing.Visible = false; });
        }
        private void Door5_Opening_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door5_Opening.Visible = true; lblCua5.Text = "Đang mở"; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door5_Opening.Visible = false; });
        }
        private void Door4_Closing_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door4_Closing.Visible = true; lblCua4.Text = "Đang đóng"; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door4_Closing.Visible = false; });
        }
        private void Door4_Opening_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door4_Opening.Visible = true; lblCua4.Text = "Đang mở"; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door4_Opening.Visible = false;  });
        }
        private void Door3_Closing_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door3_Closing.Visible = true; lblCua3.Text = "Đang đóng"; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door3_Closing.Visible = false; });
        }
        private void Door3_Opening_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door3_Opening.Visible = true; lblCua3.Text = "Đang mở"; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door3_Opening.Visible = false; });
        }
        private void Door2_Closing_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door2_Closing.Visible = true; lblCua2.Text = "Đang đóng"; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door2_Closing.Visible = false; });
        }
        private void Door2_Opening_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door2_Opening.Visible = true; lblCua2.Text = "Đang mở"; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door2_Opening.Visible = false; });
        }
        private void Door1_Closing_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door1_Closing.Visible = true; lblCua1.Text = "Đang đóng"; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door1_Closing.Visible = false; });
        }
        private void Door1_Opening_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door1_Opening.Visible = true; lblCua1.Text = "Đang mở"; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door1_Opening.Visible = false; });
        }
        // Kết thúc TRạng thái đang đóng , đang mở cửa 1 -> 6
        // Trang thái động cơ Trạm 3
        //private void S3_Station_Run_ValueChanged(object sender, TagValueChangedEventArgs e)
        //{
        //    if (e.NewValue == "1")
        //    {
        //        this.Invoke((MethodInvoker)delegate { Pic_Station3_Run.Visible = true; Pic_Station3_Alarm.Visible = false; Pic_Station3_Stop.Visible = false; });
        //    }
        //    else
        //        this.Invoke((MethodInvoker)delegate { Pic_Station3_Alarm.Visible = false; Pic_Station3_Stop.Visible = false; Pic_Station3_Run.Visible = false; });
        //}
        //private void S3_Station_Stop_ValueChanged(object sender, TagValueChangedEventArgs e)
        //{
        //    if (e.NewValue == "1")
        //    {
        //        this.Invoke((MethodInvoker)delegate { Pic_Station3_Stop.Visible = true; Pic_Station3_Alarm.Visible = false; Pic_Station3_Run.Visible = false; });
        //    }
        //    else
        //        this.Invoke((MethodInvoker)delegate { Pic_Station3_Alarm.Visible = false; Pic_Station3_Stop.Visible = false; Pic_Station3_Run.Visible = false; });
        //}
        //private void S3_Station_Alarm_ValueChanged(object sender, TagValueChangedEventArgs e)
        //{
        //    if (e.NewValue == "1")
        //    {
        //        this.Invoke((MethodInvoker)delegate { Pic_Station3_Alarm.Visible = true; Pic_Station3_Run.Visible = false; Pic_Station3_Stop.Visible = false; });
        //    }
        //    else
        //        this.Invoke((MethodInvoker)delegate { Pic_Station3_Alarm.Visible = false; Pic_Station3_Stop.Visible = false; Pic_Station3_Run.Visible = false; });
        //}
        // Trang thái động cơ Trạm 2
        //private void S2_Station_Run_ValueChanged(object sender, TagValueChangedEventArgs e)
        //{
        //    if (e.NewValue == "1")
        //    {
        //        this.Invoke((MethodInvoker)delegate { Pic_Station2_Run.Visible = true; Pic_Station2_Alarm.Visible = false; Pic_Station2_Stop.Visible = false; });
        //    }
        //    else
        //        this.Invoke((MethodInvoker)delegate { Pic_Station2_Alarm.Visible = false; Pic_Station2_Stop.Visible = false; Pic_Station2_Run.Visible = false; });
        //}
        //private void S2_Station_Stop_ValueChanged(object sender, TagValueChangedEventArgs e)
        //{
        //    if (e.NewValue == "1")
        //    {
        //        this.Invoke((MethodInvoker)delegate { Pic_Station2_Stop.Visible = true; Pic_Station2_Alarm.Visible = false; Pic_Station2_Run.Visible = false; });
        //    }
        //    else
        //        this.Invoke((MethodInvoker)delegate { Pic_Station2_Alarm.Visible = false; Pic_Station2_Stop.Visible = false; Pic_Station2_Run.Visible = false; });
        //}
        //private void S2_Station_Alarm_ValueChanged(object sender, TagValueChangedEventArgs e)
        //{
        //    if (e.NewValue == "1")
        //    {
        //        this.Invoke((MethodInvoker)delegate { Pic_Station2_Alarm.Visible = true; Pic_Station2_Run.Visible = false; Pic_Station2_Stop.Visible = false; });
        //    }
        //    else
        //        this.Invoke((MethodInvoker)delegate { Pic_Station2_Alarm.Visible = false; Pic_Station2_Stop.Visible = false; Pic_Station2_Run.Visible = false; });
        //}

        // Kết thúc Trạng thái động cơ Trạm 2


        // Trạng thái động cơ Trạm 1
        //private void S1_Station_Run_ValueChanged(object sender, TagValueChangedEventArgs e)
        //{
        //    if (e.NewValue == "1")
        //    {
        //        this.Invoke((MethodInvoker)delegate { Pic_Station1_Run.Visible = true; Pic_Station1_Stop.Visible = false; Pic_Station1_Alarm.Visible = false; });
        //    }
        //    else
        //        this.Invoke((MethodInvoker)delegate { Pic_Station1_Run.Visible = false; Pic_Station1_Stop.Visible = false; Pic_Station1_Run.Visible = false; });
        //}
        //private void S1_Station_Stop_ValueChanged(object sender, TagValueChangedEventArgs e)
        //{
        //    if (e.NewValue == "1")
        //    {
        //        this.Invoke((MethodInvoker)delegate { Pic_Station1_Stop.Visible = true; Pic_Station1_Run.Visible = false; Pic_Station1_Alarm.Visible = false; });
        //    }
        //    else
        //        this.Invoke((MethodInvoker)delegate { Pic_Station1_Alarm.Visible = false; Pic_Station1_Stop.Visible = false; Pic_Station1_Run.Visible = false; });
        //}
        //private void S1_Station_Alarm_ValueChanged(object sender, TagValueChangedEventArgs e)
        //{
        //    if (e.NewValue == "1")
        //    {
        //        this.Invoke((MethodInvoker)delegate { Pic_Station1_Alarm.Visible = true; Pic_Station1_Run.Visible = false; Pic_Station1_Stop.Visible = false; });
        //    }
        //    else
        //        this.Invoke((MethodInvoker)delegate { Pic_Station1_Alarm.Visible = false; Pic_Station1_Stop.Visible = false; Pic_Station1_Run.Visible = false; });
        //}
        // Kết thúc Trạng thái động cơ Trạm 1
        //private void DC3_Over_ValueChanged(object sender, TagValueChangedEventArgs e)
        //{
        //    if (e.NewValue == "1")
        //    {
        //        this.Invoke((MethodInvoker)delegate { Pic_Station3_Alarm.Visible = true; Pic_Station3_Run.Visible = false; Pic_Station3_Stop.Visible = false; });
        //    }
        //    else
        //        this.Invoke((MethodInvoker)delegate { Pic_Station3_Alarm.Visible = false; Pic_Station3_Run.Visible = false; Pic_Station3_Stop.Visible = false; });
        //}
        //private void DC2_Over_ValueChanged(object sender, TagValueChangedEventArgs e)
        //{
        //    if (e.NewValue == "1")
        //    {
        //        this.Invoke((MethodInvoker)delegate { Pic_Station2_Alarm.Visible = true; Pic_Station2_Run.Visible = false; Pic_Station2_Stop.Visible = false; });
        //    }
        //    else
        //        this.Invoke((MethodInvoker)delegate { Pic_Station2_Alarm.Visible = false; Pic_Station2_Run.Visible = false; Pic_Station2_Stop.Visible = false; });
        //}
        //private void DC1_Over_ValueChanged(object sender, TagValueChangedEventArgs e)
        //{
        //    if (e.NewValue == "1")
        //    {
        //        this.Invoke((MethodInvoker)delegate { Pic_Station1_Alarm.Visible = true; Pic_Station1_Run.Visible = false; Pic_Station1_Stop.Visible = false; });
        //    }
        //    else
        //        this.Invoke((MethodInvoker)delegate { Pic_Station1_Alarm.Visible = false; Pic_Station1_Run.Visible = false; Pic_Station1_Stop.Visible = false; });
        //}

        /// <summary>
        /// /////////////////////////////////////
        /// 
        //private void S3_DC3_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        //{
        //    if (e.NewValue == "1")
        //    {
        //        this.Invoke((MethodInvoker)delegate { Pic_Station3_Run.Visible = true; Pic_Station3_Alarm.Visible = false; Pic_Station3_Stop.Visible = false; });
        //    }
        //    else
        //        this.Invoke((MethodInvoker)delegate { Pic_Station3_Alarm.Visible = false; Pic_Station3_Stop.Visible = true; Pic_Station3_Run.Visible = false; });
        //}
        //private void S3_DC2_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        //{
        //    if (e.NewValue == "1")
        //    {
        //        this.Invoke((MethodInvoker)delegate { Pic_Station2_Run.Visible = true; Pic_Station2_Alarm.Visible = false; Pic_Station2_Stop.Visible = false; });
        //    }
        //    else
        //        this.Invoke((MethodInvoker)delegate { Pic_Station2_Alarm.Visible = false; Pic_Station2_Stop.Visible = true; Pic_Station2_Run.Visible = false; });
        //}
        //private void S3_DC1_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        //{
        //    if (e.NewValue == "1")
        //    {
        //        this.Invoke((MethodInvoker)delegate { Pic_Station1_Run.Visible = true; Pic_Station1_Alarm.Visible = false; Pic_Station1_Stop.Visible = false; });
        //    }
        //    else
        //        this.Invoke((MethodInvoker)delegate { Pic_Station1_Alarm.Visible = false; Pic_Station1_Stop.Visible = true; Pic_Station1_Run.Visible = false; });
        //}
        //private void S2_DC3_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        //{
        //    if (e.NewValue == "1")
        //    {
        //        this.Invoke((MethodInvoker)delegate { Pic_Station3_Run.Visible = true; Pic_Station3_Alarm.Visible = false; Pic_Station3_Stop.Visible = false; });
        //    }
        //    else
        //        this.Invoke((MethodInvoker)delegate { Pic_Station3_Alarm.Visible = false; Pic_Station3_Stop.Visible = true; Pic_Station3_Run.Visible = false; });
        //}
        //private void S2_DC2_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        //{
        //    if (e.NewValue == "1")
        //    {
        //        this.Invoke((MethodInvoker)delegate { Pic_Station2_Run.Visible = true; Pic_Station2_Alarm.Visible = false; Pic_Station2_Stop.Visible = false; });
        //    }
        //    else
        //        this.Invoke((MethodInvoker)delegate { Pic_Station2_Alarm.Visible = false; Pic_Station2_Stop.Visible = true; Pic_Station2_Run.Visible = false; });
        //}
        //private void S2_DC1_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        //{
        //    if (e.NewValue == "1")
        //    {
        //        this.Invoke((MethodInvoker)delegate { Pic_Station1_Run.Visible = true; Pic_Station1_Alarm.Visible = false; Pic_Station1_Stop.Visible = false; });
        //    }
        //    else
        //        this.Invoke((MethodInvoker)delegate { Pic_Station1_Alarm.Visible = false; Pic_Station1_Stop.Visible = true; Pic_Station1_Run.Visible = false; });
        //}
        //private void S1_DC3_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        //{
        //    if (e.NewValue == "1")
        //    {
        //        this.Invoke((MethodInvoker)delegate { Pic_Station1_Run.Visible = true; Pic_Station1_Alarm.Visible = false; Pic_Station1_Stop.Visible = false; });
        //    }
        //    else
        //        this.Invoke((MethodInvoker)delegate { Pic_Station1_Alarm.Visible = false; Pic_Station1_Stop.Visible = true; Pic_Station1_Run.Visible = false; });
        //}
        //private void S1_DC2_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        //{
        //    if (e.NewValue == "1")
        //    {
        //        this.Invoke((MethodInvoker)delegate { Pic_Station1_Run.Visible = true; Pic_Station1_Alarm.Visible = false; Pic_Station1_Stop.Visible = false; });
        //    }
        //    else
        //        this.Invoke((MethodInvoker)delegate { Pic_Station1_Alarm.Visible = false; Pic_Station1_Stop.Visible = true; Pic_Station1_Run.Visible = false; });
        //}
        //private void S1_DC1_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        //{
        //    if (e.NewValue == "1")
        //    {
        //        this.Invoke((MethodInvoker)delegate { Pic_Station1_Run.Visible = true; Pic_Station1_Alarm.Visible = false; Pic_Station1_Stop.Visible = false; });
        //    }
        //    else
        //        this.Invoke((MethodInvoker)delegate { Pic_Station1_Alarm.Visible = false; Pic_Station1_Stop.Visible = true; Pic_Station1_Run.Visible = false; });
        //}               
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
        

        //private void tm_login_Tick(object sender, EventArgs e)
        //{
        //    using (IDbConnection db = new SqlConnection(connectionString))
        //    {
        //        db.Open();

        //        var newRow = new DataVanHanhModel
        //        {
        //            CreateAt = DateTime.Now,
        //            HT_Cylinder1_1 = HT_Cylinder1_1.Text,
        //            HT_Cylinder1_2 = HT_Cylinder1_2.Text,
        //            HT_Cylinder2_1 = HT_Cylinder2_1.Text,
        //            HT_Cylinder2_2 = HT_Cylinder2_2.Text,
        //            HT_Cylinder3_1 = HT_Cylinder3_1.Text,
        //            HT_Cylinder3_2 = HT_Cylinder3_2.Text,
        //            HT_Cylinder4_1 = HT_Cylinder4_1.Text,
        //            HT_Cylinder4_2 = HT_Cylinder4_2.Text,
        //            HT_Cylinder5_1 = HT_Cylinder5_1.Text,
        //            HT_Cylinder5_2 = HT_Cylinder5_2.Text,
        //            HT_Cylinder6_1 = HT_Cylinder6_1.Text,
        //            HT_Cylinder6_2 = HT_Cylinder6_2.Text,
        //            Door1_Aperture = Door1_Aperture.Text,
        //            Door2_Aperture = Door2_Aperture.Text,
        //            Door3_Aperture = Door3_Aperture.Text,
        //            Door4_Aperture = Door4_Aperture.Text,
        //            Door5_Aperture = Door5_Aperture.Text,
        //            Door6_Aperture = Door6_Aperture.Text,
        //            Temp_Oil1 = Temp_Oil1.Text,
        //            Temp_Oil2 = Temp_Oil2.Text,
        //            Temp_Oil3 = Temp_Oil3.Text,
        //            Fllow_Door1 = Fllow_Door1.Text,
        //            Fllow_Door2 = Fllow_Door2.Text,
        //            Fllow_Door3 = Fllow_Door3.Text,
        //            Fllow_Door4 = Fllow_Door4.Text,
        //            Fllow_Door5 = Fllow_Door5.Text,
        //            Fllow_Door6 = Fllow_Door6.Text,
        //            Total_Fllow = Total_Fllow.Text,
        //            Fllow_Ho = Fllow_Ho.Text,
        //            Fllow_DauTieng = Fllow_DauTieng.Text,
        //            Fllow_BenSuc = Fllow_BenSuc.Text,
        //            Fllow_SonDai = Fllow_SonDai.Text,
        //        };

        //        string insertQuery = @"INSERT INTO Datavanhanh 
        //(CreateAt, HT_Cylinder1_1, HT_Cylinder1_2, HT_Cylinder2_1, HT_Cylinder2_2,
        // HT_Cylinder3_1, HT_Cylinder3_2, HT_Cylinder4_1, HT_Cylinder4_2,
        // HT_Cylinder5_1, HT_Cylinder5_2, HT_Cylinder6_1, HT_Cylinder6_2,
        // Door1_Aperture, Door2_Aperture, Door3_Aperture, Door4_Aperture, Door5_Aperture, Door6_Aperture,
        // Temp_Oil1, Temp_Oil2, Temp_Oil3,
        // Fllow_Door1, Fllow_Door2, Fllow_Door3, Fllow_Door4, Fllow_Door5, Fllow_Door6,
        // Total_Fllow, Fllow_Ho, Fllow_DauTieng, Fllow_BenSuc, Fllow_SonDai)
        //VALUES 
        //(@CreateAt, @HT_Cylinder1_1, @HT_Cylinder1_2, @HT_Cylinder2_1, @HT_Cylinder2_2,
        // @HT_Cylinder3_1, @HT_Cylinder3_2, @HT_Cylinder4_1, @HT_Cylinder4_2,
        // @HT_Cylinder5_1, @HT_Cylinder5_2, @HT_Cylinder6_1, @HT_Cylinder6_2,
        // @Door1_Aperture, @Door2_Aperture, @Door3_Aperture, @Door4_Aperture, @Door5_Aperture, @Door6_Aperture,
        // @Temp_Oil1, @Temp_Oil2, @Temp_Oil3,
        // @Fllow_Door1, @Fllow_Door2, @Fllow_Door3, @Fllow_Door4, @Fllow_Door5, @Fllow_Door6,
        // @Total_Fllow, @Fllow_Ho, @Fllow_DauTieng, @Fllow_BenSuc, @Fllow_SonDai)";

        //        db.Execute(insertQuery, newRow);
        //    }
        //}


    }
}
