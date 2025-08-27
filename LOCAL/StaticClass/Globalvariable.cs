using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationForm1
{
    public static class Globalvariable
    {
        public static ConfigModel ConfigSystem { get; set; } = new ConfigModel();

        public static LocationsModel LocationsInfo { get; set; } = new LocationsModel();

        public static RealtimeDisplayModel DisplaysInfo { get; set; } = new RealtimeDisplayModel();
    }
}
