using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("InventStockTakeRecordingLines")]
    public class InventStockTakeRecordingLine : GenericEntity
    {
        [Key] public Guid Id { get; set; }
        public Guid StockTakeRecordingId { get; set; }
        public string StockTakeNo { get; set; }
        public int RecordNo { get; set; }
        public string ProductCode { get; set; }
        public string Bin { get; set; }
        public string LotNo { get; set; }
        public double? ExpectedQty { get; set; }
        public double? ActualQty { get; set; }
        public int? UnitId { get; set; }
        public EnumInvenTransferStatus Status { get; set; } = EnumInvenTransferStatus.InProcess;
        public int TenantId { get; set; }
    }
}
