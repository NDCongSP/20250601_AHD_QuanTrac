using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("WarehouseShipmentLines")]
public partial class WarehouseShipmentLine : GenericEntity
{
    [Key] public Guid Id { get; set; }

    public string? ShipmentNo { get; set; }

    public string? ProductCode { get; set; }

    public int? UnitId { get; set; }

    public double? ShipmentQty { get; set; }

    public string? Location { get; set; }

    public string? Bin { get; set; }
    public EnumShipmentOrderStatus? Status { get; set; } = EnumShipmentOrderStatus.Draft;
    public double? PackedQty { get; set; }
    public DateOnly? PackedDate { get; set; }
    public string LotNo { get; set; }
    public DateOnly ExpirationDate { get; set; }
}