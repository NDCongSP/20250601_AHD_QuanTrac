using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationForm1
{
    public class Tran2Model
    {
        // [Browsable(false)] // Không cho hiện ID 
        public int Id { get; set; }
        [DisplayName("Thời Gian")]
        public DateTime CreateAt { get; set; }

        [DisplayName("DC1 Đang Chạy")] // Chú thích tiếng việt ra Dvg
        public string S1_DC1_Running { get; set; }
        [DisplayName("DC2 Đang Chạy ")] // Chú thích tiếng việt ra Dvg
        public string S1_DC2_Running { get; set; }
        [DisplayName("DC3 Đang Chạy")] // Chú thích tiếng việt ra Dvg
        public string S1_DC3_Running { get; set; }

        [DisplayName("Cửa 1 Đang Mở")] // Chú thích tiếng việt ra Dvg
        public string Door1_Opening { get; set; }

        [DisplayName("Cửa 1 Đang Đóng")] // Chú thích tiếng việt ra Dvg
        public string Door1_Closing { get; set; }

        [DisplayName("Chốt 1 Đang Mở")] // Chú thích tiếng việt ra Dvg
        public string Doorlock1_Opening { get; set; }

        [DisplayName("Chốt 1 Đang Đóng")] // Chú thích tiếng việt ra Dvg
        public string Doorlock1_Closing { get; set; }
        [DisplayName("Cửa 1 Mở Hoàn Toàn")] // Chú thích tiếng việt ra Dvg
        public string Door1_Open { get; set; }
        [DisplayName("Cửa 1 Đóng Hoàn Toàn")] // Chú thích tiếng việt ra Dvg
        public string Door1_Close { get; set; }
        [DisplayName("Chốt 1-1 Mở Hết")] // Chú thích tiếng việt ra Dvg
        public string Doorlock1_1Open { get; set; }
        [DisplayName("Chốt 1-1 Đóng Hết")] // Chú thích tiếng việt ra Dvg
        public string Doorlock1_1Close { get; set; }
        [DisplayName("Chốt 1-2 Mở Hết")] // Chú thích tiếng việt ra Dvg
        public string Doorlock1_2Open { get; set; }
        [DisplayName("Chốt 1-2 Đóng Hết")] // Chú thích tiếng việt ra Dvg
        public string Doorlock1_2Close { get; set; }
    }
}
