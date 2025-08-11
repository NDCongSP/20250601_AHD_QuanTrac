using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationForm1
{
    public class DataVanHanhModel
    {
        public DateTime CreateAt { get; set; }

        public string HT_Cylinder1_1 { get; set; }
        public string HT_Cylinder1_2 { get; set; }
        public string HT_Cylinder2_1 { get; set; }
        public string HT_Cylinder2_2 { get; set; }
        public string HT_Cylinder3_1 { get; set; }
        public string HT_Cylinder3_2 { get; set; }
        public string HT_Cylinder4_1 { get; set; }
        public string HT_Cylinder4_2 { get; set; }
        public string HT_Cylinder5_1 { get; set; }
        public string HT_Cylinder5_2 { get; set; }
        public string HT_Cylinder6_1 { get; set; }
        public string HT_Cylinder6_2 { get; set; }

        public string Door1_Aperture { get; set; }
        public string Door2_Aperture { get; set; }
        public string Door3_Aperture { get; set; }
        public string Door4_Aperture { get; set; }
        public string Door5_Aperture { get; set; }
        public string Door6_Aperture { get; set; }

        public string Temp_Oil1 { get; set; }
        public string Temp_Oil2 { get; set; }
        public string Temp_Oil3 { get; set; }

        public string Fllow_Door1 { get; set; }
        public string Fllow_Door2 { get; set; }
        public string Fllow_Door3 { get; set; }
        public string Fllow_Door4 { get; set; }
        public string Fllow_Door5 { get; set; }
        public string Fllow_Door6 { get; set; }

        public string Total_Fllow { get; set; }
        public string Fllow_Ho { get; set; }
        // Các thuộc tính này được giả định là 'decimal' vì chúng đã được xử lý trong tm_login_Tick.
        public decimal Fllow_DauTieng { get; set; }
        public decimal Fllow_BenSuc { get; set; }
        public decimal Fllow_SonDai { get; set; }
        public string Fllow_BinhNham { get; set; }
        public string Fllow_TL_CDD { get; set; }
    }
}
