using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationForm1
{
    public class DataTrangThaiModel
    {
        // [Browsable(false)] // Không cho hiện ID 
        public int Id { get; set; }
        [DisplayName("Thời Gian")]
        public DateTime CreateAt { get; set; }
        [DisplayName("Chế Độ Từ Xa")] // chốt đang mở , đang đóng
        public string S1_Remote { get; set; }
        [DisplayName("Chế Độ Tại Chổ")] // chốt đang mở , đang đóng
        public string S1_Local { get; set; }
        [DisplayName("Chế Độ Tự Động")] // chốt đang mở , đang đóng
        public string S1_Auto { get; set; }
        [DisplayName("Chế Độ Tay")] // chốt đang mở , đang đóng
        public string S1_Man { get; set; }
        [DisplayName("Dừng Khẩn Tại Chổ")] // chốt đang mở , đang đóng
        public string S1_Local_Stop { get; set; }
        [DisplayName("Dừng Khẩn Từ Xa")] // chốt đang mở , đang đóng
        public string S1_Stop_Remote { get; set; }
        [DisplayName("DC1 Đang Chạy")] // cửa đang mở , đang đóng
        public string S1_DC1_Running { get; set; }
        [DisplayName("DC2 Đang Chạy")] // cửa đang mở , đang đóng
        public string S1_DC2_Running { get; set; }
        [DisplayName("DC3 Đang Chạy")] // cửa đang mở , đang đóng
        public string S1_DC3_Running { get; set; }
        [DisplayName("Cửa 1 Đang Mở")] // cửa đang mở , đang đóng
        public string Door1_Opening { get; set; }
        [DisplayName("Cửa 1 Đang Đóng")] // cửa đang mở , đang đóng
        public string Door1_Closing { get; set; }
        [DisplayName("Cửa 2 Đang Mở")] // cửa đang mở , đang đóng
        public string Door2_Opening { get; set; }
        [DisplayName("Cửa 2 Đang Đóng")] // Chú thích tiếng việt ra Dvg
        public string Door2_Closing { get; set; }
        [DisplayName("Cửa 3 Đang Mở")] // Chú thích tiếng việt ra Dvg
        public string Door3_Opening { get; set; }
        [DisplayName("Cửa 3 Đang Đóng")] // Chú thích tiếng việt ra Dvg
        public string Door3_Closing { get; set; }
        [DisplayName("Cửa 4 Đang Mở")] // Chú thích tiếng việt ra Dvg
        public string Door4_Opening { get; set; }
        [DisplayName("Cửa 4 Đang Đóng")] // Chú thích tiếng việt ra Dvg
        public string Door4_Closing { get; set; }
        [DisplayName("Cửa 5 Đang Mở")] // Chú thích tiếng việt ra Dvg
        public string Door5_Opening { get; set; }
        [DisplayName("Cửa 5 Đang Đóng")] // Chú thích tiếng việt ra Dvg
        public string Door5_Closing { get; set; }
        [DisplayName("Cửa 6 Đang Mở")] // Chú thích tiếng việt ra Dvg
        public string Door6_Opening { get; set; }
        [DisplayName("Cửa 6 Đang Đóng")] // Chú thích tiếng việt ra Dvg
        public string Door6_Closing { get; set; }
        [DisplayName("Chốt Cửa 1 Đang Mở")] // Chú thích tiếng việt ra Dvg
        public string Doorlock1_Opening { get; set; }
        [DisplayName("Chốt Cửa 1 Đang Đóng")] // Chú thích tiếng việt ra Dvg
        public string Doorlock1_Closing { get; set; }
        [DisplayName("Chốt Cửa 2 Đang Mở")] // Chú thích tiếng việt ra Dvg
        public string Doorlock2_Opening { get; set; }
        [DisplayName("Chốt Cửa 2 Đang Đóng")] // Chú thích tiếng việt ra Dvg
        public string Doorlock2_Closing { get; set; }
        [DisplayName("Chốt Cửa 3 Đang Mở")] // Chú thích tiếng việt ra Dvg
        public string Doorlock3_Opening { get; set; }
        [DisplayName("Chốt Cửa 3 Đang Đóng")] // Chú thích tiếng việt ra Dvg
        public string Doorlock3_Closing { get; set; }
        [DisplayName("Chốt Cửa 4 Đang Mở")] // Chú thích tiếng việt ra Dvg
        public string Doorlock4_Opening { get; set; }
        [DisplayName("Chốt Cửa 4 Đang Đóng")] // Chú thích tiếng việt ra Dvg
        public string Doorlock4_Closing { get; set; }
        [DisplayName("Chốt Cửa 5 Đang Mở")] // Chú thích tiếng việt ra Dvg
        public string Doorlock5_Opening { get; set; }
        [DisplayName("Chốt Cửa 5 Đang Đóng")] // Chú thích tiếng việt ra Dvg
        public string Doorlock5_Closing { get; set; }
        [DisplayName("Chốt Cửa 6 Đang Mở")] // Chú thích tiếng việt ra Dvg
        public string Doorlock6_Opening { get; set; }
        [DisplayName("Chốt Cửa 6 Đang Đóng")] // Chú thích tiếng việt ra Dvg
        public string Doorlock6_Closing { get; set; }
        [DisplayName("Cửa 1 Mở Hoàn Toàn")] // Chú thích tiếng việt ra Dvg
        public string Door1_Open { get; set; }
        [DisplayName("Cửa 1 Đóng Hoàn Toàn ")] // Chú thích tiếng việt ra Dvg
        public string Door1_Close { get; set; }
        [DisplayName("Cửa 2 Mở Hoàn Toàn")] // Chú thích tiếng việt ra Dvg
        public string Door2_Open { get; set; }
        [DisplayName("Cửa 2 Đóng Hoàn Toàn ")] // Chú thích tiếng việt ra Dvg
        public string Door2_Close { get; set; }
        [DisplayName("Cửa 3 Mở Hoàn Toàn")] // Chú thích tiếng việt ra Dvg
        public string Door3_Open { get; set; }
        [DisplayName("Cửa 3 Đóng Hoàn Toàn ")] // Chú thích tiếng việt ra Dvg
        public string Door3_Close { get; set; }
        [DisplayName("Cửa 4 Mở Hoàn Toàn")] // Chú thích tiếng việt ra Dvg
        public string Door4_Open { get; set; }
        [DisplayName("Cửa 4 Đóng Hoàn Toàn ")] // Chú thích tiếng việt ra Dvg
        public string Door4_Close { get; set; }
        [DisplayName("Cửa 5 Mở Hoàn Toàn")] // Chú thích tiếng việt ra Dvg
        public string Door5_Open { get; set; }
        [DisplayName("Cửa 5 Đóng Hoàn Toàn ")] // Chú thích tiếng việt ra Dvg
        public string Door5_Close { get; set; }
        [DisplayName("Cửa 6 Mở Hoàn Toàn")] // Chú thích tiếng việt ra Dvg
        public string Door6_Open { get; set; }
        [DisplayName("Cửa 6 Đóng Hoàn Toàn ")] // Chú thích tiếng việt ra Dvg
        public string Door6_Close { get; set; }
        [DisplayName("Chốt 1_1 Mở Hết ")] // Chú thích tiếng việt ra Dvg
        public string Doorlock1_1Open { get; set; }
        [DisplayName("Chốt 1_1 Đóng Hết ")] // Chú thích tiếng việt ra Dvg
        public string Doorlock1_1Close { get; set; }
        [DisplayName("Chốt 1_2 Mở Hết ")] // Chú thích tiếng việt ra Dvg
        public string Doorlock1_2Open { get; set; }
        [DisplayName("Chốt 1_2 Đóng Hết ")] // Chú thích tiếng việt ra Dvg
        public string Doorlock1_2Close { get; set; }
        [DisplayName("Chốt 2_1 Mở Hết ")] // Chú thích tiếng việt ra Dvg
        public string Doorlock2_1Open { get; set; }
        [DisplayName("Chốt 2_1 Đóng Hết ")] // Chú thích tiếng việt ra Dvg
        public string Doorlock2_1Close { get; set; }
        [DisplayName("Chốt 2_2 Mở Hết ")] // Chú thích tiếng việt ra Dvg
        public string Doorlock2_2Open { get; set; }
        [DisplayName("Chốt 2_2 Đóng Hết ")] // Chú thích tiếng việt ra Dvg
        public string Doorlock2_2Close { get; set; }
        [DisplayName("Chốt 3_1 Mở Hết ")] // Chú thích tiếng việt ra Dvg
        public string Doorlock3_1Open { get; set; }
        [DisplayName("Chốt 3_1 Đóng Hết ")] // Chú thích tiếng việt ra Dvg
        public string Doorlock3_1Close { get; set; }
        [DisplayName("Chốt 3_2 Mở Hết ")] // Chú thích tiếng việt ra Dvg
        public string Doorlock3_2Open { get; set; }
        [DisplayName("Chốt 3_2 Đóng Hết ")] // Chú thích tiếng việt ra Dvg
        public string Doorlock3_2Close { get; set; }
        [DisplayName("Chốt 4_1 Mở Hết ")] // Chú thích tiếng việt ra Dvg
        public string Doorlock4_1Open { get; set; }
        [DisplayName("Chốt 4_1 Đóng Hết ")] // Chú thích tiếng việt ra Dvg
        public string Doorlock4_1Close { get; set; }
        [DisplayName("Chốt 4_2 Mở Hết ")] // Chú thích tiếng việt ra Dvg
        public string Doorlock4_2Open { get; set; }
        [DisplayName("Chốt 4_2 Đóng Hết ")] // Chú thích tiếng việt ra Dvg
        public string Doorlock4_2Close { get; set; }
        [DisplayName("Chốt 5_1 Mở Hết ")] // Chú thích tiếng việt ra Dvg
        public string Doorlock5_1Open { get; set; }
        [DisplayName("Chốt 5_1 Đóng Hết ")] // Chú thích tiếng việt ra Dvg
        public string Doorlock5_1Close { get; set; }
        [DisplayName("Chốt 5_2 Mở Hết ")] // Chú thích tiếng việt ra Dvg
        public string Doorlock5_2Open { get; set; }
        [DisplayName("Chốt 5_2 Đóng Hết ")] // Chú thích tiếng việt ra Dvg
        public string Doorlock5_2Close { get; set; }
        [DisplayName("Chốt 6_1 Mở Hết ")] // Chú thích tiếng việt ra Dvg
        public string Doorlock6_1Open { get; set; }
        [DisplayName("Chốt 6_1 Đóng Hết ")] // Chú thích tiếng việt ra Dvg
        public string Doorlock6_1Close { get; set; }
        [DisplayName("Chốt 6_2 Mở Hết ")] // Chú thích tiếng việt ra Dvg
        public string Doorlock6_2Open { get; set; }
        [DisplayName("Chốt 6_2 Đóng Hết ")] // Chú thích tiếng việt ra Dvg
        public string Doorlock6_2Close { get; set; }

    }
}
