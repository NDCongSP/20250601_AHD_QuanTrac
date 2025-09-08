using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    /// <summary>
    /// Bảng lưu thông tin alarms.
    /// </summary>
    [Table("FT04")]
    public class FT04 : IGenericEntity
    {
        [Key]
        [Browsable(false)]
        public System.Guid Id { get; set; }

        [Browsable(false)]
        public int? LocationId { get; set; }

        [Browsable(false)]
        public string? LocationName { get; set; }

        [Browsable(false)]
        public int? StationId { get; set; }

        //  [Browsable(false)]
        [DisplayName("Trạm")]
        public string? StationName { get; set; }

        [Browsable(false)]
        public string? Path { get; set; }

        [DisplayName("Tên Thiết Bị")]
        public string? TagName { get; set; }

        [DisplayName("Trạng Thái")]
        public bool? Value { get; set; } = false;

        [DisplayName("Mô Tả")]
        public string? Description { get; set; }

        [Browsable(false)]
        public string? CreateOperatorId { get; set; }

        [DisplayName("Thời Gian")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? CreateAt { get; set; }

        [Browsable(false)]
        public string? UpdateOperatorId { get; set; }

        [Browsable(false)]
        public DateTime? UpdateAt { get; set; }

        [Browsable(false)]
        public bool? IsDeleted { get; set; } = false;
    }
}
