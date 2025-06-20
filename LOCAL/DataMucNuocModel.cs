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
        [DisplayName("Mức Nước Hồ")] // chốt đang mở , đang đóng
        public string Fllow_Ho { get; set; }
        [DisplayName("Mức Nước Dầu Tiếng")] // chốt đang mở , đang đóng
        public string Fllow_DauTieng { get; set; }
        [DisplayName("Mức Nước Bến Súc")] // chốt đang mở , đang đóng
        public string Fllow_BenSuc { get; set; }
        [DisplayName("Mức Nước Sơn Đài")] // chốt đang mở , đang đóng
        public string Fllow_SonDai { get; set; }

    }
}
