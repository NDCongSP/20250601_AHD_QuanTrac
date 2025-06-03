using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("InventStockTakes")]
    public class InventStockTake : GenericEntity
    {
        [Key] public Guid Id { get; set; }
        public string StockTakeNo { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime? TransactionDate { get; set; }
        public EnumInventStockTakeStatus Status { get; set; } = EnumInventStockTakeStatus.Pending;
        public string PersonInCharge { get; set; }
        public EnumHHTStatus HHTStatus { get; set; } = EnumHHTStatus.New;
        public string? HHTInfo { get; set; } = string.Empty;
        public int TenantId { get; set; }
    }
}
