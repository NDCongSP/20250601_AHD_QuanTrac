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
    public partial class FrmDieukhienscada : Form
    {
        private Timer _timer = new Timer();
        public FrmDieukhienscada()
        {
            InitializeComponent();
            Load += FrmDieukhienscada_Load;
        }

        private void FrmDieukhienscada_Load(object sender, EventArgs e)
        {
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




                var dataDisplayStation1 = Globalvariable.RealtimeDisplays.FirstOrDefault()?.Stations.FirstOrDefault(x => x.Path == "Local Station/DauTieng/S71500/Station_1");

                if (dataDisplayStation1?.Status_Open_Door1 == true)
                {
                    Abnt_OpenDoor1.BackColor = Color.GreenYellow; 
                }
                else { Abnt_OpenDoor1.BackColor = DefaultBackColor; }
                if (dataDisplayStation1?.Status_Close_Door1 == true)
                {
                    Abnt_CloseDoor1.BackColor = Color.Red;
                }
                else { Abnt_CloseDoor1.BackColor = DefaultBackColor; }
                if (dataDisplayStation1?.Status_Open_Door2 == true)
                {
                    Abnt_OpenDoor2.BackColor = Color.GreenYellow;
                }
                else { Abnt_OpenDoor2.BackColor = DefaultBackColor; }
                if (dataDisplayStation1?.Status_Close_Door2 == true)
                {
                    Abnt_CloseDoor2.BackColor = Color.Red;
                }
                else { Abnt_CloseDoor2.BackColor = DefaultBackColor; }
                if (dataDisplayStation1?.Status_Open_Doorlock2 == true)
                {
                    Abnt_OpenDoorlock2.BackColor = Color.GreenYellow;
                }
                else { Abnt_OpenDoorlock2.BackColor = DefaultBackColor; }
                if (dataDisplayStation1?.Status_Close_Doorlock2 == true)
                {
                    Abnt_CloseDoorlock2.BackColor = Color.Red;
                }
                else { Abnt_CloseDoorlock2.BackColor = DefaultBackColor; }
                //if (dataDisplayStation1?.Lock1 == true)
                //{
                //    AbntLock.BackColor = Color.Red;
                //}
                //else { AbntLock.BackColor = DefaultBackColor; }

                // khai báo trạng thái on/off cho điều khiển . Dùng Tag value change Door1_Open_Door
                if (dataDisplayStation1?.Door1_Open_Door == true)
                {
                    AbntON1.BackColor = Color.GreenYellow;
                    AbntOFF1.BackColor = DefaultBackColor;
                }
                else { AbntON1.BackColor = DefaultBackColor;AbntOFF1.BackColor = Color.Red; }
               

                if (dataDisplayStation1?.Lock2 == true)
                {
                    Abnt_OpenDoorlock2.BackColor = Color.YellowGreen; Abnt_CloseDoorlock2.BackColor = Color.YellowGreen;
                }
                else { Abnt_OpenDoorlock2.BackColor = DefaultBackColor; Abnt_CloseDoorlock2.BackColor = DefaultBackColor; }

                var dataDisplayStation2 = Globalvariable.RealtimeDisplays.FirstOrDefault()?.Stations.FirstOrDefault(x => x.Path == "Local Station/DauTieng/S71500/Station_2");
                if (dataDisplayStation2?.Status_Open_Door1 == true)
                {
                    Abnt_OpenDoor3.BackColor = Color.GreenYellow;
                }
                else { Abnt_OpenDoor3.BackColor = DefaultBackColor; }
                if (dataDisplayStation2?.Status_Close_Door1 == true)
                {
                    Abnt_CloseDoor3.BackColor = Color.Red;
                }
                else { Abnt_CloseDoor3.BackColor = DefaultBackColor; }
                if (dataDisplayStation2?.Status_Open_Door2 == true)
                {
                    Abnt_OpenDoor4.BackColor = Color.GreenYellow;
                }
                else { Abnt_OpenDoor4.BackColor = DefaultBackColor; }
                if (dataDisplayStation2?.Status_Close_Door2 == true)
                {
                    Abnt_CloseDoor4.BackColor = Color.Red;
                }
                else { Abnt_CloseDoor4.BackColor = DefaultBackColor; }
                if (dataDisplayStation2?.Status_Open_Doorlock1 == true)
                {
                    Abnt_OpenDoorlock3.BackColor = Color.GreenYellow;
                }
                else { Abnt_OpenDoorlock3.BackColor = DefaultBackColor; }
                if (dataDisplayStation2?.Status_Close_Doorlock1 == true)
                {
                    Abnt_CloseDoorlock3.BackColor = Color.Red;
                }
                else { Abnt_CloseDoorlock3.BackColor = DefaultBackColor; }
                if (dataDisplayStation2?.Status_Open_Doorlock2 == true)
                {
                    Abnt_OpenDoorlock4.BackColor = Color.GreenYellow;
                }
                else { Abnt_OpenDoorlock4.BackColor = DefaultBackColor; }
                if (dataDisplayStation2?.Status_Close_Doorlock2 == true)
                {
                    Abnt_CloseDoorlock4.BackColor = Color.Red;
                }
                else { Abnt_CloseDoorlock4.BackColor = DefaultBackColor; }
               
                if (dataDisplayStation2?.Lock1 == true)
                    {
                    Abnt_OpenDoorlock3.BackColor = Color.YellowGreen; Abnt_CloseDoorlock3.BackColor = Color.YellowGreen;
                }
                else { Abnt_OpenDoorlock3.BackColor = DefaultBackColor; Abnt_CloseDoorlock3.BackColor = DefaultBackColor; }
                if (dataDisplayStation2?.Lock2 == true)
                    {
                    Abnt_OpenDoorlock4.BackColor = Color.YellowGreen; Abnt_CloseDoorlock4.BackColor = Color.YellowGreen;
                }
                else { Abnt_OpenDoorlock4.BackColor = DefaultBackColor; Abnt_CloseDoorlock4.BackColor = DefaultBackColor; }

                // khai báo trạng thái on/off cho điều khiển . Dùng Tag value change Door1_Open_Door
                if (dataDisplayStation2?.Door1_Open_Door == true)
                {
                    AbntON2.BackColor = Color.GreenYellow;
                    AbntOFF2.BackColor = DefaultBackColor;
                }
                else { AbntON2.BackColor = DefaultBackColor; AbntOFF2.BackColor = Color.Red; }

                var dataDisplayStation3 = Globalvariable.RealtimeDisplays.FirstOrDefault()?.Stations.FirstOrDefault(x => x.Path == "Local Station/DauTieng/S71500/Station_3");
                if (dataDisplayStation3?.Status_Open_Door1 == true)
                {
                    Abnt_OpenDoor5.BackColor = Color.GreenYellow;
                }
                else { Abnt_OpenDoor5.BackColor = DefaultBackColor; }
                if (dataDisplayStation3?.Status_Close_Door1 == true)
                {
                    Abnt_CloseDoor5.BackColor = Color.Red;
                }
                else { Abnt_CloseDoor5.BackColor = DefaultBackColor; }
                if (dataDisplayStation3?.Status_Open_Door2 == true)
                {
                    Abnt_OpenDoor6.BackColor = Color.GreenYellow;
                }
                else { Abnt_OpenDoor6.BackColor = DefaultBackColor; }
                if (dataDisplayStation3?.Status_Close_Door2 == true)
                {
                    Abnt_CloseDoor6.BackColor = Color.Red;
                }
                else { Abnt_CloseDoor6.BackColor = DefaultBackColor; }
                if (dataDisplayStation3?.Status_Open_Doorlock1 == true)
                {
                    Abnt_OpenDoorlock5.BackColor = Color.GreenYellow;
                }
                else { Abnt_OpenDoorlock5.BackColor = DefaultBackColor; }
                if (dataDisplayStation3?.Status_Close_Doorlock1 == true)
                {
                    Abnt_CloseDoorlock5.BackColor = Color.Red;
                }
                else { Abnt_CloseDoorlock5.BackColor = DefaultBackColor; }
                if (dataDisplayStation3?.Lock1 == true)
                    {
                    Abnt_OpenDoorlock5.BackColor = Color.YellowGreen; Abnt_CloseDoorlock5.BackColor = Color.YellowGreen;
                }
                else { Abnt_OpenDoorlock5.BackColor = DefaultBackColor; Abnt_CloseDoorlock5.BackColor = DefaultBackColor; }

                // khai báo trạng thái on/off cho điều khiển . Dùng Tag value change Door1_Open_Door
                if (dataDisplayStation3?.Door1_Open_Door == true)
                {
                    AbntON3.BackColor = Color.GreenYellow;
                    AbntOFF3.BackColor = DefaultBackColor;
                }
                else { AbntON3.BackColor = DefaultBackColor; AbntOFF3.BackColor = Color.Red; }


            }

            catch (Exception ex)
            {

            }
            finally
            {
                _timer.Enabled = true;
            }
        }
    }
}
