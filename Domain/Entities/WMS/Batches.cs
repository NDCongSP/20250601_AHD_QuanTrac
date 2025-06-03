using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Batches")]
    public class Batches : GenericEntity
    {
        [Key] public Guid Id { get; set; }
        public string ProductCode { get; set; }
        public int TenantId { get; set; }
        public string LotNo { get; set; }
        public DateOnly? ManufacturingDate { get; set; }
        public DateOnly? ExpirationDate { get; set; }
        public EnumStatus Status { get; set; } = EnumStatus.Activated;
    }
}
