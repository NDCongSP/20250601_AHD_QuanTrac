using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Domain.Commons;

namespace Domain.Entities
{
    [Table("ShippingRules")]
    public class ShippingRule 
    {
        [Key]
        public int Id { get; set; }

        public string CountryCode { get; set; }

        public string StateCode { get; set; }

        public int? Weight { get; set; }

        public string Courier { get; set; }
    }
}