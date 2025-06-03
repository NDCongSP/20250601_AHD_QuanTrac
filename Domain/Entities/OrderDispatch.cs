using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Domain.Commons;

namespace Domain.Entities
{
    public class OrderDispatch : AuditEntityBase, IDataKeyFilter
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string ChannelCode { get; set; }
        public string DeliveryId { get; set; }
        public string MarketDeliveryNo { get; set; }
        public string OrderId { get; set; }
        public string OrderDispatchStatus { get; set; }
        public string TrackingNo { get; set; }
        public string DeliveryCompanyCode { get; set; }
        public string ShipmentDate { get; set; }
        public string DispatchStatus { get; set; }
        public bool IsCutOff { get; set; }
        public DateTime? CutoffDate { get; set; }
        public string CutoffId { get; set; }
        public bool IsMarketShipped { get; set; }
        public bool IsCourierAssigned { get; set; }
        public string LabelFilePath { get; set; }
        public string LabelFileExtension { get; set; }
        public string InvoiceFilePath { get; set; }
        public string InvoiceFileExtension { get; set; }
        public int? CallingApiDeliveryStatus { get; set; }
        public int StockUpStatus { get; set; }
        public int FdaRegistrationStatus { get; set; }
        public string? ReferenceId { get; set; }
        public bool? IsMarketUpdated { get; set; }
        public List<OrderDispatchProduct> OrderDispatchProducts { get; set; }
        public string DataKey { get; set; }

        public OrderDispatch()
        {
            OrderDispatchProducts = new List<OrderDispatchProduct>();
        }
    }
}