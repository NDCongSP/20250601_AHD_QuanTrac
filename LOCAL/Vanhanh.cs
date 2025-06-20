using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationForm1
{
    public class Vanhanh
    {
        // [Browsable(false)] // Không cho hiện ID 
        public int Id { get; set; }
        [DisplayName("Thời Gian")]
        public DateTime CreateAt { get; set; }

        [DisplayName("Mực Nước Hồ (m)")] // Chú thích tiếng việt ra Dvg
        public string Fllow_Ho { get; set; }

        [DisplayName("Độ Mở Tràn 1 (m)")] // Chú thích tiếng việt ra Dvg
        public string Door1_Aperture { get; set; }

        [DisplayName("Độ Mở Tràn 2 (m)")] // Chú thích tiếng việt ra Dvg
        public string Door2_Aperture { get; set; }
        [DisplayName("Độ Mở Tràn 3 (m)")] // Chú thích tiếng việt ra Dvg
        public string Door3_Aperture { get; set; }
        [DisplayName("Độ Mở Tràn 4 (m)")] // Chú thích tiếng việt ra Dvg
        public string Door4_Aperture { get; set; }
        [DisplayName("Độ Mở Tràn 5 (m)")] // Chú thích tiếng việt ra Dvg
        public string Door5_Aperture { get; set; }
        [DisplayName("Độ Mở Tràn 6 (m)")] // Chú thích tiếng việt ra Dvg
        public string Door6_Aperture { get; set; }
        [DisplayName("Lưu Lượng Tổng (m3/s)")] // Chú thích tiếng việt ra Dvg
        public string Total_Fllow { get; set; }
    }
}
