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
        public double? CTDD { get; set; } = 0;

        /// <summary>
        /// Mức nước kiểm tra.
        /// </summary>
        public double? MNKT { get; set; } = 0;

        /// <summary>
        /// Muc nước thiết kế.
        /// </summary>
        public double? MNTK { get; set; } = 0;

        /// <summary>
        /// Mức nước dâng bình thường.
        /// </summary>
        public double? MNDBT { get; set; } = 0;

        /// <summary>
        /// Đường phòng lũ.
        /// ĐPL.
        /// </summary>
        [Display(Name = "ĐPL")]
        public double? DPL { get; set; }

        /// <summary>
        /// Đường phòng phá hoại.
        /// ĐPPH.
        /// </summary>
        [DisplayName("ĐPPH")]
        public double? DPPH { get; set; }

        /// <summary>
        /// Hạn chế cấp nước.
        /// HCCN.
        /// </summary>
        public double? HCCN { get; set; }
    }
}
