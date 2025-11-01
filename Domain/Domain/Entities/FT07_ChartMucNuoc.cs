using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("FT07")]
    public class FT07_ChartMucNuoc
    {
        [Key]
        public Guid Id { get; set; }

        [DisplayName("Index")]
        public int? Index { get; set; } = 0;

        /// <summary>
        /// thông tin hiển thị của trục X = X_Prefix + X_Value.
        /// </summary>
        [DisplayName("X_Prefix")]
        public string? X_Prefix { get; set; } = string.Empty;

        /// <summary>
        /// thông tin hiển thị của trục X = X_Prefix + X_Value.
        /// </summary>
        [DisplayName("X_Value")]
        public double? X_Value { get; set; } = 0;

        /// <summary>
        /// Bờ phải.
        /// </summary>
        [DisplayName("Bờ Phải")]
        public double? BoPhai { get; set; } = 0;

        /// <summary>
        /// Bờ trái.
        /// </summary>
        [DisplayName("Bờ Trái")]
        public double? BoTrai { get; set; } = 0;

        /// <summary>
        /// Q=300.
        /// </summary>
        [DisplayName("Q=300")]
        public double? Q300 { get; set; } = 0;

        /// <summary>
        /// Q=400.
        /// </summary>
        [DisplayName("Q=400")]
        public double? Q400 { get; set; } = 0;

        /// <summary>
        /// Q=600.
        /// </summary>
        [DisplayName("Q=600")]
        public double? Q600 { get; set; } = 0;

        /// <summary>
        /// Q=2800.
        /// </summary>
        [DisplayName("Q=2800")]
        public double? Q2800 { get; set; } = 0;

        /// <summary>
        /// Tên biến API sẽ get value gán cho điểm Z_Thực.
        /// </summary>
        public string? Z_Thuc { get; set; }= string.Empty;
    }
}
