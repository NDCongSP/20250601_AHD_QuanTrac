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
    
    public partial class FrmCS3 : Form
    {
        private Timer _timer = new Timer();
        public FrmCS3()
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
                            _LabMNTLCS3.Text = location.Stations.FirstOrDefault(x => x.Path.Contains("Location_Info"))?.Fllow_Ho_Final.ToString();
                            _LabQCS3.Text = location.CalculatorValue.Q_cs3.ToString();
                            //_LabDomoCS3.Text = location.Parametters.DoMoCua_a_CongSo3.ToString();
                            // _LabMNHLCS3.Text = location.Parametters.MNHL_CongSo3.ToString();
                            _LabDomoCS3.Text = Globalvariable.ConfigSystem.ParametterConfig.DoMoCua_a_CongSo3.ToString();
                            _LabMNHLCS3.Text = Globalvariable.ConfigSystem.ParametterConfig.MNHL_CongSo3.ToString();

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
