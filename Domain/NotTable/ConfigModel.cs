namespace Domain
{
    public class ConfigModel
    {
        public ParamettersModel ParametterConfig { get; set; } = new ParamettersModel();

        /// <summary>
        /// chu ky lof data (giay).
        /// </summary>
        public int DataLogInterval { get; set; } = 60;
    }
}
