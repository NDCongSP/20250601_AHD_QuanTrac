using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Commons;

namespace Domain.Entities
{
    public class TempShopifyOrderProduct : AuditEntityBase
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string OrderId { get; set; }
        public string Name { get; set; }
        public string Grams { get; set; }
        public string Price { get; set; }
        public string ProductId { get; set; }
        public string Quantity { get; set; }
        public string Sku { get; set; }
        public string Title { get; set; }
        public string TotalDiscount { get; set; }
        public string VariantId { get; set; }
    }
}