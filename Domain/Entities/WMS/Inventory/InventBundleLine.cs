using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("InventBundleLines")]
    public class InventBundleLine : GenericEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string TransNo { get; set; }
        public string ProductCode { get; set; }
        public string? Location { get; set; }
        public string Bin { get; set; }
        public string LotNo { get; set; }
        public double? DemandQty { get; set; }
        public double? ActualQty { get; set; }
        public int? UnitId { get; set; }
        public EnumStatus Status { get; set; } = EnumStatus.Inactivated;
        public DateTime? ExpirationDate { get; set; }

    }
}
