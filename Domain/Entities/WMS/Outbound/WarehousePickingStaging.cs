using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("WarehousePickingStaging")]
public partial class WarehousePickingStaging : GenericEntity
{
    [Key] public Guid Id { get; set; }

    public string PickNo { get; set; }

    public string ProductCode { get; set; }

    public string? Unit { get; set; }

    public string Location { get; set; }

    public string Bin { get; set; }

    public string LotNo { get; set; }

    public double? PickQty { get; set; }

    public double? ActualQty { get; set; }
    public EnumShipmentOrderStatus Status { get; set; } = EnumShipmentOrderStatus.Draft;
    public int? UnitId { get; set; }
    public Guid ShipmentLineId { get; set; }
    public string ProductQRCode { get; set; }
    public string ShipmentNo { get; set; }
    /// <summary>
    /// QR customer generate for management box when picking.
    /// </summary>
    public string? PickingBoxCode { get; set; }
    /// <summary>
    /// Bin.SortOrderNum.
    /// </summary>
    public int? SortOrderNum { get; set; }
}