using System.Collections.Generic;

namespace Domain
{
    public class ConfigModel
    {
        public ParamettersModel ParametterConfig { get; set; } = new ParamettersModel();

        /// <summary>
        /// chu ky lof data (giay).
        /// </summary>
        public int DataLogInterval { get; set; } = 60;

        /// <summary>
        /// Vhu kyf cuar timer (milisecond).
        /// </summary>
        public int TimerInterval { get; set; } = 5000;

        public List<CameraConfigModel> CameraConfigs { get; set; } = new List<CameraConfigModel>();
    }
}
