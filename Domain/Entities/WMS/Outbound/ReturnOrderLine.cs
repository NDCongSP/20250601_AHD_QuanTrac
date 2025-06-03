using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("ReturnOrderLines")]
public partial class ReturnOrderLine : GenericEntity
{
    [Key] public Guid Id { get; set; }

    public string ReturnOrderNo { get; set; }

    public string? Location { get; set; }

    public double? Qty { get; set; }
    public EnumStatus Status { get; set; } = EnumStatus.Activated;
    public string ProductCode { get; set; }
    public int UnitId { get; set; }
}
