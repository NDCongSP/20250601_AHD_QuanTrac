using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationForm1
{
    public class ConfigSystemModel
    {
        public string ConfigName { get; set; }
        public string ConfigValue { get; set; }
        public string ConfigDescription { get; set; }
        public ConfigSystemModel(string configName, string configValue, string configDescription)
        {
            ConfigName = configName;
            ConfigValue = configValue;
            ConfigDescription = configDescription;
        }
        public override string ToString()
        {
            return $"{ConfigName}: {ConfigValue} - {ConfigDescription}";
        }
    }
}
