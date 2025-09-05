using System;
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
        public Guid Id { get; set; }

        public int? LocationId { get; set; }
        public string? LocationName { get; set; }
        public int? StationId { get; set; }
        public string? StationName { get; set; }
        public string? Path { get; set; }
        public string? TagName { get; set; }
        public bool? Value { get; set; } = false;
        public string? Description { get; set; }

        public string? CreateOperatorId { get; set; }
        public DateTime? CreateAt { get; set; }
        public string? UpdateOperatorId { get; set; }
        public DateTime? UpdateAt { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
