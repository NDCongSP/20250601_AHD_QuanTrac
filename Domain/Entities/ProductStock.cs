using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Domain.Commons;

namespace Domain.Entities;
public partial class ProductStock: AuditEntityBase, IDataKeyFilter
{ 
    
    public int Id { get; set; }
    
    public string ProductCode { get; set; }
    
    public int CompanyId { get; set; }
    
    public string WarehouseCode { get; set; }
    
    public int? StockQuantity { get; set; }
    
    public string DataKey { get; set; }

    [MaxLength(50)]
    public string BinCode { get; set; }
    
    [MaxLength(50)]
    public string LOT { get; set; }

	public DateTime? Expried { get; set; }

}