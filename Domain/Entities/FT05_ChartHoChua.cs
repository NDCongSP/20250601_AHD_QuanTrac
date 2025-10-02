using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("FT05")]
    public class FT05_ChartHoChua
    {
        [Key]
        public Guid Id { get; set; }

        public int? Index { get; set; } = 0;

        /// <summary>
        /// Kiểu thời gian (dd/MM).
        /// </summary>
        public string? X_Value { get; set; }

        /// <summary>
        /// Cao trình đỉnh đập.
        /// CTĐĐ.
        /// </summary>
        [DisplayName("CTĐĐ")]
        public double? L_CTDD { get; set; } = 0;

        /// <summary>
        /// Mức nước kiểm tra.
        /// </summary>
        [DisplayName("MNKT")]
        public double? L_MNKT { get; set; } = 0;

        /// <summary>
        /// Muc nước thiết kế.
        /// </summary>
        public double? L_MNTK { get; set; } = 0;

        /// <summary>
        /// Mức nước dâng bình thường.
        /// </summary>
        [DisplayName("MNDBT")] 
        public double? L_MNDBT { get; set; } = 0;

        /// <summary>
        /// Đường phòng lũ.
        /// ĐPL.
        /// </summary>
        [Display(Name = "ĐPL")]
        public double? L_DPL { get; set; }

        /// <summary>
        /// Đường phòng phá hoại.
        /// ĐPPH.
        /// </summary>
        [DisplayName("ĐPPH")]
        public double? L_DPPH { get; set; }

        /// <summary>
        /// Hạn chế cấp nước.
        /// HCCN.
        /// </summary>
        [DisplayName("HCCN")] 
        public double? L_HCCN { get; set; }


        /// <summary>
        /// cấp nước.
        /// MNC.
        /// </summary>
        [DisplayName("MNC")]
        public double? L_MNC { get; set; }

        /// <summary>
        /// Vùng A: Vùng hạn chế cấp nước.
        /// </summary>
        [DisplayName("Vùng A: Vùng hạn chế cấp nước.")]
        public double? A_VungA { get; set; }

        /// <summary>
        /// Vùng B: Cấp nước bình thường.
        /// </summary>
        [DisplayName("Vùng B: Cấp nước bình thường.")]
        public double? A_VungB { get; set; }

        /// <summary>
        /// Vùng C: Cấp nước gia tăng.
        /// </summary>
        [DisplayName("Vùng C: Cấp nước gia tăng.")]
        public double? A_VungC { get; set; }
    }
}
