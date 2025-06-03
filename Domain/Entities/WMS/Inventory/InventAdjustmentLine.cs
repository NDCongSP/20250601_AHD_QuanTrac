using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("InventAdjustmentLines")]
    public class InventAdjustmentLine : GenericEntity
    {
        [Key] public Guid Id { get; set; }
        public string AdjustmentNo { get; set; }
        public string ProductCode { get; set; }
        public string ParentProductCode { get; set; }
        public string LotNo { get; set; }
        public double? Qty { get; set; }
        public int? UnitId { get; set; }
        public EnumInventoryAdjustmentStatus Status { get; set; } = EnumInventoryAdjustmentStatus.InProcess;
        public DateOnly? ExpirationDate { get; set; }
        public string Remark { get; set; }
        public string Reasons { get; set; }
        public bool IsBundle { get; set; }
        public string? BinCode { get; set; }
    }
}
