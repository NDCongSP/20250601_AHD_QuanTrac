using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("InventBundles")]
    public class InventBundle : GenericEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string ProductBundleCode { get; set; } = string.Empty;
        public string TransNo { get; set; }
        public DateTime? TransDate { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Bin { get; set; }
        public string LotNo { get; set; }
        public double? Qty { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public EnumStatusBundle Status { get; set; } = EnumStatusBundle.InProcess;
        public string PersonInCharge { get; set; }
        public int TenantId { get; set; }
        public EnumHHTStatus HHTStatus { get; set; } = EnumHHTStatus.New;
        public string? HHTInfo { get; set; } = string.Empty;

        [NotMapped]
        public string ProductCodeOrigin
        {
            get { return ProductBundleCode?.Length > 3 && ProductBundleCode?[2] == '-' ? ProductBundleCode.Substring(3) : ProductBundleCode; }
        }
    }
}
