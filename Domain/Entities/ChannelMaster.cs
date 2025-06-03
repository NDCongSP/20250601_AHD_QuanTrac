using System.ComponentModel.DataAnnotations;

using Domain.Commons;

namespace Domain.Entities
{
    public class ChannelMaster: AuditEntityBase, IDataKeyFilter
    { 
        [Key]
        public string ChannelMasterCode { get; set; }
        public string ChannelMasterName { get; set; }
        public string DataKey { get; set; }
    }
}