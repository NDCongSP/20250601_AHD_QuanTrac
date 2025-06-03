
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Domain.Entities;

[Table("ReturnOrders")]
public partial class ReturnOrder : GenericEntity
{
    [Key] public Guid Id { get; set; }

    public string ReturnOrderNo { get; set; }

    public string ShipmentNo { get; set; }

    public DateOnly? ReturnDate { get; set; }

    public string Reason { get; set; }

    public string PersonInCharge { get; set; }
    public DateOnly? ShipDate { get; set; }
    public EnumReturnOrderStatus Status { get; set; } = EnumReturnOrderStatus.Open;
}