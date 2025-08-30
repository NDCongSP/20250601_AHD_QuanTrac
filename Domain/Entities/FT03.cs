using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    /// <summary>
    /// Data Log.
    /// </summary>
    [Table("FT03")]
    public class FT03 : TagsStation, GenericEntity, ICalculatorValue
    {
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Báo data log theo event tag valuchange hay theo timer.
        /// khi report thì where theo điều kiện này để lấy dữ liệu theo timer cho các giá trị value.
        /// còn không thì lấy theo event tag valuechange cho các thông số trạng thái kiểu bool.
        /// </summary>
        public bool LogBaseInterval { get; set; } = true;

        public int LocationId { get; set; }
        public string LocationName { get; set; }

        /// <summary>
        /// Station Id.
        /// StationInfoModel.Id.
        /// </summary>
        [Display(Name = "Station Id")]
        public int StationId { get; set; }
        public string Path { get; set; } = string.Empty;

        /// <summary>
        /// Station name.
        /// StationInfoModel.Name.
        /// </summary>
        [Display(Name = "Station Name")]
        public string? StationName { get; set; }
        public string? CreateOperatorId { get; set; }
        public DateTime? CreateAt { get; set; }
        public string? UpdateOperatorId { get; set; }
        public DateTime? UpdateAt { get; set; }
        public bool? IsDeleted { get; set; }

        //cac gia tri tinh toan
        public double? Flow_DauTieng { get; set; } = 0;
        public double? Flow_BenSuc { get; set; } = 0;
        public double? Fllow_SonDai { get; set; } = 0;
        public double? Fllow_BinhNham { get; set; } = 0;
        public double? Fllow_BinhNham2 { get; set; } = 0;
        public double? Fllow_TL_CDD { get; set; } = 0;
        public double? Fllow_HL_TXL { get; set; } = 0;
        public double? Total_Fllow { get; set; } = 0;
        public double Q_Den { get; set; } = 0;
        public double Q_Di { get; set; } = 0;
        public double W_Ho { get; set; } = 0;
        public double LuuLuong { get; set; } = 0;
        public double LuuLuongTong { get; set; } = 0;
    }
}
