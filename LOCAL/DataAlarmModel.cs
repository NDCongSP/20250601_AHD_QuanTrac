using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationForm1
{
    public class DataAlarmModel
    {
        // [Browsable(false)] // Không cho hiện ID 
        public int Id { get; set; }
        [DisplayName("Thời Gian")]
        public DateTime CreateAt { get; set; }
        // Các thông tin mở rộng
        [DisplayName("Mã Thiết Bị")]
        public string DeviceCode { get; set; }

        [DisplayName("Khu Vực")]
        public string Area { get; set; }

        [DisplayName("Mức Độ Cảnh Báo")]
        public string Severity { get; set; }

        [DisplayName("Áp Suất Dầu Cửa 1 Cao")] // Chú thích tiếng việt ra Dvg
        public string Door1_PressureHigh { get; set; }

        [DisplayName("Áp Suất Dầu Cửa 1 Thấp")] // Chú thích tiếng việt ra Dvg
        public string Door1_PressureLow { get; set; }
        [DisplayName("Áp Suất Dầu Cửa 2 Cao")] // Chú thích tiếng việt ra Dvg
        public string Door2_PressureHigh { get; set; }

        [DisplayName("Áp Suất Dầu Cửa 2 Thấp")] // Chú thích tiếng việt ra Dvg
        public string Door2_PressureLow { get; set; }
        [DisplayName("Áp Suất Dầu Cửa 3 Cao")] // Chú thích tiếng việt ra Dvg
        public string Door3_PressureHigh { get; set; }

        [DisplayName("Áp Suất Dầu Cửa 3 Thấp")] // Chú thích tiếng việt ra Dvg
        public string Door3_PressureLow { get; set; }
        [DisplayName("Áp Suất Dầu Cửa 4 Cao")] // Chú thích tiếng việt ra Dvg
        public string Door4_PressureHigh { get; set; }

        [DisplayName("Áp Suất Dầu Cửa 4 Thấp")] // Chú thích tiếng việt ra Dvg
        public string Door4_PressureLow { get; set; }
        [DisplayName("Áp Suất Dầu Cửa 5 Cao")] // Chú thích tiếng việt ra Dvg
        public string Door5_PressureHigh { get; set; }

        [DisplayName("Áp Suất Dầu Cửa 5 Thấp")] // Chú thích tiếng việt ra Dvg
        public string Door5_PressureLow { get; set; }
        [DisplayName("Áp Suất Dầu Cửa 6 Cao")] // Chú thích tiếng việt ra Dvg
        public string Door6_PressureHigh { get; set; }

        [DisplayName("Áp Suất Dầu Cửa 6 Thấp")] // Chú thích tiếng việt ra Dvg
        public string Door6_PressureLow { get; set; }
        [DisplayName("DC1 Lỗi")] // Chú thích tiếng việt ra Dvg
        public string DC1_Over { get; set; }
       
        [DisplayName("DC2 Lỗi")] // Chú thích tiếng việt ra Dvg
        public string DC2_Over { get; set; }
        [DisplayName("DC3 Lỗi")] // Chú thích tiếng việt ra Dvg
        public string DC3_Over { get; set; }



    }
}
