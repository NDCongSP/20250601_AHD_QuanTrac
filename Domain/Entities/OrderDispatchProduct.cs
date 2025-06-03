using System;
using System.ComponentModel.DataAnnotations.Schema;

using Domain.Commons;

namespace Domain.Entities
{
    public class OrderDispatchProduct : AuditEntityBase, IDataKeyFilter
    {
        public int Id { get; set; }
        public int DispatchHeaderId { get; set; }
        [ForeignKey("DispatchHeaderId")]
        public OrderDispatch DispatchHeader { get; set; }
        public int CompanyId { get; set; }
        public string DeliveryId { get; set; }
        public string OrderId { get; set; }
        public int? LineNo { get; set; }
        public string ProductSku { get; set; }
        public string ItemName { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public int? ShippedQty { get; set; }
        public int? MarketShippedQty { get; set; }
        public double? Price { get; set; }
        public double? DeclaredValue { get; set; }
		public double? UnitPrice { get; set; }
		public bool isAttachedItem { get; set; }
        public string ParentItemCode { get; set; }
        public int StockUpStatus { get; set; }
        public string DataKey { get; set; }
    }
}