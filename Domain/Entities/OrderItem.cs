using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Domain.Commons;

namespace Domain.Entities
{
    public class OrderItem : AuditEntityBase, IDataKeyFilter
    {
        public int Id { get; set; }
        public int OrderHeaderId { get; set; }

        [ForeignKey("OrderHeaderId")]
        public Order OrderHeader { get; set; }
        public int CompanyId { get; set; }
        public string OrderId { get; set; }
        public int? LineNo { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string SalesProductCode { get; set; }
        public string SalesProductName { get; set; }
        public int? Quantity { get; set; }
        public double? PurchaseUnitPrice { get; set; }
        public double? DeclaredValue { get; set; }
		public double? UnitPrice { get; set; }
		public string Currency { get; set; }
        public int? ReturnQuantity { get; set; }
        public bool isAttachedItem { get; set; }
        public string ParentItemCode { get; set; }
        public string DataKey { get; set; }
    }
}