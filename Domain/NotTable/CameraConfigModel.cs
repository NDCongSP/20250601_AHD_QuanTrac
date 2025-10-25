using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class CameraConfigModel
    {
        public string? CameraTital { get; set; } = string.Empty;

        public string? HlsStreamUrl1 { get; set; } = string.Empty;

        public bool Actived { get; set; } = true;
    }
}
