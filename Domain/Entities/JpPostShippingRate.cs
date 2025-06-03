using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Commons;

namespace Domain.Entities
{
    [Table("JpPostShippingRates")]
    public class JpPostShippingRate
    {
        [Key]
        public int Id { get; set; }
        public int Weight { get; set; }
        public int? Zone1Fee { get; set; }
        public int? Zone2OceaniaFee { get; set; }
        public int? Zone2EuropeFee { get; set; }
        public int? Zone3Fee { get; set; }
        public int? NewZone1Fee { get; set; }
        public int? NewZone2Fee { get; set; }
        public int? NewZone3Fee { get; set; }
        public int? NewZone4Fee { get; set; }
        public int? NewZone5Fee { get; set; }
        public DateTime CreateAt { get; set; }
    }

}
