using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Cache;

namespace Domain.Entities;

[Table("WarehousePickingList")]
public partial class WarehousePickingList : GenericEntity
{
    [Key] public Guid Id { get; set; }

    public string PickNo { get; set; }

    public int? TenantId { get; set; }

    public string? Location { get; set; }

    public string? PersonInCharge { get; set; }

    public DateOnly? PickedDate { get; set; }

    public string? CreateOperatorId { get; set; }

    public DateTime? CreateAt { get; set; }

    public string? UpdateOperatorId { get; set; }

    public DateTime? UpdateAt { get; set; }

    public bool? IsDeleted { get; set; }

    public EnumShipmentOrderStatus? Status { get; set; } = EnumShipmentOrderStatus.Draft;

    public DateOnly? EstimatedShipDate { get; set; }
    public string? Remarks { get; set; }
    public EnumHHTStatus HHTStatus { get; set; } = EnumHHTStatus.New;
    public string? HHTInfo { get; set; } = string.Empty;
    public DateTime? PrintedDate { get; set; }
    public DateTime? RePrintedDate { get; set; }
}