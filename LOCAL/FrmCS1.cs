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
                            _LabMNTLCS1.Text = location.Stations.FirstOrDefault(x => x.Path.Contains("Location_Info"))?.Fllow_Ho_Final.ToString();
                            _LabQCS1.Text = location.CalculatorValue.Q_cs1.ToString();
                            _LabDomoCS1.Text = location.Parametters.DoMoCua_a_CongSo1.ToString();
                            _LabMNHLCS1.Text = location.Parametters.MNHL_CongSo1.ToString();
                          
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
