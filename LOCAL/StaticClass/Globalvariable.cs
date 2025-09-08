using Domain;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegistrationForm1
{
    public static class Globalvariable
    {
        public static ConfigModel ConfigSystem { get; set; } = new ConfigModel();

        public static LocationsInfo LocationsInfo { get; set; } = new LocationsInfo();

        public static RealtimeDisplays RealtimeDisplays { get; set; } = new RealtimeDisplays();

        public static FT03 DataLog { get; set; } = new FT03();

        public static FT04 AlarmDataLog { get; set; } = new FT04();

        public static void InvokeIfRequired(Control control, Action action)
        {
            if (control.InvokeRequired)
            {
                control.BeginInvoke(action);
            }
            else
            {
                action();
            }
        }

        public static ScadaUser UserInfo { get; set; } 
    }
}
