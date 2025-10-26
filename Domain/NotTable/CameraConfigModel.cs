using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class CameraConfigModel
    {
        public int CameraId { get; set; } = 1;
        public string? CameraTitle { get; set; } = string.Empty;

        public string? HlsStreamUrl { get; set; } = string.Empty;

        public bool Actived { get; set; } = true;
    }
}
