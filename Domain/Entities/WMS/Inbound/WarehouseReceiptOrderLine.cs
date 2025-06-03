using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("WarehouseReceiptOrderLine")]
public class WarehouseReceiptOrderLine : GenericEntity
{
    [Key] public Guid Id { get; set; }

    public string ReceiptNo { get; set; }

    [Required]
    public string ProductCode { get; set; }

    public string? UnitName { get; set; }

    public double? OrderQty { get; set; }

    public double? TransQty { get; set; }

    public string? Bin { get; set; }

    public string? LotNo { get; set; }

    public DateOnly? ExpirationDate { get; set; }

    public bool? Putaway { get; set; }

    public int? UnitId { get; set; }
    public EnumStatus Status { get; set; } = EnumStatus.Activated;
    public double? ArrivalNo { get; set; }
    /// <summary>
    /// if the product fails, then upload the error image name on this field.
    /// </summary>
    public string? ErrorImages { get; set; }
    /// <summary>
    /// Dùng cho trường hợp HHT insert line mới do tách LOT hoặc update hàng lỗi, này chính là ReceiptLineId của line bị tách.
    /// = WarehouseReceiptStaging.ReceiptLineId.
    /// </summary>
    public Guid? ReceiptLineIdParent { get; set; }
    public string? JanCode { get; set; }
}