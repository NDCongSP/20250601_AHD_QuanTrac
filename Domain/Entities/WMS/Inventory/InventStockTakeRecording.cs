using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("InventStockTakeRecording")]
    public class InventStockTakeRecording : GenericEntity
    {
        [Key] public Guid Id { get; set; }
        public string StockTakeNo { get; set; }
        public int RecordNo { get; set; }
        public string Location { get; set; }
        public string PersonInCharge { get; set; }
        public DateOnly TransactionDate { get; set; }
        public EnumInvenTransferStatus Status { get; set; }= EnumInvenTransferStatus.InProcess;
        public string? Remarks { get; set; }
        public EnumHHTStatus HHTStatus { get; set; } = EnumHHTStatus.New;
        public string? HHTInfo { get; set; } = string.Empty;
        public int TenantId { get; set; }
    }
}
