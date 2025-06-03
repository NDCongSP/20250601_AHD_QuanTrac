using System;
using System.ComponentModel.DataAnnotations;

using Domain.Commons;

namespace Domain.Entities
{
    public class ShippingCountry : AuditEntityBase, IDataKeyFilter
    {
        [Key]
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string DisplayName { get; set; }
        public int? DisplayOrder { get; set; }
        public string DataKey { get; set; }
    }
}