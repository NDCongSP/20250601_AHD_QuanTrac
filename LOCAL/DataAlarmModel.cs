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
        [Browsable(false)]
        public string TagName { get; set; }  // dùng để so sánh
        public int Id { get; set; }

        [DisplayName("Thời Gian")]
        public DateTime CreateAt { get; set; }

        [DisplayName("Mã Thiết Bị")]
        public string DeviceCode { get; set; }

        [DisplayName("Khu Vực")]
        public string Area { get; set; }

        [DisplayName("Mức Độ Cảnh Báo")]
        public string Severity { get; set; }

        // Cảnh báo áp suất dầu các cửa
        [DisplayName("Áp Suất Dầu Cửa 1 Cao")] public string Door1_PressureHigh { get; set; }
        [DisplayName("Áp Suất Dầu Cửa 1 Thấp")] public string Door1_PressureLow { get; set; }
        [DisplayName("Áp Suất Dầu Cửa 2 Cao")] public string Door2_PressureHigh { get; set; }
        [DisplayName("Áp Suất Dầu Cửa 2 Thấp")] public string Door2_PressureLow { get; set; }
        [DisplayName("Áp Suất Dầu Cửa 3 Cao")] public string Door3_PressureHigh { get; set; }
        [DisplayName("Áp Suất Dầu Cửa 3 Thấp")] public string Door3_PressureLow { get; set; }
        [DisplayName("Áp Suất Dầu Cửa 4 Cao")] public string Door4_PressureHigh { get; set; }
        [DisplayName("Áp Suất Dầu Cửa 4 Thấp")] public string Door4_PressureLow { get; set; }
        [DisplayName("Áp Suất Dầu Cửa 5 Cao")] public string Door5_PressureHigh { get; set; }
        [DisplayName("Áp Suất Dầu Cửa 5 Thấp")] public string Door5_PressureLow { get; set; }
        [DisplayName("Áp Suất Dầu Cửa 6 Cao")] public string Door6_PressureHigh { get; set; }
        [DisplayName("Áp Suất Dầu Cửa 6 Thấp")] public string Door6_PressureLow { get; set; }

        // Cảnh báo động cơ quá tải theo trạm
        [DisplayName("S1_DC1 Lỗi LLLLL")] public string S1_DC1_Over { get; set; }
        [DisplayName("S1_DC2 Lỗi")] public string S1_DC2_Over { get; set; }
        [DisplayName("S1_DC3 Lỗi")] public string S1_DC3_Over { get; set; }

        [DisplayName("S2_DC1 Lỗi")] public string S2_DC1_Over { get; set; }
        [DisplayName("S2_DC2 Lỗi")] public string S2_DC2_Over { get; set; }
        [DisplayName("S2_DC3 Lỗi")] public string S2_DC3_Over { get; set; }

        [DisplayName("S3_DC1 Lỗi")] public string S3_DC1_Over { get; set; }
        [DisplayName("S3_DC2 Lỗi")] public string S3_DC2_Over { get; set; }
        [DisplayName("S3_DC3 Lỗi")] public string S3_DC3_Over { get; set; }
        public string S1_Station_Alarm { get; set; }
        public string S2_Station_Alarm { get; set; }
        public string S3_Station_Alarm { get; set; }


    }
}
