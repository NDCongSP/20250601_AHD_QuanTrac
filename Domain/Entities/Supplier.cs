using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Domain.Commons;

namespace Domain.Entities;

[Table("Suppliers")]
public partial class Supplier: AuditEntityBase, IDataKeyFilter
{
    [Key] 
    public int Id { get; set; }
    
    public string? SupplierName { get; set; }
    /// <summary>
    /// TenantID in authp.Tenants table.
    /// </summary>
    public int? CompanyId { get; set; }
    
    public string? DataKey { get; set; }
    public string? SupplierId { get; set; }
    public EnumStatus Status { get; set; } = EnumStatus.Activated;
}