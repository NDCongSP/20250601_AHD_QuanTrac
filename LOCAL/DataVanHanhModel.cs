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
        // [Browsable(false)] // Không cho hiện ID 
        public int Id { get; set; }
        [DisplayName("Thời Gian")]
        public DateTime CreateAt { get; set; }

        [DisplayName("HT Xi Lanh1_1")] 
        public string HT_Cylinder1_1 { get; set; }

        [DisplayName("HT Xi Lanh1_2")] 
        public string HT_Cylinder1_2 { get; set; }

        [DisplayName("HT Xi Lanh2_1")] 
        public string HT_Cylinder2_1 { get; set; }

        [DisplayName("HT Xi Lanh2_2")] 
        public string HT_Cylinder2_2 { get; set; }

        [DisplayName("HT Xi Lanh3_1")] 
        public string HT_Cylinder3_1 { get; set; }

        [DisplayName("HT Xi Lanh3_2")] 
        public string HT_Cylinder3_2 { get; set; }

        [DisplayName("HT Xi Lanh4_1")] 
        public string HT_Cylinder4_1 { get; set; }

        [DisplayName("HT Xi Lanh4_2")] 
        public string HT_Cylinder4_2 { get; set; }
        [DisplayName("HT Xi Lanh5_1")] 
        public string HT_Cylinder5_1 { get; set; }

        [DisplayName("HT Xi Lanh5_2")] 
        public string HT_Cylinder5_2 { get; set; }
        [DisplayName("HT Xi Lanh6_1")] 
        public string HT_Cylinder6_1 { get; set; }

        [DisplayName("HT Xi Lanh6_2")] 
        public string HT_Cylinder6_2 { get; set; }


        [DisplayName("Độ Mở Tràn 1")] 
        public string Door1_Aperture { get; set; }

        [DisplayName("Độ Mở Tràn 2")] 
        public string Door2_Aperture { get; set; }
        [DisplayName("Độ Mở Tràn 3")] 
        public string Door3_Aperture { get; set; }
        [DisplayName("Độ Mở Tràn 4")] 
        public string Door4_Aperture { get; set; }
        [DisplayName("Độ Mở Tràn 5")] 
        public string Door5_Aperture { get; set; }
        [DisplayName("Độ Mở Tràn 6")] 
        public string Door6_Aperture { get; set; }

        [DisplayName("NĐ Dầu Trạm 1 ")] 
        public string Temp_Oil1 { get; set; }

        [DisplayName("NĐ Dầu Trạm 2 ")] 
        public string Temp_Oil2 { get; set; }

        [DisplayName("NĐ Dầu Trạm 3 ")] 
        public string Temp_Oil3 { get; set; }

        [DisplayName("Lưu Lượng Xả Tràn 1")] 
        public string Fllow_Door1 { get; set; }
        [DisplayName("Lưu Lượng Xả Tràn 2")] 
        public string Fllow_Door2 { get; set; }
        [DisplayName("Lưu Lượng Xả Tràn 3")] 
        public string Fllow_Door3 { get; set; }
        [DisplayName("Lưu Lượng Xả Tràn 4")] 
        public string Fllow_Door4 { get; set; }
        [DisplayName("Lưu Lượng Xả Tràn 5")] 
        public string Fllow_Door5 { get; set; }
        [DisplayName("Lưu Lượng Xả Tràn 6")] 
        public string Fllow_Door6 { get; set; }
        [DisplayName("Lưu Lượng Tổng")] 
        public string Total_Fllow { get; set; }
        [DisplayName("Mực Nước Hồ")] 
        public string Fllow_Ho { get; set; }

        [DisplayName("Mực Nước Dầu Tiếng")] 
        public string Fllow_DauTieng { get; set; }
        [DisplayName("Mực Nước Bến Súc")] 
        public string Fllow_BenSuc { get; set; }
        [DisplayName("Mực Nước Sơn Đài")] 
        public string Fllow_SonDai { get; set; }


    }
}
