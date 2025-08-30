using Domain;
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

        public static LocationsModel LocationsInfo { get; set; } = new LocationsModel();

        public static RealtimeDisplays RealtimeDisplays { get; set; } = new RealtimeDisplays();

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
    }
}
