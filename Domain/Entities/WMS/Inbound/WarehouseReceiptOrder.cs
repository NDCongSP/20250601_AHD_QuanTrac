using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("WarehouseReceiptOrder")]
public class WarehouseReceiptOrder : GenericEntity
{
    [Key] public Guid Id { get; set; }

    public string ReceiptNo { get; set; }

    public string? VendorCode { get; set; }

    public string Location { get; set; }

    public DateOnly? ExpectedDate { get; set; }

    public string? ArrivalType { get; set; }

    public int TenantId { get; set; }
    public DateOnly? PostedDate { get; set; }
    public string? PostedBy { get; set; }
    public double? ScheduledArrivalNumber { get; set; }
    public string? SupplierCode { get; set; }
    public string? DocumentNo { get; set; }

    [Required]
    public int SupplierId { get; set; }

    public string? PersonInCharge { get; set; }

    public string? ConfirmedBy { get; set; }

    public DateOnly? ConfirmedDate { get; set; }
    public EnumReceiptOrderStatus? Status { get; set; } = EnumReceiptOrderStatus.Draft;
    /// <summary>
    /// Just use Receipt and Return.
    /// </summary>
    public EnumWarehouseTransType ReferenceType { get; set; } = EnumWarehouseTransType.Receipt;
    public string? ReferenceNo { get; set; }
    public EnumHHTStatus HHTStatus { get; set; } = EnumHHTStatus.New;
    public string? HHTInfo { get; set; } = string.Empty;

    public string? Remarks { get; set; } = string.Empty;
}