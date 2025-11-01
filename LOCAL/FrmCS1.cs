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
    public partial class FrmCS1 : Form
    {
        private Timer _timer = new Timer();
        public FrmCS1()
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
                    var location = Globalvariable.RealtimeDisplays?.FirstOrDefault(loc => loc.LocationId == 1);
                    if (location != null)
                    {
                        foreach (var item in location.Stations)
                        {
                            if (item.Path == "Local Station/DauTieng/S71500/Station_1")
                            {
                                //_labALDoor1_Station1.Text = item.Al_Door1.ToString();
                                //_labALDoor2_Station1.Text = item.Al_Door2.ToString();
                                //   _labHT_Cylinder1_1.Text = item.HT_Cylinder1_1.ToString();
                                //    _labHT_Cylinder1_2.Text = item.HT_Cylinder1_2.ToString();
                                //      _labFllow_ho.Text = item.Fllow_ho.ToString();



                            }
                            else if (item.Path == "Local Station/DauTieng/S71500/Station_2")
                            {
                                //_labALDoor1_Station2.Text = item.Al_Door1.ToString();
                                //_labALDoor2_Station2.Text = item.Al_Door2.ToString();
                            }
                            else if (item.Path == "Local Station/DauTieng/S71500/Station_3")
                            {
                                //_labALDoor1_Station3.Text = item.Al_Door1.ToString();
                                //_labALDoor2_Station3.Text = item.Al_Door2.ToString();
                            }

                            _LabMNTLCS1.Text = location.Stations.FirstOrDefault(x => x.Path.Contains("Location_Info"))?.Fllow_Ho_Final.ToString();
                            _LabQCS1.Text = location.CalculatorValue.Q_cs1.ToString();

                            //_LabDomoCS1.Text = location.



                            //     _LabMNHLCS1.Text = location.Stations?.FirstOrDefault(x => x.Path.Contains
                            //_lab_Q_tr.Text = location.CalculatorValue.Q_tr.ToString();
                            //_labQ_cs1.Text = location.CalculatorValue.Q_cs1.ToString();
                            //_labQ_cs2.Text = location.CalculatorValue.Q_cs2.ToString();
                            //_labQ_cs3.Text = location.CalculatorValue.Q_cs3.ToString();
                            //_labQ_den.Text = location.CalculatorValue.Q_den.ToString();
                            //_labQ_di.Text = location.CalculatorValue.Q_di.ToString();


                        }

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
    }
}
