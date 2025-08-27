using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationForm1
{
    class Chartstatistics
    {
        public class DeviceStatistics
        {
            public int ActiveCount { get; set; }
            public int StoppedCount { get; set; }
            public int TotalCount { get; set; }
            public double ActivePercentage { get; set; }
            public double StoppedPercentage { get; set; }
        }

        public class PositionStatistics
        {
            public int Position { get; set; }
            public int ActiveCount { get; set; }
            public int StoppedCount { get; set; }
        }
        public class DeviceAlert
        {
            public string DeviceId { get; set; }
            public string DeviceName { get; set; }
            public DateTime AlertTime { get; set; }
        }
    }
}
