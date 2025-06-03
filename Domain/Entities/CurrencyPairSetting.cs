using System;
using System.ComponentModel.DataAnnotations.Schema;

using Domain.Commons;

namespace Domain.Entities
{
    public class CurrencyPairSetting : AuditEntityBase, IDataKeyFilter
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string CurrencyCodeFrom { get; set; }
        public string CurrencyCodeTo { get; set; }
        public int RateDecimalPlaces { get; set; }
        public string DataKey { get; set; }
    }
}