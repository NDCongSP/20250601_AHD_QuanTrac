
using Domain.Commons;
using System;

namespace Domain.Entities
{
    public class ProductSkuSetting : AuditEntityBase, IDataKeyFilter
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string SaleProductCode { get; set; }
        public string SaleProductName { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public DateTime? ApplyFrom { get; set; }
        public DateTime? ApplyTo { get; set; }
		public string DataKey { get; set; }
    }
}