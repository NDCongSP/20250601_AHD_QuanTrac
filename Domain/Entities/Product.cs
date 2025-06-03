using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Domain.Commons;

namespace Domain.Entities;
public partial class Product : AuditEntityBase, IDataKeyFilter
{

    public int Id { get; set; }

    public string ProductCode { get; set; }

    /// <summary>
    /// TenantID in authp.Tenants table.
    /// </summary>
    public int CompanyId { get; set; }

    public string? ProductShortCode { get; set; }

    public int VendorId { get; set; }

    public int SupplierId { get; set; }

    public EnumProductType ProductType { get; set; }

    public string? SaleProductCode { get; set; }

    public string? SaleProductName { get; set; }

    public string? ProductName { get; set; }

    public string? ProductEname { get; set; }

    public string? ProductIname { get; set; }

    public int CategoryId { get; set; }
    public bool? IsFdaRegistration { get; set; }

    public string? ProductModelNumber { get; set; }

    public string? ProductImageName { get; set; }

    public string? ProductImageUrl { get; set; }

    public int StockAvailableQuanitty { get; set; }

    public int UnitId { get; set; }

    public string? CurrencyCode { get; set; }

    public string? Description { get; set; }

    public string? HsCode { get; set; }

    public string? JanCode { get; set; }

    public double? Net { get; set; }

    public double? Weight { get; set; }

    public double? Height { get; set; }

    public double? Length { get; set; }

    public double? Depth { get; set; }

    public double? BaseCost { get; set; }

    public double? BaseCostOther { get; set; }

    public double? RegularPrice { get; set; }

    public string? Currency { get; set; }

    public string? CountryOfOrigin { get; set; }

    public EnumProductStatus ProductStatus { get; set; }

    public DateTime? RegistrationDate { get; set; }

    public string? Remark { get; set; }

    public string? InventoryMethod { get; set; }

    public int ShippingLimitDays { get; set; }
    public DateTime? FromApplyPreBundles { get; set; }
    public DateTime? ToApplyPreBundles { get; set; }

    public string? StockReceiptProcessInstruction { get; set; }
    public int? StockThreshold { get; set; }
    public bool? IndividuallyShippedItem { get; set; }
    public string? DataKey { get; set; }

    public string? MakerManagementCode { get; set; }
    public string? ProductShortName { get; set; }
    public string? ProductUrl { get; set; }
    public double? StandardPrice { get; set; }
    public string? VendorProductName { get; set; }
    public bool? WarehouseProcessingFlag { get; set; }
    public double? Width { get; set; }

    public string? ShopifyInventoryItemId { get; set; }
    public string? ShopifyLocationId { get; set; }
    public string? ShopifyAdminGraphqlApiId { get; set; }
    public bool? IsAttachedItem {get; set; }    
}