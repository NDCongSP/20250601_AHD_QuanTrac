using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Domain.Commons;

namespace Domain.Entities;
public partial class ProductBundle: AuditEntityBase, IDataKeyFilter
{ 
    
    public int Id { get; set; }
    
    public string ProductBundleCode { get; set; }
    
    public string ProductCode { get; set; }
    
    public int CompanyId { get; set; }
    
    public int Quantity { get; set; }
    
    public float BundlePriceRatio { get; set; }
    
    public string DataKey { get; set; }

}