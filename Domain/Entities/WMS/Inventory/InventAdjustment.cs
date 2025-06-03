using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("InventAdjustments")]
    public class InventAdjustment : GenericEntity
    {
        [Key] public Guid Id { get; set; }
        public string AdjustmentNo { get; set; }
        public string Description { get; set; }
        public string Bin { get; set; }
        public string ProductCode { get; set; }

        public DateTime? AdjustmentDate { get; set; }
        public EnumInventoryAdjustmentStatus Status { get; set; } = EnumInventoryAdjustmentStatus.InProcess;
        public string PersonInCharge { get; set; }
        public int TenantId { get; set; }
        public string Location { get; set; }
    }
}
