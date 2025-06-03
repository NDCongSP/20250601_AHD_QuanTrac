using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("WarehousePutAwayLines")]
public class WarehousePutAwayLine : GenericEntity
{
    public WarehousePutAwayLine()
    {
    }

    public WarehousePutAwayLine(Guid id, string putAwayNo, string productCode, int? unitId, double? journalQty, double? transQty, string bin,
        string lotNo, DateOnly? expirationDate, int tenantId, EnumStatus status, DateTime? createdAt, DateTime? updatedAt)
    {
        Id = id;
        TenantId = tenantId;
        PutAwayNo = putAwayNo;
        ProductCode = productCode;
        UnitId = unitId;
        JournalQty = journalQty;
        TransQty = transQty;
        Bin = bin;
        LotNo = lotNo;
        ExpirationDate = expirationDate;
        TenantId = tenantId;
        Status = status;
        CreateAt = createdAt;
        UpdateAt = updatedAt;
    }

    [Key] public Guid Id { get; set; }

    public string PutAwayNo { get; set; }

    public string ProductCode { get; set; }

    public int? UnitId { get; set; }

    public double? JournalQty { get; set; }

    public double? TransQty { get; set; }

    public string Bin { get; set; }

    public string LotNo { get; set; }
    public DateOnly? ExpirationDate { get; set; }
    public int TenantId { get; set; }

    public EnumStatus Status { get; set; } = EnumStatus.Activated;
    public Guid? ReceiptLineId { get; set; }
    public EnumHHTStatus HHTStatus { get; set; } = EnumHHTStatus.New;
    public string? HHTInfo { get; set; } = string.Empty;
}