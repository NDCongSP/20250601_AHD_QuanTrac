using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class ControlPlcModel
    {
        public int StationId { get; set; }
        public string StationName { get; set; }
        public int OffSerien { get; set; }
        public bool IsDoFlag { get; set; }
    }
}
