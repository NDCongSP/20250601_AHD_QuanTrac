
using Domain.Commons;
using System;

namespace Domain.Entities
{
    public class PreSetProductSetting : AuditEntityBase, IDataKeyFilter
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string SaleProductCode { get; set; }
        public string SaleProductName { get; set; }
        public string ProductBundleCode { get; set; }
        public string ProductBundleName { get; set; }
        public double RegularPrice { get; set; }
        public DateTime? FromApplyPreBundles { get; set; }
        public DateTime? ToApplyPreBundles { get; set; }
		public bool IsConvertPrice { get; set; }
		public int NumberOfTimes { get; set; }
		public double? UnitPrice { get; set; }
		public string DataKey { get; set; }
    }
}