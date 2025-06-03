using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("InventStockTakeLines")]
    public class InventStockTakeLine : GenericEntity
    {
        [Key] public Guid Id { get; set; }
        public string StockTakeNo { get; set; }
        public string ProductCode { get; set; }
        public double? ExpectedQty { get; set; }
        public double? ActualQty { get; set; }
        public int? UnitId { get; set; }
        public EnumInventStockTakeStatus Status { get; set; } = EnumInventStockTakeStatus.Pending;
    }
}
