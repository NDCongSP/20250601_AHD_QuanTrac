using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

using Domain.Commons;

namespace Domain.Entities
{
    public class Order : AuditEntityBase, IDataKeyFilter
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string WarehouseCode { get; set; }
        public string ChannelCode { get; set; }
        [ForeignKey("ChannelCode")]
        public Channel Channel { get; set; }
        public string OrderId { get; set; }
        public string OrderName { get; set; }
        public string OrderStatus { get; set; }
        public DateTime? OrderDate { get; set; }
        public double? Total { get; set; }
        public double? SubTotal { get; set; }
        public double? HandlingCharge { get; set; }
        public double? Giftvoucher { get; set; }
        public double? Point { get; set; }
        public double? Shipping { get; set; }
        public double? CodCharge { get; set; }
        public double? DiscountAmount { get; set; }
        public double? OtherDiscount { get; set; }
        public double? TaxAmount { get; set; }
        public string Currency { get; set; }
        public string DeliveryName { get; set; }
        public string DeliveryAddress1 { get; set; }
        public string DeliveryAddress2 { get; set; }
        public string DeliveryAddress3 { get; set; }
        public string DeliveryCity { get; set; }
        public string DeliveryStateCode { get; set; }
        public string DeliveryState { get; set; }
        public string DeliveryCountryCode { get; set; }
        public string DeliveryCountryName { get; set; }
        public string DeliveryZipcode { get; set; }
        public string DeliveryPhone { get; set; }
        public string DeliveryMail { get; set; }
        public string CustomerId { get; set; }
        public string OrderedCompanyName { get; set; }
        public string BillCity { get; set; }
        public string BillState { get; set; }
        public string BillCountry { get; set; }
        public string BillMail { get; set; }
        public string OrderDeliveryCompany { get; set; }
        public string Remarks { get; set; }
        public string HoldJudgmentMemo { get; set; }
        public int OnHoldStatus { get; set; }
        public int SubscriptionStatus { get; set; }
        public int HadCheckAttachItem { get; set; }
        public int StockUpStatus { get; set; }
        public string ShopifyTags { get; set; }
        public string InternalRemarks { get; set; }
        public string ReturnStatus { get; set; }
        public DateTime? ShippedDate { get; set; }
        public bool? IsDeltaData { get; set; }
        public int? AutoSplitDeliveryStatus { get; set; }
        public bool? IsMarketUpdated { get; set; }
        public DateTime? MarketUpdateAt { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public string DataKey { get; set; }
        public string InstructionToCourier  { get; set;}
        public Order()
        {
            OrderItems = new List<OrderItem>();
        }
    }
}