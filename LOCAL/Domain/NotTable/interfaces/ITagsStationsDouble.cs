using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface ITagsStationsDouble
    {
        //Tag device goc
        public double? HT_Cylinder1_1 { get; set; }
        public double? HT_Cylinder1_2 { get; set; }
        public double? HT_Cylinder2_1 { get; set; }
        public double? HT_Cylinder2_2 { get; set; }
        public double? Door1_Aperture { get; set; }
        public double? Door2_Aperture { get; set; }
        public double? S1_Temp_Oil { get; set; } 
        public double? Pressure_Oil_Door1 { get; set; } 
        public double? Pressure_Oil_Door2 { get; set; } 
        public double? Fllow_Door1 { get; set; } 
        public double? Fllow_Door2 { get; set; }
        public double? Total_Fllow { get; set; }

        //gia tri sau cung = origin + Offset
        public double? HT_Cylinder1_1_Final { get; set; } 
        public double? HT_Cylinder1_2_Final { get; set; } 
        public double? HT_Cylinder2_1_Final { get; set; } 
        public double? HT_Cylinder2_2_Final { get; set; } 
        public double? Door1_Aperture_Final { get; set; } 
        public double? Door2_Aperture_Final { get; set; } 
        public double? S1_Temp_Oil_Final { get; set; } 
        public double? Pressure_Oil_Door1_Final { get; set; } 
        public double? Pressure_Oil_Door2_Final { get; set; } 
        public double? Fllow_Door1_Final { get; set; } 
        public double? Fllow_Door2_Final { get; set; }

        public double? Q_i_1 { get; set; }
        public double? Q_i_2 { get; set; }
    }
}
