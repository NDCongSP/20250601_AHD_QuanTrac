using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("ShippingCarriers")]
    public class ShippingCarrier : GenericEntity
    {
        [Key] public Guid Id { get; set; }

        public string? ShippingCarrierCode { get; set; }
        public string ShippingCarrierName { get; set; }
        public EnumStatus Status { get; set; } = EnumStatus.Activated;
        public string? PrinterName { get; set; }
    }
}