using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationForm1
{
    public class DataTrangThaiTramModel
    {
        // [Browsable(false)] // Không cho hiện ID 
        public int Id { get; set; }
        [DisplayName("Thời Gian")]
        public DateTime CreateAt { get; set; }
        [DisplayName("Chế Độ Từ Xa")] // chốt đang mở , đang đóng
        public string Remote { get; set; }
        [DisplayName("Chế Độ Tại Chổ")] // chốt đang mở , đang đóng
        public string Local { get; set; }
        [DisplayName("Chế Độ Tự Động")] // chốt đang mở , đang đóng
        public string Auto { get; set; }
        [DisplayName("Chế Độ Tay")] // chốt đang mở , đang đóng
        public string Man { get; set; }
        [DisplayName("Dừng Khẩn Tại Chổ")] // chốt đang mở , đang đóng
        public string Local_Stop { get; set; }
    }
}
