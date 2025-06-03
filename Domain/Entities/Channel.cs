using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Domain.Commons;

namespace Domain.Entities
{
    public class Channel: AuditEntityBase, IDataKeyFilter
    { 
        [Key]
        public string ChannelCode { get; set; }
        public int CompanyId { get; set; }
        public string ChannelName { get; set; }
        public string ChannelMasterCode { get; set; }
        [ForeignKey("ChannelMasterCode")]
        public ChannelMaster ChannelMaster { get; set; }
        public string ChannelMasterName { get; set; }
        public string PaymentMethod { get; set; }
        public string BillCalcType { get; set; }
        public string Currency { get; set; }
        public string LanguageCode { get; set; }
        public bool? IsActive { get; set; }
        public string DataKey { get; set; }
    }
}