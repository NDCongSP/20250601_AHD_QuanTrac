using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    /// <summary>
    /// Bảng lưu thông tin alarms.
    /// </summary>
    [Table("FT04")]
    public class FT04 : GenericEntity
    {
        [Key]
        public Guid Id { get; set; }

        public int? LocationId { get; set; }
        public string? LocationName { get; set; }
        public int? StationId { get; set; }
        public string? StationName { get; set; }
        public string? Path { get; set; }
        public string? TagName { get; set; }
        public double? Value { get; set; } = 0;
        public string? Description { get; set; }

        public string? CreateOperatorId { get; set; }
        public DateTime? CreateAt { get; set; }
        public string? UpdateOperatorId { get; set; }
        public DateTime? UpdateAt { get; set; }
        public bool? IsDeleted { get; set; }
        DateTime? GenericEntity.CreateAt { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        DateTime? GenericEntity.UpdateAt { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
