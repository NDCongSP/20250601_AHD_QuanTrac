using Domain.Commons;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("ProductCategories")]
public partial class ProductCategory : AuditEntityBase, IDataKeyFilter
{
    [Key]
    public int Id { get; set; }
    public string? CategoryName { get; set; }
    public string? Description { get; set; }
    public EnumStatus? Status { get; set; }
    public int? CompanyId { get; set; }
    public string? DataKey { get; set; }

    /// <summary>
    /// For slip delivery printing.
    /// For Philippines team use that field to print when linking with shipping company.
    /// </summary>
    public string SlipDeliveryPrinting { get; set; }
}