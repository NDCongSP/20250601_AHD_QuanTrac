using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("WarehouseShipments")]
public partial class WarehouseShipment : GenericEntity
{
    [Key] public Guid Id { get; set; }

    public string ShipmentNo { get; set; }

    public string? SalesNo { get; set; }

    public int TenantId { get; set; }

    public string? Location { get; set; }

    public DateOnly? PlanShipDate { get; set; }

    public string? PersonInCharge { get; set; }

    public string? ShippingCarrierCode { get; set; }

    public string? ShippingAddress { get; set; }

    public string? Telephone { get; set; }

    public string? TrackingNo { get; set; }

    public string? Email { get; set; }
    public EnumShipmentOrderStatus? Status { get; set; } = EnumShipmentOrderStatus.Draft;
    public string? PickingNo { get; set; }
    public string? LocationName { get; set; }

    public string? TenantName { get; set; }

    public string? PersonInChargeName { get; set; }
    public string? BinId { get; set; }
    public string? Address { get; set; }
    /// <summary>
    /// just use Shipment and movement.
    /// </summary>
    public EnumWarehouseTransType ShipmentType { get; set; } = EnumWarehouseTransType.Shipment;

    /// <summary>
    /// 0: Not yet process Pick up API.
    ///1: Completed execute Pick up API.
    /// giá trị khởi tạo là -1.
    ///khi packed xong thì update nó là 0 cho bên DHL sử dụng.
    /// </summary>
    public int DHLPickup { get; set; }
    /// <summary>
    /// sẽ có 1 nút trên màn hình shipment call DHL API, lúc đó mới update thoi gian.
    /// </summary>
    public DateTime? DHLPickupDatetime { get; set; }
    public int? OrderDispatchId { get; set; }

    /// <summary>
    /// box type for package, select folow shipping carrier code.
    /// </summary>
    public Guid? ShippingBoxesId { get; set; }
    public string? ShippingBoxesName { get; set; }
}