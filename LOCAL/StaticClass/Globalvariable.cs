using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationForm1
{
    public static class Globalvariable
    {
        public static ConfigSystemModel ConfigSystem { get; set; } 

        public static string ConnectionString { get; set; }

        public static List<TagsValueModel> TagsValues { get; set; } = new List<TagsValueModel>();

        public static int On_Off { get; set; }
    }
}
