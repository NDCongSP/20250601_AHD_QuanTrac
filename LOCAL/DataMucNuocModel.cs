using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationForm1
{
    public class DataMucNuocModel
    {

        // [Browsable(false)] // Không cho hiện ID 
        public int Id { get; set; }
        [DisplayName("Thời Gian")]
        public DateTime CreateAt { get; set; }
        [DisplayName("Mức Nước Hồ")] 
        public string Fllow_Ho { get; set; }
        [DisplayName("Mức Nước TV Dầu Tiếng")] 
        public string Fllow_DauTieng { get; set; }
        [DisplayName("Mức Nước Bến Súc")] 
        public string Fllow_BenSuc { get; set; }
        [DisplayName("Mức Nước Sơn Đài")] 
        public string Fllow_SonDai { get; set; }
        [DisplayName("Mức Nước Bình Nhâm")] 
        public string Fllow_BinhNham { get; set; }
        [DisplayName("Mức Nước TL_CDD")] // Mực Nước Thủy Lợi Cầu Đường Dầu Tiếng (API)
        public string Fllow_TL_CDD { get; set; }


    }
}
